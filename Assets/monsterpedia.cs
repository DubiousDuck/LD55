using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBook : MonoBehaviour
{
    public GameObject book;
    public bool isBookOpen = false;

    public void OnButtonClick()
    {
        if (book != null)
        {
            isBookOpen = !isBookOpen;
            book.SetActive(isBookOpen);
        }
        else
        {
            Debug.LogError("book object is not assigned!");
        }
    }
}