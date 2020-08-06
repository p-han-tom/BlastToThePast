using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LevelsBeatenCounter : MonoBehaviour
{
    void Start()
    {
        int levelsBeaten = PlayerPrefs.GetInt("LevelsBeaten", 0);
        if (levelsBeaten < 15) transform.Find("Text").GetComponent<TextMeshProUGUI>().text = levelsBeaten.ToString()+"/15 levels beaten";
        else transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "Thanks for playing!";
    }
}
