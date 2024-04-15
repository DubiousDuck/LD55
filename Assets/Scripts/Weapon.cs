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
            collider.gameObject.GetComponent<Damageable>().takeDamage(weaponDamage, stunDuration, owner);
            if(poisonActive){
                Instantiate(projectile, collider.transform.position, this.transform.rotation);
            }
            Debug.Log("weapon hit enemy");
        }
    }
}
