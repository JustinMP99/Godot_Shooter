using Godot;
using System;

public partial class EnemyController : RigidBody3D
{

    [Export] private float speed;
    
    public override void _Ready()
    {
        
    }

    public override void _PhysicsProcess(double delta)
    {
        MoveAndCollide(Transform.Basis.Z * (float)delta * speed);
    }
}
