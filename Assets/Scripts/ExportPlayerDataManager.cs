using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ExportPlayerDataManager : MonoBehaviour
{
    private PlayerData playerData;

    public void ExportPlayerData()
    {
        playerData = new PlayerData
        {
            Name = "sara",
            LastScore = PlayerPrefs.GetInt("LastScore"),
            HighestScore = PlayerPrefs.GetInt("HighScore"),
            CurrentRank = 0,
            MusicVolume =  PlayerPrefs.GetFloat("MusicVolume", 0.5f),
            SFXVolume = PlayerPrefs.GetFloat("SFXVolume", 0.75f),
            IsVibrationEnabled = PlayerPrefs.GetInt("VibrationEnabled", 1) == 1
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
