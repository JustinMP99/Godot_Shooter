using Godot;
using System;

public partial class SceneManager : Node
{

    [ExportCategory("Debug")]
    [Export] private bool debugEnabled;
    
    [ExportCategory("Managers")]
    [Export] private SaveManager saveManager;
    [Export] private UIManager UIManager;
    [Export] private EnemySpawner enemySpawner;
    [Export] private BulletManager bulletManager;
    
    [ExportCategory("Environment Variables")]
    [Export] private Node levelNode;
    [Export] private Node3D startPosition;
    [Export] private Timer introTimer; //Counts down from 3 when the player presses the start button
    [Export] private Timer roundTimer;
    private PlayerController player;
  
    [ExportCategory("Gameplay Values")]
    [Export] private int round;
    [Export] private int score;
    private int enemiesLeft; //the amount of enemies that must be defeated to end the round
    private int enemiesLeftMax = 10;
    private int introCount; // Counts the seconds on the intro timer

    public override void _Ready()
    {
        
        //load configuration file
        LoadConfig();
        
        //load player data
        LoadPlayerData();
        
        //setup UI
        UISetup();
        
        //additional startup calls
        enemySpawner.Startup();
        bulletManager.Startup();
        
    }

    public override void _Process(double delta)
    {
        UIManager.SetFramerateLabelText(Engine.GetFramesPerSecond());
    }
    
    #region Startup Functions

    private void LoadConfig()
    {
        bool configLoaded = saveManager.LoadConfig();

        if (configLoaded)
        {
            UIManager.SetMasterSliderValue(AudioServer.GetBusVolumeLinear(0));
            UIManager.SetSFXSliderValue(AudioServer.GetBusVolumeLinear(1));
            UIManager.SetMusicSliderValue(AudioServer.GetBusVolumeLinear(2));
        
            SetResolution(GameData.Instance.ResolutionValue);
            SetFullscreen(GameData.Instance.Fullscreen);
        }
        else
        {
            GD.Print("Creating new config file");
            UIManager.SetMasterSliderValue(AudioServer.GetBusVolumeLinear(0));
            UIManager.SetSFXSliderValue(AudioServer.GetBusVolumeLinear(1));
            UIManager.SetMusicSliderValue(AudioServer.GetBusVolumeLinear(2));
            
            SetResolution(3);
            SetFullscreen(false);
        }

    }

    private void LoadPlayerData()
    {
        player = PlayerController.Instance;
        player.Position = new Vector3(0.0f, 0.0f, 10.0f);
        player.SetTakingInput(false);
        
        //Assign Signal Functions
        player.PauseSignal += ActivatePause;
        player.PlayerHit += UpdateGameUI;
        player.PlayerDied += GameOver;
        player.EnemyDefeated += DefeatedEnemy;
        player.bulletManager = bulletManager;
        
        bool loadResult = saveManager.load();

        if (!loadResult)
        {
            player.Credits = 0;
            player.Stats.CurrentHealth = 50;
            player.Stats.MaxHealth = 50;
            player.Stats.HealthLevel = 1;
            saveManager.Save();
        }
    }

    private void UISetup()
    {
        UIManager.SetMainUIState(true);
        UIManager.SetOptionsUIState(false);
        UIManager.SetPauseUIState(false);
        UIManager.SetGameUIState(false);
        UIManager.SetResultUIState(false);
        UIManager.SetShopUIState(false);
        UIManager.Main_SetCreditsText(player.Credits);

        if (debugEnabled)
        {
            UIManager.SetDebugUIState(true);
        }
        else
        {
            UIManager.SetDebugUIState(false);
        }
        
    }
    
    #endregion
    
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
        UIManager.playerInfoBox.SetHealthBarCurrent(player.GetCurrentHealth());
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

    #region Supporting Functions
    
    private void SetResolution(int index)
    {
        
        GameData.Instance.ResolutionValue = index;
        GD.Print("Resolution Index: " + index);
        
        switch (index)
        {
            
            case 0:
                GetWindow().SetSize(new Vector2I(3840, 2160));
                break;
            case 1:
                GetWindow().SetSize(new Vector2I(2560, 1440));
                break;
            case 2:
                GetWindow().SetSize(new Vector2I(1920, 1080));
                break;
            case 3:
                GetWindow().SetSize(new Vector2I(1280, 720));
                break;
            
        }
    }

    private void SetFullscreen(bool state)
    {

        GameData.Instance.Fullscreen = state;
        GD.Print("Fullscreen State: " + state);
        if (state)
        {
            DisplayServer.WindowSetMode(DisplayServer.WindowMode.ExclusiveFullscreen);
        }
        else
        {
            DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);
        }
    }

    #endregion
 
}