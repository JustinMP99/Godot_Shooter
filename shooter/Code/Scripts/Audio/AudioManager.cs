using Godot;
using System;
using Godot.Collections;

public partial class AudioManager : Node
{
    public static AudioManager Instance { get; private set; }

    private Dictionary<string, SoundQueue> soundQueueByName = new Dictionary<string, SoundQueue>();
    private Dictionary<string, SoundPool> soundPoolByName = new Dictionary<string, SoundPool>();

    [ExportCategory("Shoot Sounds")] 
    [Export] private AudioStreamPlayer shootSound;
    
    public override void _Ready()
    {
        Instance = this;
        
        soundQueueByName.Add("ShootSoundQueue", GetNode<SoundQueue>("ShootSoundQueue"));
        
    }

    public void PlayShootSound()
    {
        shootSound.Play();
    }
    
}
