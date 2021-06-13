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

    private void Update()
    {
        if (!IsAlive)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.2f);
            foreach (Collider2D coll in colliders)
            {
                if (coll.gameObject.tag == "MagicObject")
                {
                    if (coll.gameObject.GetComponent<MagicObject>().DefaultColor == GameInstance.MagicColor.GREEN)
                    {
                        Flourish();
                    }
                    else
                    {
                        //Explode Effect
                        coll.gameObject.GetComponent<MagicObject>().Kill();
                        GameInstance.Instance.MyGameMode.GameOver();
                    }
                }
                else if (coll.gameObject.tag == "Player")
                {
                    coll.gameObject.GetComponent<PlayerCharacter>().Kill();
                }
            }
        }
    }

    public void Flourish()
    {
        if (!IsAlive)
        {
            //Player Effect
            AliveSprite.GetComponent<Animator>().Play("DeathTileFlourish");
            DeathSprite.SetActive(false);
            AliveSprite.SetActive(true);
            IsAlive = true;
        }
    }

    /*
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
    */
}
