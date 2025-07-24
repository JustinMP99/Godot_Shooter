using Godot;
using System;

public partial class UIManager
{
    
    public void SetShopUIState(bool state)
    {
        shopUIGroup.Visible = state;
    }

    public void Shop_SetCreditsText(int newCredits)
    {
        shopCreditsLabel.Text = "Credits: " + newCredits.ToString();
    }

    public void Shop_SetHealthLevelText(int currentLevel, int maxLevel)
    {
        healthUpgradePanel.SetLevelLabel(currentLevel, maxLevel);
    }
}