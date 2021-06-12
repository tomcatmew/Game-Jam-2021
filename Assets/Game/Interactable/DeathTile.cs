using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTile : MonoBehaviour
{
    [SerializeField] private GameObject DeathSprite;
    [SerializeField] private GameObject AliveSprite;

    [SerializeField] private bool IsAlive = false;

    private void Start()
    {
        if (IsAlive)
        {
            DeathSprite.SetActive(false);
            AliveSprite.SetActive(true);

        }
        else
        {
            AliveSprite.SetActive(false);
            DeathSprite.SetActive(true);
        }
    }

    void Flourish()
    {
        if (!IsAlive)
        {
            //Player Effect
            DeathSprite.SetActive(false);
            AliveSprite.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log(col.attachedRigidbody.gameObject.tag);
        if (!IsAlive)
        {
            if (col.attachedRigidbody.gameObject.tag == "Player")
            {
                //Player Effect Here
                col.attachedRigidbody.gameObject.GetComponent<PlayerCharacter>().Kill();
            }
        }
    }

}
