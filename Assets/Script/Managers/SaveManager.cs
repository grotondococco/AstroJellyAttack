using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    //savepath: C:\Users\username\AppData\LocalLow\DirtyBits\AstroJellyAttack

    public static SaveManager istance;
    private static string optionsPath;
    private static string gameDataPath;
    private static string resolutionOptionsPath;
    private static string powerupSettingsPath;
    private static string controlsSettingsPath;
    private static GameData gameData;
    private static AudioOptions audioOptions;
    private static ResolutionOptions resolutionOptions;
    private static PowerupSettings powerupSettings;
    private static ControlsOptions controlsOptions;
    public static SaveManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            Load();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void Load()
    {
        optionsPath = Application.persistentDataPath + "/options.json";
        if (File.Exists(optionsPath))
        {
            string jsonEntry = File.ReadAllText(optionsPath);
            audioOptions = JsonUtility.FromJson<AudioOptions>(jsonEntry);
        }
        else
        {
            audioOptions = new AudioOptions();
            SaveOptions(audioOptions);
        }
        resolutionOptionsPath = Application.persistentDataPath + "/resoptions.json";

        if (File.Exists(resolutionOptionsPath))
        {
            string jsonEntry = File.ReadAllText(optionsPath);
            resolutionOptions = JsonUtility.FromJson<ResolutionOptions>(jsonEntry);
        }
        else
        {
            resolutionOptions = new ResolutionOptions(Screen.currentResolution.width, Screen.currentResolution.height,Screen.currentResolution.refreshRate);
            SaveOptions(resolutionOptions);
        }

        controlsSettingsPath = Application.persistentDataPath + "/controls.json";
        if (File.Exists(controlsSettingsPath))
        {
            string jsonEntry = File.ReadAllText(controlsSettingsPath);
            controlsOptions = JsonUtility.FromJson<ControlsOptions>(jsonEntry);
        }
        else
        {
            controlsOptions = new ControlsOptions();
            SaveControlOptions(controlsOptions);
        }


        gameDataPath = Application.persistentDataPath + "/gamedata.json";
        powerupSettingsPath = Application.persistentDataPath + "/powerups.json";

    }
    
    public static void SaveOptions(AudioOptions audioOptions)
    {
        string audioOptionsString = JsonUtility.ToJson(audioOptions);
        File.WriteAllText(optionsPath, audioOptionsString);
    }

    public static void SaveOptions(ResolutionOptions resolutionOptions)
    {
        string resolutionOptionsString = JsonUtility.ToJson(resolutionOptions);
        File.WriteAllText(resolutionOptionsPath, resolutionOptionsString);
    }

    public static void SavePowerups(PowerupSettings powerupSettings) {
        string powerupSettingsString = JsonUtility.ToJson(powerupSettings);
        File.WriteAllText(powerupSettingsPath, powerupSettingsString);
        LoadPowerups();
    }

    public static void SaveControlOptions(ControlsOptions co) {
        string controlOptionsString = JsonUtility.ToJson(co);
        File.WriteAllText(controlsSettingsPath, controlOptionsString);
        LoadControlsOptions();
    }

    public static void SaveGame(GameData gameData) {
        string gameDataOptions = JsonUtility.ToJson(gameData);
        File.WriteAllText(gameDataPath, gameDataOptions);
    }

    public static void LoadData()
    {
        if (File.Exists(gameDataPath))
        {
            string jsonEntry = File.ReadAllText(gameDataPath);
            gameData = JsonUtility.FromJson<GameData>(jsonEntry);
        }
        else Debug.Log("SaveDataCorrupted");
    }

    public static void LoadPowerups() {
        powerupSettingsPath = Application.persistentDataPath + "/powerups.json";
        if (File.Exists(powerupSettingsPath))
        {
            string jsonEntry = File.ReadAllText(powerupSettingsPath);
            powerupSettings = JsonUtility.FromJson<PowerupSettings>(jsonEntry);
        }
        else Debug.Log("PowerUpDataCorrupted");
    }

    public static void LoadControlsOptions() {
        controlsSettingsPath = Application.persistentDataPath + "/controls.json";
        if (File.Exists(controlsSettingsPath))
        {
            string jsonEntry = File.ReadAllText(controlsSettingsPath);
            controlsOptions = JsonUtility.FromJson<ControlsOptions>(jsonEntry);
        }
        else Debug.Log("ControlsOptionsDataCorrupted");
    }

    public static void NewGame()
    {
        gameData = new GameData();
        SaveGame(gameData);
        powerupSettings = new PowerupSettings();
        SavePowerups(powerupSettings);
    }

    public static bool GameDataExist()
    {
        if (File.Exists(gameDataPath)) return true;
        else return false;
    }

    public static GameData getGameData()
    {
        LoadData();
        return gameData;
    }

    public static AudioOptions getAudioOptions()
    {
        return audioOptions;
    }

    public static ResolutionOptions getGameResolution()
    {
        return resolutionOptions;
    }

    public static PowerupSettings GetPowerupSettings() {
        LoadPowerups();
        return powerupSettings;
    }
    public static ControlsOptions GetControlsOptions()
    {
        LoadControlsOptions();
        return controlsOptions;
    }

}
