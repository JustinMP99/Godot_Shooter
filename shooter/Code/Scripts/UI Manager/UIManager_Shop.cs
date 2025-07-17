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

    public void SetShopCreditsText(int newCredits)
    {
        _shopCreditsLabel.Text = newCredits.ToString();
    }
    
}
