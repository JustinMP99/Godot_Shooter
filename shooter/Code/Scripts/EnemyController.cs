using Godot;
using System;

public partial class EnemyController : RigidBody3D
{

    [Export] private float speed;

    private bool isActive;
    [Export] private float lifetime;
    [Export] private float maxLifetime;
    
    public override void _Ready()
    {
        
    }

    public void _Process(double delta)
    {

        GD.Print("Process Active");
        if (isActive)
        {
            this.lifetime += 0.1f;
            GD.Print("Current Lifetime: " + lifetime);
            if (lifetime >= maxLifetime)
            {
                Release();
            }
        }
    
    }

    public override void _PhysicsProcess(double delta)
    {

        if (isActive)
        {
            MoveAndCollide(Transform.Basis.Z * (float)delta * speed);
        }
       
    }

    public void Release()
    {
        GD.Print("Calling Release");
        DisableEnemy();
    }

    public void EnableEnemy()
    {
        SetProcess(true);
        SetPhysicsProcess(true);
        Visible = true;
        isActive = true;
    }
    
    public void DisableEnemy()
    {
        SetProcess(false);
        SetPhysicsProcess(false);
        Visible = false;
        isActive = false;
    }
    
    public bool GetIsActive()
    {
        return isActive;
    }
    
    public void SetIsActive(bool state)
    {
        isActive = state;
    }
    
}
