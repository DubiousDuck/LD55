using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject weapon;
    public bool canAttack = true;
    public float attackCoolDown = 2.0f;
    public bool isAttacking = false;
    
    void Update(){
        if (Input.GetAxis("Fire1") > 0){
            if (canAttack){
                attack();
            }
        }
    }

    void attack(){
        canAttack = false;
        isAttacking = true;
        Animator anim = weapon.GetComponent<Animator>();
        anim.SetTrigger("Attack");
        StartCoroutine(ResetAttackCooldown());
    }

    IEnumerator ResetAttackCooldown(){
        StartCoroutine(ResetIsAttacking());
        yield return new WaitForSeconds(attackCoolDown);
        canAttack = true;
    }

    IEnumerator ResetIsAttacking(){
        yield return new WaitForSeconds(attackCoolDown);
        isAttacking = false;
    }
}
