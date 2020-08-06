using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarThresholds : MonoBehaviour
{
    public float star3t;
    public float star2t;
    public int HowManyStars(float score) {
        if (score >= star3t) return 3;
        else if (score >= star2t) return 2;
        else return 1;
    }
}
