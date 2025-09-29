using Godot;
using System;

public partial class UpgradePanel : Control
{

    [Export] private Label titleLabel;
    [Export] private Label descriptionLabel;
    [Export] private Label costLabel;
    [Export] private Label levelLabel;
    [Export] private Button upgradeButton;


    public void SetUpgradeButtonState(bool state)
    {
        upgradeButton.Disabled = state;
    }

    public void SetCostLabel(string text)
    {
        costLabel.Text = text;
    }
    
    public void SetLevelLabel(int currentLevel, int maxLevel)
    {
        levelLabel.Text = currentLevel.ToString() + " / " + maxLevel.ToString();
    }

    public void SetDescription(string newDescription)
    {
        descriptionLabel.Text = newDescription;
    }
    
}
