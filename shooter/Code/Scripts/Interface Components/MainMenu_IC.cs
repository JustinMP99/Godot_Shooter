using Godot;
using System;

public partial class MainMenu_IC : InterfaceComponent
{
    
    private Label CreditsLabel;
    private Label subTitleLabel;
    public Button StartButton;

    public override void FindNodes()
    {
        CreditsLabel = interfaceGroup.GetNode<Label>("Credits Label");
        subTitleLabel = interfaceGroup.GetNode<Label>("Sub-title Label");
        StartButton = interfaceGroup.GetNode<Button>("CenterContainer/PanelContainer/Button Container/Start Button");
    }

    public void SetCreditsText(int newCredits)
    {
        CreditsLabel.Text = "Credits: " + newCredits.ToString();
    }

    public void SetSubTitleText(string newSubTitle)
    {
        subTitleLabel.Text = newSubTitle;
    }
    
}
