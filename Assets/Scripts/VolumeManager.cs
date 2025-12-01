using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    // Assign these in the Inspector using the Audio Mixer window
    public AudioMixer mainMixer;
    
    // Assign these in the Inspector using the UI Sliders
    public Slider musicSlider;
    public Slider sfxSlider;

    // Use a constant for the minimum volume in dB (near silence)
    private const float MinDecibels = -80f; 

    void Start()
    {
        // Load saved values or set default, then update the UI
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
        
        // Apply the loaded value immediately
        SetMusicVolume(musicSlider.value);
        SetSFXVolume(sfxSlider.value);

        // Link the slider events to the script methods
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    // Converts linear slider value (0 to 1) to logarithmic decibels (dB)
    private float LinearToDecibels(float linear)
    {
        // If the slider is at 0, return the minimum decibel value
        if (linear == 0) return MinDecibels; 
        
        // Standard formula to convert linear volume to dB
        return 20f * Mathf.Log10(linear); 
    }

    public void SetMusicVolume(float volume)
    {
        float db = LinearToDecibels(volume);
        mainMixer.SetFloat("MusicVolume", db);
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float volume)
    {
        float db = LinearToDecibels(volume);
        mainMixer.SetFloat("SFXVolume", db);
        PlayerPrefs.SetFloat("SFXVolume", volume);
        PlayerPrefs.Save();
    }
}
