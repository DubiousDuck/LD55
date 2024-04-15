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
    public Sprite batSprite, spiderSprite, slimeSprite, vultureSprite, wormSprite, dwarfSprite, ghostSprite;
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
        updateSprite();
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
        Debug.Log("dropping skill");
        foreach (Transform child in transform){
            if(child.name != "text" && child.gameObject.name != "Button" && child.gameObject.name != "Image" && child.gameObject.name != "sprite"){
                GameObject.Destroy(child.gameObject);
            }
            Image sprite = transform.Find("sprite").GetComponent<Image>();
            sprite.sprite = null;
        }
        emptyfy();
        skillManager.isFull[index] = false;
    }
    void updateSprite(){
        SkillInfo childInfo = transform.GetComponentInChildren<SkillInfo>();
        if(childInfo != null){
            Image sprite = transform.Find("sprite").GetComponent<Image>();
            Sprite spriteToBe = null;
            switch(childInfo.skillName){
                case "Bat":
                    spriteToBe = batSprite;
                    break;
                case "Spider":
                    spriteToBe = spiderSprite;
                    break;
                case "Slime":
                    spriteToBe = slimeSprite;
                    break;
                case "Dwarf":
                    spriteToBe = dwarfSprite;
                    break;
                case "Worm":
                    spriteToBe = wormSprite;
                    break;
                case "Ghost":
                    spriteToBe = ghostSprite;
                    break;
                case "Vulture":
                    spriteToBe = vultureSprite;
                    break;
            }
            sprite.sprite = spriteToBe;
        }
    }
}
