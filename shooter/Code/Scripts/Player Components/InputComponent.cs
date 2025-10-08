using Godot;
using System;


public partial class InputComponent : Node
{
    [Signal] public delegate void PauseSignalEventHandler();
    [Signal] public delegate void ShootSignalEventHandler();
    
    [ExportCategory("Debug")]
    [Export] private bool debug = false;
    [ExportGroup("Specific Debug")]
    [Export] private bool stateSwitchDebug;

    [ExportCategory("Core Input Data")]
    [Export] private InputState inputState;
    private bool takingInput;
    public Vector3 direction;
    public Vector3 rotation;
    public Button CurrentButton;

    public override void _Process(double delta)
    {
        if (takingInput)
        {
            switch (inputState)
            {
                case InputState.Game:
                    GameInputCheck();
                    break;
                case InputState.Menu:
                    MenuInputCheck();
                    break;
            }
        }
    }

    private void GameInputCheck()
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

    private void MenuInputCheck()
    {

        if (debug)
        {
            GD.Print("Menu Input Check....");
        }
        
        if (Input.IsActionJustPressed("menu_up"))
        {
            var control = CurrentButton.FindValidFocusNeighbor(Side.Top);
            if (debug)
            {
                GD.Print("Menu Up");
            }
            if (control == null)
            {
                GD.Print("Control is null");
            }
            else
            {
                GD.Print("Control is not null");
                CurrentButton = control as Button;
                CurrentButton.GrabFocus();
            }
        }
        if (Input.IsActionJustPressed("menu_down"))
        {
            var control = CurrentButton.FindValidFocusNeighbor(Side.Bottom);
            if (debug)
            {
                GD.Print("Menu Down");
                //GD.Print("Node Path: " + path);
            }
            //var node = GetNode(path);
            if (control == null)
            {
                GD.Print("Control is null");
            }
            else
            {
                GD.Print("Control is not null");
                CurrentButton = control as Button;
                CurrentButton.GrabFocus();
            }
            //CurrentButton.GrabFocus();
        }
    }

    public void SwitchInputState(InputState newState)
    {
        inputState = newState;
        if (debug && stateSwitchDebug)
        {
            GD.Print("New Input State: " + inputState.ToString());
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