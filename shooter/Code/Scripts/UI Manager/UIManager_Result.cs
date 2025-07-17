using Godot;
using System;

public partial class UIManager
{

    [ExportCategory("Result UI")] 
    [Export] private CanvasLayer _resultUIGroup;
    [Export] private Label _resultScoreLabel;
    [Export] private Label _resultCreditsEarnedLabel;
    [Export] private Label _resultTotalCreditsLabel;
    
    public void SetResultUIState(bool state)
    {
        _resultUIGroup.Visible = state;
    }

    public void Result_SetScoreText(int newScore)
    {
        _resultScoreLabel.Text = "Score: " + newScore;
    }

    public void Result_SetCreditsEarnedText(int newCredits)
    {
        _resultCreditsEarnedLabel.Text = "Credits Earned: " + newCredits.ToString();
    }

    public void Result_SetTotalCreditsText(int newCredits)
    {
        _resultTotalCreditsLabel.Text = "Total Credits: " + newCredits.ToString();
    }
    
}
