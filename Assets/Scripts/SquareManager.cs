using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SquareManager : MonoBehaviour
{
    public static SquareManager instance;
    
    [Header("Square Settings")]
    public GameObject squarePrefab;
    public Transform center;
    public Transform obstacleParent;
    public float innerRadius;
    public float outerRadius;

    private void Start()
    {
        instance = this;
    }

    public void AddSquare()
    {
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;

        bool isInner = Random.value < 0.5f;
        
        float radius = isInner ? innerRadius : outerRadius;

        Vector3 pos = center.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;

        GameObject newObs = Instantiate(squarePrefab, pos, Quaternion.identity,  obstacleParent);

        Vector3 direction = center.position - newObs.transform.position;
        float centerAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        newObs.transform.rotation = Quaternion.Euler(0, 0, centerAngle - 90);
    }
}
