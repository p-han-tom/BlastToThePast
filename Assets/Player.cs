using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    bool isAttacking;
    float movementSpeed = 5f;
    float lastVelocity;

    Vector2 movement;
    Animator animator;
    Rigidbody2D rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {   
        CheckInput();
    }

    void FixedUpdate() {
        Move(movement);
    }

    void CheckInput() {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        animator.SetBool("moving", (rb.velocity == Vector2.zero || isAttacking) ? false : true);

    }

    void Move(Vector2 movement) {
        if (movement.y != 0) lastVelocity = movement.y;
        rb.velocity = movement * movementSpeed;

        if (isAttacking) rb.velocity *= 0.5f;

        if (!isAttacking) {
            // Rotate entity while moving
            if (rb.velocity.x != 0) transform.localRotation = (rb.velocity.x > 0) ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);

            // Rotate entity while idle
        }
    }
}
