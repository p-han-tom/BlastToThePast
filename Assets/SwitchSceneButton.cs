using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSceneButton : MonoBehaviour
{

    private HUDControl hud;
    public GameObject toSetActive;

    void Start() {
        hud = GameObject.Find("HUD").GetComponent<HUDControl>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            toSetActive.SetActive(true);

        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            toSetActive.SetActive(false);
            
        }
    }
}
