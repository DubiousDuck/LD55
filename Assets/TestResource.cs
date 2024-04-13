using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestResource : MonoBehaviour
{
    [SerializeField] ManaBar manaBar;
    public float currentHealth = 0, maxHealth = 100;
    public float currentMana = 0, maxMana = 100;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        currentMana = maxMana;
        manaBar.SetManaBar(currentMana);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            currentHealth = currentHealth - 10;
            currentMana = currentMana - 10;
            manaBar.SetManaBar(currentMana);
        }
    }
}
