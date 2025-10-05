using Godot;
using System;
using System.Collections.Generic;

[Tool]
public partial class SoundQueue : Node
{
    [Export] public int instanceCount { get; set; } = 1;

    private int next = 0;
    private List<AudioStreamPlayer> audioStreamPlayers = new List<AudioStreamPlayer>();


    public override void _Ready()
    {
        if (GetChildCount() == 0)
        {
            GD.Print("No AudioStreamPlayer child found!");
            return;
        }

        var child = GetChild(0);
        if (child is AudioStreamPlayer audioStreamPlayer)
        {
            audioStreamPlayers.Add(audioStreamPlayer);
            for (int i = 0; i < instanceCount; i++)
            {
                AudioStreamPlayer duplicate = audioStreamPlayer.Duplicate() as AudioStreamPlayer;
                AddChild(duplicate);
                audioStreamPlayers.Add(duplicate);
            }
        }
    }

    public override string[] _GetConfigurationWarnings()
    {
        if (GetChildCount() == 0)
        {
            return new string[] { "No children found! One AudioStreamPlayer is expected" };
        }

        if (GetChild(0) is not AudioStreamPlayer)
        {
            return new string[] { "First child is expected to be an AudioStreamPlayer!" };
        }

        return base._GetConfigurationWarnings();
    }

    public void PlaySound()
    {
        if (!audioStreamPlayers[next].Playing)
        {
            audioStreamPlayers[next].Play();
            next %= audioStreamPlayers.Count;
        }
    }
}