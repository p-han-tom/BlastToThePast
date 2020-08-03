using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class greenEnemyControl : MonoBehaviour
{
    public LayerMask groundLayer;
    public LayerMask playerLayer;
    private float speed = 2f;
    private Transform feetPos;
    private Transform lookAheadGroundPos;
    private Transform lookAheadWallPos;
    private Rigidbody2D rb;
    private int direction = 1;
    void Start()
    {
        feetPos = transform.Find("Feet");
        lookAheadGroundPos = transform.Find("LookAheadGround");
        lookAheadWallPos = transform.Find("LookAheadWall");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
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
    public void die() {
        Destroy(gameObject);
    }
}
