using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleManager : MonoBehaviour
{
    [Header("Obstacle Settings")]
    public GameObject obstaclePrefab;
    public Transform center;
    public Transform obstacleParent;
    public float innerRadius;
    public float outerRadius;

    [Header("Spawn Settings")]
    public int maxObstacles = 10;

    private List<GameObject> obstacles = new List<GameObject>();

    private float obstacleHeight;
    

    [ContextMenu("OnSquareCollected")]
    public void OnSquareCollected()
    {
        int action = Random.Range(0, 2); // 0 = toggle existing, 1 = add new

        if (action == 0 && obstacles.Count > 0)
            ToggleRandomObstacle();
        else
            AddObstacle();
    }

    private void ToggleRandomObstacle()
    {
        GameObject obstacle = obstacles[Random.Range(0, obstacles.Count)];

        Vector3 dir = (obstacle.transform.position - center.position).normalized;
        float currentRadius = Vector3.Distance(center.position, obstacle.transform.position);
        float newRadius = Mathf.Approximately(currentRadius, innerRadius) ? outerRadius : innerRadius;

        obstacle.transform.position = center.position + dir * newRadius;
    }

    private void AddObstacle()
    {
        if (obstacles.Count >= maxObstacles)
            return;

        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;

        bool isInner = Random.value < 0.5f;
        
        float radius = isInner ? innerRadius : outerRadius;

        Vector3 pos = center.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;

        GameObject newObs = Instantiate(obstaclePrefab, pos, Quaternion.identity,  obstacleParent);
        obstacles.Add(newObs);

        Vector3 direction = center.position - newObs.transform.position;
        float centerAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        newObs.transform.rotation = Quaternion.Euler(0, 0, centerAngle - 90);
    }
}
