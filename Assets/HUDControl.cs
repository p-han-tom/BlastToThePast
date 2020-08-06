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
    private GameObject instructions;
    private GameObject pauseMenu;

    public bool paused = false;

    void Start()
    {
        rewindsDisplay = transform.Find("Rewinds").GetComponent<TextMeshProUGUI>();
        timerDisplay = transform.Find("Timer").GetComponent<TextMeshProUGUI>();
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        restartPrompt = transform.Find("RestartPrompt").gameObject;
        restartPrompt.SetActive(false);
        instructions = transform.Find("Instructions").gameObject;
        instructions.SetActive(false);
        pauseMenu = transform.Find("Pause Menu").gameObject;
        pauseMenu.SetActive(false);
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
            Unpause();
            audioManager.Play("RestartLevel");
            Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        }

        // Listen for pause
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) {
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
        pauseMenu.SetActive(true);
    }

    public void Unpause() {
        Time.timeScale = 1;
        paused = false;
        pauseMenu.SetActive(false);
        DisableInstructions();
    }
    public void GoToHomeMenu() {
        Unpause();
        SceneManager.LoadScene("Main Menu");
    }
    public void EnableInstructions(){instructions.SetActive(true);}
    public void DisableInstructions(){instructions.SetActive(false);}
    void Update()
    {
        UpdateTimer();
        CheckInput();
    }
}
