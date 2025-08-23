using Godot;
using System;
using System.Collections.Generic;

public partial class PowerUpManager : Node
{

    
    [ExportCategory("Power Up Data")]
    [Export] private int desiredPowerUps;
    [Export] private PackedScene powerUpsPrefab;
    [Export] private PowerUp healthPowerUp;
    [Export] private PowerUp fireTypePowerUp;
    
    //Pool Data
    private List<PowerUp> powerUpsPool;
    private List<PowerUp> activePowerUps;
    private int poolIter;
    private int poolIterMax;
    
    public void Startup()
    {
        //generate all
    }

    public override void _Process(double delta)
    {

        if (!Global.gamePaused)
        {
            //move powerups
            MoveActivePowerUps();
        }
        
    }
    
    private void OnTimerTimeout()
    {

        // if ()
        // {
        //     
        // }
        
    }

    private void MoveActivePowerUps()
    {

        // for (int i = 0; i < activePowerUps.Count; i++)
        // {
        //     //move here
        // }
        
        //remove all power ups that have finished traveling from the active list
        
    }
    
}
