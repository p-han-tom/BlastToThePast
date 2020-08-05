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
    public GameObject portalParticlePrefab;
    public GameObject inactivePortalParticlePrefab;
    GameObject inactivePortalParticles;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if (orbsRequired == 0) active = true;
        else inactivePortalParticles = Instantiate(inactivePortalParticlePrefab, transform.position, Quaternion.identity);
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
            other.gameObject.GetComponent<Player>().EnterPortalWin(transform.position);
        }
    }
    public void ActivatePortal() {
        Destroy(inactivePortalParticles);
        GameObject.Find("Audio Manager").GetComponent<AudioManager>().Play("PortalOpen");
        active = true;
        rotateSpeedCurrent = rotateSpeedActive;
        sr.sprite = activeSprite;
        Instantiate(portalParticlePrefab, transform.position, Quaternion.identity);
    }
}
