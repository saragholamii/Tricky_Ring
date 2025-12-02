using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    
    [Header("Scores")]
    public TextMeshProUGUI  scoreText;
    public TextMeshProUGUI  highScoreText;
    
    private int score;
    private int highScore;

    private void Start()
    {
        instance = this;
        highScore = PlayerPrefs.GetInt(PlayerPrefsDictionary.PlayerHighScore, 0);
        highScoreText.text = "high score : " + highScore;
    }

    public void UpdateScore(int point = 1)
    {
        score += point;
        scoreText.text = score.ToString();
    }

    public void UpdateHighScore()
    {
        if(CheckHighScore())
            PlayerPrefs.SetInt(PlayerPrefsDictionary.PlayerHighScore, score);
    }

    public void UpdateLastScore()
    {
        PlayerPrefs.SetInt(PlayerPrefsDictionary.PlayerHighScore, score);
    }
    
    private bool CheckHighScore()
    {
        return score > highScore;
    }
    
}
