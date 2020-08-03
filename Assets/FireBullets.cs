using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullets : MonoBehaviour
{

    public GameObject bulletPrefab;
    GameObject bullet;

    public int reflections;
    public float maxLength;
    public LineRenderer lineRenderer;
    RaycastHit2D hit2D;
    Ray2D ray;

    Transform firepoint;

    void Start() {
        firepoint = transform.Find("FirePoint");
    }
    
    public void FireBullet(Vector3 mousePos) {


        // bullet = Instantiate(bulletPrefab, transform.Find("FirePoint").position, Quaternion.identity);
        // bullet.GetComponent<Rigidbody2D>().velocity = direction * 50f;
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, firepoint.position);
        

        Vector3 direction = new Vector3(mousePos.x - firepoint.position.x, mousePos.y - firepoint.position.y, 0);
        RaycastHit2D rayInfo = Physics2D.Raycast(firepoint.position, direction);
        
        for (int i = 0; i < reflections; i ++) {
            if (rayInfo) {
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, rayInfo.point);
                direction = Vector2.Reflect(direction,rayInfo.normal);
                rayInfo = Physics2D.Raycast(rayInfo.point, direction);
            } else {
                lineRenderer.positionCount ++;
                lineRenderer.SetPosition(lineRenderer.positionCount-1, direction * 100);
                
            }
        }
        

    }
}
