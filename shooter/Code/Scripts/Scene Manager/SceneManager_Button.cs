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
        interfaceManager.PlayerInfoBox.SetHealthBarMax(player.Stats.GetMaxHealth());
        interfaceManager.PlayerInfoBox.SetHealthBarCurrent(player.Stats.GetCurrentHealth());
        interfaceManager.Game_SetScoreValueText(score);

        interfaceManager.Game_SetHudState(false);

        player.animController.SetShipFlyInTrue();
        player.animController.SetReticleFlyInTrue();

        //Start timer
        StartIntroTimer();
    }

    public void Main_QuitGameFunction()
    {
        GetTree().Quit();
    }

    public void Main_OptionsButtonFunction()
    {
        //set UI states
        interfaceManager.SetMainUIState(false);
        interfaceManager.SetOptionsUIState(true);
    }

    public void Main_ShopButtonFunction()
    {
        //Set Main UI State
        interfaceManager.SetMainUIState(false);

        interfaceManager.BackButton.GrabFocus();
        
        //Set Shop UI Data
        interfaceManager.Shop_SetCreditsText(player.Stats.GetCredits());

        if (player.Stats.GetHealthLevel() == 5)
        {
            interfaceManager.HealthUpgradePanel.SetDescription("Health has been fully upgraded");
            interfaceManager.HealthUpgradePanel.SetCostLabel(" ");
            interfaceManager.HealthUpgradePanel.SetUpgradeButtonDisabledState(true);
        }
        else
        {
            interfaceManager.HealthUpgradePanel.SetDescription("Increase Health by " + healthUpgradeAmount.ToString() +
                                                               " points");
            //interfaceManager.Shop_SetHealthDescriptionText(healthUpgradeAmount);
            interfaceManager.HealthUpgradePanel.SetCostLabel("Cost: " + healthUpgradeCost.ToString());
        }

        interfaceManager.HealthUpgradePanel.SetLevelLabel(player.Stats.GetHealthLevel(), 5);

        if (player.Stats.GetFireRateLevel() == 5)
        {
            interfaceManager.FireRateUpgradePanel.SetDescription("Fire Rate has been fully upgraded");
            interfaceManager.FireRateUpgradePanel.SetCostLabel(" ");
            interfaceManager.FireRateUpgradePanel.SetUpgradeButtonDisabledState(true);
        }
        else
        {
            interfaceManager.FireRateUpgradePanel.SetDescription("Increase Fire Rate by " + fireRateUpgradeAmount +
                                                                 " points");
            //interfaceManager.Shop_SetHealthDescriptionText(healthUpgradeAmount);
            interfaceManager.FireRateUpgradePanel.SetCostLabel("Cost: " + healthUpgradeCost.ToString());
        }

        interfaceManager.FireRateUpgradePanel.SetLevelLabel(player.Stats.GetFireRateLevel(), 5);

        if (player.Stats.GetSpeedLevel() == 5)
        {
            interfaceManager.SpeedUpgradePanel.SetDescription("Speed has been fully upgraded");
            interfaceManager.SpeedUpgradePanel.SetCostLabel(" ");
            interfaceManager.SpeedUpgradePanel.SetUpgradeButtonDisabledState(true);
        }
        else
        {
            interfaceManager.SpeedUpgradePanel.SetDescription("Increase Speed by " + speedUpgradeAmount + " point");
            interfaceManager.SpeedUpgradePanel.SetCostLabel("Cost: " + speedUpgradeCost);
        }

        interfaceManager.SpeedUpgradePanel.SetLevelLabel(player.Stats.GetSpeedLevel(), 5);

        player.Input.CurrentButton = interfaceManager.BackButton;
        interfaceManager.BackButton.GrabFocus();
        
        
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
        saveManager.NewSave();
        saveManager.load();
        //Refresh Credits UI
        interfaceManager.Main_SetCreditsText(player.Stats.GetCredits());
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

        player.Input.SetTakingInput(true);
        
        player.Input.SwitchInputState(InputState.Game);
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

        player.Input.CurrentButton = interfaceManager.StartButton;
        interfaceManager.StartButton.GrabFocus();
    }

    #endregion

    #region Result UI Button Functions

    private void Result_RestartButtonFunction()
    {
        player.Stats.SetCurrentHealth(player.Stats.GetMaxHealth());

        saveManager.Save();
    }

    private void Result_MainMenuButtonFunction()
    {
        interfaceManager.Main_SetCreditsText(player.Stats.GetCredits());

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

        interfaceManager.Main_SetCreditsText(player.Stats.GetCredits());

        player.Input.CurrentButton = interfaceManager.StartButton;
        
        interfaceManager.StartButton.GrabFocus();

        interfaceManager.SetMainUIState(true);
    }

    private void Shop_UpgradeHealthButtonFunction()
    {
        if (player.Stats.GetHealthLevel() != 5)
        {
            if (player.Stats.GetCredits() >= healthUpgradeCost)
            {
                //subtract credits
                player.Stats.DecreaseCredits(healthUpgradeCost);
                //increase health level
                player.Stats.IncrementHealthLevel();
                //set max health
                player.Stats.SetMaxHealth(player.Stats.GetMaxHealth() + healthUpgradeAmount);
                //set current health
                player.Stats.SetCurrentHealth(player.Stats.GetMaxHealth());
                //update health upgrade cost and amount based on the new health level
                RefreshHealthUpgradeValues(player.Stats.GetHealthLevel());
                //update UI
                interfaceManager.HealthUpgradePanel.SetLevelLabel(player.Stats.GetHealthLevel(), 5);
                interfaceManager.HealthUpgradePanel.SetDescription("Increase Health by " + healthUpgradeAmount +
                                                                   " points");
                interfaceManager.HealthUpgradePanel.SetCostLabel("Cost: " + healthUpgradeCost.ToString());
                interfaceManager.Shop_SetCreditsText(player.Stats.GetCredits());
                //save 
                saveManager.Save();
            }
            else
            {
                //Notify player that they do not have enough credits
            }
        }

        if (player.Stats.GetHealthLevel() == 5)
        {
            //Disable health upgrade button
            interfaceManager.HealthUpgradePanel.SetUpgradeButtonDisabledState(true);
            //Set description text
            interfaceManager.HealthUpgradePanel.SetDescription("Health has been fully upgraded");
            interfaceManager.HealthUpgradePanel.SetCostLabel(" ");
            interfaceManager.HealthUpgradePanel.SetUpgradeButtonDisabledState(true);
        }
    }

    private void Shop_UpgradeFireRateButtonFunction()
    {
        if (player.Stats.GetFireRateLevel()!= 5)
        {
            if (player.Stats.GetCredits() >= fireRateUpgradeCost)
            {
                //subtract credits
                player.Stats.DecreaseCredits(fireRateUpgradeCost);
             
                //increase fire rate level
                player.Stats.IncrementFireRateLevel();

                //set fire rate
                player.Stats.SetFireRate(player.Stats.GetFireRate() - fireRateUpgradeAmount);
                //Reset shoot timer
                player.Gun.UpdateShootTimer(player.Stats.GetFireRate());
                GD.Print("Fire Rate Upgrade Amount: " + fireRateUpgradeAmount);

                //update health upgrade cost and amount based on the new health level
                RefreshFireRateUpgradeValues(player.Stats.GetFireRateLevel());

                //update UI
                interfaceManager.FireRateUpgradePanel.SetLevelLabel(player.Stats.GetFireRateLevel(), 5);
                interfaceManager.FireRateUpgradePanel.SetDescription("Increase Fire Rate by" + fireRateUpgradeAmount +
                                                                     " points");
                interfaceManager.FireRateUpgradePanel.SetCostLabel("Cost: " + fireRateUpgradeCost.ToString());
                interfaceManager.Shop_SetCreditsText(player.Stats.GetCredits());

                //save 
                saveManager.Save();
            }
            else
            {
                //notify player that they do not have enough credits
            }
        }

        if (player.Stats.GetFireRateLevel() == 5)
        {
            //Disable health upgrade button
            interfaceManager.FireRateUpgradePanel.SetUpgradeButtonDisabledState(true);
            //Set description text
            interfaceManager.FireRateUpgradePanel.SetDescription("Fire Rate has been fully upgraded");
            interfaceManager.FireRateUpgradePanel.SetCostLabel(" ");
            interfaceManager.FireRateUpgradePanel.SetUpgradeButtonDisabledState(true);
        }
    }

    private void Shop_UpgradeSpeedButtonFunction()
    {
        if (player.Stats.GetSpeedLevel() != 5)
        {
            if (player.Stats.GetCredits() >= speedUpgradeCost)
            {
                //subtract credits
                player.Stats.DecreaseCredits(speedUpgradeCost);

                //increase fire rate level
                player.Stats.IncrementSpeedLevel();
                
                //set fire rate
                player.Stats.SetSpeed(player.Stats.GetSpeed() + speedUpgradeAmount);
                //Reset shoot timer
                player.Gun.UpdateShootTimer(player.Stats.GetFireRate());
                GD.Print("Speed Upgrade Amount: " + speedUpgradeAmount);

                //update health upgrade cost and amount based on the new health level
                RefreshFireRateUpgradeValues(player.Stats.GetSpeedLevel());

                //update UI
                interfaceManager.SpeedUpgradePanel.SetLevelLabel(player.Stats.GetSpeedLevel(), 5);
                interfaceManager.SpeedUpgradePanel.SetDescription("Increase Speed by" + speedUpgradeAmount + " point");
                interfaceManager.SpeedUpgradePanel.SetCostLabel("Cost: " + speedUpgradeCost.ToString());
                interfaceManager.Shop_SetCreditsText(player.Stats.GetCredits());

                //save 
                saveManager.Save();
            }
            else
            {
                //notify player that they do not have enough credits
            }
        }

        if (player.Stats.GetSpeedLevel() == 5)
        {
            //Disable health upgrade button
            interfaceManager.SpeedUpgradePanel.SetUpgradeButtonDisabledState(true);
            //Set description text
            interfaceManager.SpeedUpgradePanel.SetDescription("Speed has been fully upgraded");
            interfaceManager.SpeedUpgradePanel.SetCostLabel(" ");
            interfaceManager.SpeedUpgradePanel.SetUpgradeButtonDisabledState(true);
        }
    }

    #endregion
}