using System;
using System.Collections;
using System.Collections.Generic;
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
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "high score : " + highScore;
    }

    public void UpdateScore(int point = 1)
    {
        score += point;
        scoreText.text = score.ToString();
    }

    private bool CheckHighScore()
    {
        return score > highScore;
    }

    private void UpdateHighScore(int newHighScore)
    {
        PlayerPrefs.SetInt("HighScore", newHighScore);
    }
    
    
}
