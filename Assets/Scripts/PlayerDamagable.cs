using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageable : DamageAble1
{
    [SerializeField] TestResource testResource;
    // Start is called before the first frame update
    void Start()
    {
     testResource.ResetResource();   
    }

    void takeDamageCallBack(float newHealth){
        updateResource(newHealth);
    }

    void updateResource(float newHealth){
        testResource.updateHealth(newHealth);
    }
}
