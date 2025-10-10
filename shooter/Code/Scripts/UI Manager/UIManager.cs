using Godot;
using System;


public enum InterfaceGroup
{
    Main,
    Options,
    Game,
    Result,
    Upgrade,
    Pause,
    Help
}

public partial class UIManager : Node
{

    [ExportGroup("Interface Components")]
    [Export] public MainMenu_IC MainMenu;
    [Export] public Upgrade_IC Upgrade;
    [Export] public Options_IC Options;
    [Export] public Game_IC Game;
    [Export] public Result_IC Result;
    [Export] public Pause_IC Pause;

    [ExportCategory("Debug UI")]
    [Export] private CanvasLayer debugUIGroup;
    [Export] private Label FramerateLabel;

    public override void _Ready()
    {
    }

    public void SwitchInterfaceGroup(InterfaceGroup group)
    {
        switch (group)
        {
            case InterfaceGroup.Main:
                Pause.SetUIState(false);
                Upgrade.SetUIState(false);
                Result.SetUIState(false);
                Options.SetUIState(false);
                Game.SetUIState(false);
                MainMenu.SetUIState(true);
                break;
            case InterfaceGroup.Game:
                Pause.SetUIState(false);
                Upgrade.SetUIState(false);
                Result.SetUIState(false);
                Options.SetUIState(false);
                MainMenu.SetUIState(false);
                Game.SetUIState(true);
                break;
            case InterfaceGroup.Result:
                Pause.SetUIState(false);
                Upgrade.SetUIState(false);
                Options.SetUIState(false);
                Game.SetUIState(false);
                MainMenu.SetUIState(false);
                Result.SetUIState(true);
                break;
            case InterfaceGroup.Options:
                Pause.SetUIState(false);
                Upgrade.SetUIState(false);
                Result.SetUIState(false);
                Game.SetUIState(false);
                MainMenu.SetUIState(false);
                Options.SetUIState(true);
                break;
            case InterfaceGroup.Upgrade:
                Pause.SetUIState(false);
                Result.SetUIState(false);
                Options.SetUIState(false);
                Game.SetUIState(false);
                MainMenu.SetUIState(false);
                Upgrade.SetUIState(true);
                break;
            case InterfaceGroup.Pause:
                Result.SetUIState(false);
                Options.SetUIState(false);
                Game.SetUIState(false);
                MainMenu.SetUIState(false);
                Upgrade.SetUIState(false);
                Pause.SetUIState(true);
                break;
        }
    }

    #region Debug UI Functions

    public void SetDebugUIState(bool state)
    {
        debugUIGroup.Visible = state;
    }

    public void SetFramerateLabelText(double value)
    {
        FramerateLabel.Text = "FPS: " + value;
    }

    #endregion
    
}