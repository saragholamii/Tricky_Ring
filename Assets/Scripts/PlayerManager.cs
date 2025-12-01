using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerManager : MonoBehaviour
{
    public GameObject player;
    public Transform center;

    public float innerRadius;
    public float outerRadius;
    private float currentRadius;
    
    bool isMobile = (Application.isMobilePlatform);
    
    void Start()
    {
        currentRadius = outerRadius; 
    }
    
    private void Update()
    {
        
#if UNITY_STANDALONE || UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            ToggleInOut();
        }
#endif
        
#if UNITY_ANDROID || UNITY_IOS

        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            ToggleInOut();
        }
#endif
        
    }
    
    void ToggleInOut()
    {
        if(player == null)
            return;
        
        currentRadius = Mathf.Approximately(currentRadius, innerRadius) ? outerRadius : innerRadius;
        
        Vector3 dir = (player.transform.position - center.transform.position).normalized;
        player.transform.position = center.transform.position + dir * currentRadius;
    }
}
