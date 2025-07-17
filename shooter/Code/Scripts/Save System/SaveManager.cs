using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Godot;
using Godot.Collections;

public partial class SaveManager : Node
{
    
    
    private string savePath => "user://savegame.json";

    public void SaveGame(SaveData data)
    {
        
        var file = FileAccess.Open(savePath, FileAccess.ModeFlags.Write);
        if (file != null)
        {
            //
            // string jsonData = Json.Stringify(data.PlayerData);
        
            file.StoreVar(data.PlayerData);
            file.Close();   
        }
        else
        {
            GD.Print("Save Filepath is NULL");
        }

    }
    
    public SaveData LoadGame()
    {

        SaveData data = new SaveData();

        if (FileAccess.FileExists(savePath))
        {
            var file = FileAccess.Open(savePath, FileAccess.ModeFlags.Read);

            var saveData = file.GetVar();

            // int count = saveData["Credits"];
            //
            // var jsonData = file.GetAsText();
            // var parsedData = Json.ParseString(jsonData);
            //
            // int count = ;
            
            // data.PlayerData["Credits"] = parsedData.;
            // GD.Print(jsonData);
            file.Close();
        }
        

        return data;
        
    }
    
    
}
