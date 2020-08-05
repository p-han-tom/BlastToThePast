using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonControl : MonoBehaviour
{
    public Sprite pressedSprite;
    private Sprite defaultSprite;
    private bool pressed = false;
    private SpriteRenderer sr;
    private AudioManager audioManager;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        defaultSprite = sr.sprite;
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            audioManager.Play("ButtonPress");
            pressed = true;
            sr.sprite = pressedSprite;
        }
    }
    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            audioManager.Play("ButtonRelease");
            pressed = false;
            sr.sprite = defaultSprite;
        }
    }
    public bool isPressed() {return pressed;}
}
