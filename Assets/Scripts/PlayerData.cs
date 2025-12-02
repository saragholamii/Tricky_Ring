using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct PlayerData
{
    public string name;
    public int dailyScore;
    public int weeklyScore;
    public int highestScore;
    public int dailyRank;
    public int weeklyRank;
    public int allTimeRank;
    public float musicVolume;
    public float sFXVolume;
    public bool isVibrationEnabled;
}
