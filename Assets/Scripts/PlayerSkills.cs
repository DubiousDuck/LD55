using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    public GameObject batPrefab, spiderPrefab, vulturePrefab, wormPrefab, dwarfPrefab, slimePrefab, ghostPrefab;
    public GameObject player;
    public SummonArray summonArray;
    void Start(){
        player = GameObject.Find("Player");
    }
    public void ActivateSkill(string skillName){
        switch(skillName){
            case "Bat":
                player.GetComponent<PlayerController>().jumpVar = 2;
                break;
            case "Spider":
                player.GetComponentInChildren<Weapon>().poisonActive = true;
                break;
        }
    }

    public void ActivateSummon(string summonName){
        //instantiate each summons to an array
        GameObject toBeSummoned = null;
        switch(summonName){
            case "Bat":
                toBeSummoned = Instantiate(batPrefab, player.transform.position, player.transform.rotation, player.transform);
                break;
            case "Spider":
                toBeSummoned = Instantiate(spiderPrefab, player.transform.position, player.transform.rotation, player.transform);
                break;
            case "Vulture":
                toBeSummoned = Instantiate(vulturePrefab, player.transform.position, player.transform.rotation, player.transform);
                break;
            case "Worm":
                toBeSummoned = Instantiate(wormPrefab, player.transform.position, player.transform.rotation, player.transform);
                break;
            case "Dwarf":
                toBeSummoned = Instantiate(dwarfPrefab, player.transform.position, player.transform.rotation, player.transform);
                break;
            case "Slime":
                toBeSummoned = Instantiate(slimePrefab, player.transform.position, player.transform.rotation, player.transform);
                break;
            case "Ghost":
                toBeSummoned = Instantiate(ghostPrefab, player.transform.position, player.transform.rotation, player.transform);
                break;

        }
        toBeSummoned.transform.parent = null;
        toBeSummoned.tag = "Allies";
        toBeSummoned.gameObject.layer = 7;
        summonArray.AddSummon(toBeSummoned);
    }

    public void DeactivateSkill(string skillName){
        switch(skillName){
            case "Bat":
                player.GetComponent<PlayerController>().jumpVar = 1;
                break;
            case "Spider":
                player.GetComponentInChildren<Weapon>().poisonActive = false;
                break;
        }
    }

    public void DeactivateSummon(string summonName){
        GameObject toBeDestroyed = null;
        switch(summonName){
            case "Bat":
                toBeDestroyed = GameObject.Find("Bat(Clone)");
                break;
            case "Spider":
                toBeDestroyed = GameObject.Find("Spider(Clone)");
                break;
            case "Vulture":
                toBeDestroyed = GameObject.Find("Vulture(Clone)");
                break;
            case "Worm":
                toBeDestroyed = GameObject.Find("Worm(Clone)");
                break;
            case "Dwarf":
                toBeDestroyed = GameObject.Find("Dwarf(Clone)");
                break;
            case "Slime":
                toBeDestroyed = GameObject.Find("Slime(Clone)");
                break;
            case "Ghost":
                toBeDestroyed = GameObject.Find("Ghost(Clone)");
                break;
        }
        summonArray.RemoveSummon(toBeDestroyed);
        Destroy(toBeDestroyed);
    }
}
