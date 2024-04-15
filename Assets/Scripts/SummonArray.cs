using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonArray : MonoBehaviour
{
    public List<GameObject> summoned;

    public void AddSummon(GameObject newSummon){
        summoned.Add(newSummon);
    }

    public void RemoveSummon(GameObject toBeRemoved){
        summoned.Remove(toBeRemoved);
    }
}
