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
                player.GetComponent<PlayerController>().maxJumps = 2;
                break;
            case "Spider":
                player.GetComponentInChildren<Weapon>().poisonActive = true;
                break;
        }
    }

    public void ActivateSummon(string summonName){
        //instantiate each summons to an array
        GameObject toBeSummoned = null;
        Debug.Log(player.transform.position);
        switch(summonName){
            case "Bat":
                toBeSummoned = Instantiate(batPrefab, player.transform.position, player.transform.rotation);
                break;
            case "Spider":
                toBeSummoned = Instantiate(spiderPrefab, player.transform.position, player.transform.rotation);
                break;
            case "Vulture":
                toBeSummoned = Instantiate(vulturePrefab, player.transform.position, player.transform.rotation);
                break;
            case "Worm":
                toBeSummoned = Instantiate(wormPrefab, player.transform.position, player.transform.rotation);
                break;
            case "Dwarf":
                toBeSummoned = Instantiate(dwarfPrefab, player.transform.position, player.transform.rotation);
                break;
            case "Slime":
                toBeSummoned = Instantiate(slimePrefab, player.transform.position, player.transform.rotation);
                break;
            case "Ghost":
                toBeSummoned = Instantiate(ghostPrefab, player.transform.position, player.transform.rotation);
                break;

        }
        toBeSummoned.tag = "Allies";
        toBeSummoned.gameObject.layer = LayerMask.NameToLayer("Allies");
        summonArray.AddSummon(toBeSummoned);
        toBeSummoned.transform.position = player.transform.position;
    }

    public void DeactivateSkill(string skillName){
        switch(skillName){
            case "Bat":
                player.GetComponent<PlayerController>().maxJumps = 1;
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
