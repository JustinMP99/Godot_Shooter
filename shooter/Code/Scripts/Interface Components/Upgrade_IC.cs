using Godot;
using System;

public partial class Upgrade_IC : InterfaceComponent
{
    
    private Label creditsLabel;
    public UpgradePanel HealthUpgradePanel;
    public UpgradePanel FireRateUpgradePanel;
    public UpgradePanel SpeedUpgradePanel;
    public Button BackButton;
    
    public override void FindNodes()
    {
        creditsLabel = interfaceGroup.GetNode<Label>("Credits Label");
        HealthUpgradePanel = interfaceGroup.GetNode<UpgradePanel>("Health Panel");
        FireRateUpgradePanel = interfaceGroup.GetNode<UpgradePanel>("Fire Rate Panel");
        SpeedUpgradePanel = interfaceGroup.GetNode<UpgradePanel>("Speed Panel");
        BackButton = interfaceGroup.GetNode<Button>("Back Button");
    }
    
    public void SetCreditsText(int newCredits)
    {
        creditsLabel.Text = "Credits: " + newCredits.ToString();
    }
    
}
