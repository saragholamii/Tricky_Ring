using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject player;
    public float speed;

    public float innerRadius;
    public float outerRadius;
    private float currentRadius;
    
    void Start()
    {
        currentRadius = outerRadius; 
    }
    
    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, speed * Time.deltaTime));
        
        if (Input.GetMouseButtonDown(0))
            ToggleInOut();
    }
    
    void ToggleInOut()
    {
        currentRadius = Mathf.Approximately(currentRadius, innerRadius) ? outerRadius : innerRadius;
        
        Vector3 dir = (player.transform.position - transform.position).normalized;
        player.transform.position = transform.position + dir * currentRadius;
    }
}
