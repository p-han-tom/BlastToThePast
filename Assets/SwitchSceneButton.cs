using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSceneButton : MonoBehaviour
{

    private HUDControl hud;

    void Start() {
        hud = GameObject.Find("HUD").GetComponent<HUDControl>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            // hud.Pause();

        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            // hud.Unpause();
            
        }
    }
}
