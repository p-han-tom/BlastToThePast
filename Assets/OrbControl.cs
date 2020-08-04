using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbControl : MonoBehaviour
{
    private float amplitude = 0.1f;
    private float frequency = 1f;

    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();
    void Start()
    {
        posOffset = transform.position;
    }

    void Update()
    {
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = tempPos;
    }
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<Player>().gainOrb();
            Collected();
        }            
    }
    void Collected() {
        Destroy(gameObject);
    }
}
