using Godot;
using System;

public partial class SceneManager
{
    #region Main UI Button Functions

    public void Main_StartGameFunction()
    {
        //Set UI states
        interfaceManager.SetMainUIState(false);
        interfaceManager.SetGameUIState(true);
        
        //Reset temp game values
        score = 0;
        Global.Round = 0;
        enemiesLeft = 0;
        
        enemySpawner.ResetTimerValue();

        //Configure Player data
        player.Reset();
        player.Position = startPosition.Position;
        
        //Set UI Data
        interfaceManager.PlayerInfoBox.SetHealthBarMax(player.GetMaxHealth());
        interfaceManager.PlayerInfoBox.SetHealthBarCurrent(player.GetCurrentHealth());
        interfaceManager.Game_SetScoreValueText(score);
        
        interfaceManager.Game_SetHudState(false);
        
        player.SetShipFlyInTrue();
        player.SetReticleFlyInTrue();
        
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
        interfaceManager.SetMainUIState(false);
        interfaceManager.SetOptionsUIState(true);
    }
    
    public void Main_ShopButtonFunction()
    {
        //Set Main UI State
        interfaceManager.SetMainUIState(false);

        //Set Shop UI Data
        if (player.Stats.HealthLevel != 5)
        {
            
        }
        interfaceManager.Shop_SetCreditsText(player.Credits);
        interfaceManager.Shop_SetHealthDescriptionText(healthUpgradeCost);
        interfaceManager.Shop_SetHealthLevelText(player.Stats.HealthLevel, 5);

        //Set Shop UI State
        interfaceManager.SetShopUIState(true);
    }
    
    public void Main_StartGameHover()
    {
        interfaceManager.Main_SetSubTitleText("Start the game and see how many waves you can survive!");
    }

    public void Main_QuitGameHover()
    {
        interfaceManager.Main_SetSubTitleText("Close the game");
    }

    public void Main_UpgradeHover()
    {
        interfaceManager.Main_SetSubTitleText("Use the credits you've earned to upgrade your ship");
    }

    public void Main_OptionsHover()
    {
        interfaceManager.Main_SetSubTitleText("Change audio level and more");
    }
    
    public void Main_ButtonHoverExit()
    {
        interfaceManager.Main_SetSubTitleText("Micro game created by Justin Philie");
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

    private void Options_EnterCheatButtonFunction()
    {
        
        //Get string from input field
        string cheatCode = interfaceManager.GetCheatIFString();

        //make player invincible
        if (cheatCode.Contains("UNBEATABLE"))
        {

            bool result = player.SetInvincibleState();
            if (player.Invincible)
            {
                interfaceManager.SetCheatDescriptionLabelText("You are now invincible");
            }
            else
            {
                interfaceManager.SetCheatDescriptionLabelText("You are now vincible");
            }
        }
        //make bullets instakill
        if (cheatCode.Contains("ONESHOT"))
        {
            bool result = bulletManager.SetBulletsInstaKillState();
            //print out result
            if (result)
            {
                interfaceManager.SetCheatDescriptionLabelText("Enabled oneshot bullets");
            }
            else
            {
                interfaceManager.SetCheatDescriptionLabelText("Disabled oneshot bullets");
            }
        }
    }
    
    private void Options_ResetSaveDataButtonFunction()
    {
        
        //Enable Delete Save Panel
        
        interfaceManager.SetDeleteSavePanelState(true);
        
    }

    private void Options_YesDeleteButtonFunction()
    {
        saveManager.ResetSave();
        saveManager.load();
        //Refresh Credits UI
        interfaceManager.Main_SetCreditsText(player.Credits);
        interfaceManager.SetDeleteSavePanelState(false);
    }

    private void Options_NoDeleteButtonFunction()
    {
        //Disable Delete Save Panel
        interfaceManager.SetDeleteSavePanelState(false);
    }
    
    private void Options_BackButtonFunction()
    {
        GD.Print("Resolution Setting: " + GameData.Instance.ResolutionValue);
        GD.Print("Fullscreen Setting: " + GameData.Instance.Fullscreen);
        //set ui state
        interfaceManager.SetOptionsUIState(false);
        interfaceManager.SetMainUIState(true);
        saveManager.SaveConfig();
    }
    
    #endregion
    
    #region Game UI Button Functions

    #endregion

    #region Pause UI Button Functions

    private void Pause_ResumeButtonFunction()
    {
        //Set UI States
        interfaceManager.SetPauseUIState(false);
        interfaceManager.SetGameUIState(true);
        Global.GamePaused = false;
        enemySpawner.ResumeTimer();
        powerUpManager.ResumeTimer();
        if (roundTimer.Paused)
        {
            roundTimer.Paused = false;
        }

        if (introTimer.Paused)
        {
            introTimer.Paused = false;
        }
        player.SetTakingInput(true);
    }

    private void Pause_QuitButtonFunction()
    {

        player.Position = new Vector3(0.0f, 0.0f, 100.0f);
        //Destroy all enemies

        //Set UI States
        interfaceManager.SetPauseUIState(false);
        interfaceManager.SetMainUIState(true);
        
        if (roundTimer.Paused)
        {
            roundTimer.Paused = false;
        }

        if (introTimer.Paused)
        {
            introTimer.Paused = false;
        }
        
        introTimer.Stop();
        roundTimer.Stop();
        enemySpawner.StopTimer();
        powerUpManager.StopTimer();

        //Reset game data
        Global.GamePaused = false;
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
        interfaceManager.Main_SetCreditsText(player.Credits);

        interfaceManager.SetResultUIState(false);

        interfaceManager.SetMainUIState(true);

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
        interfaceManager.SetShopUIState(false);

        interfaceManager.Main_SetCreditsText(player.Credits);

        interfaceManager.SetMainUIState(true);
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
            player.SetMaxHealth(player.GetMaxHealth() + healthUpgradeCost);
            player.SetCurrentHealth(player.GetMaxHealth());
            
            RefreshHealthUpgradeValues(player.Stats.HealthLevel);
            interfaceManager.Shop_SetHealthLevelText(player.Stats.HealthLevel, 5);
            interfaceManager.Shop_SetHealthDescriptionText(healthUpgradeCost);
            interfaceManager.Shop_SetCreditsText(player.Credits);
            saveManager.Save();
        }
    }

    #endregion
}