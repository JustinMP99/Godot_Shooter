using Godot;
using System;


public enum EnemyClass
{
    Base_Unit,
    Tank_Unit,
    Speedstr_Unit
}

[GlobalClass]
public partial class EnemyStats : BaseStats
{
    [Export] public EnemyClass Class { get; set; }
}