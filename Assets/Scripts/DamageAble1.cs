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



    

    public virtual void regenHealthCallBack(float health){}
    public virtual void takeDamageCallBack(float health){}


}
