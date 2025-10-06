using Godot;
using System;

public partial class InputComponent : Node
{
    [Signal] public delegate void PauseSignalEventHandler();
    [Signal] public delegate void ShootSignalEventHandler();
    
    [ExportCategory("Debug")]
    [Export] private bool debug = false;

    [ExportCategory("Core Input Data")]
    private bool takingInput;
    public Vector3 direction;
    public Vector3 rotation;

    public override void _Process(double delta)
    {
        if (takingInput)
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
                EmitSignal(SignalName.ShootSignal);
            }

            if (Input.IsActionJustPressed("Pause"))
            {

                EmitSignal(SignalName.PauseSignal);

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

    #region Getter

    public bool GetTakingInput()
    {
        return takingInput;
    }

    #endregion

    #region Setter

    public void SetTakingInput(bool state)
    {
        takingInput = state;
    }

    #endregion
}