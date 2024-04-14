using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAble1 : MonoBehaviour
{
    [SerializeField] public float health, maxHealth = 100;
    private float minHealth = 0;

    void Start()
    {
        health = maxHealth;
    }

    public void regenHealth(float amount){
        health += amount;
        if (health > maxHealth){
            health = maxHealth;
        }
        regenHealthCallBack(health);
    }

    public void takeDamage(float amount){
        health -= amount;
        if (health < minHealth){
            Debug.Log(this.name + " is dead");
        }
        takeDamageCallBack(health);
    }

    public virtual void regenHealthCallBack(float health){}
    public virtual void takeDamageCallBack(float health){}


}
