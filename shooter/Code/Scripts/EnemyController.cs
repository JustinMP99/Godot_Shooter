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

    public override void _PhysicsProcess(double delta)
    {
        lifetime += 0.1f;
        MoveAndCollide(Transform.Basis.Z * (float)delta * speed);
    }

    public void Release()
    {
        GD.Print("Calling Release");
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
