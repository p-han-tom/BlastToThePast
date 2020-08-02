using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isAttacking;
    bool isBeingAttacked = false;
    float movementSpeed = 5f;
    float attackCooldown;
    float attackCooldownDuration = 0.3f;
    int rewindIndex = 1;


    Vector2 movement;
    Vector2 direction;
    Vector3 mousePos;
    List<Vector3> rewindPositions = new List<Vector3>();

    Animator animator;
    Rigidbody2D rb;

    public GameObject afterimagePrefab;
    GameObject afterimage;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        afterimage = Instantiate(afterimagePrefab, transform.position, Quaternion.identity);
    }

    void Update()
    {   
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
        if (attackCooldown > 0f) {
            attackCooldown -= Time.deltaTime;
        }

        CheckInput();
    }

    void FixedUpdate() {
        if (isBeingAttacked) return;
        Move(movement);
    }

    void CheckInput() {
        // Mouse position relative to player
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        direction.Normalize();

        // Check for movement and change animations accordingly
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        animator.SetBool("moving", (rb.velocity == Vector2.zero || isAttacking) ? false : true);

        // Listen for rewind key press
        if (Input.GetKeyDown(KeyCode.E)) {
            Rewind();
        }

        if (Input.GetMouseButton(0) && attackCooldown <= 0f) {
            BasicAttack();
        }

    }

    void Move(Vector2 movement) {
        rb.velocity = movement * movementSpeed;

        if (isAttacking) rb.velocity *= 0.5f;
        else {
            // Rotate entity while moving
            if (rb.velocity.x != 0) transform.localRotation = (rb.velocity.x > 0) ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);
        }
    }

    void Rewind() {
        transform.position = rewindPositions[rewindIndex];

        // Reset rewind variables and teleport afterimage back to player
        rewindIndex = 1;
        rewindPositions.Clear();
        afterimage.transform.position = transform.position;
        afterimage.GetComponent<Animator>().Rebind();
    }

    void BasicAttack() {
        animator.SetTrigger("attack");
        transform.localRotation = (mousePos.x >= transform.position.x) ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);
    }

    public IEnumerator MiniDash() {
        isBeingAttacked = true;
        rb.AddForce(direction * 8f, ForceMode2D.Impulse);
        // Create Crescent

        yield return new WaitForSeconds(0.05f);
        isBeingAttacked = false;

    }

    public void goOnCooldown() {
        attackCooldown = attackCooldownDuration;
    }
}
