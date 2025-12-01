using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXAudioManager : MonoBehaviour
{
    public static SFXAudioManager Instance;
    public List<SoundEffect> soundEffects = new List<SoundEffect>();
    public AudioSource audioSource;

    private void Start()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySFX(string sfxName)
    {
        SoundEffect sfx = soundEffects.Find(sound => sound.name == sfxName);

        if (sfx == null)
        {
            Debug.LogWarning($"SFX Audio Manager: Sound effect '{sfxName}' not found in the list.");
            return;
        }

        audioSource.PlayOneShot(sfx.clip);
    }
}

[Serializable]
public class SoundEffect
{
    public string name;      
    public AudioClip clip;  
}
