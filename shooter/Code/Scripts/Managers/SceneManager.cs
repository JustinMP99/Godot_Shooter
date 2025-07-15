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
        UIManager.SetResultUIState(false);
        
        //Spawner Startup
        enemySpawner.Startup();

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
        UIManager.Game_SetHealthBarCurrent(player.GetCurrentHealth());
    }

    public void GameOver()
    {
        
        //Stop Spawning Enemies
        enemySpawner.StopTimer();

        int tempCredits = score / 10;
        credits += tempCredits;
        
        //Update UI
        UIManager.SetGameUIState(false);
        UIManager.SetResultUIState(true);
       
        UIManager.Result_SetScoreText(score);
        UIManager.Result_SetCreditsEarnedText(tempCredits);
        UIManager.Result_SetTotalCreditsText(credits);
        
    }

    public void DefeatedEnemy()
    {
        //Increase Score
        score += 100;
        //Update UI
        UIManager.Game_SetScoreText(score);
    }
    
}
