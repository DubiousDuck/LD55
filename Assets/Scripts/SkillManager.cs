using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static int SKILL_COUNT = 5;
    [SerializeField] List<SkillInfo> skillList;
    bool[] activated = new bool[SKILL_COUNT];
    public float totalManaCost;
    // Start is called before the first frame update
    void Start()
    {
        DeactivateAll();
    }

    // Update is called once per frame
    void Update()
    {
        totalManaCost = updateManaCost();
    }

    public void DeactivateAll(){
        for(int i = 0; i<activated.Length; i++){
            activated[i] = false;
        }
    }
    public void SetState(int index, bool state){
        activated[index] = state;
    }

    float updateManaCost(){
        float total = 0;
        for(int i = 0; i < skillList.Count; i++){
            float boolToInt = activated[i]? 1:0;
            total += skillList[i].manaCost * boolToInt;
        }
        return total;
    }
}
