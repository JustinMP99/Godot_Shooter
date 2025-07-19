using Godot;
using System;

public partial class UIManager
{
    [ExportCategory("Main UI")] 
    [Export] private CanvasLayer mainUIGroup;
    [Export] private Label mainCreditsLabel;

    public void SetMainUIState(bool state)
    {
        mainUIGroup.Visible = state;
    }

    public void Main_SetCreditsText(int newCredits)
    {
        mainCreditsLabel.Text = "Credits: " + newCredits.ToString();
    }
}