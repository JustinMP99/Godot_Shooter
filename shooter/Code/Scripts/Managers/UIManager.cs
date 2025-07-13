using Godot;
using System;

public partial class UIManager : Node
{

    [ExportCategory("UI Groups")] 
    [Export] private CanvasLayer mainUIGroup;
    [Export] private CanvasLayer pauseUIGroup;
    [Export] private CanvasLayer gameUIGroup;

    [ExportCategory("Main UI")] 
    [Export] private Button main_StartGameButton;
    [Export] private Button main_QuitGameButton;

    [ExportCategory("Pause UI")] 
    [Export] private Button pause_QuitButton;
    
    [ExportCategory("Game UI")] 
    [Export] private Label scoreLabel;
    [Export] private ProgressBar healthBar;
    
    public override void _Ready()
    {
        
    }

    public void SetScoreText(int newScore)
    {
        scoreLabel.Text = "Score: " + newScore.ToString();
    }

    #region UI States

    public void SetMainUIState(bool state)
    {
        mainUIGroup.Visible = state;
    }

    public void SetPauseUIState(bool state)
    {
        pauseUIGroup.Visible = state;
    }
    
    public void SetGameUIState(bool state)
    {
        gameUIGroup.Visible = state;
    }

    #endregion

    #region Getter

    
    

    #endregion

    #region Setter

    public void SetHealthBarMax(int newMax)
    {
        healthBar.MaxValue = newMax;
    }

    public void SetHealthBarCurrent(int newCurrent)
    {
        healthBar.Value = newCurrent;
    }

    #endregion
    
    
}
