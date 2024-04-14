using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBarManager : MonoBehaviour
{
    public const int BOX_COUNT = 5;
    [SerializeField] List<SkillBox> skillBoxList;
    [SerializeField] SkillManager skillManager;
    bool[] stateList = new bool[BOX_COUNT];

    void Start()
    {
        refresh_hotbar_num();
        reset_box_state();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)){
            set_box_state(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)){
            set_box_state(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)){
            set_box_state(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)){
            set_box_state(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5)){
            set_box_state(4);
        }
    }
    
    void set_box_state(int index){
        if (stateList[index]){
                skillBoxList[index].deactivate();
                stateList[index] = false;
                skillManager.SetState(index, false);
            }else{
                skillBoxList[index].activate();
                stateList[index] = true;
                skillManager.SetState(index, true);   
            }
    }
    //Thanks ChatGPT
    void refresh_hotbar_num(){
        for (int i = 0; i < skillBoxList.Count; i++){
            SkillBox box = skillBoxList[i];
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

    public void reset_box_state(){
        for (int i = 0; i < stateList.Length; i++){
            stateList[i] = false;
        }
        foreach(SkillBox box in skillBoxList){
            box.deactivate();
        }
    }
}
