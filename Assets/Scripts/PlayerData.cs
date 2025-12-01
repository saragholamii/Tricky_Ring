using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct PlayerData
{
    public string Name;
    public int LastScore;
    public int HighestScore;
    public int CurrentRank;
    public float MusicVolume;
    public float SFXVolume;
    public bool IsVibrationEnabled;
}
