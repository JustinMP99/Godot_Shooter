using Godot;
using System;
using Godot.Collections;

public partial class AudioManager : Node
{
    public static AudioManager Instance { get; private set; }

    private Dictionary<string, SoundQueue> soundQueueByName = new Dictionary<string, SoundQueue>();
    private Dictionary<string, SoundPool> soundPoolByName = new Dictionary<string, SoundPool>();

    [ExportCategory("SFX")]
    [Export] private AudioStreamPlayer shootSound;

    [Export] private AudioStreamPlayer playerDeath;
    [Export] private AudioStreamPlayer enemyDeath;

    [ExportCategory("Music")]
    [Export] private AudioStreamPlayer mainMusic;

    public override void _Ready()
    {
        Instance = this;

        //soundQueueByName.Add("ShootSoundQueue", GetNode<SoundQueue>("ShootSoundQueue"));
    }

    public void PlayShootSound()
    {
        shootSound.Play();
    }

    public void PlayPlayerDeathSound()
    {
        playerDeath.Play();
    }

    public void PlayEnemyDeathSound()
    {
        enemyDeath.Play();
    }

    public void PlayEnemyDamageSound()
    {
    }
}