using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class greenEnemyControl : MonoBehaviour
{
    // LayerMasks
    public LayerMask groundLayer;
    public LayerMask playerLayer;

    // Movement variables
    private Transform feetPos;
    private Transform lookAheadGroundPos;
    private Transform lookAheadWallPos;
    private int direction = 1;
    private float speed = 2f;

    // Components
    private Rigidbody2D rb;

    // Rewind variables
    List<Vector3> rewindPositions = new List<Vector3>();
    int rewindIndex = 1;

    // Prefabs
    public GameObject afterimagePrefab;
    GameObject afterimage;

    void Start()
    {
        feetPos = transform.Find("Feet");
        lookAheadGroundPos = transform.Find("LookAheadGround");
        lookAheadWallPos = transform.Find("LookAheadWall");
        rb = GetComponent<Rigidbody2D>();
        afterimage = Instantiate(afterimagePrefab, transform.position, Quaternion.identity);
        afterimage.GetComponent<Animator>().runtimeAnimatorController = GetComponent<Animator>().runtimeAnimatorController;
        afterimage.GetComponent<SpriteRenderer>().color = new Color(10, 196, 60, 0.75f);
        // afterimage.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        MoveAfterImage();
        lookAhead();
        rb.velocity = new Vector2(speed * direction, rb.velocity.y);
    }

    void lookAhead()
    {
        if (!Physics2D.OverlapCircle(lookAheadGroundPos.position, 0.2f, groundLayer))
        {
            turnAround();
        }
        if (Physics2D.OverlapCircle(lookAheadWallPos.position, 0.2f, groundLayer))
        {
            turnAround();
        }
    }

    public void turnAround()
    {
        direction *= -1;
        transform.eulerAngles = (direction < 0) ? new Vector3(0, 180, 0) : new Vector3(0, 0, 0);
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().die();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlayerHurtBox")
        {
            other.gameObject.transform.parent.GetComponent<Player>().bounce();
            die();
        }
    }

    void MoveAfterImage()
    {
        rewindPositions.Add(transform.position);

        // After 2 seconds, an afterimage will start tracking the player's previous position
        if (rewindPositions.Count > 120)
        {
            rewindIndex++;

            // Change animations for if player is moving or idle and rotate player
            if (rewindPositions[rewindIndex].x != rewindPositions[rewindIndex - 2].x || rewindPositions[rewindIndex].y != rewindPositions[rewindIndex - 2].y)
            {
                afterimage.transform.localRotation = (rewindPositions[rewindIndex].x > rewindPositions[rewindIndex - 2].x) ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);
            }
            else
            {
                if (transform.position == afterimage.transform.position) afterimage.transform.localRotation = transform.localRotation;
            }
            afterimage.transform.position = rewindPositions[rewindIndex];
        }
    }

    public void die() {
        Destroy(afterimage);
        Destroy(gameObject);
    }
}
