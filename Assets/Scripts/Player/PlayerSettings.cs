using System;
using UnityEngine;

public class PlayerSettings
{

    public const string Path = "/player_settings.json";
    public int VSyncCount { get; set; } = 1;
    public int TargetFrameRate { get; set; } = 144;
    public bool ShowFPS { get; set; } = false;

    private int currentVSyncCount;
    private int currentTargetFramerate;

    public PlayerSettings()
    {
        InputManager.Instance.OnVsyncPressed += UpdateVSyncCount;
    }

    private void UpdateVSyncCount(bool increase)
    {
        UpdateVSyncCount(increase ? VSyncCount + 1 : VSyncCount - 1);
    }
    public void UpdateVSyncCount(int vSyncCount)
    {
        VSyncCount = Math.Clamp(vSyncCount, 0, 4);
        QualitySettings.vSyncCount = VSyncCount;
        Save();
    }

    public void UpdateTargetFrameRate(int targetFrameRate)
    {
        TargetFrameRate = targetFrameRate;
        Application.targetFrameRate = TargetFrameRate;
        Save();
    }

    public void ToggleShowFPS()
    {
        ShowFPS = !ShowFPS;
        Save();
    }

    public void Save()
    {
        JsonDataService dataService = new JsonDataService();
        dataService.SaveData(Path, this, false);
    }

    public static PlayerSettings Load()
    {
        var dataService = new JsonDataService();        
        try
        {
            var playerSettings = dataService.LoadData<PlayerSettings>(Path, false);
            return playerSettings;
        }
        catch
        {
            var playerSettings = new PlayerSettings();
            playerSettings.Save();
            return playerSettings;
        }
    }
}