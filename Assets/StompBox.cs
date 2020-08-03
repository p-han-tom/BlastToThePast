using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompBox : MonoBehaviour
{
    private Player player;
    private BoxCollider2D col;
    void Start()
    {
        player = transform.parent.GetComponent<Player>();
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