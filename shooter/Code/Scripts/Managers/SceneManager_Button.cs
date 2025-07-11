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
        
    }

    public void QuitGameFunction()
    {
        GetTree().Quit();
    }


}
