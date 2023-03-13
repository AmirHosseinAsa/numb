using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleRespawner : MonoBehaviour {

    public float moveSpeed = 2.5f;
    public GameObject[] randObstacle;

    const float perioud = 4.5f;
    void Start()
    {
        InvokeRepeating("GenerateRandomObst", 0, perioud);
    }
    void GenerateRandomObst()
    {
         Destroy(Instantiate(randObstacle[Random.Range(0,randObstacle.Length)],transform.position,Quaternion.identity,transform),10);
    }
}
