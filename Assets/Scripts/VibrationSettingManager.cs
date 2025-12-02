using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class VibrationSettingManager : MonoBehaviour
{
    public Slider vibrationSlider; 
    
    private bool isVibrationEnabled = true;
    
    void Start()
    {
        int savedState = PlayerPrefs.GetInt(PlayerPrefsDictionary.PlayerVibrationState, 1);
        isVibrationEnabled = (savedState == 1);
        vibrationSlider.value = savedState;

        vibrationSlider.onValueChanged.AddListener(SetVibrationToggle);
    }
    
    public void SetVibrationToggle(float value)
    {
        isVibrationEnabled = (value == 1);
        
        PlayerPrefs.SetInt(PlayerPrefsDictionary.PlayerVibrationState, isVibrationEnabled ? 1 : 0);
        PlayerPrefs.Save();

        
        if (isVibrationEnabled)
        {
            VibrationManager.instance.SetVibrationEnabled(true);
            VibrationManager.instance.Vibrate();
        }
        else
        {
            VibrationManager.instance.SetVibrationEnabled(false);
        }
    }
    
}
