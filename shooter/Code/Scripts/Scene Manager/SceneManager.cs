using Godot;
using System;

public partial class SceneManager : Node
{
    [ExportCategory("Debug")]
    [Export] private bool debug;

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

    [ExportCategory("Upgrade Values")]
    [Export] private int healthUpgradeCost; //Cost of the Health upgrade

    [Export] private int healthUpgradeAmount; //Amount of Health points added upon upgrade
    [Export] private int fireRateUpgradeCost;
    [Export] private float fireRateUpgradeAmount;
    [Export] private int speedUpgradeCost;
    [Export] private float speedUpgradeAmount;

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

        //load setting configuration file
        LoadConfig();

        //load player save data
        LoadSave();

        //load player data
        PlayerSetup();

        //Setup game data
        GameDataSetup();

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

    /// <summary>
    /// Loads settings config file
    /// </summary>
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

    private void LoadSave()
    {
        bool loadResult = saveManager.load();

        if (!loadResult)
        {
            saveManager.NewSave();
        }
    }

    private void PlayerSetup()
    {
        player = PlayerController.Instance;
        player.Position = startPosition.Position;
        player.bulletManager = bulletManager;
        SetPlayerSignals();
        player.FindNodes();
    }

    /// <summary>
    /// Sets game data based on Player information
    /// </summary>
    private void GameDataSetup()
    {
        RefreshHealthUpgradeValues(player.Stats.HealthLevel);
        RefreshFireRateUpgradeValues(player.Stats.FireRateLevel);
        RefreshSpeedUpgradeValues(player.Stats.SpeedLevel);
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

        if (debug)
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
        Global.Round++;

        if (Global.Round == 1)
        {
            enemiesLeftMax = 5;
        }
        else if (enemiesLeftMax != 80)
        {
            enemiesLeftMax += 5;
        }

        if (Global.Round % 5 == 0)
        {
            enemySpawner.DecrementTimer();
        }

        enemiesLeft = enemiesLeftMax;
        interfaceManager.Game_SetRoundLabelText(Global.Round);
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
            player.Input.SetTakingInput(true);
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
            player.ResetShootTimer();
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

        Global.GamePaused = true;

        //Pause all timers
        powerUpManager.PauseTimer();
        enemySpawner.PauseTimer();

        if (!roundTimer.Paused)
        {
            roundTimer.Paused = true;
        }

        if (!introTimer.Paused)
        {
            introTimer.Paused = true;
        }

        player.Input.SetTakingInput(false);
    }

    public void UpdateGameUI()
    {
        interfaceManager.PlayerInfoBox.SetHealthBarCurrent(player.GetCurrentHealth());
    }

    public void GameOver()
    {
        //Stop Spawning Enemies
        enemySpawner.StopTimer();
        powerUpManager.StopTimer();

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

    public void ShootTypeSwitchEvent(PowerUpStats_ShootType shootStats)
    {
        powerUpTimeCurrent = powerUpTimeMax;
        //start power up timer
        powerUpTimer.Start();
        player.SwitchShootType(shootStats.ShootType);
        //player.UpdateFireRate(shootStats.FireRate);
    }

    #endregion

    #region Supporting Functions

    private void SetPlayerSignals()
    {
        player.PauseSignal += ActivatePause;
        player.PlayerHealed += UpdateGameUI;
        player.PlayerHit += UpdateGameUI;
        player.PlayerDied += GameOver;
        player.EnemyDefeated += DefeatedEnemy;
        player.ShootTypePowerUp += ShootTypeSwitchEvent;
    }

    /// <summary>
    /// Refreshes the HealthUpgradeCost & HealthUpgradeAmount variables based on Players stat level
    /// </summary>
    /// <param name="level"></param>
    private void RefreshHealthUpgradeValues(int level)
    {
        switch (level)
        {
            case 1:
                healthUpgradeCost = 50;
                healthUpgradeAmount = 50;
                break;
            case 2:
                healthUpgradeCost = 100;
                healthUpgradeAmount = 100;
                break;
            case 3:
                healthUpgradeCost = 200;
                healthUpgradeAmount = 150;
                break;
            case 4:
                healthUpgradeCost = 400;
                healthUpgradeAmount = 200;
                break;
            case 5:
                healthUpgradeCost = 800;
                healthUpgradeAmount = 300;
                break;
        }
    }

    /// <summary>
    /// Refreshes the FireRateUpgradeCost & FireRateUpgradeAmount variables based on Players stat level
    /// </summary>
    /// <param name="level"> The current level of the Fire Rate</param>
    private void RefreshFireRateUpgradeValues(int level)
    {
        switch (level)
        {
            case 1:
                fireRateUpgradeCost = 50;
                fireRateUpgradeAmount = 0.05f;
                break;
            case 2:
                fireRateUpgradeCost = 100;
                fireRateUpgradeAmount = 0.05f;
                break;
            case 3:
                fireRateUpgradeCost = 200;
                fireRateUpgradeAmount = 0.05f;
                break;
            case 4:
                fireRateUpgradeCost = 400;
                fireRateUpgradeAmount = 0.05f;
                break;
            case 5:
                fireRateUpgradeCost = 800;
                fireRateUpgradeAmount = 0.05f;
                break;
        }
    }

    private void RefreshSpeedUpgradeValues(int level)
    {
        switch (level)
        {
            case 1:
                speedUpgradeCost = 50;
                speedUpgradeAmount = 1.0f;
                break;
            case 2:
                speedUpgradeCost = 100;
                speedUpgradeAmount = 1.0f;
                break;
            case 3:
                speedUpgradeCost = 200;
                speedUpgradeAmount = 1.0f;
                break;
            case 4:
                speedUpgradeCost = 400;
                speedUpgradeAmount = 1.0f;
                break;
            case 5:
                speedUpgradeCost = 800;
                speedUpgradeAmount = 1.0f;
                break;
        }
    }

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