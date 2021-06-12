using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeMagic : MagicObject
{
    public override bool CheckMove(Vector2 Input)
    {
        if (Input.x >= MovementThreshold && !Moved)
        {
            RaycastHit2D Hit = Physics2D.Raycast(gameObject.transform.position, gameObject.transform.right, GameInstance.Instance.TileSize, BlockMovementLayer);
            if (Hit.collider == null)
            {
                return true;
            }
            else if(Hit.collider.gameObject.tag == "Destroyable")
            {
                Hit.collider.gameObject.GetComponent<Obstacle>().Destory();
            }
        }
        else if (Input.x <= -MovementThreshold && !Moved)
        {
            RaycastHit2D Hit = Physics2D.Raycast(gameObject.transform.position, -gameObject.transform.right, GameInstance.Instance.TileSize, BlockMovementLayer);
            if (Hit.collider == null)
            {
                //Move Left
                return true;
            }
            else if (Hit.collider.gameObject.tag == "Destroyable")
            {
                Hit.collider.gameObject.GetComponent<Obstacle>().Destory();
            }
        }
        else if (Input.y >= MovementThreshold && !Moved)
        {
            RaycastHit2D Hit = Physics2D.Raycast(gameObject.transform.position, gameObject.transform.up, GameInstance.Instance.TileSize, BlockMovementLayer);
            if (Hit.collider == null)
            {
                //Move Up
                return true;
            }
            else if (Hit.collider.gameObject.tag == "Destroyable")
            {
                Hit.collider.gameObject.GetComponent<Obstacle>().Destory();
            }
        }
        else if (Input.y <= -MovementThreshold && !Moved)
        {
            RaycastHit2D Hit = Physics2D.Raycast(gameObject.transform.position, -gameObject.transform.up, GameInstance.Instance.TileSize, BlockMovementLayer);
            if (Hit.collider == null)
            {
                //Move Down
                return true;
            }
            else if (Hit.collider.gameObject.tag == "Destroyable")
            {
                Hit.collider.gameObject.GetComponent<Obstacle>().Destory();
            }
        }
        return false;
    }
}
