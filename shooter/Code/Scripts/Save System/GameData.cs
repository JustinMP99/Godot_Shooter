using Godot;
using System;
using Godot.Collections;

public partial class GameData : Node
{
    public static GameData Instance { get; private set; }
    public int _credits { get; set; }
    public int _healthLevel { get; set; }
    public int _currentHealth { get; set; }
    public int _maxHealth { get; set; }

    public Godot.Collections.Dictionary data = new Dictionary()
    {
        { "Credits", 0 },
        { "WeaponLevel", 1 },
        { "CurrentHealthLevel", 1 },
        { "MaxHeathLevel", 5 }
    };

    public override void _Ready()
    {
        Instance = this;
        data["Credits"] = 0;
        data["WeaponLevel"] = 1;
        data["CurrentHealthLevel"] = 1;
        data["MaxHealthLevel"] = 5;
    }
}