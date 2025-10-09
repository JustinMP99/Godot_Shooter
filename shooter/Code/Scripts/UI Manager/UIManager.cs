using Godot;
using System;

public partial class UIManager : Node
{
    //[ExportCategory("Main UI")]

    [ExportGroup("Interface Components")]
    [Export] public MainMenu_IC MainMenu;
    [Export] public Upgrade_IC Upgrade;
    [Export] public Options_IC Options;
    [Export] public Game_IC Game;

    
    [ExportCategory("Pause UI")]
    [Export] private CanvasLayer pauseUIGroup;
    [Export] public Button pauseResumeButton;

    [ExportCategory("Result UI")]
    [Export] private CanvasLayer resultUIGroup;
    [Export] private Label resultScoreLabel;
    [Export] private Label resultCreditsEarnedLabel;
    [Export] private Label resultTotalCreditsLabel;

    [ExportCategory("Debug UI")]
    [Export] private CanvasLayer debugUIGroup;
    [Export] private Label FramerateLabel;

    public override void _Ready()
    {
    }

    #region Pause UI Functions

    public void SetPauseUIState(bool state)
    {
        pauseUIGroup.Visible = state;
    }

    #endregion

    #region Result UI Functions

    public void SetResultUIState(bool state)
    {
        resultUIGroup.Visible = state;
    }

    public void Result_SetScoreText(int newScore)
    {
        resultScoreLabel.Text = "Score: " + newScore;
    }

    public void Result_SetCreditsEarnedText(int newCredits)
    {
        resultCreditsEarnedLabel.Text = "Credits Earned: " + newCredits.ToString();
    }

    public void Result_SetTotalCreditsText(int newCredits)
    {
        resultTotalCreditsLabel.Text = "Total Credits: " + newCredits.ToString();
    }

    #endregion

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

    #region Getter

    #endregion

    #region Setter

    #endregion
}