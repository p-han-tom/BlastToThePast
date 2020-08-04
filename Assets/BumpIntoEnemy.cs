using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumpIntoEnemy : MonoBehaviour
{
    Enemy parent;
    void Start()
    {
        parent = transform.parent.GetComponent<Enemy>();
    }
    void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponent<Enemy>() != null) {
            parent.turnAround();
        }    
    }
}
