using Godot;
using System;

public partial class UIManager
{
   
    public void SetPauseUIState(bool state)
    {
        pauseUIGroup.Visible = state;
    }
}