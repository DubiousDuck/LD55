using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    public void ActivateSkill(string skillName){
        switch(skillName){
            case "Bat":
                GameObject.Find("Player").GetComponent<PlayerController>().jumpVar = 2;
                break;
        }
    }

    public void ActivateSummon(string summonName){
        //instantiate each summons to an array
    }

    public void DeactivateSkill(string skillName){
        switch(skillName){
            case "Bat":
                GameObject.Find("Player").GetComponent<PlayerController>().jumpVar = 1;
                break;
        }
    }

    public void DeactivateSummon(string skillName){
        //Destroy summon and remove from list
    }
}
