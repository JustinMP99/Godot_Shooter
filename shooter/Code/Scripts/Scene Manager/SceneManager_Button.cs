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
        _player.Reparent(_levelNode);
        _player.Position = _startPosition.Position;
        _player.SetTakingInput(true);
        _player.SetSpeed(8.0f);
        
        
        if ( _player == null)
        {
            GD.Print("Player is null");
        }
        
        //Assign Signal Functions
        _player.PauseSignal += ActivatePause;
        _player.PlayerHit += UpdateGameUI;
        _player.PlayerDied += GameOver;
        _player.EnemyDefeated += DefeatedEnemy;
        

        //Set UI Data
        _uiManager.Game_SetHealthBarCurrent( _player.GetCurrentHealth());
        _uiManager.Game_SetHealthBarMax( _player.GetMaxHealth());
        
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
        _uiManager.Shop_SetCreditsText(_player._credits);
        _uiManager.Shop_SetHealthLevelText(_player.stats._healthLevel, 5);
        
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
        //PlayerController.Instance.QueueFree();
        _player.Position = new Vector3(0.0f, 0.0f, 10.0f);
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

        _player.stats._currentHealth = _player.stats._maxHealth;
        
        _saveManager.Save();
        
    }

    private void Result_MainMenuButtonFunction()
    {
        
        _uiManager.Main_SetCreditsText(_player._credits);

        _player.stats._currentHealth = _player.stats._maxHealth;
        
        _uiManager.SetResultUIState(false);
        
        _uiManager.SetMainUIState(true);
        
        _saveManager.Save();
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
        
        _uiManager.Main_SetCreditsText(_player._credits);
        
        _uiManager.SetMainUIState(true);

    }

    private void Shop_UpgradeHealthButtonFunction()
    {

        if (_player.stats._healthLevel != 5)
        {

            switch (_player.stats._healthLevel)
            {
                
                case 1:

                    _player._credits -= 50;
                    
                    break;
                
                case 2:
                    _player._credits -= 100;
                    break;
                
                case 3:
                    _player._credits -= 150;
                    break;
                case 4:
                    _player._credits -= 200;
                    break;
            }
           
            _player.stats._healthLevel++;
            _player.SetMaxHealth(_player.GetMaxHealth() + 50);
            _player.SetCurrentHealth(_player.GetMaxHealth());
            _uiManager.Shop_SetHealthLevelText(_player.stats._healthLevel, 5);
            _uiManager.Shop_SetCreditsText(_player._credits);
            _saveManager.Save();
        }
        
    }

    #endregion
    
}
