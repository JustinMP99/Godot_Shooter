using Godot;
using System;

public partial class SceneManager : Node
{

    [Export] private UIManager UIManager;
    [Export] private EnemySpawner enemySpawner;
    
    [Export] private PackedScene playerScene;
    [Export] private Node levelNode;
    [Export] private bool gamePaused;

    [Export] private Node3D startPosition;
    
    [ExportCategory("Player Data")] 
    [Export] private int score;
    
    private PlayerController player;

    private int credits;
    
    public override void _Ready()
    {
        
        //UI Setup
        UIManager.SetMainUIState(true);
        UIManager.SetPauseUIState(false);
        UIManager.SetGameUIState(false);

    }

    public void ActivatePause()
    {
        
        UIManager.SetGameUIState(false);
        
        UIManager.SetPauseUIState(true);

        gamePaused = true;
        
        player.SetTakingInput(false);

    }

    public void UpdateGameUI()
    {
        UIManager.SetHealthBarCurrent(player.GetCurrentHealth());
    }
    
    
}
