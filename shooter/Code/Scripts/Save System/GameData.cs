using Godot;
using System;
using Godot.Collections;

public partial class GameData : Node
{
    public static GameData Instance { get; private set; }
    public int Credits { get; set; }
    public int HealthLevel { get; set; }
    public bool Fullscreen { get; set; }
    public int ResolutionValue { get; set; }

    public override void _Ready()
    {
        Instance = this;
    }
}