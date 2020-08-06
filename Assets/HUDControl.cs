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
    private bool timerStopped = false;
    private TextMeshProUGUI timerDisplay;
    private AudioManager audioManager;
    private GameObject restartPrompt;

    public bool paused = false;

    void Start()
    {
        rewindsDisplay = transform.Find("Rewinds").GetComponent<TextMeshProUGUI>();
        timerDisplay = transform.Find("Timer").GetComponent<TextMeshProUGUI>();
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        restartPrompt = transform.Find("RestartPrompt").gameObject;
        restartPrompt.SetActive(false);
    }

    public void IncreaseRewinds()
    {
        rewinds++;
        rewindsDisplay.text = "Rewinds: " + rewinds;
    }

    void UpdateTimer()
    {
        if (timerStopped == false)
        {
            timer += Time.deltaTime;
            if (timer >= 20f && SceneManager.GetActiveScene().name != "Main Menu") PromptRestart();
            timerDisplay.text = "TIME: " + Math.Round(timer, 3);
        }
    }
    public void StopTimer() { timerStopped = true; }
    public float GetTimer() {return timer;}
    void CheckInput()
    {
        // Listen for restart
        if (Input.GetKeyDown(KeyCode.R))
        {
            audioManager.Play("RestartLevel");
            Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        }

        // Listen for pause
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (paused) {
                Unpause();
            } else {
                Pause();
            }
        }
    }
    public void PromptRestart() {restartPrompt.SetActive(true);}
    public void Pause() {
        Time.timeScale = 0;
        paused = true;
    }

    public void Unpause() {
        Time.timeScale = 1;
        paused = false;
    }
    void Update()
    {
        UpdateTimer();
        CheckInput();
    }
}
