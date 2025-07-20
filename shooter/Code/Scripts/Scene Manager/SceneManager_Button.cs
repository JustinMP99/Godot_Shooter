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
        round = 1;

        UIManager.Game_SetScoreText(score);
        player.Reparent(levelNode);
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

    public void Main_SettingsButtonFunction()
    {
        
    }
    
    public void Main_ShopButtonFunction()
    {
        //Set Main UI State
        UIManager.SetMainUIState(false);

        //Set Shop UI Data
        UIManager.Shop_SetCreditsText(player.Credits);
        UIManager.Shop_SetHealthLevelText(player.Stats.HealthLevel, 5);

        //Set Shop UI State
        UIManager.SetShopUIState(true);
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
        //PlayerController.Instance.QueueFree();
        player.Position = new Vector3(0.0f, 0.0f, 10.0f);
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
        player.Stats.CurrentHealth = player.Stats.MaxHealth;

        saveManager.Save();
    }

    private void Result_MainMenuButtonFunction()
    {
        UIManager.Main_SetCreditsText(player.Credits);

        player.Stats.CurrentHealth = player.Stats.MaxHealth;

        UIManager.SetResultUIState(false);

        UIManager.SetMainUIState(true);

        saveManager.Save();
    }

    private void Result_QuitButtonFunction()
    {
        //Save Data HERE!!!
        saveManager.Save();

        GetTree().Quit();
    }

    #endregion

    #region Shop UI Button Functions

    private void Shop_BackButtonFunction()
    {
        UIManager.SetShopUIState(false);

        UIManager.Main_SetCreditsText(player.Credits);

        UIManager.SetMainUIState(true);
    }

    private void Shop_UpgradeHealthButtonFunction()
    {
        if (player.Stats.HealthLevel != 5)
        {
            switch (player.Stats.HealthLevel)
            {
                case 1:

                    player.Credits -= 50;

                    break;

                case 2:
                    player.Credits -= 100;
                    break;

                case 3:
                    player.Credits -= 150;
                    break;
                case 4:
                    player.Credits -= 200;
                    break;
            }

            player.Stats.HealthLevel++;
            player.SetMaxHealth(player.GetMaxHealth() + 50);
            player.SetCurrentHealth(player.GetMaxHealth());
            UIManager.Shop_SetHealthLevelText(player.Stats.HealthLevel, 5);
            UIManager.Shop_SetCreditsText(player.Credits);
            saveManager.Save();
        }
    }

    #endregion
}