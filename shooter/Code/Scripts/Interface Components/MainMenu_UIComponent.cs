using Godot;
using System;

public partial class MainMenu_UIComponent : Node
{
    
    [Export] private CanvasLayer mainUI;
    [Export] private Label mainCreditsLabel;
    [Export] private Label subTitleLabel;
    [Export] public Button StartButton;
    
    public void SetUIState(bool state)
    {
        mainUI.Visible = state;
    }

    public void SetCreditsText(int newCredits)
    {
        mainCreditsLabel.Text = "Credits: " + newCredits.ToString();
    }

    public void SetSubTitleText(string newSubTitle)
    {
        subTitleLabel.Text = newSubTitle;
    }
    
}
