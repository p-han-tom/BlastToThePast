using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullets : MonoBehaviour
{

    public GameObject bulletPrefab;
    GameObject bullet;
    

    public void FireBullet(Vector2 direction) {
        bullet = Instantiate(bulletPrefab, transform.Find("FirePoint").position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().AddForce(direction * 2000f);
    }
}
