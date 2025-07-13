using Godot;
using System;

public partial class PlayerController : CharacterBody3D
{
    [Signal] 
    public delegate void PauseSignalEventHandler();

    [Export] private float speed;
    [Export] private bool takingInput;
    [Export] private Node3D bulletPosition;
    [Export] private PackedScene bulletPrefab;
    
    private Vector3 targetVelocity = Vector3.Zero;
    
    public override void _Ready()
    {
        
    }

    public override void _Process(double delta)
    {
        if (takingInput)
        {
            if (Input.IsActionJustPressed("Pause"))
            {
            
                PauseFunction();
            
            }
        }
        
    }

    public override void _PhysicsProcess(double delta)
    {

        if (takingInput)
        {
            
            Vector3 direction = Vector3.Zero;

            // if (Input.IsActionPressed("Move_Forward"))
            // {
            //     GD.Print("Moving Forward");
            //     
            //     direction.Z -= 1.0f;
            // }
            // if (Input.IsActionPressed("Move_Down"))
            // {
            //     direction.Z += 1.0f;
            // }

            if (Input.IsActionPressed("Move_Left"))
            {
                direction.X -= 1.0f;
            }
            if (Input.IsActionPressed("Move_Right"))
            {
                direction.X += 1.0f;
            }
            

            if (direction !=  Vector3.Zero)
            {
                
                direction = direction.Normalized();

                targetVelocity.X = direction.X * speed;
                targetVelocity.Z = direction.Z * speed;

                Velocity = targetVelocity;
                MoveAndSlide();

            }
            
        }
        
    }
    
    private void PauseFunction()
    {
        
        GD.Print("Emitting Pause Signal");
        //Emit Signal
        EmitSignal(SignalName.PauseSignal);

    }

    public void SetTakingInput(bool state)
    {
        takingInput = state;
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
    
}
