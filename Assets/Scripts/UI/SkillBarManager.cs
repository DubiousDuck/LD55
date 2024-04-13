using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBarManager : MonoBehaviour
{
    [SerializeField] List<SkillBox> skillBoxList;
    void Start()
    {
        refresh_hotbar_num();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // void refresh_hotbar_num(){
    //     for (int i = 0; i < skillBoxList.Count; i++){
    //         SkillBox box = skillBoxList[i];
    //         box.transform.Find("text").GetComponent<TMPro.TextMeshProUGUI>().SetText((i + 1).ToString());
    //     }
    // }
    
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
}
