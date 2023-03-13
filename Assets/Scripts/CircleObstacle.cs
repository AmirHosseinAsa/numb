using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleObstacle : MonoBehaviour {


    float moveSpeed;


    void Start()
    {
        moveSpeed = GetComponentInParent<ObstacleRespawner>().moveSpeed;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, -moveSpeed);
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, moveSpeed* 10 * Time.deltaTime));
    }
}
