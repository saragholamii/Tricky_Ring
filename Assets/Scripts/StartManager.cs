using System;
using System.Collections;
using System.Collections.Generic;
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
        playerName.text = PlayerPrefs.GetString("PlayerName", "Sara");
    }

    private void LeadHighScore()
    {
        highScore.text = "High Score: " +  PlayerPrefs.GetInt("HighScore", 0).ToString();
    }
    
    public void OnStartClicked()
    {
        SceneManager.LoadScene("Game");
    }
}
