using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static int SKILL_COUNT = 5;
    public bool[] isFull;
    public GameObject[] slots;
    [SerializeField] List<SkillInfo> skillsObtained;
    bool[] activated = new bool[SKILL_COUNT];
    public float totalManaCost;
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
            set_box_state(0);
        }
        if (Input.GetKeyDown(KeyCode.Q)){
            set_box_state(0, "special");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)){
            set_box_state(1);
        }
        if (Input.GetKeyDown(KeyCode.W)){
            set_box_state(1, "special");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)){
            set_box_state(2);
        }
        if (Input.GetKeyDown(KeyCode.E)){
            set_box_state(2, "special");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)){
            set_box_state(3);
        }
        if (Input.GetKeyDown(KeyCode.R)){
            set_box_state(3, "special");
        }
        if (Input.GetKeyDown(KeyCode.Alpha5)){
            set_box_state(4);
        }
        if (Input.GetKeyDown(KeyCode.T)){
            set_box_state(4, "special");
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
    public void SetState(int index, bool state){
        activated[index] = state;
    }

    float updateManaCost(){
        float total = 0;
        for(int i = 0; i < skillsObtained.Count; i++){
            float boolToInt = activated[i]? 1:0;
            total += skillsObtained[i].manaCost * boolToInt;
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
            if (box.currState == SkillBox.State.NormalActivated){
                    if (ability_type == "normal"){
                        box.deactivate();
                    }else box.activate_special();
                }else if (box.currState == SkillBox.State.SpecialActivated){
                    if (ability_type == "special"){
                        box.deactivate();
                    }else box.activate_normal();
                }else{
                    if (ability_type == "special"){
                        box.activate_special();
                    }else box.activate_normal();
                }
        }
    }
}
