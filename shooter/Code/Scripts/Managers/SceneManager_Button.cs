using Godot;
using System;

public partial class SceneManager
{

    #region Main UI Button Functions

    public void Main_StartGameFunction()
    {
        
        //Disable Main UI
        UIManager.SetMainUIState(false);
        
        //Enable Game UI
        UIManager.SetGameUIState(true);

        score = 0;
        
        UIManager.Game_SetScoreText(score);
        
        //Create Player
        player = playerScene.Instantiate() as PlayerController;
        levelNode.AddChild(player);
        player.Position = startPosition.Position;
        player.SetTakingInput(true);
        player.SetSpeed(8.0f);
        
        
        if (player == null)
        {
            GD.Print("Player is null");
        }
        
        //Assign Signal Functions
        player.PauseSignal += ActivatePause;
        player.PlayerHit += UpdateGameUI;
        player.PlayerDied += GameOver;
        player.EnemyDefeated += DefeatedEnemy;
        

        //Set UI Data
        UIManager.Game_SetHealthBarCurrent(player.GetCurrentHealth());
        UIManager.Game_SetHealthBarMax(player.GetMaxHealth());
        
        //Start Timer
        enemySpawner.StartTimer();

    }

    public void Main_QuitGameFunction()
    {
        GetTree().Quit();
    }

    public void Main_ShopButtonFunction()
    {
        
    }

    #endregion

    #region Game UI Button Functions

    #endregion

    #region Pause UI Button Functions
    
    private void Pause_ResumeButtonFunction()
    {
        
        //Set UI States
        UIManager.SetPauseUIState(false);
        UIManager.SetGameUIState(true);
        Global.gamePaused = false;
        player.SetTakingInput(true);

    }

    private void Pause_QuitButtonFunction()
    {
        
        //Destroy Player Object
        player.QueueFree();
        
        //Destroy all enemies
        
        //Set UI States
        UIManager.SetPauseUIState(false);
        UIManager.SetMainUIState(true);
        
        enemySpawner.StopTimer();
        
        //Reset game data
        Global.gamePaused = false;
        score = 0;

    }
    
    #endregion

    #region Result UI Button Functions

    private void Result_RestartButtonFunction()
    {

    }

    private void Result_MainMenuButtonFunction()
    {
        
        UIManager.Main_SetCreditsText(credits);
        
        UIManager.SetResultUIState(false);
        UIManager.SetMainUIState(true);
        
    }

    private void Result_QuitButtonFunction()
    {
        //Save Data HERE!!!
        
        
        GetTree().Quit();
        
    }

    #endregion
    
    
}
