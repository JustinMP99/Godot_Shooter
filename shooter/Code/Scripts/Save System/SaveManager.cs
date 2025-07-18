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

    public void Save()
    {

        //Convert data dictionary to string for json file
        string json = Json.Stringify(GameData.Instance.data);

        GD.Print(json);
        
        //Get the global path for the User directory
        string path = ProjectSettings.GlobalizePath("user://");

        //Check if Directory exits
        if (!Directory.Exists(path + "/Saves"))
        {
            //Create Directory if it doesn't exist
            Directory.CreateDirectory(path+ "/Saves");
        }
        
        //Join the globalized path with the save folder + file
        path = Path.Join(path, "/Saves/savegame.json");
        File.WriteAllText(path, json);
        
    }

    public void load()
    {
        string data = string.Empty;

        string path = ProjectSettings.GlobalizePath("user://");
        
        if (!File.Exists(path + "/Saves/savegame.json"))
        {

            return;

        }

        data = File.ReadAllText(path + "/Saves/savegame.json");

        GD.Print(data);
        
        Json jsonLoader = new Json();

        Error error = jsonLoader.Parse(data);

        if (error != Error.Ok)
        {
            GD.Print(error);
            return;
        }

        Godot.Collections.Dictionary tempData = (Godot.Collections.Dictionary)jsonLoader.Data;

        //GameData.Instance.data = tempData;
        
        GameData.Instance.data["Credits"] = tempData["Credits"];
        GameData.Instance.data["WeaponLevel"] = tempData["WeaponLevel"];
        GameData.Instance.data["CurrentHealthLevel"] = tempData["CurrentHealthLevel"];
        GameData.Instance.data["MaxHealthLevel"] = tempData["MaxHealthLevel"];

    }
    
    #region Save Resource

    public void Save_Resource()
    {
        
        

    }
    
    #endregion
    
}
