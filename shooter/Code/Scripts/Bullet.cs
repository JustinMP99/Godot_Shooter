using Godot;
using System;

public partial class Bullet : RigidBody3D
{

    [Export] private float lifetime;
    [Export] private float maxLifetime;
    [Export] private float speed;

    public override void _Ready()
    {
        
    }

    public override void _PhysicsProcess(double delta)
    {

        MoveAndCollide(-Transform.Basis.Z * (float)delta * speed);
        lifetime += 0.1f;

        if (lifetime >= maxLifetime)
        {
            this.QueueFree();
        }

    }
}
