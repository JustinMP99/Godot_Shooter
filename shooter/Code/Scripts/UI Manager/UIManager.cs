using Godot;
using System;

public partial class UIManager : Node
{
    [ExportCategory("Main UI")] 
    [Export] private CanvasLayer mainUIGroup;
    [Export] private Label mainCreditsLabel;
    [Export] private Label subTitleLabel;
    
    [ExportCategory("Shop UI")] 
    [Export] private CanvasLayer shopUIGroup;
    [Export] private Label shopCreditsLabel;
    [Export] private Label shopHealthLevelLabel;
    [Export] private UpgradePanel healthUpgradePanel;

    [ExportCategory("Options UI")] [Export]
    private CanvasLayer optionsUIGroup;
    
    [ExportCategory("Game UI")]
    [Export] private CanvasLayer gameUIGroup;
    [Export] private Label gameScoreValueLabel;
    [Export] private ProgressBar healthBar;
    [Export] private Label countdownLabel;
    [Export] private Label roundLabel;
    
    [ExportCategory("Pause UI")]
    [Export] private CanvasLayer pauseUIGroup;
    
    [ExportCategory("Result UI")] 
    [Export] private CanvasLayer resultUIGroup;
    [Export] private Label resultScoreLabel;
    [Export] private Label resultCreditsEarnedLabel;
    [Export] private Label resultTotalCreditsLabel;

    
    
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
    
    
    #region Getter

    #endregion

    #region Setter

    #endregion
}