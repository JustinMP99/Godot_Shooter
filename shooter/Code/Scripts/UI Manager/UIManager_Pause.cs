using Godot;
using System;

public partial class UIManager
{
    [ExportCategory("Pause UI")]
    [Export] private CanvasLayer pauseUIGroup;

    public void SetPauseUIState(bool state)
    {
        pauseUIGroup.Visible = state;
    }
}