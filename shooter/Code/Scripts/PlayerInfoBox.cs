using Godot;
using System;

public partial class PlayerInfoBox : Panel
{
    [Export] private ProgressBar healthBar;
    [Export] private ProgressBar powerUpBar;


    public void SetHealthBarMax(int newMax)
    {
        healthBar.MaxValue = newMax;
    }

    public void SetHealthBarCurrent(int newCurrent)
    {
        healthBar.Value = newCurrent;
    }

    public void SetPowerUpBarCurrent(float newCurrent)
    {
        powerUpBar.Value = newCurrent;
    }

    public void SetPowerUpBarMax(int newMax)
    {
        powerUpBar.MaxValue = newMax;
    }
}