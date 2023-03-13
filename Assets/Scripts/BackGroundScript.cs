using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScript : MonoBehaviour {

    [SerializeField]
    private float bgSpeed = 1.5f;

    void FixedUpdate(){

        transform.Translate(Vector3.down * bgSpeed * Time.fixedDeltaTime);
    
    }

}
