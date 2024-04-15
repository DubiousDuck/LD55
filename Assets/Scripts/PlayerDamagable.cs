using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageable : MonoBehaviour, Damageable
{
    [SerializeField] TestResource testResource;
    public float health, maxHealth = 100;
    // Start is called before the first frame update
    public void Start()
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
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        StartCoroutine(this.gameObject.GetComponent<PlayerController>().stun(stunTime));
        takeDamageCallBack(health);
    }
    public void takeDamageCallBack(float newHealth){
        StartCoroutine(changeColorBack());
        testResource.updateHealth(newHealth);
        Debug.Log("i'm hurt");
    }

    public void regenHealthCallBack(float newHealth){
        testResource.updateHealth(newHealth);
        Debug.Log("i'm healed");
    }

    private IEnumerator changeColorBack(){
        yield return new WaitForSeconds(0.5f);
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
