using Godot;
using System;

public partial class UIManager : Node
{
    [ExportCategory("Main UI")]
    [Export] private CanvasLayer mainUIGroup;
    [Export] private Label mainCreditsLabel;
    [Export] private Label subTitleLabel;
    [Export] public Button StartButton;
    
    [ExportCategory("Shop UI")]
    [Export] private CanvasLayer shopUIGroup;
    [Export] private Label shopCreditsLabel;
    [Export] private Label shopHealthLevelLabel;
    [Export] public UpgradePanel HealthUpgradePanel;
    [Export] public UpgradePanel FireRateUpgradePanel;
    [Export] public UpgradePanel SpeedUpgradePanel;

    [ExportCategory("Options UI")]
    [Export] private CanvasLayer optionsUIGroup;
    [Export] private Label cheatDescriptionLabel;
    [Export] private LineEdit cheatIF;
    [Export] private Slider masterVolumeSlider;
    [Export] private Slider sfxVolumeSlider;
    [Export] private Slider musicVolumeSlider;
    [Export] private Panel deleteSavePanel;

    [ExportCategory("Game UI")]
    [Export] public PlayerInfoBox PlayerInfoBox;
    [Export] private CanvasLayer gameUIGroup;
    [Export] private Label gameScoreValueLabel;
    [Export] private Label countdownLabel;
    [Export] private Label roundLabel;

    [ExportCategory("Pause UI")]
    [Export] private CanvasLayer pauseUIGroup;
    [Export] public Button pauseResumeButton;

    [ExportCategory("Result UI")]
    [Export] private CanvasLayer resultUIGroup;

    [Export] private Label resultScoreLabel;
    [Export] private Label resultCreditsEarnedLabel;
    [Export] private Label resultTotalCreditsLabel;

    [ExportCategory("Debug UI")]
    [Export] private CanvasLayer debugUIGroup;

    [Export] private Label FramerateLabel;

    public override void _Ready()
    {
    }

    #region Main UI Functions

    public void SetMainUIState(bool state)
    {
        mainUIGroup.Visible = state;
    }

    public void Main_SetCreditsText(int newCredits)
    {
        mainCreditsLabel.Text = "Credits: " + newCredits.ToString();
    }

    public void Main_SetSubTitleText(string newSubTitle)
    {
        subTitleLabel.Text = newSubTitle;
    }

    #endregion

    #region Options UI Functions

    public void SetOptionsUIState(bool state)
    {
        optionsUIGroup.Visible = state;
    }

    public void SetDeleteSavePanelState(bool state)
    {
        deleteSavePanel.Visible = state;
    }

    public void SetMasterSliderValue(float value)
    {
        masterVolumeSlider.Value = value;
    }

    public void SetSFXSliderValue(float value)
    {
        sfxVolumeSlider.Value = value;
    }

    public void SetMusicSliderValue(float value)
    {
        musicVolumeSlider.Value = value;
    }

    // public void SetFullscreenToggleState(bool state)
    // {
    //     fullscreenToggle.ToggleMode = state;
    // }

    // public void SetResolutionOptionButtonValue(int value)
    // {
    //     resolutionOptionButton.Selected = value;
    // }

    public void SetCheatDescriptionLabelText(string description)
    {
        cheatDescriptionLabel.Text = description;
    }

    public string GetCheatIFString()
    {
        return cheatIF.Text;
    }

    #endregion

    #region Game UI functions

    public void SetGameUIState(bool state)
    {
        gameUIGroup.Visible = state;
    }

    public void Game_ShowRound()
    {
        //Hide healthbar, credits, etc
        PlayerInfoBox.Visible = false;
        gameScoreValueLabel.Visible = false;

        //Show countdown/round label
        //countdownLabel.Visible = true;
    }

    public void Game_HideRound()
    {
        //Hide countdown/round label
        countdownLabel.Visible = false;

        //Show healthbar, credits, etc

        PlayerInfoBox.Visible = true;
        gameScoreValueLabel.Visible = true;
    }

    public void Game_SetScoreValueText(int newScore)
    {
        gameScoreValueLabel.Text = newScore.ToString();
    }

    public void Game_SetCountDownLabelText(string timeVal)
    {
        countdownLabel.Text = timeVal;
    }

    public void Game_SetCountDownLabelText(int timeVal)
    {
        countdownLabel.Text = timeVal.ToString();
    }

    public void Game_SetCountDownLabelState(bool state)
    {
        countdownLabel.Visible = state;
    }

    public void Game_SetRoundLabelText(int round)
    {
        roundLabel.Text = "Round " + round.ToString();
    }

    public void Game_SetRoundLabelState(bool state)
    {
        roundLabel.Visible = state;
    }

    public void Game_SetHudState(bool state)
    {
        PlayerInfoBox.Visible = state;
        gameScoreValueLabel.Visible = state;
    }

    #endregion

    #region Pause UI Functions

    public void SetPauseUIState(bool state)
    {
        pauseUIGroup.Visible = state;
    }

    #endregion

    #region Result UI Functions

    public void SetResultUIState(bool state)
    {
        resultUIGroup.Visible = state;
    }

    public void Result_SetScoreText(int newScore)
    {
        resultScoreLabel.Text = "Score: " + newScore;
    }

    public void Result_SetCreditsEarnedText(int newCredits)
    {
        resultCreditsEarnedLabel.Text = "Credits Earned: " + newCredits.ToString();
    }

    public void Result_SetTotalCreditsText(int newCredits)
    {
        resultTotalCreditsLabel.Text = "Total Credits: " + newCredits.ToString();
    }

    #endregion

    #region Upgrade UI Functions

    public void SetShopUIState(bool state)
    {
        shopUIGroup.Visible = state;
    }

    public void Shop_SetCreditsText(int newCredits)
    {
        shopCreditsLabel.Text = "Credits: " + newCredits.ToString();
    }

    #endregion

    #region Debug UI Functions

    public void SetDebugUIState(bool state)
    {
        debugUIGroup.Visible = state;
    }

    public void SetFramerateLabelText(double value)
    {
        FramerateLabel.Text = "FPS: " + value;
    }

    #endregion

    #region Getter

    #endregion

    #region Setter

    #endregion
}