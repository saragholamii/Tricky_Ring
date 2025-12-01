using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardButtonController : MonoBehaviour
{
    [SerializeField] private Transform leaderboardDailyButton;
    [SerializeField] private Transform leaderboardWeeklyButton;
    [SerializeField] private Transform leaderboardAllTimeButton;
    
    
    public void LeaderboardDailyButtonPressed()
    {
        EventManager.TriggerLeaderboardButtonPressed(leaderboardDailyButton.position);
    }

    public void LeaderboardWeeklyButtonPressed()
    {
        EventManager.TriggerLeaderboardButtonPressed(leaderboardWeeklyButton.position);
    }

    public void LeaderboardAllTimeButtonPressed()
    {
        EventManager.TriggerLeaderboardButtonPressed(leaderboardAllTimeButton.position);
    }


}
