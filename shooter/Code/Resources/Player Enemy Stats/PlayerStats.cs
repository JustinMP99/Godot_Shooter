using Godot;
using System;


[GlobalClass]
public partial class PlayerStats : BaseStats
{
    [Export] public int HealthLevel { get; set; }
    [Export] public int FireRateLevel { get; set; }
    [Export] public double FireRate { get; set; }
    [Export] public int SpeedLevel { get; set; }
}