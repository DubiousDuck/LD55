using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject owner;
    public WeaponController weaponController;
    private float weaponDamage = 5f;
    private float stunDuration = 0.5f;

    private void OnTriggerEnter2D(Collider2D collider){
        if (collider.tag == "Enemies" && weaponController.isAttacking){
            collider.gameObject.GetComponent<EnemyAI>().takeDamage(weaponDamage, stunDuration, owner);
            Debug.Log("weapon hit enemy");
        }
    }
}
