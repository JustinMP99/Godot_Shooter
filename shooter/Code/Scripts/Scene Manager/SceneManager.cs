using Godot;
using System;

public partial class SceneManager : Node
{
    [Export] private SaveManager saveManager;
    [Export] private UIManager UIManager;
    [Export] private EnemySpawner enemySpawner;
    [Export] private Node levelNode;
    [Export] private Node3D startPosition;
    
    [Export] private Timer introTimer; //Counts down from 3 when the player presses the start button
    private int introCount; // Counts the seconds on the intro timer
    
    [ExportCategory("Round Variables")]
    [Export] private int round;
    private int enemiesLeft; //the amount of enemies that must be defeated to end the round
    private int enemiesLeftMax = 10;
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
        
        player = PlayerController.Instance;
        player.Position = new Vector3(0.0f, 0.0f, 10.0f);
        player.SetTakingInput(false);
        
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

        UIManager.Main_SetCreditsText(player.Credits);
        
    }

    public override void _Process(double delta)
    {
        
    }

    #region Round Functions
    
    private void StartNewRound()
    {
        
        round++;
        enemiesLeftMax += (int)GD.RandRange(1.0, 20.0);
        enemiesLeft = enemiesLeftMax;
        UIManager.Game_SetRoundLabelText(round);
        UIManager.Game_SetRoundLabelState(true);
        GD.Print("Enemies Left: " + enemiesLeft);
        roundTimer.Start();
        
    }

    #endregion
    
    #region Timer Functions
    
    private void StartIntroTimer()
    {
        UIManager.Game_SetCountDownLabelState(true);
        introCount = 3;
        UIManager.Game_SetCountDownLabelText(introCount);
        introTimer.Start();
    }
    
    private void IntroTimerTimeout()
    {
        
        //End round timer
        introCount--;
        if (introCount <= 0)
        {
            UIManager.Game_SetCountDownLabelState(false);
            StartNewRound();
        }
        else
        {
            UIManager.Game_SetCountDownLabelText(introCount);
            introTimer.Start();
        }
        
    }

    private void RoundTimerTimeout()
    {
        UIManager.Game_SetRoundLabelState(false);
        UIManager.Game_SetHudState(true);
        enemySpawner.StartTimer();
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

        enemiesLeft--;
        if (enemiesLeft <= 0)
        {
            //End round
            StartNewRound();
            
            enemySpawner.DisableAllEnemies();
            
        }
        
        //Update UI
        UIManager.Game_SetScoreValueText(score);
    }


    #endregion
    
}