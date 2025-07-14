using Godot;
using System;

public partial class UIManager
{
    
    [ExportCategory("Game UI")] 
    [Export] private CanvasLayer gameUIGroup;
    [Export] private Label game_ScoreLabel;
    [Export] private ProgressBar healthBar;
    
    
    public void SetGameUIState(bool state)
    {
        gameUIGroup.Visible = state;
    }
    
    public void Game_SetScoreText(int newScore)
    {
        game_ScoreLabel.Text = "Score: " + newScore.ToString();
    }
    
    public void Game_SetHealthBarMax(int newMax)
    {
        healthBar.MaxValue = newMax;
    }

    public void Game_SetHealthBarCurrent(int newCurrent)
    {
        healthBar.Value = newCurrent;
    }
    
}
