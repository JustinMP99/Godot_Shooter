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
            { "Credits", tempPlayer.Stats.GetCredits() },
            { "HealthLevel", tempPlayer.Stats.GetHealthLevel() },
            { "CurrentHealth", tempPlayer.Stats.GetCurrentHealth() },
            { "MaxHealth", tempPlayer.Stats.GetMaxHealth() },
            { "FireRateLevel", tempPlayer.Stats.GetFireRateLevel() },
            { "FireRate", tempPlayer.Stats.GetFireRate() },
            { "SpeedLevel", tempPlayer.Stats.GetSpeedLevel() },
            { "Speed", tempPlayer.Stats.GetSpeed() }
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
        tempPlayer.Stats.SetCredits(tempData["Credits"].AsInt32());

        tempPlayer.Stats.SetHealthLevel(tempData["HealthLevel"].AsInt32());
        tempPlayer.Stats.SetCurrentHealth(tempData["MaxHealth"].AsInt32());
        tempPlayer.Stats.SetMaxHealth(tempData["MaxHealth"].AsInt32());

        tempPlayer.Stats.SetFireRateLevel(tempData["FireRateLevel"].AsInt32());
        tempPlayer.Stats.SetFireRate(tempData["FireRate"].AsDouble());

        tempPlayer.Stats.SetSpeedLevel(tempData["SpeedLevel"].AsInt32());
        tempPlayer.Stats.SetSpeed((float)tempData["Speed"]);

        //tempPlayer.SetShootTimerWait(tempPlayer.Stat.GetFireRate());

        return true;
    }

    /// <summary>
    /// Creates a new save file with reset information
    /// </summary>
    /// <returns></returns>
    public bool NewSave()
    {
        PlayerController tempPlayer = PlayerController.Instance;
        Godot.Collections.Dictionary tempData = new Dictionary();
        tempData = new Dictionary()
        {
            { "Credits", 0 },
            { "HealthLevel", 1 },
            { "CurrentHealth", 50 },
            { "MaxHealth", 50 },
            { "FireRateLevel", 1 },
            { "FireRate", 0.3 },
            { "SpeedLevel", 1 },
            { "Speed", 5.0f }
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

    public void SaveConfig()
    {
        var config = new ConfigFile();

        float masterVolume = AudioServer.GetBusVolumeLinear(0);
        float sfxVolume = AudioServer.GetBusVolumeLinear(1);
        float musicVolume = AudioServer.GetBusVolumeLinear(2);

        config.SetValue("Audio", "MasterLevel", masterVolume);
        config.SetValue("Audio", "SFXLevel", sfxVolume);
        config.SetValue("Audio", "MusicLevel", musicVolume);
        config.SetValue("Screen", "Fullscreen", GameData.Instance.Fullscreen);
        config.SetValue("Screen", "ResolutionValue", GameData.Instance.ResolutionValue);

        config.Save(configPath);
    }

    public bool LoadConfig()
    {
        var config = new ConfigFile();

        Error err = config.Load(configPath);
        if (err != Error.Ok)
        {
            GD.Print(err);
            GD.Print("Error Reading file");
            return false;
        }

        float masterLevel = (float)config.GetValue("Audio", "MasterLevel");
        float sfxLevel = (float)config.GetValue("Audio", "SFXLevel");
        float musicLevel = (float)config.GetValue("Audio", "MusicLevel");
        GameData.Instance.Fullscreen = (bool)config.GetValue("Screen", "Fullscreen");
        GameData.Instance.ResolutionValue = (int)config.GetValue("Screen", "ResolutionValue");

        AudioServer.SetBusVolumeLinear(0, masterLevel);
        AudioServer.SetBusVolumeLinear(1, sfxLevel);
        AudioServer.SetBusVolumeLinear(2, musicLevel);

        return true;
    }

    #endregion
}