using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyControl : Enemy
{
    // LayerMasks
    public LayerMask groundLayer;
    public LayerMask playerLayer;
    public bool spiked = false;

    // Movement variables
    private Transform feetPos;
    private Transform lookAheadGroundPos;
    private Transform lookAheadWallPos;
    private float speed = 2f;

    // Components
    private Rigidbody2D rb;
    void Start()
    {
        feetPos = transform.Find("Feet");
        lookAheadGroundPos = transform.Find("LookAheadGround");
        lookAheadWallPos = transform.Find("LookAheadWall");
        rb = GetComponent<Rigidbody2D>();
        afterimage = Instantiate(afterimagePrefab, transform.position, Quaternion.identity);
        afterimage.GetComponent<Animator>().runtimeAnimatorController = GetComponent<Animator>().runtimeAnimatorController;
        afterimage.GetComponent<SpriteRenderer>().color = new Color(10, 196, 60, 0.75f);
        GetComponent<Animator>().SetBool("moving", true);
        // afterimage.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        lookAhead();
        rb.velocity = new Vector2(speed * direction, rb.velocity.y);
    }
    void FixedUpdate() {
        MoveAfterImage();
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
        transform.localRotation = (direction < 0) ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().die();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlayerHurtBox" && spiked == false)
        {
            other.gameObject.transform.parent.GetComponent<Player>().bounce();
            die();
        }
    }
    public bool isSpiked() {
        return spiked;
    }
    public void die()
    {
        Destroy(afterimage);
        Destroy(gameObject);
    }
}
