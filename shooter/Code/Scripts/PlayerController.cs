using Godot;
using System;

public partial class PlayerController : CharacterBody3D
{
    [Signal] 
    public delegate void PauseSignalEventHandler();

    [Export] private float speed;
    
    public override void _Ready()
    {
        
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("Pause"))
        {
            
            PauseFunction();
            
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        
    }


    private void PauseFunction()
    {
        
        GD.Print("Emitting Pause Signal");
        //Emit Signal
        EmitSignal(SignalName.PauseSignal);

    }
    
}
