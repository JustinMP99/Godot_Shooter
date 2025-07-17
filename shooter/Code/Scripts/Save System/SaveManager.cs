using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Godot;
using Godot.Collections;

public partial class SaveManager : Node
{
    
    
    private string savePath => "user://savegame.tres";

    public void SaveGame(SaveData data)
    {
        

    }
    
    public SaveData LoadGame()
    {

        SaveData data = new SaveData();

        if (data == null)
        {
            GD.Print("Loaded Data is Null!!");
        }
        return data;
        
    }
    
    
}
