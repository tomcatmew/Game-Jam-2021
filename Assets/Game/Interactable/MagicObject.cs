using System.Collections.Generic;
using UnityEngine;

public class MagicObject : MonoBehaviour
{

    public PlayerController MyPlayerController;

    public bool IsGrabed = false;

    public GameInstance.MagicColor DefaultColor = GameInstance.MagicColor.BLUE;

    [SerializeField] private LayerMask BlockMovementLayer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Move(Vector2 Input)
    {
        if (!IsGrabed)
        {
            return;
        }

    }

    public List<GameObject> CheckNearMagic()
    {
        List<GameObject> list = new List<GameObject>();

        // find x direction
        RaycastHit2D Hit = Physics2D.Raycast(gameObject.transform.position, new Vector2(GameInstance.Instance.TileSize, 0f), gameObject.layer);

        if (Hit.collider != null && Hit.collider.CompareTag("MagicObject"))
        {
            list.Add(Hit.collider.gameObject);
        }

        // find x direction
        Hit = Physics2D.Raycast(gameObject.transform.position, new Vector2(-GameInstance.Instance.TileSize, 0f), gameObject.layer);
        if (Hit.collider != null && Hit.collider.CompareTag("MagicObject"))
        {
            list.Add(Hit.collider.gameObject);
        }

        // find y direction
        Hit = Physics2D.Raycast(gameObject.transform.position, new Vector2(0f, GameInstance.Instance.TileSize), gameObject.layer);
        if (Hit.collider != null && Hit.collider.CompareTag("MagicObject"))
        {
            list.Add(Hit.collider.gameObject);
        }

        // find y direction
        Hit = Physics2D.Raycast(gameObject.transform.position, new Vector2(0f, -GameInstance.Instance.TileSize), gameObject.layer);
        if (Hit.collider != null && Hit.collider.CompareTag("MagicObject"))
        {
            list.Add(Hit.collider.gameObject);
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
        RaycastHit2D Hit = Physics2D.Raycast(gameObject.transform.position, Input * GameInstance.Instance.TileSize, gameObject.layer);

        if (Hit.collider == null || !Hit.collider.CompareTag("MagicObject"))
        {
            return;
        }

        MagicObject TargetObject = Hit.collider.gameObject.GetComponent<MagicObject>();
        GameInstance.MagicColor ComposedColor = GetComposedColor(TargetObject.DefaultColor);
        Vector3 TargetPos = gameObject.transform.position;

        if (ComposedColor.Equals(this.DefaultColor))
        {
            return;
        }

        Destroy(TargetObject);
        Destroy(gameObject);
        CreateMagicObject(ComposedColor, TargetPos);
    }

    public void DecomposeMagicObject(Vector2 Input, GameInstance.MagicColor magicColor)
    {
        Vector3 TargetPos = gameObject.transform.position + new Vector3(Input.x, Input.y, 0) * GameInstance.Instance.TileSize;
        CreateMagicObject(magicColor, TargetPos);

        GameInstance.MagicColor DecomposedColor = GetDecomposedColor(magicColor);
        Vector3 OriginPos = gameObject.transform.position;

        Destroy(gameObject);

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
