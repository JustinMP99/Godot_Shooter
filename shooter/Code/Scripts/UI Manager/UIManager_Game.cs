using Godot;
using System;

public partial class UIManager
{
    
    public void SetGameUIState(bool state)
    {
        gameUIGroup.Visible = state;
    }

    public void Game_ShowRound()
    {
        
        //Hide healthbar, credits, etc
        playerInfoBox.Visible = false;
        gameScoreValueLabel.Visible = false;
        
        //Show countdown/round label
        //countdownLabel.Visible = true;

    }
    
    public void Game_HideRound()
    {
        
        //Hide countdown/round label
        countdownLabel.Visible = false;
        
        //Show healthbar, credits, etc
        
        playerInfoBox.Visible = true;
        gameScoreValueLabel.Visible = true;
        
    }
    
    public void Game_SetScoreValueText(int newScore)
    {
        gameScoreValueLabel.Text = newScore.ToString();
    }
    
    public void Game_SetCountDownLabelText(string timeVal)
    {
        countdownLabel.Text = timeVal;
    }
    public void Game_SetCountDownLabelText(int timeVal)
    {
        countdownLabel.Text = timeVal.ToString();
    }
    public void Game_SetCountDownLabelState(bool state)
    {
        countdownLabel.Visible = state;
    }

    public void Game_SetRoundLabelText(int round)
    {
        roundLabel.Text = "Round " + round.ToString();
    }

    public void Game_SetRoundLabelState(bool state)
    {
        roundLabel.Visible = state;
    }

    public void Game_SetHudState(bool state)
    {
        playerInfoBox.Visible = state;
        gameScoreValueLabel.Visible = state;
    }
    
    
}