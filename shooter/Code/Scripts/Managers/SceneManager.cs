using Godot;
using System;

public partial class SceneManager : Node
{

    [Export] private UIManager UIManager;
    [Export] private PackedScene playerScene;
    [Export] private PlayerController player;
    [Export] private Node levelNode;
    [Export] private bool gamePaused;

    [ExportCategory("Player Data")] 
    [Export] private int score;
    
    public override void _Ready()
    {
        //UI Setup
        
        
    }

    public void ActivatePause()
    {
        
        UIManager.SetGameUIState(false);
        
        UIManager.SetPauseUIState(true);

        gamePaused = true;

    }
    
}
