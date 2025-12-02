using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VibrationManager : MonoBehaviour
{
    public static VibrationManager instance;

    private bool isVibrationEnabled;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        int savedState = PlayerPrefs.GetInt("VibrationEnabled", 1);
        SetVibrationEnabled(savedState == 1);
    }

    public void SetVibrationEnabled(bool isEnabled)
    {
        isVibrationEnabled = isEnabled;
    }
    
    public void Vibrate(long milliseconds = 250)
    {
        if (isVibrationEnabled)
        {
#if UNITY_ANDROID || UNITY_IOS
            Handheld.Vibrate();
#elif UNITY_EDITOR
            Debug.Log("Vibrate: Testing on Editor (Duration: " + milliseconds + "ms)");
#endif
        }
    }
    
    
}
