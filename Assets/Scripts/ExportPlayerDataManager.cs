using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using DefaultNamespace;
using UnityEngine;

public class ExportPlayerDataManager : MonoBehaviour
{
    private PlayerData playerData;

    public void ExportPlayerData()
    {
        playerData = new PlayerData
        {
            name = PlayerPrefs.GetString(PlayerPrefsDictionary.PlayerName),
            dailyScore = PlayerPrefs.GetInt(PlayerPrefsDictionary.PlayerDailyScore),
            highestScore = PlayerPrefs.GetInt(PlayerPrefsDictionary.PlayerHighScore),
            dailyRank = PlayerPrefs.GetInt(PlayerPrefsDictionary.PlayerDailyRank),
            weeklyRank = PlayerPrefs.GetInt(PlayerPrefsDictionary.PlayerWeeklyRank),
            allTimeRank = PlayerPrefs.GetInt(PlayerPrefsDictionary.PlayerAllTimeRank),
            musicVolume =  PlayerPrefs.GetFloat(PlayerPrefsDictionary.PlayerMusicVolume, 0.5f),
            sFXVolume = PlayerPrefs.GetFloat(PlayerPrefsDictionary.PlayerSFXVolume, 0.75f),
            isVibrationEnabled = PlayerPrefs.GetInt(PlayerPrefsDictionary.PlayerVibrationState, 1) == 1
        };
        
        // 2. Convert the PlayerData object to a JSON formatted string
        // The 'true' argument pretty prints the JSON, making it readable.
        string json = JsonUtility.ToJson(playerData, true);

        // 3. Define the file path
        // Application.persistentDataPath gives you a reliable, OS-specific path.
        // On Windows: C:\Users\Username\AppData\LocalLow\CompanyName\ProductName
        // On Mac: ~/Library/Application Support/CompanyName/ProductName
        string path = Path.Combine(Application.persistentDataPath, "player_data_export.json");

        try
        {
            // 4. Write the JSON string to the file
            File.WriteAllText(path, json);
            Debug.Log($"Data successfully exported to: {path}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to export data: {e.Message}");
        }
        
    }
}
