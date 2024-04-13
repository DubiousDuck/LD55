using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBox : MonoBehaviour
{
    enum State {Activated, Deactivated};
    State myState;
    // Start is called before the first frame update
    void Start()
    {
        myState = State.Deactivated;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void deactivate(){
        myState = State.Deactivated;
        UpdateVisual();
    }
    public void activate(){
        myState = State.Activated;
        UpdateVisual();
    }
    void UpdateVisual(){
        if (myState == State.Activated){
            this.GetComponent<Image>().color = Color.yellow;
            Debug.Log(this.name + " is activated");

        }else if (myState == State.Deactivated){
            this.GetComponent<Image>().color = new Color32(70, 70, 70, 255);
            Debug.Log(this.name + " is deactivated");
        }
    }
}
