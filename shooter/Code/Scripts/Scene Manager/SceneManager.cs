using Godot;
using System;

public partial class SceneManager : Node
{
    [Export] private SaveManager saveManager;
    [Export] private UIManager UIManager;
    [Export] private EnemySpawner enemySpawner;
    [Export] private Node levelNode;
    [Export] private Node3D startPosition;

    [ExportCategory("Round Variables")]
    [Export] private int round;
    [Export] private Timer roundTimer;
    
    [ExportCategory("Player Variables")] 
    [Export] private int score;
    private PlayerController player;

    public override void _Ready()
    {
        //UI Setup
        UIManager.SetMainUIState(true);
        UIManager.SetPauseUIState(false);
        UIManager.SetGameUIState(false);
        UIManager.SetResultUIState(false);
        UIManager.SetShopUIState(false);

        //Spawner Startup
        enemySpawner.Startup();

        round = 0;
        
        player = PlayerController.Instance;
        player.Position = new Vector3(0.0f, 0.0f, 10.0f);
        player.SetTakingInput(false);
        player.Reparent(levelNode);
        
        //Assign Signal Functions
        player.PauseSignal += ActivatePause;
        player.PlayerHit += UpdateGameUI;
        player.PlayerDied += GameOver;
        player.EnemyDefeated += DefeatedEnemy;
        
        bool loadResult = saveManager.load();

        if (!loadResult)
        {
            player.Credits = 0;
            player.Stats.CurrentHealth = 50;
            player.Stats.MaxHealth = 50;
            player.Stats.HealthLevel = 1;
            saveManager.Save();
            GD.Print("First time save");
        }

        //_saveManager.Save();

        UIManager.Main_SetCreditsText(player.Credits);
    }

    #region Timer Functions


    private void StartRoundTimer()
    {
        roundTimer.SetProcess(true);
        roundTimer.Start();
    }
    
    private void GameBegin()
    {
        
    }

    #endregion

    #region Signal Functions
    
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
        player.Credits += tempCredits;

        //Update UI
        UIManager.SetGameUIState(false);
        UIManager.SetResultUIState(true);

        UIManager.Result_SetScoreText(score);
        UIManager.Result_SetCreditsEarnedText(tempCredits);
        UIManager.Result_SetTotalCreditsText(player.Credits);
    }

    public void DefeatedEnemy()
    {
        //Increase Score
        score += 100;
        //Update UI
        UIManager.Game_SetScoreText(score);
    }


    #endregion
    
}