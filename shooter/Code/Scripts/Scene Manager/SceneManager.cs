using Godot;
using System;

public partial class SceneManager : Node
{
    [Export] private SaveManager saveManager;
    [Export] private UIManager UIManager;
    [Export] private EnemySpawner enemySpawner;
    [Export] private PackedScene playerScene;
    [Export] private Node levelNode;
    //[Export] private bool gamePaused;
    [Export] private Node3D startPosition;
    [ExportCategory("Player Data")] 
    [Export] private int score;
    private PlayerController player;
    private int credits;
    
    public override void _Ready()
    {

       
        saveManager.load();
        
        //UI Setup
        UIManager.SetMainUIState(true);
        UIManager.SetPauseUIState(false);
        UIManager.SetGameUIState(false);
        UIManager.SetResultUIState(false);
        
        //Spawner Startup
        enemySpawner.Startup();
        
        UIManager.Main_SetCreditsText(GameData.Instance.data["Credits"].AsInt32());
        //saveManager.Save();
    }

    public void ActivatePause()
    {
        
        UIManager.SetGameUIState(false);
        
        UIManager.SetPauseUIState(true);

        Global.gamePaused = true;
        
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
        GameData.Instance.data["Credits"] = GameData.Instance.data["Credits"].AsInt32() + tempCredits;
        
        //Update UI
        UIManager.SetGameUIState(false);
        UIManager.SetResultUIState(true);
       
        UIManager.Result_SetScoreText(score);
        UIManager.Result_SetCreditsEarnedText(tempCredits);
        UIManager.Result_SetTotalCreditsText(GameData.Instance.credits);
        
    }

    public void DefeatedEnemy()
    {
        //Increase Score
        score += 100;
        //Update UI
        UIManager.Game_SetScoreText(score);
    }
    
}
