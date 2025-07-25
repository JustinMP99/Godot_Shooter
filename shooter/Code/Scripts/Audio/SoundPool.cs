using Godot;
using System;
using System.Collections.Generic;

public partial class SoundPool : Node
{

    private List<SoundQueue> sounds = new List<SoundQueue>();
    private RandomNumberGenerator randNumber = new RandomNumberGenerator();
    private int lastIndex = -1;
    
    public override void _Ready()
    {
        foreach (var child in GetChildren())
        {
            if (child is SoundQueue soundQueue)
            {
                sounds.Add(soundQueue);
            }
        }
    }

    public void PlayRandomSound()
    {
        int index;

        do
        {
            index = randNumber.RandiRange(0, sounds.Count - 1);
        } while (index == lastIndex);

        lastIndex = index;
        sounds[index].PlaySound();
    }
    
    
}
