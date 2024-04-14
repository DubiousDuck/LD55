using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class HealthBarScript : MonoBehaviour
{
    public Slider slider; 

    public void SetMaxHealth (int health)
    {
        slider.maxValue = health;
        slider.value = health;  
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }
}

//public int maxHealth = 10; 
//public int currentHealth; 
//void Start() 
//{currentHealth=MaxHealth}; 