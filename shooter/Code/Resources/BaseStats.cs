using Godot;
using System;

public abstract partial class BaseStats : Resource
{
    
    [Export] public float Speed { get; set; }
    [Export] public int CurrentHealth { get; set; }
    [Export] public int MaxHealth { get; set; }
    
}
