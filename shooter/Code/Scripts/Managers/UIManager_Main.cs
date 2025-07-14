using Godot;
using System;

public partial class UIManager
{
    
    [ExportCategory("Main UI")] 
    [Export] private CanvasLayer mainUIGroup;
    [Export] private Label main_CreditsLabel;

    public void SetMainUIState(bool state)
    {
        mainUIGroup.Visible = state;
    }

    public void Main_SetCreditsText(int newCredits)
    {
        main_CreditsLabel.Text = "Credits: " + newCredits.ToString();
    }
    
}
