using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Player : Rewinder
{
    // Attacking variables
    bool isAttacking;
    bool isBeingAttacked;

    // Movement and jump related variables
    public LayerMask groundLayer;
    bool isGrounded;
    bool isJumping;
    Transform feetPos;
    float checkRadius = 0.025f;
    float movementSpeed = 5f;
    float jumpForce = 10f;
    float moveInput;
    float jumpTimer;
    float jumpDuration = 0.2f;

    // Aiming stuff
    Vector2 direction;
    Vector3 mousePos;

    // Components
    Animator animator;
    Rigidbody2D rb;

    // Gun related
    Transform pivot;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        afterimage = Instantiate(afterimagePrefab, transform.position, Quaternion.identity);
        afterimage.GetComponent<Animator>().runtimeAnimatorController = GetComponent<Animator>().runtimeAnimatorController;
        feetPos = transform.Find("Feet");
        pivot = transform.Find("Pivot");
    }

    void Update()
    {
        MoveAfterImage();
        RotateGun();
        
    }
    void FixedUpdate() {
        CheckInput();
        Move();
    }

    void CheckInput()
    {
        // Mouse position relative to player
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        direction.Normalize();

        // Check for movement and change animations accordingly
        moveInput = Input.GetAxisRaw("Horizontal");
        animator.SetBool("moving", (rb.velocity == Vector2.zero || isAttacking) ? false : true);

        // Listen for rewind key press
        if (Input.GetKeyDown(KeyCode.E))
        {
            Rewind();
        }

        // Listen for gun shot
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(transform.Find("Pivot").Find("Gun").GetComponent<FireBullets>().FireBullet(mousePos));
        }
    }

    void Move()
    {
        // Set jump variables
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, groundLayer);
        rb.gravityScale = (isGrounded) ? 5f : 8f;

        // Start the jump
        if (Math.Abs(rb.velocity.y) <= 0.1f && Input.GetKey(KeyCode.W) && isGrounded && !isJumping)
        {
            rb.velocity = Vector2.up * jumpForce;
            isJumping = true;
            jumpTimer = jumpDuration;
        }

        // Jump higher while key is pressed
        if (isJumping)
        {
            if (jumpTimer > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimer -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
                rb.gravityScale = 8f;
            }
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            isJumping = false;
            rb.gravityScale = 8f;
        }

        rb.velocity = new Vector2(moveInput * movementSpeed, rb.velocity.y);
        // Rotate entity while moving
        if (rb.velocity.x != 0) transform.localRotation = (rb.velocity.x < 0) ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);

    }

    void RotateGun()
    {
        pivot.localScale = (mousePos.x > transform.position.x) ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);
        pivot.up = direction;

    }
    public bool getIsGrounded()
    {
        return isGrounded;
    }
    public void bounce()
    {
        rb.velocity = Vector2.up * jumpForce;
        isJumping = true;
        jumpTimer = jumpDuration;
    }
    public void die() {

        Debug.Log("AHHHHHHHHHHHHHHH FUCK");
    }
    public void win() {
        Debug.Log("dub");
    }
}
