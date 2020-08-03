﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullets : MonoBehaviour
{

    public int reflections;
    LineRenderer lineRenderer;
    RaycastHit2D hit2D;
    bool inWall;
    Transform firepoint;

    void Start() {
        firepoint = transform.Find("FirePoint");
        lineRenderer = transform.root.Find("Line").GetComponent<LineRenderer>();
    }
    
    public IEnumerator FireBullet(Vector3 mousePos) {

        if (!inWall) {
            lineRenderer.positionCount = 1;
            lineRenderer.SetPosition(0, firepoint.position);
            

            Vector3 direction = new Vector3(mousePos.x - firepoint.position.x, mousePos.y - firepoint.position.y, 0);
            RaycastHit2D rayInfo = Physics2D.Raycast(firepoint.position, direction);
            
            for (int i = 0; i < reflections; i ++) {
                if (rayInfo) {
                    lineRenderer.positionCount++;
                    lineRenderer.SetPosition(lineRenderer.positionCount - 1, rayInfo.point);
                    direction = Vector2.Reflect(direction,rayInfo.normal);
                    rayInfo = Physics2D.Raycast(new Vector2(rayInfo.point.x + rayInfo.normal.x, rayInfo.point.y + rayInfo.normal.y), direction);
                } else {
                    lineRenderer.positionCount ++;
                    lineRenderer.SetPosition(lineRenderer.positionCount-1, direction * 100);
                }
            }

            lineRenderer.enabled = true;
            yield return new WaitForSeconds(0.02f);
            lineRenderer.enabled = false;
        }

    }

    void OnTriggerEnter2D(Collider2D other) {
        // Eventually comparetag for wall
        if (other.gameObject.CompareTag("Mirror")) {
            inWall = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        // Eventually comparetag for wall
        if (other.gameObject.CompareTag("Mirror")) {
            inWall = false;
        }
    }
}
