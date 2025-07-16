using Godot;
using System;

/// <summary>
/// Represents a bullet in the game, responsible for movement, lifetime management,
/// and handling collision with enemies.
/// </summary>
public partial class Bullet : RigidBody3D
{
    /// <summary>
    /// Current lifetime of the bullet.
    /// </summary>
    [Export] private float lifetime;

    /// <summary>
    /// Maximum allowed lifetime before the bullet is destroyed.
    /// </summary>
    [Export] private float maxLifetime;

    /// <summary>
    /// Speed at which the bullet travels.
    /// </summary>
    [Export] private float speed;
    
    /// <summary>
    /// Signal emitted when the bullet hits an enemy and is about to be destroyed.
    /// </summary>
    [Signal]
    public delegate void FinalShotEventHandler();

    /// <summary>
    /// Called when the node is added to the scene.
    /// </summary>
    public override void _Ready()
    {
        
    }

    /// <summary>
    /// Handles bullet movement and lifetime each physics frame.
    /// </summary>
    /// <param name="delta">Time elapsed since the last frame.</param>
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
                this.QueueFree();
            }   
        }
    }

    /// <summary>
    /// Called when the bullet collides with another body.
    /// If the body is an enemy, disables the enemy, emits a signal, and destroys the bullet.
    /// </summary>
    /// <param name="node">The node the bullet collided with.</param>
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
