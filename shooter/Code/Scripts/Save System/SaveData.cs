using Godot;
using System;
using Godot.Collections;

public class SaveData
{
    public Vector3 PlayerPosition { get; set; }
    public int PlayerHealth { get; set; }
    public string[] Inventory { get; set; }
}
