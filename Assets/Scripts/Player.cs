using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Square"))
        {
            Destroy(collision.gameObject);
            ObstacleManager.instance.OnSquareCollected();
            ScoreManager.instance.UpdateScore();
            SquareManager.instance.AddSquare();
        }
        else if (collision.CompareTag("Obstacle"))
        {
            Debug.Log("Obstacle Detected");
        }
    }
}
