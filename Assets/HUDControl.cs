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
    private GameObject clearedPopup;
    private TextMeshProUGUI clearedStats;
    private TextMeshProUGUI clearedHighscore;
    private GameObject levelSelectPopup;
    private String sceneName;

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
        clearedPopup = transform.Find("Cleared Popup").gameObject;
        clearedStats = clearedPopup.transform.Find("Stats").GetComponent<TextMeshProUGUI>();
        clearedHighscore = clearedPopup.transform.Find("Highscore").GetComponent<TextMeshProUGUI>();
        clearedPopup.SetActive(false);
        levelSelectPopup = transform.Find("Level Select").gameObject;
        levelSelectPopup.SetActive(false);
        sceneName = SceneManager.GetActiveScene().name;
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
            if (timer >= 20f && sceneName != "Main Menu") PromptRestart();
            timerDisplay.text = "TIME: " + Math.Round(timer, 3);
        }
    }
    public void StopTimer() { timerStopped = true; }
    public float GetTimer() { return timer; }
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
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (paused)
            {
                Unpause();
            }
            else
            {
                Pause();
            }
        }
    }
    public void PromptRestart() { restartPrompt.SetActive(true); }
    public void Pause()
    {
        PlayerPrefs.DeleteAll();
        Time.timeScale = 0;
        paused = true;
        pauseMenu.SetActive(true);
    }
    public void LevelCleared()
    {
        string levelKey = "Level" + SceneManager.GetActiveScene().name;
        float bestTime = PlayerPrefs.GetFloat(levelKey + "BestTime", 29.999f);
        int bestRewinds = PlayerPrefs.GetInt(levelKey + "BestRewinds", 19);
        float highscore = PlayerPrefs.GetFloat(levelKey + "Highscore", 0);
        float score = (29.999f - timer) * (19 - rewinds);
        if (highscore < score)
        {
            Debug.Log("WEOFIJ");
            PlayerPrefs.SetFloat(levelKey + "Highscore", score);
            highscore = score;
            PlayerPrefs.SetFloat(levelKey + "BestTime", timer);
            bestTime = timer;
            PlayerPrefs.SetInt(levelKey + "BestRewinds", rewinds);
            bestRewinds = rewinds;
            clearedStats.text = "<size=60><color=#ffa726>new highscore!</color></size>";
            clearedHighscore.text = "<color=#ffa726>Best clear</color>: <color=#91a7ff>" + Math.Round(bestTime, 3) + " seconds</color> using <color=#42bd41>" + bestRewinds + " rewinds</color>\nHighscore: " + Math.Round(highscore, 0);
        }
        else
        {
            clearedStats.text = "Cleared in <color=#91a7ff>" + Math.Round(timer, 3) + " seconds</color> using <color=#42bd41>" + rewinds + " rewinds</color>\nTotal score: " + Math.Round(score, 0);
            clearedHighscore.text = "<color=#ffa726>Best clear</color>: <color=#91a7ff>" + Math.Round(bestTime, 3) + " seconds</color> using <color=#42bd41>" + bestRewinds + " rewinds</color>\nHighscore: " + Math.Round(highscore, 0);
        }
        clearedPopup.SetActive(true);
    }
    public void GoToNextLevel() { Debug.Log("next level brah"); }
    public void Unpause()
    {
        Time.timeScale = 1;
        paused = false;
        pauseMenu.SetActive(false);
        DisableInstructions();
    }
    public void GoToHomeMenu()
    {
        Unpause();
        SceneManager.LoadScene("Main Menu");
    }
    public void EnableInstructions() { instructions.SetActive(true); }
    public void DisableInstructions() { instructions.SetActive(false); }
    public void EnableLevelSelect() { levelSelectPopup.SetActive(true); }
    public void DisableLevelSelect() { levelSelectPopup.SetActive(false); }
    public void SelectLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        StartCoroutine(GameObject.Find("LevelLoader").GetComponent<LevelLoader>().fade(sceneName));

    }

    void Update()
    {
        UpdateTimer();
        CheckInput();
    }
}
