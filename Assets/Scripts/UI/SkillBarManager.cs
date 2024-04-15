using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBarManager : MonoBehaviour
{
    public const int BOX_COUNT = 5;
    [SerializeField] List<SkillBox> skillBoxList;
    [SerializeField] SkillManager skillManager;
    SkillBox.State[] stateList = new SkillBox.State[BOX_COUNT];

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
