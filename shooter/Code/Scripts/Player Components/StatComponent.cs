using Godot;
using System;

public partial class StatComponent : Node
{

    [ExportCategory("Debug")]
    [Export] private bool debug = false;

    [ExportCategory("Stats")]
    [Export] private PlayerStats Stats;

}
