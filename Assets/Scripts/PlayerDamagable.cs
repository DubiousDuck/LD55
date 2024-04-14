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

    public override void takeDamageCallBack(float newHealth){
        updateResource(newHealth);
        Debug.Log("i'm hurt");
    }

    void updateResource(float newHealth){
        testResource.updateHealth(newHealth);
    }
}
