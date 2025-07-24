using Godot;
using System;

public partial class AudioManager : Node
{
    
    public static AudioManager Instance { get; private set; }

    [Export] private AudioStreamPlayer shootSound;
    
    public override void _Ready()
    {
        Instance = this;
    }

    public void PlayShootSound()
    {
        shootSound.Play();
    }
    
}
