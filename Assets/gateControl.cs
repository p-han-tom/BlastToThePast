using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gateControl : MonoBehaviour
{
    public GameObject button;
    public float openSpeed = 0.5f;
    public float closeSpeed = 0.1f;
    private buttonControl bc;
    private Vector3 startPos;
    private Vector3 endPos;
    void Start()
    {
        bc = button.transform.GetComponent<buttonControl>();
        startPos = transform.position;
        endPos = transform.Find("End position").position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != endPos && bc.isPressed())
        {
            transform.position = Vector3.MoveTowards(transform.position, endPos, openSpeed * Time.deltaTime);
        }
        else if (transform.position != startPos && !bc.isPressed())
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos, closeSpeed * Time.deltaTime);
        }
    }
}
