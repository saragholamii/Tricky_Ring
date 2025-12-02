using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardRow : MonoBehaviour
{
    public TextMeshProUGUI rankText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI scoreText;

    public Image leaderboardCard;
    public GameObject localPlayerCard;
    public GameObject otherPlayerCard;

    public void SetData(int rank, string name, int score)
    {
        rankText.text = rank.ToString();
        nameText.text = name;
        scoreText.text = score.ToString();
    }

    public void ChangeColorForLocalPlayer()
    {
        Debug.Log("find sara");
        localPlayerCard.SetActive(true);
        otherPlayerCard.SetActive(false);
    }

    public void ChangeColorForOtherPlayer()
    {
        localPlayerCard.SetActive(false);
        otherPlayerCard.SetActive(true);
    }
}
