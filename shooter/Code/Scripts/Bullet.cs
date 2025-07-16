using Godot;
using System;

public partial class Bullet : RigidBody3D
{

    [Export] private float lifetime;
    [Export] private float maxLifetime;
    [Export] private float speed;
    
    [Signal]
    public delegate void FinalShotEventHandler();

    public override void _Ready()
    {
        
    }

    public override void _PhysicsProcess(double delta)
    {

        if (!Global.gamePaused)
        {
            MoveAndCollide(-Transform.Basis.Z * (float)delta * speed);
            lifetime += 0.1f;

            if (lifetime >= maxLifetime)
            {
                this.QueueFree();
            }   
        }

    }

    public void OnBodyEntered(Node3D node)
    {
        
        if (node is EnemyController enemy)
        {
            
            GD.Print("Hit Enemy!");
            enemy.DisableEnemy();
            EmitSignal(SignalName.FinalShot);
            this.QueueFree();
            
        }
        
    }
    
}
