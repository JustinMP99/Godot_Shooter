using Godot;
using System;

public partial class PlayerRaycast : RayCast3D
{
    public override void _Process(double delta)
    {
        if (IsColliding())
        {
            var hit = GetCollider();
            GD.Print("Object Hit: " + hit);
        }
    }
}
