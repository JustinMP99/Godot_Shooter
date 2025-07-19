using Godot;
using System;

public partial class UIManager
{
    [ExportCategory("Result UI")] 
    [Export] private CanvasLayer resultUIGroup;
    [Export] private Label resultScoreLabel;
    [Export] private Label resultCreditsEarnedLabel;
    [Export] private Label resultTotalCreditsLabel;

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
}