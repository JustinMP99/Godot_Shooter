using Godot;
using System;

public partial class UIManager : Node
{

    [ExportCategory("UI Groups")] 
    [Export] private CanvasGroup mainUIGroup;
    [Export] private CanvasGroup gameUIGroup;

    [ExportCategory("Main UI")] 
    [Export] private Button startGameButton;
    [Export] private Button quitGameButton;
    
    public override void _Ready()
    {
        
    }

    public void SetMainUIState(bool state)
    {
        mainUIGroup.Visible = state;
    }

    public void SetGameUIState(bool state)
    {
        gameUIGroup.Visible = state;
    }
    
}
