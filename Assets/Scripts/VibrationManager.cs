using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VibrationManager : MonoBehaviour
{
    // Assign this in the Inspector using the UI Slider (Min=0, Max=1, Whole Numbers=True)
    public Slider vibrationSlider; 

    private bool _isVibrationEnabled = true;

    void Start()
    {
        // Load saved state (1 for On, 0 for Off)
        int savedState = PlayerPrefs.GetInt("VibrationEnabled", 1);
        _isVibrationEnabled = (savedState == 1);
        vibrationSlider.value = savedState;

        // Link the slider event to the script method
        vibrationSlider.onValueChanged.AddListener(SetVibrationToggle);
    }

    public void SetVibrationToggle(float value)
    {
        // Value will be 0 (Off) or 1 (On) because Whole Numbers is checked
        _isVibrationEnabled = (value == 1);
        
        PlayerPrefs.SetInt("VibrationEnabled", _isVibrationEnabled ? 1 : 0);
        PlayerPrefs.Save();

        Debug.Log("Vibration Toggled: " + _isVibrationEnabled);
        
        // Provide haptic feedback instantly when the user enables it
        if (_isVibrationEnabled)
        {
            DoVibrate();
        }
    }

    // Call this method whenever you want the phone to vibrate in your game
    public void DoVibrate()
    {
        if (_isVibrationEnabled)
        {
            // The Handheld.Vibrate() function is the cross-platform Unity call.
            Handheld.Vibrate(); 
        }
    }
    
    // Public getter to check the state from other scripts
    public bool IsVibrationEnabled()
    {
        return _isVibrationEnabled;
    }
}
