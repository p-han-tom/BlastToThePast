using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    // Update is called once per frame
    Transform leader;
    Vector3 leaderVelocity;
    Vector3 mousePos;
    Vector3 leaderPos;
    float xMaxDist = 1f;
    float yMaxDist = 1f;
    float cameraTime = 0.1f;
    float maxCameraSpeed = 100f;
    void Start() {
        leader = GameObject.Find("Player").transform;
    }
    void FixedUpdate()
    {
        if (leader != null)
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(leader.position.x, leader.position.y, transform.position.z), ref leaderVelocity, cameraTime, maxCameraSpeed);
        }
    }
    public IEnumerator cameraShake(float duration, float magnitude)
    {
        float timeElapsed = 0f;
        while (timeElapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.position = new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z);

            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }
}
