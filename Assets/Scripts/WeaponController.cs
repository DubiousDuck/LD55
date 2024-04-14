using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject weapon;
    public bool canAttack = true;
    public float attackCoolDown = 2.0f;
    
    void Update(){
        if (Input.GetAxis("Fire1") > 0){
            if (canAttack){
                attack();
            }
        }
    }

    void attack(){
        canAttack = false;
        Animator anim = weapon.GetComponent<Animator>();
        anim.SetTrigger("Attack");
        StartCoroutine(ResetAttackCooldown());
    }

    IEnumerator ResetAttackCooldown(){
        yield return new WaitForSeconds(attackCoolDown);
        canAttack = true;
    }
}
