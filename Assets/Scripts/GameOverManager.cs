using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager instance;
    public GameObject gameOverPanel;
    public GameObject gamePanel;
    
    void Start()
    {
        instance = this;
    }

    public void GameOver()
    {
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(true);
        
        ScoreManager.instance.UpdateHighScore();
    }
    
}
