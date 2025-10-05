using Godot;
using System;

public partial class VolumeSlider : HSlider
{
    [Export] private string audioBus;
    [Export] private int busIndex;

    public override void _Ready()
    {
        busIndex = AudioServer.GetBusIndex(audioBus);
        this.ValueChanged += OnValueChanged;
    }

    private void OnValueChanged(double value)
    {
        AudioServer.SetBusVolumeDb(busIndex, Mathf.LinearToDb((float)value));
    }
}