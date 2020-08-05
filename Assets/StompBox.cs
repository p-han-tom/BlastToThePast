using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompBox : MonoBehaviour
{
    private Player player;
    private Rigidbody2D rb;
    private BoxCollider2D col;
    void Start()
    {
        player = transform.parent.GetComponent<Player>();
        rb = player.GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        col.enabled = false;
    }
    void Update()
    {
        if (player.getIsGrounded() == false)
        {
            col.enabled = true;
        }
        else
        {
            col.enabled = false;
        }
    }
}