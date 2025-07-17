using Godot;
using System;

public partial class UIManager
{
    
    [ExportCategory("Game UI")] 
    [Export] private CanvasLayer _gameUIGroup;
    [Export] private Label _gameScoreLabel;
    [Export] private ProgressBar _healthBar;
    
    
    public void SetGameUIState(bool state)
    {
        _gameUIGroup.Visible = state;
    }
    
    public void Game_SetScoreText(int newScore)
    {
        _gameScoreLabel.Text = "Score: " + newScore.ToString();
    }
    
    public void Game_SetHealthBarMax(int newMax)
    {
        _healthBar.MaxValue = newMax;
    }

    public void Game_SetHealthBarCurrent(int newCurrent)
    {
        _healthBar.Value = newCurrent;
    }
    
}
