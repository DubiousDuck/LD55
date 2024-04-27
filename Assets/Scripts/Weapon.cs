using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject owner;
    public WeaponController weaponController;
    private float weaponDamage = 5f;
    private float stunDuration = 0.5f;
    public GameObject projectile;
    public bool poisonActive = false;

    private void OnTriggerEnter2D(Collider2D collider){
        if (collider.tag == "Enemies" && weaponController.isAttacking){
            Damageable target = collider.gameObject.GetComponent<Damageable>();
            if(target != null){
                target.takeDamage(weaponDamage, stunDuration, owner);
                owner.GetComponent<PlayerDamageable>().agro = collider.gameObject;
            }
        }
    }
}
