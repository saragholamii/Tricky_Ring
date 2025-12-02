using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LeaderboardHighlightMovementManager : MonoBehaviour
{
    [Header("Header Buttons")]
    public Transform dailyButton;
    public Transform weeklyButton;
    public Transform allTimeButton;
    public Transform highlightButton;

    public void OnClickDailyButton()
    {
        ChangePosition(dailyButton.position);
    }

    public void OnClickWeeklyButton()
    {
        ChangePosition(weeklyButton.position);
    }

    public void OnClickAllTimeButton()
    {
        ChangePosition(allTimeButton.position);
    }
    
    private void ChangePosition(Vector3 position)
    {
        highlightButton.DOMove(position, 0.75f)
            .SetEase(Ease.OutQuint);
    }
}
