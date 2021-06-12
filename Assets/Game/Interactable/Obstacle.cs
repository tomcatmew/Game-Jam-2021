using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private bool IsDestoryed = false;
    [SerializeField] private GameObject OpenSprite;
    [SerializeField] private GameObject CloseSprite;

    // Start is called before the first frame update
    void Start()
    {
        if (IsDestoryed)
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

    public void Destory()
    {
        //Play Effect
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        CloseSprite.SetActive(false);
        OpenSprite.SetActive(true);
    }
}
