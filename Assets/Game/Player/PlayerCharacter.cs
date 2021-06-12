using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public PlayerController MyPlayerController;

    public bool AutoPocessOnCreate = true;

    private bool IsMovable = true;

    private bool IsPocessed = false;
    private bool IsAlive = true;

    [SerializeField] private float MovementThreshold = 0.5f;
    [SerializeField] private float MoveTime = 0.3f;
    [SerializeField] private LayerMask BlockMovementLayer;

    private float CurrAnimTime = 0f;

    private GameInstance.FacingDir Facing = GameInstance.FacingDir.DOWN;

    private bool Moved = false;
    private bool InMoveAnim = false;
    private Vector3 TargetPos = Vector3.zero;
    private Vector3 StartPos = Vector3.zero;

    private void Awake()
    {

    }

    private void Start()
    {
        if (MyPlayerController == null && AutoPocessOnCreate)
        {
            GameInstance.Instance.MyPlayerController.ControlledCharacter = this;
            MyPlayerController = GameInstance.Instance.MyPlayerController;
            IsPocessed = true;
        }
        else
        {
            IsPocessed = false;
        }

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

    public void Kill()
    {
        if (IsAlive)
        {
            IsMovable = false;
            IsAlive = false;
            //Play Dead Animation
            GameInstance.Instance.MyGameMode.GameOver();
        }
    }

    public Vector2 GetFacingDir()
    {
        switch (Facing)
        {
            case GameInstance.FacingDir.UP:
                return gameObject.transform.up;
            case GameInstance.FacingDir.DOWN:
                return -gameObject.transform.up;
            case GameInstance.FacingDir.LEFT:
                return -gameObject.transform.right;
            case GameInstance.FacingDir.RIGHT:
                return gameObject.transform.right;
        }

        return new Vector2(0, 0);
    }

    public void Move(Vector2 Input, bool IsDragging)
    {
        if (IsMovable)
        {
            bool CanMove = false;

            if (Input.x >= MovementThreshold && !Moved)
            {
                RaycastHit2D FacingDirHit = Physics2D.Raycast(gameObject.transform.position, GetFacingDir(), GameInstance.Instance.TileSize, BlockMovementLayer);
                RaycastHit2D MoveDirHit = Physics2D.Raycast(gameObject.transform.position, gameObject.transform.right, GameInstance.Instance.TileSize, BlockMovementLayer);
                if (!IsDragging)
                {
                    Facing = GameInstance.FacingDir.RIGHT;
                }

                if (IsDragging && FacingDirHit.collider != null)
                {
                    if (FacingDirHit.collider.gameObject.tag == "MagicObject")
                    {
                        CanMove = FacingDirHit.collider.gameObject.GetComponent<MagicObject>().CheckMove(Input);
                    }
                }

                if (MoveDirHit.collider == null || (CanMove && FacingDirHit.collider == MoveDirHit.collider))
                {
                    //Move Right
                    if (IsDragging && FacingDirHit.collider != null && FacingDirHit.collider.gameObject.tag == "MagicObject") FacingDirHit.collider.gameObject.GetComponent<MagicObject>().Move(Input);
                    TargetPos = gameObject.transform.position + new Vector3(GameInstance.Instance.TileSize, 0f, 0f);
                    Moved = true;
                    InMoveAnim = false;
                    if (!IsDragging)
                    {
                        Facing = GameInstance.FacingDir.RIGHT;
                    }

                }
            }
            else if (Input.x <= -MovementThreshold && !Moved)
            {
                RaycastHit2D FacingDirHit = Physics2D.Raycast(gameObject.transform.position, GetFacingDir(), GameInstance.Instance.TileSize, BlockMovementLayer);
                RaycastHit2D MoveDirHit = Physics2D.Raycast(gameObject.transform.position, -gameObject.transform.right, GameInstance.Instance.TileSize, BlockMovementLayer);
                if (!IsDragging)
                {
                    Facing = GameInstance.FacingDir.LEFT;
                }

                if (IsDragging && FacingDirHit.collider != null)
                {
                    if (FacingDirHit.collider.gameObject.tag == "MagicObject")
                    {
                        CanMove = FacingDirHit.collider.gameObject.GetComponent<MagicObject>().CheckMove(Input);
                    }
                }

                if (MoveDirHit.collider == null || (CanMove && FacingDirHit.collider == MoveDirHit.collider))
                {
                    //Move Left
                    if (IsDragging && FacingDirHit.collider != null && FacingDirHit.collider.gameObject.tag == "MagicObject") FacingDirHit.collider.gameObject.GetComponent<MagicObject>().Move(Input);
                    TargetPos = gameObject.transform.position + new Vector3(-GameInstance.Instance.TileSize, 0f, 0f);
                    Moved = true;
                    InMoveAnim = false;
                    if (!IsDragging)
                    {
                        Facing = GameInstance.FacingDir.LEFT;
                    }

                }

            }
            else if (Input.y >= MovementThreshold && !Moved)
            {
                RaycastHit2D FacingDirHit = Physics2D.Raycast(gameObject.transform.position, GetFacingDir(), GameInstance.Instance.TileSize, BlockMovementLayer);
                RaycastHit2D MoveDirHit = Physics2D.Raycast(gameObject.transform.position, gameObject.transform.up, GameInstance.Instance.TileSize, BlockMovementLayer);
                if (!IsDragging)
                {
                    Facing = GameInstance.FacingDir.UP;
                }

                if (IsDragging && FacingDirHit.collider != null)
                {
                    if (FacingDirHit.collider.gameObject.tag == "MagicObject")
                    {
                        CanMove = FacingDirHit.collider.gameObject.GetComponent<MagicObject>().CheckMove(Input);
                    }
                }

                if (MoveDirHit.collider == null || (CanMove && FacingDirHit.collider == MoveDirHit.collider))
                {
                    if (IsDragging && FacingDirHit.collider != null && FacingDirHit.collider.gameObject.tag == "MagicObject") FacingDirHit.collider.gameObject.GetComponent<MagicObject>().Move(Input);
                    TargetPos = gameObject.transform.position + new Vector3(0f, GameInstance.Instance.TileSize, 0f);
                    Moved = true;
                    InMoveAnim = false;
                    if (!IsDragging)
                    {
                        Facing = GameInstance.FacingDir.UP;
                    }
                }

            }
            else if (Input.y <= -MovementThreshold && !Moved)
            {
                RaycastHit2D FacingDirHit = Physics2D.Raycast(gameObject.transform.position, GetFacingDir(), GameInstance.Instance.TileSize, BlockMovementLayer);
                RaycastHit2D MoveDirHit = Physics2D.Raycast(gameObject.transform.position, -gameObject.transform.up, GameInstance.Instance.TileSize, BlockMovementLayer);
                if (!IsDragging)
                {
                    Facing = GameInstance.FacingDir.DOWN;
                }

                if (IsDragging && FacingDirHit.collider != null)
                {
                    if (FacingDirHit.collider.gameObject.tag == "MagicObject")
                    {
                        CanMove = FacingDirHit.collider.gameObject.GetComponent<MagicObject>().CheckMove(Input);
                    }
                }

                if (MoveDirHit.collider == null || (CanMove && FacingDirHit.collider == MoveDirHit.collider))
                {
                    //Move Down
                    if (IsDragging && FacingDirHit.collider != null && FacingDirHit.collider.gameObject.tag == "MagicObject") FacingDirHit.collider.gameObject.GetComponent<MagicObject>().Move(Input);
                    TargetPos = gameObject.transform.position + new Vector3(0f, -GameInstance.Instance.TileSize, 0f);
                    Moved = true;
                    InMoveAnim = false;
                    if (!IsDragging)
                    {
                        Facing = GameInstance.FacingDir.DOWN;
                    }
                }
            }
        }
    }
}

