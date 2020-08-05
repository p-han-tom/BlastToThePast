using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class HUDControl : MonoBehaviour
{
    private int rewinds;
    private TextMeshProUGUI rewindsDisplay;

    private float timer;
    private TextMeshProUGUI timerDisplay;

    void Start()
    {
        rewindsDisplay = transform.Find("Rewinds").GetComponent<TextMeshProUGUI>();
        timerDisplay = transform.Find("Timer").GetComponent<TextMeshProUGUI>();
    }

    public void IncreaseRewinds() {
        rewinds ++;
        rewindsDisplay.text = "Rewinds: " + rewinds;
    } 

    void UpdateTimer() {
        timer += Time.deltaTime;
        timerDisplay.text = "TIME: " + Math.Round(timer, 3);
    }

    void Update()
    {
        UpdateTimer();   
    }
}
