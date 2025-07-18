using Godot;
using System;

public partial class UIManager
{

    [ExportCategory("Shop UI")] 
    [Export] private CanvasLayer _shopUIGroup;

    [Export] private Label _shopCreditsLabel;
    [Export] private Label _shopHealthLevelLabel;
    
    
    public void SetShopUIState(bool state)
    {
        _shopUIGroup.Visible = state;
    }

    public void Shop_SetCreditsText(int newCredits)
    {
        _shopCreditsLabel.Text = newCredits.ToString();
    }

    public void Shop_SetHealthLevelText(int currentLevel, int maxLevel)
    {
        _shopHealthLevelLabel.Text = currentLevel.ToString() + "/" + maxLevel.ToString();
    }
    
}
