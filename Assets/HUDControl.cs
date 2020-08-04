using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class HUDControl : MonoBehaviour
{
    private int shotsFiredCount;
    private TextMeshProUGUI shotsFiredDisplay;

    private float timer;
    private TextMeshProUGUI timerDisplay;

    void Start()
    {
        shotsFiredDisplay = transform.Find("Shots Fired").GetComponent<TextMeshProUGUI>();
        timerDisplay = transform.Find("Timer").GetComponent<TextMeshProUGUI>();
    }

    public void IncreaseShotsFired() {
        shotsFiredCount ++;
        shotsFiredDisplay.text = "Shots Fired: " + shotsFiredCount;
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
