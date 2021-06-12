using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private bool IsOpen = false;
    [SerializeField] private GameObject OpenSprite;
    [SerializeField] private GameObject CloseSprite;

    // Start is called before the first frame update
    void Start()
    {
        if (IsOpen)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            CloseSprite.SetActive(false);
            OpenSprite.SetActive(true);
        }
        else
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            CloseSprite.SetActive(true);
            OpenSprite.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        //Play Effect
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        CloseSprite.SetActive(false);
        OpenSprite.SetActive(true);
    }

    public void Close()
    {
        //Play Effect
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        CloseSprite.SetActive(true);
        OpenSprite.SetActive(false);
    }
}
