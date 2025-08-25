using Godot;
using System;
using System.Collections.Generic;

public partial class PowerUpManager : Node
{

    [ExportCategory("Spawner Components")]
    [Export] private Timer spawnTimer;
    
    [ExportCategory("Power Up Data")]
    [Export] private int desiredPowerUps;

    [Export] private PackedScene powerUpsPrefab ;
    [Export] private PowerUpStats healthPowerUp;
    [Export] private PowerUpStats shootTypePowerUp;
    
    //Pool Data
    private List<PowerUp> powerUpsPool;
    private List<PowerUp> activePowerUps;
    private int poolIter;
    private int poolIterMax;
    
    public void Startup()
    {
        powerUpsPool = new List<PowerUp>();
        activePowerUps = new List<PowerUp>();

        poolIterMax = desiredPowerUps;
        
        for (int i = 0; i < desiredPowerUps; i++)
        {
            PowerUp powerUp = powerUpsPrefab.Instantiate() as PowerUp;
            //Temp
            powerUp.Stats = shootTypePowerUp;
            
            powerUp.Disable();
            powerUpsPool.Add(powerUp);
            AddChild(powerUp);
        }
    }

    public override void _Process(double delta)
    {

        if (!Global.gamePaused)
        {
            //move powerups
            MoveActivePowerUps(delta);
        }
        
    }
    
    private void MoveActivePowerUps(double delta)
    {

        for (int i = 0; i < activePowerUps.Count; i++)
        {
            activePowerUps[i].MovePowerUp(delta);
        }
        
        //remove all power ups that have finished traveling from the active list
        activePowerUps.RemoveAll(e => !e.isActive);
    }
    
    private void OnTimerTimeout()
    {

        if (!powerUpsPool[poolIter].isActive)
        {
            GD.Print("Iter: " + poolIter);
            GD.Print("Spawning PowerUp");
            powerUpsPool[poolIter].Enable();
            powerUpsPool[poolIter].Position = new Vector3((float)GD.RandRange(-6.0, 6.0), 0.0f, -20.0f);
            activePowerUps.Add(powerUpsPool[poolIter]);
            poolIter++;
            if (poolIter >= poolIterMax)
            {
                GD.Print("Reseting Pool Iter!");
                poolIter = 0;
            }
        }
        else
        {
            GD.Print("Skipping Power Up Spawn");
            GD.Print("Pool Iter Value: " + poolIter);
        } 
    }

    public void StartTimer()
    {
        spawnTimer.Start();
    }
    public void StopTimer()
    {
        spawnTimer.Stop();
    }
    public void PauseTimer()
    {
        spawnTimer.Paused = true;
    }
    public void ResumeTimer()
    {
        spawnTimer.Paused = false;
    }
}
