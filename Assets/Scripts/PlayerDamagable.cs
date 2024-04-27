using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDamageable : MonoBehaviour, Damageable
{
    [SerializeField] TestResource testResource;
    public GameObject agro;
    public float detectionRange = 5f;
    public float maxFollowRange = 10f;
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
        this.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
        Debug.Log(this.GetComponent<SpriteRenderer>().color + "");
        StartCoroutine(this.gameObject.GetComponent<PlayerController>().stun(stunTime));
        takeDamageCallBack(health);
        if(damager)
            agro = damager;
    }
    public void takeDamageCallBack(float newHealth){
        StartCoroutine(changeColorBack());
        testResource.updateHealth(newHealth);
        Debug.Log("i'm hurt");
        if (newHealth <= 0){
            SceneManager.LoadScene(4);
        }
    }

    public void regenHealthCallBack(float newHealth){
        testResource.updateHealth(newHealth);
        Debug.Log("i'm healed");
    }

    private IEnumerator changeColorBack(){
        yield return new WaitForSeconds(0.5f);
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public GameObject getAgro()
    {
        if (agro)
            return agro;
        else
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(this.transform.position, detectionRange, 1 << LayerMask.NameToLayer("Enemies"));
            if (colliders.Length == 0)
                return this.gameObject;
            else
                return colliders[0].gameObject;
        }
    }
}
