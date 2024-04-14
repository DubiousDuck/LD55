using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour
{
    public static Vector3 pos(GameObject obj) //so i don't have to type this every time
    {
        return obj.transform.position;
    }
}
