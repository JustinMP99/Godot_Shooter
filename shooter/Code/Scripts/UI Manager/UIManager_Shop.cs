using Godot;
using System;

public partial class UIManager
{

    [ExportCategory("Shop UI")] 
    [Export] private CanvasLayer shopUIGroup;


    public void SetShopUIState(bool state)
    {
        shopUIGroup.Visible = state;
    }
    
}
