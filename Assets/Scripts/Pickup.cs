using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private SkillManager skillManager;
    public GameObject skillIcon;
    public SkillInfo skillInfo;

    private void Start(){
        skillManager = GameObject.Find("SkillManager").GetComponent<SkillManager>();
    }

    private void OnTriggerEnter2D(Collider2D collider){
        Debug.Log("i got picked up");
        if(collider.gameObject.tag == "Player"){
            
            for(int i = 0; i< skillManager.slots.Length; i++){
                if(skillManager.isFull[i] == false){
                    //add skill to the inventory
                    Instantiate(skillIcon, skillManager.slots[i].transform, false);
                    Destroy(this.gameObject);
                    skillManager.isFull[i] = true;
                    break;
                }
            }
        }
    }
}
