using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static int SKILL_COUNT = 5;
    public bool[] isFull;
    public GameObject[] slots;
    public float totalManaCost;
    public PlayerSkills playerSkills;
    // Start is called before the first frame update
    void Start()
    {
        refresh_hotbar_num();
        reset_slot_states();
    }
    // Update is called once per frame
    void Update()
    {
        totalManaCost = updateManaCost();

        if (Input.GetKeyDown(KeyCode.Alpha1)){
            if(Input.GetKey(KeyCode.LeftShift)){
                set_box_state(0, "special");
            }else set_box_state(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)){
            if(Input.GetKey(KeyCode.LeftShift)){
                set_box_state(1, "special");
            }else set_box_state(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)){
            if(Input.GetKey(KeyCode.LeftShift)){
                set_box_state(2, "special");
            }else set_box_state(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)){
            if(Input.GetKey(KeyCode.LeftShift)){
                set_box_state(3, "special");
            }else set_box_state(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5)){
           if(Input.GetKey(KeyCode.LeftShift)){
                set_box_state(4, "special");
            }else set_box_state(4);
        }
    }

    void refresh_hotbar_num(){
        for (int i = 0; i < slots.Length; i++){
            SkillBox box = slots[i].GetComponent<SkillBox>();
            Transform textTransform = box.transform.Find("text");
            if (textTransform != null){
                TMPro.TextMeshProUGUI textMesh = textTransform.GetComponent<TMPro.TextMeshProUGUI>();
                if (textMesh != null){
                    textMesh.SetText((i + 1).ToString());
                }
                else {
                    Debug.LogWarning("TextMeshProUGUI component not found in " + textTransform.name);
                }
            }
            else {
                Debug.LogWarning("Child object 'text' not found in " + box.name);
            }
        }
    }

    public void reset_slot_states(){
        for (int i = 0; i < slots.Length; i++){
            SkillBox box = slots[i].GetComponent<SkillBox>();
            box.emptyfy();
        }
    }

    public void deactivate_all(){
        for (int i = 0; i < slots.Length; i++){
            SkillBox box = slots[i].GetComponent<SkillBox>();
            SkillInfo info = box.GetComponentInChildren<SkillInfo>();
            box.deactivate();
            playerSkills.DeactivateSkill(info.skillName + "");
            playerSkills.DeactivateSummon(info.skillName + "");
        }
    }

    float updateManaCost(){
        float total = 0;
        for (int i = 0; i < slots.Length; i++){
            SkillBox box = slots[i].GetComponent<SkillBox>();
            SkillBox.State state = box.currState;
            float cost;
            SkillInfo info = box.GetComponentInChildren<SkillInfo>();
            if (state == SkillBox.State.NormalActivated){
                cost = info.passiveManaCost;
            }else if (state == SkillBox.State.SpecialActivated){
                cost = info.summonManaCost;
            }else cost = 0;
            total += cost;
        }
        return total;
    }

    public void updateSlots(){
        for(int i = 0; i< slots.Length; i++){
            if (isFull[i]){
                slots[i].GetComponent<SkillBox>().deactivate();
            }
        }
    }

    void set_box_state(int index, string ability_type = "normal"){
        SkillBox box = slots[index].GetComponent<SkillBox>();
        if(isFull[index]){
            SkillInfo info = box.GetComponentInChildren<SkillInfo>();
            if (box.currState == SkillBox.State.NormalActivated){
                    if (ability_type == "normal"){
                        box.deactivate();
                    }else {
                        box.activate_special();
                        playerSkills.ActivateSummon(info.skillName + "");
                    }
                    playerSkills.DeactivateSkill(info.skillName + "");
                }else if (box.currState == SkillBox.State.SpecialActivated){
                    if (ability_type == "special"){
                        box.deactivate();
                    }else{
                        box.activate_normal();
                        playerSkills.ActivateSkill(info.skillName + "");
                    }
                    playerSkills.DeactivateSummon(info.skillName + "");
                }else{
                    if (ability_type == "special"){
                        box.activate_special();
                        playerSkills.ActivateSummon(info.skillName + "");
                    }else{
                        box.activate_normal();
                        playerSkills.ActivateSkill(info.skillName + "");
                    }
                }
        }
    }
}
