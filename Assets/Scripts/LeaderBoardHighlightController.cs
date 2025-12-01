using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoardHighlightController : MonoBehaviour
{
   
    private void OnEnable()
    {
        EventManager.OnLeaderboardButtonPressed += ChangeHighlightPosition;
    }

    private void OnDisable()
    {
        EventManager.OnLeaderboardButtonPressed -= ChangeHighlightPosition;
    }

    private void ChangeHighlightPosition(Vector3 position)
    {
        transform.position = position;
    }
}
