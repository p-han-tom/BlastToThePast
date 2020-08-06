using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition; 

    void Start() {
        if (PlayerPrefs.GetString("Loaded") == "false") {
            transition.SetTrigger("Loaded");
            PlayerPrefs.SetString("Loaded", "true");
        }
        
    }
    
    public IEnumerator fade(string scene) {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(scene);
    }
}
