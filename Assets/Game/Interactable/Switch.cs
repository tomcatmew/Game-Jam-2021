using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] private GameInstance.MagicColor RequiredColor = GameInstance.MagicColor.RED;
    [SerializeField] private List<Door> ControlledDoors;

    [SerializeField] private Sprite RedSwitchSprite;
    [SerializeField] private Sprite BlueSwitchSprite;
    [SerializeField] private Sprite YellowSwitchSprite;

    public bool IsActive = false;
     
    // Start is called before the first frame update
    void Start()
    {
        if(ControlledDoors.Count == 0)
        {
            Debug.Log("Warnning! Switch Not Connected to Any Doors");
        }

        switch (RequiredColor)
        {
            case GameInstance.MagicColor.RED:
                gameObject.GetComponent<SpriteRenderer>().sprite = RedSwitchSprite;
                break;
            case GameInstance.MagicColor.BLUE:
                gameObject.GetComponent<SpriteRenderer>().sprite = BlueSwitchSprite;
                break;
            case GameInstance.MagicColor.YELLOW:
                gameObject.GetComponent<SpriteRenderer>().sprite = YellowSwitchSprite;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.2f);
        foreach (Collider2D coll in colliders)
        {
            if (coll.gameObject.tag == "MagicObject")
            {
                if (coll.gameObject.GetComponent<MagicObject>().DefaultColor == RequiredColor)
                {
                    IsActive = true;
                }
                else
                {
                    IsActive = false;
                }
            }
            else
            {
                IsActive = false;
            }
        }

        if(colliders.Length == 0)
        {
            IsActive = false;
        }

        foreach (Door door in ControlledDoors)
        {
            door.TryActive();
        }
    }
}
