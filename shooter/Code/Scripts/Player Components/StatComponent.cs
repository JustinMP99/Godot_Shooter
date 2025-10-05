using Godot;
using System;

public partial class StatComponent : Node
{
    [ExportCategory("Debug")]
    [Export] private bool debug = false;

    [ExportCategory("Stats")]
    [Export] private int Credits = 0;

    [Export] private PlayerStats stats;

    public void DecreaseHealth(int damage)
    {
        stats.CurrentHealth -= damage;
    }
    
    public void IncreaseCredits(int increaseAmount)
    {
        Credits += increaseAmount;
    }

    public void DecreaseCredits(int decreaseAmount)
    {
        Credits -= decreaseAmount;
    }

    public void IncrementHealthLevel()
    {
        stats.HealthLevel++;
    }

    public void IncrementFireRateLevel()
    {
        stats.FireRateLevel++;
    }

    public void IncrementSpeedLevel()
    {
        stats.SpeedLevel++;
    }

    #region Getter

    public int GetCredits()
    {
        return Credits;
    }

    public int GetHealthLevel()
    {
        return stats.HealthLevel;
    }

    public int GetCurrentHealth()
    {
        return stats.CurrentHealth;
    }

    public int GetMaxHealth()
    {
        return stats.MaxHealth;
    }

    public int GetFireRateLevel()
    {
        return stats.FireRateLevel;
    }

    /// <summary>
    /// Returns fire rate stored in attached PlayerStats
    /// </summary>
    public double GetFireRate()
    {
        return stats.FireRate;
    }

    public int GetSpeedLevel()
    {
        return stats.SpeedLevel;
    }

    public float GetSpeed()
    {
        return stats.Speed;
    }

    #endregion

    #region Setter

    public void SetCredits(int newValue)
    {
        Credits = newValue;
    }

    public void SetHealthLevel(int newLevel)
    {
        stats.HealthLevel = newLevel;
    }

    public void SetCurrentHealth(int newCurrent)
    {
        stats.CurrentHealth = newCurrent;
    }

    public void SetMaxHealth(int newMax)
    {
        stats.MaxHealth = newMax;
    }

    public void SetFireRateLevel(int newLevel)
    {
        stats.FireRateLevel = newLevel;
    }

    public void SetFireRate(double newRate)
    {
        stats.FireRate = newRate;
    }

    public void SetSpeedLevel(int newLevel)
    {
        stats.SpeedLevel = newLevel;
    }

    public void SetSpeed(float newSpeed)
    {
        stats.Speed = newSpeed;
    }

    #endregion
}