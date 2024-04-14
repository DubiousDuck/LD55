using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 modifier = new Vector3(0, 0, -10);
    public float followSpeedFraction = 0.5f; //1 is max

    public void Update()
    {
        Vector3 diff = target.position + modifier - this.transform.position;
        this.transform.position += diff * followSpeedFraction;
    }

    public void modify(Vector3 modify)
    {
        modifier = modify;
    }
}
