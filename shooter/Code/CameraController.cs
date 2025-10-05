using Godot;
using System;

public partial class CameraController : Camera3D
{
    [ExportCategory("Camera Settings")]
    public bool rotateCamera { get; set; }

    public override void _Ready()
    {
        base._Ready();
    }

    public override void _Process(double delta)
    {
        if (rotateCamera)
        {
            this.Rotate(Vector3.Up, 0.1f);
        }
    }
}