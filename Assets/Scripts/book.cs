using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    [SerializeField] float pageSpeed = 0.5f;
    [SerializeField] List<Transform> pages;
    int index = -1;
    bool rotate = false;
    public GameObject buttonRight;
    public GameObject buttonLeft;

    // Start is called before the first frame update
    void Start()
    {
        InitialState();
    }

    public void InitialState()
    {
        gameObject.SetActive(false);
        for (int i = 0; i < pages.Count; i++)
        {
            pages[i].transform.rotation = Quaternion.identity;
        }
        pages[0].SetAsLastSibling();
        buttonLeft.SetActive(false);
        buttonRight.SetActive(false); // Hide the right button initially
    }

    public void ShowBook()
    {
        // Toggle the visibility of the book
        gameObject.SetActive(!gameObject.activeSelf);

        if (gameObject.activeSelf)
        {
            index = 0; // Reset index when showing the book
            ForwardButtonActions(); // Update button visibility
buttonLeft.SetActive(false);
        }
        else
        {
            buttonLeft.SetActive(false); // Hide left button when hiding the book
            buttonRight.SetActive(false); // Hide right button when hiding the book
        }
    }

    public void RotateForward()
    {
        if (rotate == true || index >= pages.Count -1) { return; }
        index++;
        float angle = 180;
        ForwardButtonActions();
        pages[index].SetAsLastSibling();
        StartCoroutine(Rotate(angle, true));
    }


    public void ForwardButtonActions()
{
    

if (buttonLeft.activeInHierarchy == false)
        {
            buttonLeft.SetActive(true); //every time we turn the page forward, the back button should be activated
        }
        if (index == pages.Count - 1)
        {
            buttonRight.SetActive(false); //if the page is last then we turn off the forward button
        }
        else
        {
            buttonRight.SetActive(true);
        }
}
    

    public void RotateBack()
    {
        if (rotate == true || index <= 0) { return; }
        
        float angle = 0;
        
        pages[index].SetAsLastSibling();
        BackButtonActions();
        StartCoroutine(Rotate(angle, false));
    }

    public void BackButtonActions()
    {
    

if (buttonRight.activeInHierarchy == false)
        {
            buttonRight.SetActive(true); //every time we turn the page back, the forward button should be activated
        }
        if (index -1 == -1)
        {
            buttonLeft.SetActive(false); //if the page is first then we turn off the back button
        }
    }

    IEnumerator Rotate(float angle, bool forward)
    {
        float value = 0f;
        while (true)
        {
            rotate = true;
            Quaternion targetRotation = Quaternion.Euler(0, angle, 0);
            value += Time.deltaTime * pageSpeed;
            pages[index].rotation = Quaternion.Slerp(pages[index].rotation, targetRotation, value);
            float angle1 = Quaternion.Angle(pages[index].rotation, targetRotation);
            if (angle1 < 0.1f)
            {
                if (forward == false)
                {
                    index--;
                }
                rotate = false;
                break;
            }
            yield return null;
        }
    }
}
