using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Player : MonoBehaviour
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

    // Rewind ability
    List<Vector3> rewindPositions = new List<Vector3>();
    int rewindIndex = 1;

    // Components
    Animator animator;
    Rigidbody2D rb;

    // Prefabs
    public GameObject afterimagePrefab;
    GameObject afterimage;

    // Gun related
    Transform pivot;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        afterimage = Instantiate(afterimagePrefab, transform.position, Quaternion.identity);
        feetPos = transform.Find("Feet");
        pivot = transform.Find("Pivot");
    }

    void Update()
    {   
        MoveAfterImage();
        pivot.localScale = (mousePos.x > transform.position.x) ? new Vector3(-1,1,1) : new Vector3(1,1,1);
        pivot.up = direction;
        
        Vector3 clampedAngles = pivot.eulerAngles;
        if (clampedAngles.z > 180) clampedAngles.z -= 360;
        clampedAngles.z = Mathf.Clamp(clampedAngles.z, -100, 100);
        pivot.eulerAngles = clampedAngles;

    }

    void FixedUpdate() {
        CheckInput();
        Move();
    }

    void CheckInput() {
        // Mouse position relative to player
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        direction.Normalize();

        // Check for movement and change animations accordingly
        moveInput = Input.GetAxisRaw("Horizontal");
        animator.SetBool("moving", (rb.velocity == Vector2.zero || isAttacking) ? false : true);

        // Listen for rewind key press
        if (Input.GetKeyDown(KeyCode.E)) {
            Rewind();
        }
    }

    void Move() {
        // Set jump variables
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, groundLayer);
        rb.gravityScale = (isGrounded) ? 5f : 8f;

        // Start the jump
        if (Math.Abs(rb.velocity.y) <= 0.1f && Input.GetKey(KeyCode.W) && isGrounded) {
            rb.velocity = Vector2.up * jumpForce;
            isJumping = true;
            jumpTimer = jumpDuration;
        }

        // Jump higher while key is pressed
        if (isJumping) {
            if (jumpTimer > 0) {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimer-=Time.deltaTime;
            } else {
                isJumping = false;
                rb.gravityScale = 8f;
            }
        } else if (Input.GetKeyUp(KeyCode.W)) {
            isJumping = false;
            rb.gravityScale = 8f;
        }

        rb.velocity = new Vector2(moveInput * movementSpeed, rb.velocity.y);
        // Rotate entity while moving
        if (rb.velocity.x != 0) transform.localRotation = (rb.velocity.x > 0) ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);
        
    }

    void Rewind() {
        transform.position = rewindPositions[rewindIndex];

        // Reset rewind variables and teleport afterimage back to player
        rewindIndex = 1;
        rewindPositions.Clear();
        afterimage.transform.position = transform.position;
        afterimage.GetComponent<Animator>().Rebind();
    }

    void MoveAfterImage() {
        rewindPositions.Add(transform.position);

        // After 2 seconds, an afterimage will start tracking the player's previous position
        if (rewindPositions.Count > 120) {
            rewindIndex ++;

            // Change animations for if player is moving or idle and rotate player
            if (rewindPositions[rewindIndex].x != rewindPositions[rewindIndex-2].x || rewindPositions[rewindIndex].y != rewindPositions[rewindIndex-2].y) {
                afterimage.GetComponent<Animator>().SetBool("moving", true);
                afterimage.transform.localRotation = (rewindPositions[rewindIndex].x > rewindPositions[rewindIndex-2].x) ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);
            } else {
                afterimage.GetComponent<Animator>().SetBool("moving", false);
                if (transform.position == afterimage.transform.position) afterimage.transform.localRotation = transform.localRotation;
            }
            afterimage.transform.position = rewindPositions[rewindIndex];
        }
    }
}
