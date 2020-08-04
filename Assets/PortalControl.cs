using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalControl : MonoBehaviour
{
    public int orbsRequired = 0;
    private bool active = false;
    private float rotateSpeedInactive = 50f;
    private float rotateSpeedActive = 80f;
    private float rotateSpeedCurrent;
    private SpriteRenderer sr;
    public Sprite activeSprite;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if (orbsRequired == 0) active = true;
        rotateSpeedCurrent = rotateSpeedInactive;
        if (active) ActivatePortal();
    }
    void Update()
    {
        transform.Rotate(0, 0, rotateSpeedCurrent * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (active && other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().win();
        }
    }
    public void ActivatePortal() {
        active = true;
        rotateSpeedCurrent = rotateSpeedActive;
        sr.sprite = activeSprite;
    }
}
