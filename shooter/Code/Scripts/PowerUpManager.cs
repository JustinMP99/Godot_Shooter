using Godot;
using System;
using System.Collections.Generic;

public partial class PowerUpManager : Node
{

    [ExportCategory("Spawner Components")]
    [Export] private Timer spawnTimer;
    
    [ExportCategory("Power Up Data")]
    [Export] private int desiredPowerUps;
    private PackedScene powerUpsPrefab ;
    private PowerUpStats_Health healthPowerUp ;
    private PowerUpStats_ShootType shootTypePowerUp;
    private Material healthMaterial;
    private Material shootTypeMaterial;
    
    //Pool Data
    private List<PowerUp> powerUpsPool;
    private List<PowerUp> activePowerUps;
    private int poolIter;
    private int poolIterMax;
    
    public void Startup()
    {

        powerUpsPrefab = GD.Load<PackedScene>("res://Level/Prefabs/GameObjects/PowerUp.tscn");
        healthPowerUp = GD.Load<PowerUpStats_Health>("res://Level/Resource Objects/Powerups/HealthPowerUp.tres");
        shootTypePowerUp = GD.Load<PowerUpStats_ShootType>("res://Level/Resource Objects/Powerups/ShootTypePowerUp.tres");

        healthMaterial = GD.Load<Material>("res://Art/Materials/Power Ups/health_powerup_material.tres");
        shootTypeMaterial = GD.Load<Material>("res://Art/Materials/Power Ups/shoottype_powerup_material.tres");
        
        powerUpsPool = new List<PowerUp>();
        activePowerUps = new List<PowerUp>();

        poolIterMax = desiredPowerUps;
        
        for (int i = 0; i < desiredPowerUps; i++)
        {
            PowerUp powerUp = powerUpsPrefab.Instantiate() as PowerUp;
            powerUp.Disable();
            powerUpsPool.Add(powerUp);
            AddChild(powerUp);
        }
    }

    public override void _Process(double delta)
    {

        if (!Global.GamePaused)
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
        
        int type;
        PowerUp powerUp = powerUpsPool[poolIter];
        
        //spawn power up
        if (!powerUp.isActive)
        {
            
            //generate type
            type = GD.RandRange(0, 1);

            switch (type)
            {
                case 0:
                    SpawnHealthPowerUp(powerUp);
                    break;
                case 1:
                    SpawnShootTypePowerUp(powerUp);
                    break;
            }
            
            powerUp.Enable();
            powerUp.Position = new Vector3((float)GD.RandRange(-6.0, 6.0), 0.0f, -20.0f);
            activePowerUps.Add(powerUp);
            poolIter++;
            if (poolIter >= poolIterMax)
            {
                poolIter = 0;
            }
        }
    }
    
    private void SpawnHealthPowerUp(PowerUp powerUp)
    {
        
        PowerUpStats_Health newStats = healthPowerUp.Duplicate(false) as PowerUpStats_Health;

        switch (Global.Round)
        {
            case 0:
                newStats.healthRestoreAmount = 50;
                break;
            case 1:
                
                break;
            
        }
        
        powerUp.Stats = newStats;
        powerUp.SetMaterial(healthMaterial);
    }

    private void SpawnShootTypePowerUp( PowerUp powerUp)
    {
        PowerUpStats_ShootType newStats = shootTypePowerUp.Duplicate(false) as PowerUpStats_ShootType;

       // int type = GD.RandRange(1, 2);
        int type = 1;

        if (type == 1)
        {
            newStats.ShootType = ShootType.Shotgun;
            newStats.FireRate = 0.25f;
        }
        else if(type == 2)
        {
            newStats.ShootType = ShootType.Spread_Random;
            newStats.FireRate = 0.10f;
        }
        
        powerUp.Stats = newStats;
        powerUp.SetMaterial(shootTypeMaterial);
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
