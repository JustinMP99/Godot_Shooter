using Godot;
using System;

public partial class PlayerInfoBox : Panel
{
    
    [Export] private ProgressBar healthBar;
    [Export] private ProgressBar shieldBar;

    
    public void SetHealthBarMax(int newMax)
    {
        healthBar.MaxValue = newMax;
    }
    
    public void SetHealthBarCurrent(int newCurrent)
    {
        healthBar.Value = newCurrent;
    }

}
