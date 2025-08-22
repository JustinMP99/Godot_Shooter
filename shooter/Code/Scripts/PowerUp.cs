using Godot;
using System;


public enum PowerUpType
{
    Health, //Restores Health
    Shoot_Type, //Changes Shoot type for a brief period
    
}

[GlobalClass]
public partial class PowerUp : Resource
{

    [Export] private float speed;
    [Export] private PowerUpType shootType;

}
