using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAbility : MonoBehaviour
{
    public void onEnterAbility() {
        GetComponent<Player>().isAttacking = true;
    } 

    public void onExitAbility() {
        GetComponent<Player>().isAttacking = false;
        GetComponent<Player>().goOnCooldown();
        GetComponent<Animator>().Rebind();

    }

    public bool cancelAttack = false;
    public void attackCheckpoint() {
        StartCoroutine(GetComponent<Player>().MiniDash());
        if (cancelAttack) {
            cancelAttack = false;
            GetComponent<Player>().isAttacking = false;
            GetComponent<Player>().goOnCooldown();
            GetComponent<Animator>().Rebind();
        } 
    }


}
