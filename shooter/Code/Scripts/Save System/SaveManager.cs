using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Godot;
using Godot.Collections;

public partial class SaveManager : Node
{
    
    
    private string savePath => "user://GodotShooter/savegame.json";

    public void SaveGame(SaveData data)
    {
        string json = JsonSerializer.Serialize(data);
        using var file = FileAccess.Open(savePath, FileAccess.ModeFlags.Write);
        file.StoreString(json);
        GD.Print("Game saved to ", savePath);
    }
    
    public SaveData LoadGame()
    {
        if (!FileAccess.FileExists(savePath))
        {
            GD.PrintErr("Save file not found.");
            return null;
        }

        using var file = FileAccess.Open(savePath, FileAccess.ModeFlags.Read);
        string json = file.GetAsText();
        SaveData data = JsonSerializer.Deserialize<SaveData>(json);
        GD.Print("Game loaded from ", savePath);
        return data;
    }
    
    
}
