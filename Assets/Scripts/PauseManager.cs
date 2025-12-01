using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
   public GameObject panel;
   public AudioSource audioSource;
   
   private bool paused = false;
   
   private void Start()
   {
      panel.SetActive(false);
   }

   public void PauseGame()
   {
      if (!paused)
      {
         paused = true;
         panel.SetActive(true);
         audioSource.Pause();
         Time.timeScale = 0;
         return;
      }
     
      paused = false;
      panel.SetActive(false);
      audioSource.Play();
      Time.timeScale = 1;
      
   }

   

   
}
