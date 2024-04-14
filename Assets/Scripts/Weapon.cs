using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponController weaponController;

    private void OnTriggerEnter2D(Collider2D collider){
        if (collider.tag == "Enemies"){
            
        }
    }
}
