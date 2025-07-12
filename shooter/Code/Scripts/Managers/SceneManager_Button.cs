using Godot;
using System;

public partial class SceneManager
{

    #region Main UI Button Functions

    public void StartGameFunction()
    {
        
        //Disable Main UI
        UIManager.SetMainUIState(false);
        
        //Enable Game UI
        UIManager.SetGameUIState(true);

        score = 0;
        
        UIManager.SetScoreText(score);
        
        //Create Player
        player = playerScene.Instantiate() as PlayerController;
        levelNode.AddChild(player);
        player.Position = new Vector3(0.0f, 0.0f, 0.0f);

        if (player == null)
        {
            GD.Print("Player is null");
        }
        
        //Assign Signal Functions
        player.PauseSignal += ActivatePause;


        //Start Timer

    }

    public void QuitGameFunction()
    {
        GetTree().Quit();
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
        gamePaused = false;

    }

    private void Pause_QuitButtonFunction()
    {
        
        //Destroy Player Object
        player.QueueFree();
        
        //Destroy all enemies
        
        //Set UI States
        UIManager.SetPauseUIState(false);
        UIManager.SetMainUIState(true);
        
        //Reset game data
        gamePaused = false;
        score = 0;

    }
    
    
    #endregion
    
    



}
