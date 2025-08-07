using Godot;
using System;

public partial class Bullet : RigidBody3D
{
 
    [ExportCategory("Bullet Stats")]
    [Export] private float speed;
    [Export] private int damage;
    public bool InstaKill;
    [ExportCategory("Lifetime Variables")]
    [Export] private float lifetime;
    [Export] private float maxLifetime;
    [Signal] public delegate void FinalShotEventHandler();

    public override void _Ready()
    {
    }
    
    public override void _PhysicsProcess(double delta)
    {
        if (!Global.gamePaused)
        {
            // Move the bullet forward along its local Z axis.
            MoveAndCollide(-Transform.Basis.Z * (float)delta * speed);

            // Increment the bullet's lifetime.
            lifetime += 0.1f;

            // Destroy the bullet if it exceeds its maximum lifetime.
            if (lifetime >= maxLifetime)
            {
                Position = new Vector3(-100.0f, 0.0f, 0.0f);
                Disable();
            }
        }
    }
    
    public void OnBodyEntered(Node3D node)
    {
        if (node is EnemyController enemy)
        {

            if (!InstaKill)
            {
                bool KilledEnemy = enemy.TakeDamage(damage);
                if (KilledEnemy)
                {
                    enemy.Disable();
                    EmitSignal(SignalName.FinalShot);
                }
            }
            else
            {
                enemy.Disable();
                EmitSignal(SignalName.FinalShot);
            }
            
            Position = new Vector3(-100.0f, 0.0f, 0.0f);
            Disable();
        }
    }

    public void Enable()
    {
        SetProcess(true);
        SetPhysicsProcess(true);
        Visible = true;
        lifetime = 0.0f;
    }
    
    public void Disable()
    { 
        SetProcess(false);
        SetPhysicsProcess(false);
        Visible = false;
        FinalShot += null;
    }
    
}
