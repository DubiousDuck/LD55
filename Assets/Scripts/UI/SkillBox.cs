using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBox : MonoBehaviour
{
    public enum State {Empty, NormalActivated, SpecialActivated, Deactivated};
    public State currState;
    private SkillManager skillManager;
    public SkillInfo skillInfo;
    public int index;
    // Start is called before the first frame update
    void Start()
    {
        currState = State.Empty;
        skillManager = GameObject.FindObjectOfType<SkillManager>();
    }

    void Update(){
        
    }

    public void emptyfy(){
        currState = State.Empty;
        UpdateVisual();
    }
    public void deactivate(){
        currState = State.Deactivated;
        UpdateVisual();
    }
    public void activate_normal(){
        currState = State.NormalActivated;
        UpdateVisual();
    }
    public void activate_special(){
        currState = State.SpecialActivated;
        UpdateVisual();
    }
    void UpdateVisual(){
        if (currState == State.NormalActivated){
            this.GetComponent<Image>().color = Color.yellow;
            Debug.Log(this.name + " is normal activated");

        }else if (currState == State.SpecialActivated){
            this.GetComponent<Image>().color = Color.blue;
            Debug.Log(this.name + " is special activated");
        }else if (currState == State.Deactivated){
            this.GetComponent<Image>().color = new Color32(70, 70, 70, 255);
            Debug.Log(this.name + " is deactivated");
        }else if (currState == State.Empty){
            this.GetComponent<Image>().color = Color.white;
            Debug.Log(this.name + " is empty");
        }
    }

    public void dropSkill(){
        foreach (Transform child in transform){
            if(child.name != "text" && child.gameObject.name != "Button"){
                GameObject.Destroy(child.gameObject);
            }
        }
        emptyfy();
        skillManager.isFull[index] = false;
    }
}
