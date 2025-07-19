using Godot;
using System;

public partial class UIManager
{
    [ExportCategory("Shop UI")] 
    [Export] private CanvasLayer shopUIGroup;
    [Export] private Label shopCreditsLabel;
    [Export] private Label shopHealthLevelLabel;


    public void SetShopUIState(bool state)
    {
        shopUIGroup.Visible = state;
    }

    public void Shop_SetCreditsText(int newCredits)
    {
        shopCreditsLabel.Text = newCredits.ToString();
    }

    public void Shop_SetHealthLevelText(int currentLevel, int maxLevel)
    {
        shopHealthLevelLabel.Text = currentLevel.ToString() + "/" + maxLevel.ToString();
    }
}