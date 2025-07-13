using Godot;
using System;

public partial class SceneManager : Node
{

    [Export] private UIManager UIManager;
    [Export] private PackedScene playerScene;
    [Export] private Node levelNode;
    [Export] private bool gamePaused;

    [Export] private Node3D startPosition;
    
    [ExportCategory("Player Data")] 
    [Export] private int score;
    
    private PlayerController player;
    
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
