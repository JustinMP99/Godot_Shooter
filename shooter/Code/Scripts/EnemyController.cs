using Godot;
using System;

public partial class EnemyController : RigidBody3D
{

    private bool isActive;
    [Export] private float speed;
    [Export] private Area3D area;
    [Export] private CollisionShape3D collider;
    
    public override void _Ready()
    {
        
    }

    public void _Process(double delta)
    {
    
    }

    public override void _PhysicsProcess(double delta)
    {

        MoveAndCollide(Transform.Basis.Z * (float)delta * speed);
        if (Position.Z >= 5.0f)
        {
            Visible = false;
            isActive = false;
        }
       
    }

    public void Release()
    {
       
    }

    public void EnableEnemy()
    {
        
        isActive = true;
        SetProcess(true);
        SetPhysicsProcess(true);
        Visible = true;

    }
    
    public void DisableEnemy()
    {
        isActive = false;
        SetProcess(false);
        SetPhysicsProcess(false);
        Visible = false;
        Position = new Vector3(10.0f, 10.0f, 10.0f);
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
