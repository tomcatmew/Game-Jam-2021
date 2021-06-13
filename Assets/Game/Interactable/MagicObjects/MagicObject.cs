using System.Collections.Generic;
using UnityEngine;

public class MagicObject : MonoBehaviour
{
    public bool IsGrabed = false;

    [SerializeField] public GameInstance.MagicColor DefaultColor = GameInstance.MagicColor.BLUE;

    [SerializeField] protected LayerMask BlockMovementLayer;

    [SerializeField] protected float MovementThreshold = 0.5f;
    [SerializeField] private float MoveTime = 0.3f;

    private float CurrAnimTime = 0f;

    protected bool Moved = false;
    private bool InMoveAnim = false;
    private Vector3 TargetPos = Vector3.zero;
    private Vector3 StartPos = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Moved)
        {
            if (!InMoveAnim)
            {
                StartPos = gameObject.transform.position;
                CurrAnimTime = 0f;
                InMoveAnim = true;
            }
            else
            {
                float t = Mathf.Clamp01(CurrAnimTime / MoveTime);
                Vector3 CurrPos = Vector3.Lerp(StartPos, TargetPos, t);
                gameObject.transform.position = CurrPos;
                CurrAnimTime += Time.deltaTime;
                if (CurrAnimTime >= MoveTime)
                {
                    gameObject.transform.position = TargetPos;
                    CurrAnimTime = 0f;
                    InMoveAnim = false;
                    Moved = false;
                }
            }
        }
    }
    virtual public bool CheckMove(Vector2 Input)
    {
        if (Input.x >= MovementThreshold && !Moved)
        {
            RaycastHit2D Hit = Physics2D.Raycast(gameObject.transform.position, gameObject.transform.right, GameInstance.Instance.TileSize, BlockMovementLayer);
            if (Hit.collider == null)
            {
                //Move Right
                return true;
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
        }
        else if (Input.y >= MovementThreshold && !Moved)
        {
            RaycastHit2D Hit = Physics2D.Raycast(gameObject.transform.position, gameObject.transform.up, GameInstance.Instance.TileSize, BlockMovementLayer);
            if (Hit.collider == null)
            {
                //Move Up
                return true;
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
        }
        return false;
    }

    public void Move(Vector2 Input)
    {
        if (Input.x >= MovementThreshold && !Moved)
        {
            RaycastHit2D Hit = Physics2D.Raycast(gameObject.transform.position, gameObject.transform.right, GameInstance.Instance.TileSize, BlockMovementLayer);
            if (Hit.collider == null)
            {
                //Move Right
                TargetPos = gameObject.transform.position + new Vector3(GameInstance.Instance.TileSize, 0f, 0f);
                Moved = true;
                InMoveAnim = false;
            }
        }
        else if (Input.x <= -MovementThreshold && !Moved)
        {
            RaycastHit2D Hit = Physics2D.Raycast(gameObject.transform.position, -gameObject.transform.right, GameInstance.Instance.TileSize, BlockMovementLayer);
            if (Hit.collider == null)
            {
                //Move Left
                TargetPos = gameObject.transform.position + new Vector3(-GameInstance.Instance.TileSize, 0f, 0f);
                Moved = true;
                InMoveAnim = false;
            }
        }
        else if (Input.y >= MovementThreshold && !Moved)
        {
            RaycastHit2D Hit = Physics2D.Raycast(gameObject.transform.position, gameObject.transform.up, GameInstance.Instance.TileSize, BlockMovementLayer);
            if (Hit.collider == null)
            {
                //Move Up
                TargetPos = gameObject.transform.position + new Vector3(0f, GameInstance.Instance.TileSize, 0f);
                Moved = true;
                InMoveAnim = false;
            }
        }
        else if (Input.y <= -MovementThreshold && !Moved)
        {
            RaycastHit2D Hit = Physics2D.Raycast(gameObject.transform.position, -gameObject.transform.up, GameInstance.Instance.TileSize, BlockMovementLayer);
            if (Hit.collider == null)
            {
                //Move Down
                TargetPos = gameObject.transform.position + new Vector3(0f, -GameInstance.Instance.TileSize, 0f);
                Moved = true;
                InMoveAnim = false;
            }
        }
    }

    public List<Vector3> CheckNearDecompMagic()
    {
        List<Vector3> list = new List<Vector3>();

        // find x direction
        RaycastHit2D Hit = Physics2D.Raycast(gameObject.transform.position, transform.right, GameInstance.Instance.TileSize, ~gameObject.layer);
        if (Hit.collider == null)
        {
            list.Add(transform.right);

        }

        // find x direction
        Hit = Physics2D.Raycast(gameObject.transform.position, -transform.right, GameInstance.Instance.TileSize, ~gameObject.layer);
        if (Hit.collider == null)
        {
            list.Add(-transform.right);
        }

        // find y direction
        Hit = Physics2D.Raycast(gameObject.transform.position, transform.up, GameInstance.Instance.TileSize, ~gameObject.layer);
        if (Hit.collider == null )
        {
            list.Add(transform.up);
        }

        // find y direction
        Hit = Physics2D.Raycast(gameObject.transform.position, -transform.up, GameInstance.Instance.TileSize, ~gameObject.layer);
        if (Hit.collider == null )
        {
            list.Add(-transform.up);
        }


        return list;
    }

    public List<Vector3> CheckNearMagic()
    {
        List<Vector3> list = new List<Vector3>();

        // find x direction
        RaycastHit2D Hit = Physics2D.Raycast(gameObject.transform.position, new Vector2(-GameInstance.Instance.TileSize, 0f), GameInstance.Instance.TileSize, ~gameObject.layer);
        if (Hit.collider != null && Hit.collider.CompareTag("MagicObject"))
        {
            if (GetComposedColor(Hit.collider.gameObject.GetComponent<MagicObject>().DefaultColor) != DefaultColor)
            {
                list.Add(-transform.right);
            }
        }

        // find x direction
        //Hit = Physics2D.Raycast(gameObject.transform.position, new Vector2(-GameInstance.Instance.TileSize/2, 0f), ~gameObject.layer);
        Hit = Physics2D.Raycast(gameObject.transform.position, new Vector2(GameInstance.Instance.TileSize, 0f), GameInstance.Instance.TileSize, ~gameObject.layer);
        if (Hit.collider != null && Hit.collider.CompareTag("MagicObject"))
        {
            if (GetComposedColor(Hit.collider.gameObject.GetComponent<MagicObject>().DefaultColor) != DefaultColor)
            {
                list.Add(transform.right);
            }
        }

        // find y direction
        //Hit = Physics2D.Raycast(gameObject.transform.position, new Vector2(0f, GameInstance.Instance.TileSize/2), ~gameObject.layer);
        Hit = Physics2D.Raycast(gameObject.transform.position, new Vector2(0f,-GameInstance.Instance.TileSize), GameInstance.Instance.TileSize, ~gameObject.layer);
        if (Hit.collider != null && Hit.collider.CompareTag("MagicObject"))
        {
            if (GetComposedColor(Hit.collider.gameObject.GetComponent<MagicObject>().DefaultColor) != DefaultColor)
            {
                list.Add(-transform.up);
            }
        }

        // find y direction
        //Hit = Physics2D.Raycast(gameObject.transform.position, new Vector2(0f, -GameInstance.Instance.TileSize/2), ~gameObject.layer);
        Hit = Physics2D.Raycast(gameObject.transform.position, new Vector2(0f, GameInstance.Instance.TileSize), GameInstance.Instance.TileSize, ~gameObject.layer);
        if (Hit.collider != null && Hit.collider.CompareTag("MagicObject"))
        {
            if (GetComposedColor(Hit.collider.gameObject.GetComponent<MagicObject>().DefaultColor) != DefaultColor)
            {
                list.Add(transform.up);
            }
        }


        return list;
    }

    public List<GameInstance.MagicColor> GetCurrentColors()
    {
        List<GameInstance.MagicColor> list = new List<GameInstance.MagicColor>();

        if (DefaultColor.Equals(GameInstance.MagicColor.PURPLE))
        {
            list.Add(GameInstance.MagicColor.BLUE);
            list.Add(GameInstance.MagicColor.RED);
            return list;
        }

        if (DefaultColor.Equals(GameInstance.MagicColor.GREEN))
        {
            list.Add(GameInstance.MagicColor.BLUE);
            list.Add(GameInstance.MagicColor.YELLOW);
            return list;
        }

        if (DefaultColor.Equals(GameInstance.MagicColor.ORANGE))
        {
            list.Add(GameInstance.MagicColor.RED);
            list.Add(GameInstance.MagicColor.YELLOW);
            return list;
        }

        return list;
    }

    public List<Vector2> GetAvailableDirection()
    {
        List<Vector2> list = new List<Vector2>();

        // find x direction
        RaycastHit2D Hit = Physics2D.Raycast(gameObject.transform.position, new Vector2(GameInstance.Instance.TileSize, 0f), BlockMovementLayer);
        if (Hit.collider == null)
        {
            list.Add(new Vector2(1f, 0f));
        }

        // find x direction
        Hit = Physics2D.Raycast(gameObject.transform.position, new Vector2(-GameInstance.Instance.TileSize, 0f), BlockMovementLayer);
        if (Hit.collider == null)
        {
            list.Add(new Vector2(-1f, 0f));
        }

        // find y direction
        Hit = Physics2D.Raycast(gameObject.transform.position, new Vector2(0f, GameInstance.Instance.TileSize), BlockMovementLayer);
        if (Hit.collider == null)
        {
            list.Add(new Vector2(0f, 1f));
        }

        // find y direction
        Hit = Physics2D.Raycast(gameObject.transform.position, new Vector2(0f, -GameInstance.Instance.TileSize), BlockMovementLayer);
        if (Hit.collider == null)
        {
            list.Add(new Vector2(0f, -1f));
        }

        return list;
    }

    public void ComposeMagicColor(Vector2 Input)
    {
        RaycastHit2D Hit = Physics2D.Raycast(gameObject.transform.position, Input, GameInstance.Instance.TileSize, ~gameObject.layer);

        if (Hit.collider == null || !Hit.collider.CompareTag("MagicObject"))
        {
            return;
        }

        Debug.Log(Hit.collider.gameObject.name);

        MagicObject TargetObject = Hit.collider.gameObject.GetComponent<MagicObject>();
        GameInstance.MagicColor ComposedColor = GetComposedColor(TargetObject.DefaultColor);
        Vector3 TargetPos = gameObject.transform.position;

        if (ComposedColor.Equals(this.DefaultColor))
        {
            return;
        }

        TargetObject.Kill();
        Kill();
        CreateMagicObject(ComposedColor, TargetPos);


    }

    public void Kill()
    {
        gameObject.SetActive(false);
        Destroy(gameObject, 0.1f);
    }

    public void DecomposeMagicObject(Vector2 Input, GameInstance.MagicColor magicColor)
    {
        RaycastHit2D Hit = Physics2D.Raycast(gameObject.transform.position, Input, GameInstance.Instance.TileSize, ~gameObject.layer);

        if (Hit.collider != null)
        {
            return;
        }

        Vector3 TargetPos = gameObject.transform.position + new Vector3(Input.x, Input.y, 0) * GameInstance.Instance.TileSize;
        CreateMagicObject(magicColor, TargetPos);

        GameInstance.MagicColor DecomposedColor = GetDecomposedColor(magicColor);
        Vector3 OriginPos = gameObject.transform.position;

        Kill();

        CreateMagicObject(DecomposedColor, OriginPos);
    }

    private GameInstance.MagicColor GetDecomposedColor(GameInstance.MagicColor DecomposedColor)
    {
        if (this.DefaultColor.Equals(GameInstance.MagicColor.ORANGE))
        {
            return DecomposedColor.Equals(GameInstance.MagicColor.RED) ? GameInstance.MagicColor.YELLOW : GameInstance.MagicColor.RED;
        }
        else if (this.DefaultColor.Equals(GameInstance.MagicColor.PURPLE))
        {
            return DecomposedColor.Equals(GameInstance.MagicColor.RED) ? GameInstance.MagicColor.BLUE : GameInstance.MagicColor.RED;
        }
        else if (this.DefaultColor.Equals(GameInstance.MagicColor.GREEN))
        {
            return DecomposedColor.Equals(GameInstance.MagicColor.BLUE) ? GameInstance.MagicColor.YELLOW : GameInstance.MagicColor.BLUE;
        }

        return this.DefaultColor;
    }

    private void CreateMagicObject(GameInstance.MagicColor magicColor, Vector3 TargetPos)
    {
        switch (magicColor)
        {
            case GameInstance.MagicColor.RED:
                Instantiate(GameInstance.Instance.RedMagic, TargetPos, Quaternion.identity);
                break;
            case GameInstance.MagicColor.YELLOW:
                Instantiate(GameInstance.Instance.YellowMagic, TargetPos, Quaternion.identity);
                break;
            case GameInstance.MagicColor.BLUE:
                Instantiate(GameInstance.Instance.BlueMagic, TargetPos, Quaternion.identity);
                break;
            case GameInstance.MagicColor.PURPLE:
                Instantiate(GameInstance.Instance.PurpleMagic, TargetPos, Quaternion.identity);
                break;
            case GameInstance.MagicColor.GREEN:
                Instantiate(GameInstance.Instance.GreenMagic, TargetPos, Quaternion.identity);
                break;
            case GameInstance.MagicColor.ORANGE:
                Instantiate(GameInstance.Instance.OrangeMagic, TargetPos, Quaternion.identity);
                break;
        }
    }

    private GameInstance.MagicColor GetComposedColor(GameInstance.MagicColor MagicColor)
    {
        if ((MagicColor.Equals(GameInstance.MagicColor.BLUE) && this.DefaultColor.Equals(GameInstance.MagicColor.RED))
            || (this.DefaultColor.Equals(GameInstance.MagicColor.BLUE) && MagicColor.Equals(GameInstance.MagicColor.RED)))
        {
            return GameInstance.MagicColor.PURPLE;
        }

        if ((MagicColor.Equals(GameInstance.MagicColor.BLUE) && this.DefaultColor.Equals(GameInstance.MagicColor.YELLOW))
            || (this.DefaultColor.Equals(GameInstance.MagicColor.BLUE) && MagicColor.Equals(GameInstance.MagicColor.YELLOW)))
        {
            return GameInstance.MagicColor.GREEN;
        }

        if ((MagicColor.Equals(GameInstance.MagicColor.RED) && this.DefaultColor.Equals(GameInstance.MagicColor.YELLOW))
            || (this.DefaultColor.Equals(GameInstance.MagicColor.RED) && MagicColor.Equals(GameInstance.MagicColor.YELLOW)))
        {
            return GameInstance.MagicColor.ORANGE;
        }

        return this.DefaultColor;
    }
}
