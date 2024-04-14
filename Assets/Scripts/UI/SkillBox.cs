using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBox : MonoBehaviour
{
    public enum State {Empty, Activated, Deactivated};
    public State currState;
    // Start is called before the first frame update
    void Start()
    {
        currState = State.Empty;
    }

    public void deactivate(){
        currState = State.Deactivated;
        UpdateVisual();
    }
    public void activate(){
        currState = State.Activated;
        UpdateVisual();
    }
    void UpdateVisual(){
        if (currState == State.Activated){
            this.GetComponent<Image>().color = Color.yellow;
            Debug.Log(this.name + " is activated");

        }else if (currState == State.Deactivated){
            this.GetComponent<Image>().color = new Color32(70, 70, 70, 255);
            Debug.Log(this.name + " is deactivated");
        }else if (currState == State.Empty){
            this.GetComponent<Image>().color = Color.white;
            Debug.Log(this.name + " is empty");
        }
    }
}
