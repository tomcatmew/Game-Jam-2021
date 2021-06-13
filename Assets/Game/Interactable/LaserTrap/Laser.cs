using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float LaserDistance = 100f;
    [SerializeField] private LayerMask BlockLaserLayer;
    [SerializeField] private LayerMask PlayerLayer;

    public GameObject laserPoint;
    public GameObject laserStart;
    public GameObject laserStraight;
    public GameObject laserEnd;
    public GameObject laserTail;
    public Direction direction;

    private List<GameObject> laserList;

    private GameObject prevHit;

    private bool isDraw = false;

    public enum Direction
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }

    private void Awake()
    {
        laserList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        ShootLaser();
    }

    void ShootLaser()
    {
        RaycastHit2D Hit = Physics2D.Raycast(transform.position, GetLaserDerection(), Mathf.Infinity, BlockLaserLayer + PlayerLayer);
        if (Hit.collider != null)
        {
            if (!Hit.collider.gameObject.Equals(prevHit))
            {
                removeLaser();
                DrawLaser(transform.position, Hit.point);
                prevHit = Hit.collider.gameObject;
                isDraw = false;
            }
            if (Hit.collider.gameObject.tag == "Player")
            {
                Hit.collider.gameObject.GetComponent<PlayerCharacter>().Kill();
            }
            else if (Hit.collider.gameObject.tag == "MagicObject")
            {
                if (Hit.collider.gameObject.GetComponent<MagicObject>().DefaultColor != GameInstance.MagicColor.PURPLE)
                {
                    Hit.collider.gameObject.GetComponent<MagicObject>().Kill();
                    GameInstance.Instance.MyGameMode.GameOver();
                }
            }
        }
        else
        {
            if (!isDraw)
            {
                DrawLaser(transform.position, transform.position + transform.position.normalized * LaserDistance);
                isDraw = true;
            }
        }

    }

    private void removeLaser()
    {
        for (int i = 0; i < laserList.Count; i++)
        {
            Destroy(laserList[i]);
        }

        laserList.Clear();
    }

    private void DrawLaser(Vector2 StartPos, Vector2 EndPos)
    {
        float length = (EndPos - StartPos).magnitude / GameInstance.Instance.TileSize;
        Quaternion quaternion = GetLaserQuaternion(direction);

        if ((int) length <= 1)
        {
            for (int i = 0; i < length; i++)
            {
                Vector2 TmpPos = StartPos + (EndPos - StartPos).normalized * i * GameInstance.Instance.TileSize;
                Vector3 InitialPos = new Vector3(TmpPos.x, TmpPos.y, 0f);
                if (i == 0)
                {
                    GameObject Laser = Instantiate(laserPoint, InitialPos, quaternion);
                    laserList.Add(Laser);
                }
                else
                {
                    GameObject Laser = Instantiate(laserTail, InitialPos, quaternion);
                    laserList.Add(Laser);
                }
            }
        }
        else
        {
            for (int i = 0; i < length; i++)
            {
                Vector2 TmpPos = StartPos + (EndPos - StartPos).normalized * i * GameInstance.Instance.TileSize;
                Vector3 InitialPos = new Vector3(TmpPos.x, TmpPos.y, 0f);
                if (i == 0)
                {
                    Debug.Log("draw 0");
                    GameObject Laser = Instantiate(laserPoint, InitialPos, quaternion);
                    laserList.Add(Laser);
                }
                else if (i == 1)
                {
                    Debug.Log("draw 1");
                    GameObject Laser = Instantiate(laserStart, InitialPos, quaternion);
                    laserList.Add(Laser);
                }
                else if (i + 1 > length)
                {
                    GameObject Laser = Instantiate(laserEnd, InitialPos, quaternion);
                    laserList.Add(Laser);
                }
                else
                {
                    GameObject Laser = Instantiate(laserStraight, InitialPos, quaternion);
                    laserList.Add(Laser);
                }
            }
        }


    }

    private Vector2 GetLaserDerection()
    {
        if (direction.Equals(Direction.LEFT))
        {
            return -gameObject.transform.right;
        }
        else if (direction.Equals(Direction.UP))
        {
            return gameObject.transform.up;
        }
        else if (direction.Equals(Direction.RIGHT))
        {
            return gameObject.transform.right;
        }
        else
        {
            return -gameObject.transform.up;
        }
    }

    private Quaternion GetLaserQuaternion(Direction direction)
    {
        if (direction.Equals(Direction.LEFT))
        {
            return new Quaternion();
        }
        else if (direction.Equals(Direction.UP))
        {
            return Quaternion.Euler(new Vector3(0f, 0f, -90f));
        }
        else if (direction.Equals(Direction.RIGHT))
        {
            return Quaternion.Euler(new Vector3(0f, 0f, -180f));
        }
        else
        {
            return Quaternion.Euler(new Vector3(0f, 0f, 90f));
        }
    }

    public void Kill()
    {
        gameObject.SetActive(false);
        removeLaser();
        Destroy(gameObject, 0.1f);
    }
}
