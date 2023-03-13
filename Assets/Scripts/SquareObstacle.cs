using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareObstacle : MonoBehaviour {

    float moveSpeed;


    void Start()
    {
        moveSpeed = GetComponentInParent<ObstacleRespawner>().moveSpeed;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, -moveSpeed);
    }

}
