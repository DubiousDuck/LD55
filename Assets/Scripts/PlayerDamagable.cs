using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageable : MonoBehaviour, Damageable
{
    [SerializeField] TestResource testResource;
    public float health, maxHealth = 100;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        testResource.ResetResource();
    }
    public void regenHealth(float amount){
        health += amount;
        if (health > maxHealth){
            health = maxHealth;
        }
        regenHealthCallBack(health);
    }
    public void takeDamage(float amount, float stunTime, GameObject damager = null){
        health -= amount;
        takeDamageCallBack(health);
    }
    public void takeDamageCallBack(float newHealth){
        testResource.updateHealth(newHealth);
        Debug.Log("i'm hurt");
    }

    public void regenHealthCallBack(float newHealth){
        testResource.updateHealth(newHealth);
        Debug.Log("i'm healed");
    }
}
