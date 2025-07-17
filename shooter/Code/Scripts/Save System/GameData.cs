using Godot;
using System;
using Godot.Collections;
public partial class GameData : Node
{
    public static GameData Instance { get; private set; }
    public int credits { get; set; }

    public Godot.Collections.Dictionary data = new Dictionary()
    {
        {"Credits", 0},
        {"WeaponLevel", 1},
        {"HealthLevel", 1}
    };
    
    public override void _Ready()
    {
        Instance = this;
        data["Credits"] = 0;
        data["WeaponLevel"] = 1;
        data["HealthLevel"] = 1;
    }
}
