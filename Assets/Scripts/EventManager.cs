using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action<Vector3> OnLeaderboardButtonPressed;
    public static void TriggerLeaderboardButtonPressed(Vector3 position)
    {
        OnLeaderboardButtonPressed?.Invoke(position);
    }
}
