using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI highScore;

    private void Start()
    {
        LoadPlayerName();
        LeadHighScore();
    }

    private void LoadPlayerName()
    {
        playerName.text = PlayerPrefs.GetString(PlayerPrefsDictionary.PlayerName, "Sara");
    }

    private void LeadHighScore()
    {
        highScore.text = "High Score: " +  PlayerPrefs.GetInt(PlayerPrefsDictionary.PlayerHighScore, 0).ToString();
    }
    
    public void OnStartClicked()
    {
        SceneManager.LoadScene("Game");
    }
}
