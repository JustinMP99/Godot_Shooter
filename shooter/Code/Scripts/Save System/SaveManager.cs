using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Godot;
using Godot.Collections;
using FileAccess = System.IO.FileAccess;

public partial class SaveManager : Node
{
    private string savePath => "user://savegame.json";
    private string configPath = "user://settings.cfg";
    
    public void Save()
    {
        PlayerController tempPlayer = PlayerController.Instance;
        Godot.Collections.Dictionary tempData = new Dictionary();
        tempData = new Dictionary()
        {
            { "Credits", tempPlayer.Credits },
            { "HealthLevel", tempPlayer.Stats.HealthLevel },
            { "CurrentHealth", tempPlayer.Stats.CurrentHealth },
            { "MaxHealth", tempPlayer.Stats.MaxHealth },
        };

        //Convert data dictionary to string for json file
        string json = Json.Stringify(tempData);

        //Get the global path for the User directory
        string path = ProjectSettings.GlobalizePath("user://");

        //Check if Directory exits
        if (!Directory.Exists(path + "/Saves"))
        {
            //Create Directory if it doesn't exist
            Directory.CreateDirectory(path + "/Saves");
        }

        //Join the globalized path with the save folder + file
        path = Path.Join(path, "/Saves/savegame.json");
        File.WriteAllText(path, json);

        tempData = null;
    }

    public bool load()
    {
        PlayerController tempPlayer = PlayerController.Instance;
        string data = string.Empty;

        string path = ProjectSettings.GlobalizePath("user://");

        if (!File.Exists(path + "/Saves/savegame.json"))
        {
            //If the file doesn't exist, consider this the first time playing
            return false;
        }

        data = File.ReadAllText(path + "/Saves/savegame.json");

        Json jsonLoader = new Json();

        Error error = jsonLoader.Parse(data);

        if (error != Error.Ok)
        {
            GD.Print(error);
            return false;
        }

        Godot.Collections.Dictionary tempData = (Godot.Collections.Dictionary)jsonLoader.Data;

        //GameData.Instance.data = tempData;
        tempPlayer.Credits = tempData["Credits"].AsInt32();
        tempPlayer.Stats.HealthLevel = tempData["HealthLevel"].AsInt32();
        tempPlayer.Stats.MaxHealth = tempData["MaxHealth"].AsInt32();
        tempPlayer.Stats.CurrentHealth = tempPlayer.Stats.MaxHealth;

        return true;
    }

    public bool ResetSave()
    {
        PlayerController tempPlayer = PlayerController.Instance;
        Godot.Collections.Dictionary tempData = new Dictionary();
        tempData = new Dictionary()
        {
            { "Credits", 0 },
            { "HealthLevel", 1 },
            { "CurrentHealth", 50 },
            { "MaxHealth", 50 },
        };

        //Convert data dictionary to string for json file
        string json = Json.Stringify(tempData);
        
        //Get the global path for the User directory
        string path = ProjectSettings.GlobalizePath("user://");

        //Check if Directory exits
        if (!Directory.Exists(path + "/Saves"))
        {
            //Create Directory if it doesn't exist
            Directory.CreateDirectory(path + "/Saves");
        }

        //Join the globalized path with the save folder + file
        path = Path.Join(path, "/Saves/savegame.json");
        File.WriteAllText(path, json);

        return true;
    }
    
    #region Config Save 

    

    #endregion
    
    
    #region Save Resource

    public void Save_Resource()
    {
    }

    #endregion
}