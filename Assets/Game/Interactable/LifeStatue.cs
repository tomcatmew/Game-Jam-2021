using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeStatue : MonoBehaviour
{
    private bool IsUsed = false;
    [SerializeField] private GameObject AliveSprite;
    [SerializeField] private GameObject UsedSprite;

    private void Start()
    {
        AliveSprite.SetActive(true);
        UsedSprite.SetActive(false);
    }

    private void Update()
    {
        if (!IsUsed)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.2f);
            foreach (Collider2D coll in colliders)
            {
                if (coll.gameObject.tag == "MagicObject")
                {
                    if (coll.gameObject.GetComponent<MagicObject>().DefaultColor == GameInstance.MagicColor.GREEN)
                    {
                        Cleansing();
                        AliveSprite.SetActive(false);
                        UsedSprite.SetActive(true);
                        IsUsed = true;
                    }

                }
            }
        }
    }

    void Cleansing()
    {
        //play effect
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, GameInstance.Instance.TileSize * 2f);
        foreach (Collider2D coll in colliders)
        {
            if (coll.gameObject.tag == "Dispealable")
            {
                if (coll.gameObject.GetComponent<Laser>())
                {
                    coll.gameObject.GetComponent<Laser>().Kill();
                }
                else if(coll.gameObject.GetComponent<DeathTile>())
                {
                    coll.gameObject.GetComponent<DeathTile>().Flourish();
                }
            }
        }
    }
}
