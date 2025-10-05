using Godot;
using System;


public enum PowerUpType
{
    Health, //Restores Health
    Shoot_Type, //Changes Shoot type for a brief period
}

/// <summary>
/// The base class for all power ups
/// </summary>
[GlobalClass]
public partial class PowerUpStats : Resource
{
    [Export] public float Speed;
    [Export] public PowerUpType Type;
}