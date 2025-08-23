using Godot;
using System;


public partial class PowerUp : RigidBody3D
{

    public bool isActive;
    [ExportCategory("PowerUp Stats")]
    [Export] public PowerUpStats Stats;

    [ExportCategory("Lifetime Variables")]
    [Export] private float lifetime;
    [Export] private float maxLifetime;


    public void MovePowerUp(double delta)
    {
        MoveAndCollide(Transform.Basis.Z * (float)delta * Stats.Speed);

        lifetime += 0.1f;

        if (Position.Z <= -55.0f)
        {
            
            Disable();
            
        }
    }

    public void OnBodyEntered(Node3D node)
    {
        
        
    }
    
    public void Enable()
    {
        isActive = true;
        Visible = true;
    }

    public void Disable()
    {
        isActive = false;
        Visible = false;
        Position = new Vector3(10.0f, 10.0f, 10.0f);
    }
    
}
