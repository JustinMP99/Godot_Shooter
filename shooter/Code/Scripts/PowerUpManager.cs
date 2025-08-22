using Godot;
using System;
using System.Collections.Generic;

public partial class PowerUpManager : Node
{

    [Export] private int desiredPowerUps;
    [Export] private PackedScene powerUpsPrefab;
    private List<PowerUp> powerUpsPool;
    private List<PowerUp> activePowerUps;
    private int currentListIter;
    private int maxListIter;

    [ExportCategory("Power Up Resources")]
    [Export] private PowerUp healthPowerUp;
    [Export] private PowerUp fireTypePowerUp;
    

    public void Startup()
    {
        //generate all
    }

    public override void _Process(double delta)
    {

        if (!Global.gamePaused)
        {
            //move powerups
        }
        
    }

    private void MoveActivePowerUps()
    {

        for (int i = 0; i < activePowerUps.Count; i++)
        {
            //move here
        }
        
        //remove all power ups that have finished traveling from the active list
        
    }
    
    
    
}
