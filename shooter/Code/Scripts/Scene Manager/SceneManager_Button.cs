using Godot;
using System;

public partial class SceneManager
{

    #region Main UI Button Functions

    public void Main_StartGameFunction()
    {
        
        //Disable Main UI
        _uiManager.SetMainUIState(false);
        
        //Enable Game UI
        _uiManager.SetGameUIState(true);

        _score = 0;
        
        _uiManager.Game_SetScoreText(_score);
        
        //Create Player
        _player = _playerScene.Instantiate() as PlayerController;
        _levelNode.AddChild(_player);
        _player.Position = _startPosition.Position;
        _player.SetTakingInput(true);
        _player.SetSpeed(8.0f);
        
        
        if (_player == null)
        {
            GD.Print("Player is null");
        }
        
        //Assign Signal Functions
        _player.PauseSignal += ActivatePause;
        _player.PlayerHit += UpdateGameUI;
        _player.PlayerDied += GameOver;
        _player.EnemyDefeated += DefeatedEnemy;
        

        //Set UI Data
        _uiManager.Game_SetHealthBarCurrent(_player.GetCurrentHealth());
        _uiManager.Game_SetHealthBarMax(_player.GetMaxHealth());
        
        //Start Timer
        _enemySpawner.StartTimer();

    }

    public void Main_QuitGameFunction()
    {
        GetTree().Quit();
    }

    public void Main_ShopButtonFunction()
    {
        
        //Set Main UI State
        _uiManager.SetMainUIState(false);
        
        
        //Set Shop UI Data
        _uiManager.SetShopCreditsText(GameData.Instance.data["Credits"].AsInt32());
        
        
        //Set Shop UI State
        _uiManager.SetShopUIState(true);
        
    }

    #endregion

    #region Game UI Button Functions

    #endregion

    #region Pause UI Button Functions
    
    private void Pause_ResumeButtonFunction()
    {
        
        //Set UI States
        _uiManager.SetPauseUIState(false);
        _uiManager.SetGameUIState(true);
        Global.gamePaused = false;
        _player.SetTakingInput(true);

    }

    private void Pause_QuitButtonFunction()
    {
        
        //Destroy Player Object
        _player.QueueFree();
        
        //Destroy all enemies
        
        //Set UI States
        _uiManager.SetPauseUIState(false);
        _uiManager.SetMainUIState(true);
        
        _enemySpawner.StopTimer();
        
        //Reset game data
        Global.gamePaused = false;
        _score = 0;

    }
    
    #endregion

    #region Result UI Button Functions

    private void Result_RestartButtonFunction()
    {

        _saveManager.Save();
        
        
    }

    private void Result_MainMenuButtonFunction()
    {

        _saveManager.Save();
        
        _uiManager.Main_SetCreditsText(GameData.Instance.data["Credits"].AsInt32());
        
        _uiManager.SetResultUIState(false);
        _uiManager.SetMainUIState(true);
        
    }

    private void Result_QuitButtonFunction()
    {
        //Save Data HERE!!!
        _saveManager.Save();
        
        GetTree().Quit();
        
    }

    #endregion

    #region Shop UI Button Functions

    private void Shop_BackButtonFunction()
    {

        _uiManager.SetShopUIState(false);
        
        _uiManager.Main_SetCreditsText(GameData.Instance.data["Credits"].AsInt32());
        
        _uiManager.SetMainUIState(true);

    }

    #endregion
    
}
