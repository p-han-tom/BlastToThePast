using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollisionHandler : MonoBehaviour
{
    Vector2 lastVelocity;
    void Update()
    {
        lastVelocity = GetComponent<Rigidbody2D>().velocity;
    }

    void OnCollisionEnter2D(Collision2D other) {
        // Check if other is mirror and reflect
        if (other.gameObject.tag == "Mirror") {
            Vector2 wallNormal = other.contacts[0].normal;
            Vector2 newDirection = Vector2.Reflect(lastVelocity, wallNormal).normalized;

            GetComponent<Rigidbody2D>().velocity = newDirection*25f;
        } else {
            Destroy(gameObject);

        }
    }
}
