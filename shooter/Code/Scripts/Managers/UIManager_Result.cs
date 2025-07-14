using Godot;
using System;

public partial class UIManager
{

    [ExportCategory("Result UI")] 
    [Export] private CanvasLayer resultUIGroup;

    [Export] private Label result_ScoreLabel;
    [Export] private Label result_CreditsEarnedLabel;
    [Export] private Label result_TotalCreditsLabel;
    

    public void SetResultUIState(bool state)
    {
        resultUIGroup.Visible = state;
    }

    public void Result_SetScoreText(int newScore)
    {
        result_ScoreLabel.Text = "Score: " + newScore;
    }

    public void Result_SetCreditsEarnedText(int newCredits)
    {
        result_CreditsEarnedLabel.Text = "Credits Earned: " + newCredits.ToString();
    }

    public void Result_SetTotalCreditsText(int newCredits)
    {
        result_TotalCreditsLabel.Text = "Total Credits: " + newCredits.ToString();
    }
    
}
