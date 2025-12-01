using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerManager : MonoBehaviour
{
    public GameObject player;
    public float speed;

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
        transform.Rotate(new Vector3(0, 0, speed * Time.deltaTime));
        
#if UNITY_STANDALONE || UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            if(EventSystem.current.IsPointerOverGameObject())
                return;
            ToggleInOut();
        }
#endif
        
#if UNITY_ANDROID || UNITY_IOS

        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            if(EventSystem.current.IsPointerOverGameObject())
                return;
            ToggleInOut();
        }
#endif
        
    }
    
    void ToggleInOut()
    {
        currentRadius = Mathf.Approximately(currentRadius, innerRadius) ? outerRadius : innerRadius;
        
        Vector3 dir = (player.transform.position - transform.position).normalized;
        player.transform.position = transform.position + dir * currentRadius;
    }
}
