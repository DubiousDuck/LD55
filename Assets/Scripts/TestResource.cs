using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestResource : MonoBehaviour
{
    [SerializeField] ManaBar manaBar;
    [SerializeField] HealthBar healthBar;
    [SerializeField] SkillManager skillManager;
    [SerializeField] SkillBarManager skillBarManager;
    [SerializeField] PlayerDamageable playerDamageable;
    public float currentHealth = 0, maxHealth = 100;
    public float currentMana = 0, maxMana = 100;

    float manaCostPerSec = 0;
    float manaRecoverPerSec = 10;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = playerDamageable.maxHealth;
        InvokeRepeating("manaUpdateCaller", 2.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        manaCostPerSec = skillManager.totalManaCost;
        manaBar.SetManaBar(currentMana);
        healthBar.SetHealthBar(currentHealth);
    }

    public void ResetResource(){
        currentHealth = maxHealth;
        currentMana = maxMana;
        manaBar.SetManaBar(currentMana);
        healthBar.SetHealthBar(currentHealth);
    }

    void manaUpdateCaller(){
        if (manaCostPerSec == 0){
            manaRecover(manaRecoverPerSec);
        }else{
            manaDrain(manaCostPerSec);
        }
    }

    void manaDrain(float amount){
        currentMana = currentMana - amount;
        if (currentMana < 0){
            currentMana = 0;
            skillManager.DeactivateAll();
            skillBarManager.reset_box_state();
        }
    }

    void manaRecover(float amount){
        currentMana = currentMana + amount;
        if (currentMana > maxMana){
            currentMana = maxMana;
        }
    }

    public void updateHealth(float newHealth){
        currentHealth = newHealth;
        healthBar.SetHealthBar(currentHealth);
    }
}
