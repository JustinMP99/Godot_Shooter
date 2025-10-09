using Godot;
using System;

public partial class Game_IC : InterfaceComponent
{
    public PlayerInfoBox PlayerInfoBox;
    private Label scoreLabel;
    private Label countdownLabel;
    private Label roundLabel;
    
    public override void FindNodes()
    {
        PlayerInfoBox = interfaceGroup.GetNode<PlayerInfoBox>("PlayerInfoBox");
        scoreLabel = interfaceGroup.GetNode<Label>("Score Label");
        countdownLabel = interfaceGroup.GetNode<Label>("Countdown Label");
        roundLabel = interfaceGroup.GetNode<Label>("Round Label");
    }

    public void ShowRound()
    {
        PlayerInfoBox.Visible = false;
        scoreLabel.Visible = false;
    }

    public void HideRound()
    {
        countdownLabel.Visible = false;
        PlayerInfoBox.Visible = true;
        scoreLabel.Visible = true;
    }

    public void SetScoreValueText(int newScore)
    {
        scoreLabel.Text = newScore.ToString();
    }

    public void SetCountDownLabelText(string timeVal)
    {
        countdownLabel.Text = timeVal;
    }

    public void SetCountDownLabelText(int timeVal)
    {
        countdownLabel.Text = timeVal.ToString();
    }

    public void SetCountDownLabelState(bool state)
    {
        countdownLabel.Visible = state;
    }

    public void SetRoundLabelText(int round)
    {
        roundLabel.Text = "Round " + round.ToString();
    }

    public void SetRoundLabelState(bool state)
    {
        roundLabel.Visible = state;
    }

    public void SetHudState(bool state)
    {
        PlayerInfoBox.Visible = state;
        scoreLabel.Visible = state;
    }
}
