using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPickup : MonoBehaviour
{
    void OnTriggerEnter2d(Collider2D other){
        Debug.Log("I am touched");
    }
}
