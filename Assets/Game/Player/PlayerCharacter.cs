using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public PlayerController MyPlayerController;

    public bool AutoPocessOnCreate = true;

    private bool IsMovable = true;

    private bool IsPocessed = false;

    [SerializeField] private float MovementThreshold = 0.5f;
    [SerializeField] private float MoveTime = 0.3f;

    private float CurrAnimTime = 0f;

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
                    CurrAnimTime = 0f;
                    InMoveAnim = false;
                    Moved = false;
                }
            }
        }
    }

    public void Move(Vector2 Input)
    {
        if (IsMovable)
        {
            if(Input.x >= MovementThreshold && !Moved)
            {
                //Move Right
                TargetPos = gameObject.transform.position + new Vector3(GameInstance.Instance.TileSize,0f,0f);
                Moved = true;
                InMoveAnim = false;
            }
            else if(Input.x <= -MovementThreshold && !Moved)
            {
                //Move Left
                TargetPos = gameObject.transform.position + new Vector3(-GameInstance.Instance.TileSize, 0f, 0f);
                Moved = true;
                InMoveAnim = false;
            }
            else if(Input.y >= MovementThreshold && !Moved)
            {
                //Move Up
                TargetPos = gameObject.transform.position + new Vector3(0f, GameInstance.Instance.TileSize, 0f);
                Moved = true;
                InMoveAnim = false;
            }
            else if(Input.y <= -MovementThreshold && !Moved)
            {
                //Move Down
                TargetPos = gameObject.transform.position + new Vector3(0f, -GameInstance.Instance.TileSize, 0f);
                Moved = true;
                InMoveAnim = false;
            }
        }
    }

}
