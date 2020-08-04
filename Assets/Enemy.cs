using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Rewinder
{
    protected int direction = 1;
    public override void Rewind() {
        base.Rewind();
        if (gameObject.CompareTag("Enemy")) {
            if (afterimage.transform.eulerAngles.y != transform.eulerAngles.y) {
                direction*=-1;
                transform.localRotation = (transform.localRotation == Quaternion.Euler(0,0,0)) ? Quaternion.Euler(0,180,0) : Quaternion.Euler(0,0,0);
            }
        }
    }
}
