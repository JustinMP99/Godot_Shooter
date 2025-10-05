using Godot;
using System;

[GlobalClass]
public partial class PowerUpStats_ShootType : PowerUpStats
{
    [Export] public ShootType ShootType;
    [Export] public float FireRate;
}