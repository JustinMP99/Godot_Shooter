using Godot;
using System;

public partial class SceneManager : Node
{
    [Export] private SaveManager _saveManager;
    [Export] private UIManager _uiManager;
    [Export] private EnemySpawner _enemySpawner;
    [Export] private PackedScene _playerScene;
    [Export] private Node _levelNode;
    //[Export] private bool gamePaused;
    [Export] private Node3D _startPosition;
    [ExportCategory("Player Data")] 
    [Export] private int _score;

    private PlayerController _player;
    
    public override void _Ready()
    {
        
        
        //UI Setup
        _uiManager.SetMainUIState(true);
        _uiManager.SetPauseUIState(false);
        _uiManager.SetGameUIState(false);
        _uiManager.SetResultUIState(false);
        _uiManager.SetShopUIState(false);
        
        //Spawner Startup
        _enemySpawner.Startup();
        
        _player = PlayerController.Instance;
        _player.Position = new Vector3(0.0f, 0.0f, 10.0f);
        
        bool loadResult = _saveManager.load();

        if (!loadResult)
        {
            _player._credits = 0;
            _player.stats._currentHealth = 50;
            _player.stats._maxHealth = 50;
            _player.stats._healthLevel = 1;
            _saveManager.Save();
            GD.Print("First time save");
        }
        
        //_saveManager.Save();
        
        _uiManager.Main_SetCreditsText(_player._credits);
    }
    
    public void ActivatePause()
    {
        
        _uiManager.SetGameUIState(false);
        
        _uiManager.SetPauseUIState(true);

        Global.gamePaused = true;
        
        _player.SetTakingInput(false);
        
    }

    public void UpdateGameUI()
    {
        _uiManager.Game_SetHealthBarCurrent( _player.GetCurrentHealth());
    }

    public void GameOver()
    {
        
        //Stop Spawning Enemies
        _enemySpawner.StopTimer();

        int tempCredits = _score / 10;
       _player._credits += tempCredits;
        
        //Update UI
        _uiManager.SetGameUIState(false);
        _uiManager.SetResultUIState(true);
       
        _uiManager.Result_SetScoreText(_score);
        _uiManager.Result_SetCreditsEarnedText(tempCredits);
        _uiManager.Result_SetTotalCreditsText(_player._credits);
        
    }

    public void DefeatedEnemy()
    {
        //Increase Score
        _score += 100;
        //Update UI
        _uiManager.Game_SetScoreText(_score);
    }
    
}
