using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private bool StartingOpen = false;
    
    [SerializeField] private GameObject OpenSprite;
    [SerializeField] private GameObject CloseSprite;

    [SerializeField] private List<Switch> ControllSwitchs;

    private bool IsOpen;

    // Start is called before the first frame update
    void Start()
    {
        if (ControllSwitchs.Count == 0)
        {
            Debug.Log("Warnning! Door Not Connected to Any Switches");
        }

        if (StartingOpen)
        {
            Open();
        }
        else
        {
            Close();
        }

        IsOpen = StartingOpen;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TryActive()
    {
        bool IsAllActive = true;
        //Play Effect
        foreach (Switch Swi in ControllSwitchs)
        {
            if (!Swi.IsActive)
            {
                IsAllActive = false;
                break;
            }
        }

        if (IsAllActive)
        {
            if (StartingOpen) Close();
            else Open();
        }
        else
        {
            if (StartingOpen) Open();
            else Close();
        }
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
