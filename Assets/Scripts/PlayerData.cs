using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct PlayerData
{
    public string Name;
    public int dailyScore;
    public int WeeklyScore;
    public int HighestScore;
    public int DailyRank;
    public int WeeklyRank;
    public int AllTimeRank;
    public float MusicVolume;
    public float SFXVolume;
    public bool IsVibrationEnabled;
}
