using Godot;
using System;

public partial class UIManager
{
    [ExportCategory("Game UI")]
    [Export] private CanvasLayer gameUIGroup;
    [Export] private Label gameScoreValueLabel;
    [Export] private ProgressBar healthBar;
    [Export] private Label countDownLabel;

    [Export] private Node gameMainSubGroup;
    [Export] private Node gameRoundSubGroup;


    public void SetGameUIState(bool state)
    {
        gameUIGroup.Visible = state;
    }

    public void Game_ShowRound()
    {
        
        //Hide healthbar, credits, etc
        healthBar.Visible = false;
        gameScoreValueLabel.Visible = false;
        
        //Show countdown/round label
        countDownLabel.Visible = true;

    }
    
    public void Game_HideRound()
    {
        
        //Hide countdown/round label
        countDownLabel.Visible = false;
        
        //Show healthbar, credits, etc
        
        healthBar.Visible = true;
        gameScoreValueLabel.Visible = true;
        

    }
    
    
    public void Game_SetScoreValueText(int newScore)
    {
        gameScoreValueLabel.Text = newScore.ToString();
    }

    public void Game_SetHealthBarMax(int newMax)
    {
        healthBar.MaxValue = newMax;
    }

    public void Game_SetHealthBarCurrent(int newCurrent)
    {
        healthBar.Value = newCurrent;
    }

    public void Game_SetCountDownLabelText(string timeVal)
    {
        countDownLabel.Text = timeVal;
    }
    public void Game_SetCountDownLabelText(int timeVal)
    {
        countDownLabel.Text = timeVal.ToString();
    }

    public void Game_SetCountDownLabelState(bool state)
    {
        countDownLabel.Visible = state;
    }
    
}