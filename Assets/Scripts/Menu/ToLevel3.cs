using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToLevel3 : MonoBehaviour
{
    public Scene nextScene;
    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.name == "Player"){
            SceneManager.LoadScene("Level3");
        }
    }
}
