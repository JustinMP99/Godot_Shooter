using Godot;
using System;


public partial class PowerUp : RigidBody3D
{

    
    [ExportCategory("PowerUp Stats")]
    [Export] private PowerUpStats stats;

    [ExportCategory("Lifetime Variables")]
    [Export] private float lifetime;
    [Export] private float maxLifetime;


    public void MovePowerUp(double delta)
    {
        MoveAndCollide(-Transform.Basis.Z * (float)delta * stats.Speed);

        lifetime += 0.1f;

        if (Position.Z <= -55.0f)
        {
            
            Disable();
            
        }
    }

    public void OnBodyEntered(Node3D node)
    {
        //hit player now do power up
        switch (stats.Type)
        {
            case PowerUpType.Health:

                GD.Print("Restored Player Health!");
                PlayerController player = node as PlayerController;
                
                PowerUpStats_Health tempStats = stats as PowerUpStats_Health;
                
                player.Heal(tempStats.healthRestoreAmount);
                
                break;
        }
        
    }
    
    public void Enable()
    {
        
    }

    public void Disable()
    {
        
    }
    

}
