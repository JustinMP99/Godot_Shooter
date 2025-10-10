using Godot;
using System;

public partial class Result_IC : InterfaceComponent
{
    private Label scoreLabel;
    private Label creditsEarnedLabel;
    private Label totalCreditsLabel;
    public Button MainMenuButton;

    public override void FindNodes()
    {
        scoreLabel = interfaceGroup.GetNode<Label>("Data Center Container/VBoxContainer/Score Label");
        creditsEarnedLabel = interfaceGroup.GetNode<Label>("Data Center Container/VBoxContainer/Credits Earned Label");
        totalCreditsLabel = interfaceGroup.GetNode<Label>("Data Center Container/VBoxContainer/Total Credits Label");
        MainMenuButton = interfaceGroup.GetNode<Button>("Button Center Container/HBoxContainer/Main Menu Button");
    }

    public void SetScoreText(int newScore)
    {
        scoreLabel.Text = "Score: " + newScore;
    }

    public void SetCreditsEarnedText(int newCredits)
    {
        creditsEarnedLabel.Text = "Credits Earned: " + newCredits.ToString();
    }

    public void SetTotalCreditsText(int newCredits)
    {
        totalCreditsLabel.Text = "Total Credits: " + newCredits.ToString();
    }
    
    
}
