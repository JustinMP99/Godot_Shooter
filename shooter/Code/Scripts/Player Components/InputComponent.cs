using Godot;
using System;

public partial class InputComponent : Node
{

    [ExportCategory("Debug")]
    [Export] private bool debug = false;
    
    [ExportCategory("Core Input Data")]
    public Vector3 direction;
    public Vector3 rotation;
    
    

    public override void _Process(double delta)
    {
        direction = Vector3.Zero;
        rotation = Vector3.Zero;

    
        if (Input.IsActionPressed("Move_Left"))
        {
            direction.X -= 1.0f;
            //rotation.Z += 0.25f;
            rotation.Z += 0.25f;
        }
        if (Input.IsActionPressed("Move_Right"))
        {
            direction.X += 1.0f;
            rotation.Z -= 0.25f;
        }
        
        if (Input.IsActionPressed("Shoot"))
        {
        }
    
        if (Input.IsActionJustPressed("Pause"))
        {
        }
        
        if (direction != Vector3.Zero)
        {
            direction = direction.Normalized();
        }
        
        if (debug)
        {
            GD.Print("Input Direction: " + direction);    
        }
    }
}
