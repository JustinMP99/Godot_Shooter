using Godot;
using System;

public partial class SceneManager : Node
{

    [ExportCategory("Debug")]
    [Export] private bool debugEnabled;
    
    [ExportCategory("Managers")]
    [Export] private SaveManager saveManager;
    [Export] private UIManager interfaceManager;
    [Export] private EnemySpawner enemySpawner;
    [Export] private BulletManager bulletManager;
    [Export] private PowerUpManager powerUpManager;
    
    [ExportCategory("Environment Variables")]
    [Export] private Node levelNode;
    [Export] private Node3D startPosition;
    private Timer introTimer; //Counts down from 3 when the player presses the start button
    private Timer roundTimer;
    private Timer powerUpTimer;
    private PlayerController player;

    [ExportCategory("Gameplay Values")]
    
    [Export] private int round;
    [Export] private int score;
    [Export] private float powerUpTimeMax;
    private float powerUpTimeCurrent;
    private int enemiesLeft; //the amount of enemies that must be defeated to end the round
    private int enemiesLeftMax = 10;
    private int introCount; // Counts the seconds on the intro timer

    public override void _Ready()
    {

        introTimer = GetNode<Timer>("Intro Timer");
        roundTimer = GetNode<Timer>("Round Timer");
        powerUpTimer = GetNode<Timer>("Power Up Timer");
        
        //load configuration file
        LoadConfig();
        
        //load player data
        PlayerSetup();
        
        //setup UI
        UISetup();
        
        //additional startup calls
        enemySpawner.Startup();
        bulletManager.Startup();
        powerUpManager.Startup();
        
    }

    public override void _Process(double delta)
    {
        interfaceManager.SetFramerateLabelText(Engine.GetFramesPerSecond());
    }
    
    #region Startup Functions

    private void LoadConfig()
    {
        bool configLoaded = saveManager.LoadConfig();

        if (configLoaded)
        {
            interfaceManager.SetMasterSliderValue(AudioServer.GetBusVolumeLinear(0));
            interfaceManager.SetSFXSliderValue(AudioServer.GetBusVolumeLinear(1));
            interfaceManager.SetMusicSliderValue(AudioServer.GetBusVolumeLinear(2));
        
            SetResolution(GameData.Instance.ResolutionValue);
            SetFullscreen(GameData.Instance.Fullscreen);
        }
        else
        {
            GD.Print("Creating new config file");
            interfaceManager.SetMasterSliderValue(AudioServer.GetBusVolumeLinear(0));
            interfaceManager.SetSFXSliderValue(AudioServer.GetBusVolumeLinear(1));
            interfaceManager.SetMusicSliderValue(AudioServer.GetBusVolumeLinear(2));
            
            SetResolution(3);
            SetFullscreen(false);
        }

    }

    private void PlayerSetup()
    {
        player = PlayerController.Instance;

        player.Position = new Vector3(0.0f, 0.0f, 10.0f);
        player.SetTakingInput(false);
        
        //assign signal functions
        player.PauseSignal += ActivatePause;
        player.PlayerHealed += UpdateGameUI;
        player.PlayerHit += UpdateGameUI;
        player.PlayerDied += GameOver;
        player.EnemyDefeated += DefeatedEnemy;
        player.ShootTypePowerUp += ShootTypeSwitchEvent;
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
        interfaceManager.SetMainUIState(true);
        interfaceManager.SetOptionsUIState(false);
        interfaceManager.SetPauseUIState(false);
        interfaceManager.SetGameUIState(false);
        interfaceManager.SetResultUIState(false);
        interfaceManager.SetShopUIState(false);
        interfaceManager.Main_SetCreditsText(player.Credits);

        interfaceManager.PlayerInfoBox.SetPowerUpBarMax((int)powerUpTimeMax);
        interfaceManager.PlayerInfoBox.SetPowerUpBarCurrent(0.0f);
        
        if (debugEnabled)
        {
            interfaceManager.SetDebugUIState(true);
        }
        else
        {
            interfaceManager.SetDebugUIState(false);
        }
        
    }
    
    #endregion
    
    #region Round Functions
    
    private void StartNewRound()
    {
        
        round++;
        enemiesLeftMax += (int)GD.RandRange(1.0, 20.0);
        enemiesLeft = enemiesLeftMax;
        interfaceManager.Game_SetRoundLabelText(round);
        interfaceManager.Game_SetRoundLabelState(true);
        GD.Print("Enemies Left: " + enemiesLeft);
        roundTimer.Start();
        
    }

    #endregion
    
    #region Timer Functions
    
    private void StartIntroTimer()
    {
        interfaceManager.Game_SetCountDownLabelState(true);
        introCount = 3;
        interfaceManager.Game_SetCountDownLabelText(introCount);
        introTimer.Start();
    }
    
    private void IntroTimerTimeout()
    {
        
        //End round timer
        introCount--;
        if (introCount <= 0)
        {
            interfaceManager.Game_SetCountDownLabelState(false);
            StartNewRound();
        }
        else
        {
            interfaceManager.Game_SetCountDownLabelText(introCount);
            introTimer.Start();
        }
        
    }

    private void RoundTimerTimeout()
    {
        interfaceManager.Game_SetRoundLabelState(false);
        interfaceManager.Game_SetHudState(true);
        enemySpawner.StartTimer();
        powerUpManager.StartTimer();
    }

    private void PowerUpTimerTimeout()
    {
        powerUpTimeCurrent -= 0.1f;
        if (powerUpTimeCurrent <= 1)
        {
            interfaceManager.PlayerInfoBox.SetPowerUpBarCurrent(0.0f);
            player.SwitchShootType(ShootType.Single);
        }
        else
        {
            interfaceManager.PlayerInfoBox.SetPowerUpBarCurrent(powerUpTimeCurrent);
            powerUpTimer.Start();
        }
        
    }

    #endregion

    #region Signal Functions
    
    public void ActivatePause()
    {
        interfaceManager.SetGameUIState(false);

        interfaceManager.SetPauseUIState(true);

        Global.gamePaused = true;

        player.SetTakingInput(false);
    }

    public void UpdateGameUI()
    {
        interfaceManager.PlayerInfoBox.SetHealthBarCurrent(player.GetCurrentHealth());
    }

    public void GameOver()
    {
        //Stop Spawning Enemies
        enemySpawner.StopTimer();

        int tempCredits = score / 10;
        player.Credits += tempCredits;

        //Update UI
        interfaceManager.SetGameUIState(false);
        interfaceManager.SetResultUIState(true);

        interfaceManager.Result_SetScoreText(score);
        interfaceManager.Result_SetCreditsEarnedText(tempCredits);
        interfaceManager.Result_SetTotalCreditsText(player.Credits);
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
        interfaceManager.Game_SetScoreValueText(score);
    }

    public void ShootTypeSwitchEvent()
    {

        powerUpTimeCurrent = powerUpTimeMax;
        
        //start power up timer
        powerUpTimer.Start();
        
        //set shoot type
        player.SwitchShootType(ShootType.Spread_Random);
        
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