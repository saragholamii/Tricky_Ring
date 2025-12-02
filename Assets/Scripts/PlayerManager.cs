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
    
    void Start()
    {
        currentRadius = outerRadius; 
    }
    
    private void Update()
    {
        
#if UNITY_STANDALONE || UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
                ToggleInOut();
        }
#endif
        
#if UNITY_ANDROID || UNITY_IOS

        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                    {
                        ToggleInOut();
                    }
                }
            }
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
