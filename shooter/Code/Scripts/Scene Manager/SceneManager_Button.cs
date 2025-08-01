using Godot;
using System;

public partial class SceneManager
{
    #region Main UI Button Functions

    public void Main_StartGameFunction()
    {
        //Set UI states
        UIManager.SetMainUIState(false);
        UIManager.SetGameUIState(true);
        
        //Reset temp game values
        score = 0;
        round = 0;
        enemiesLeft = 0;

        //Configure Player data
        player.Position = startPosition.Position;
        player.SetTakingInput(true);
        player.SetCurrentHealth(player.GetMaxHealth());
        player.Reparent(levelNode);
        
        //Set UI Data
        UIManager.playerInfoBox.SetHealthBarMax(player.GetMaxHealth());
        UIManager.playerInfoBox.SetHealthBarCurrent(player.GetCurrentHealth());
        UIManager.Game_SetScoreValueText(score);
        
        UIManager.Game_SetHudState(false);
        
        //Start timer
        StartIntroTimer();
        
    }

    public void Main_QuitGameFunction()
    {
        GetTree().Quit();
    }

    public void Main_OptionsButtonFunction()
    {
        
        //UIManager.SetMasterSliderValue(AudioServer.);
        
        //set UI states
        UIManager.SetMainUIState(false);
        UIManager.SetOptionsUIState(true);
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
    
    public void Main_StartGameHover()
    {
        UIManager.Main_SetSubTitleText("Start the game and see how many waves you can survive!");
    }

    public void Main_QuitGameHover()
    {
        UIManager.Main_SetSubTitleText("Close the game");
    }

    public void Main_UpgradeHover()
    {
        UIManager.Main_SetSubTitleText("Use the credits you've earned to upgrade your ship");
    }

    public void Main_OptionsHover()
    {
        UIManager.Main_SetSubTitleText("Change audio level and more");
    }
    
    public void Main_ButtonHoverExit()
    {
        UIManager.Main_SetSubTitleText("Micro game created by Justin Philie");
    }
    
    #endregion

    #region Options UI Button Functions

    private void Options_SimpleShootToggleFunction()
    {
        player.simpleShoot = !player.simpleShoot;
    }

    private void Options_FullscreenToggleFunction(bool toggled)
    {

       SetFullscreen(toggled);
        
    }

    private void Options_ResolutionDropDown(int index)
    {
        GD.Print(index);
        SetResolution(index);
    }

    private void Options_ResetSaveDataButtonFunction()
    {
        
        //Enable Delete Save Panel
        
        UIManager.SetDeleteSavePanelState(true);
        
    }

    private void Options_YesDeleteButtonFunction()
    {
        saveManager.ResetSave();
        saveManager.load();
        //Refresh Credits UI
        UIManager.Main_SetCreditsText(player.Credits);
        UIManager.SetDeleteSavePanelState(false);
    }

    private void Options_NoDeleteButtonFunction()
    {
        //Disable Delete Save Panel
        UIManager.SetDeleteSavePanelState(false);
    }
    
    private void Options_BackButtonFunction()
    {
        GD.Print("Resolution Setting: " + GameData.Instance.resolutionValue);
        GD.Print("Fullscreen Setting: " + GameData.Instance.isFullscreen);
        //set ui state
        UIManager.SetOptionsUIState(false);
        UIManager.SetMainUIState(true);
        saveManager.SaveConfig();
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