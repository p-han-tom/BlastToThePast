using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject enemy;
    float frequency = 15f;
    float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        timer = frequency;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0) {
            timer -= Time.deltaTime;
        } else {
            Instantiate(enemy, transform.position, Quaternion.identity);
            timer = frequency;
        }
    }
}
