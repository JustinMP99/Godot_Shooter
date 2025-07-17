using Godot;
using System;

public partial class UIManager
{
    
    [ExportCategory("Main UI")] 
    [Export] private CanvasLayer _mainUIGroup;
    [Export] private Label _mainCreditsLabel;

    public void SetMainUIState(bool state)
    {
        _mainUIGroup.Visible = state;
    }

    public void Main_SetCreditsText(int newCredits)
    {
        _mainCreditsLabel.Text = "Credits: " + newCredits.ToString();
    }
    
}
