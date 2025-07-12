using Godot;
using System;

public partial class SceneManager
{

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


}
