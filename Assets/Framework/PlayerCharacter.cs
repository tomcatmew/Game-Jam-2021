using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public PlayerController MyPlayerController;

    [SerializeField] private LayerMask GroundLayer;
    [SerializeField] private Transform GroundCheckPos;
    [SerializeField] private float GroundCheckRadius = .2f;
    [SerializeField] private Transform HeadCheckPos;


    public bool AutoPocessOnCreate = true;

    [Range(0f,100f)] public float WalkSpeed = 20;
    [Range(0f, 100f)] public float RunSpeed = 60;
    [Range(0f, 100f)] public float JumpSpeed = 40;
    [Range(0f, 1f)] public float SmoothTime = 0.3F;

    private Rigidbody2D MyRigidbody2D;
    private Vector2 CurrVelocity = Vector2.zero;

    private bool IsMovable = true;
    private bool IsOnGround;

    private bool IsPocessed = false;

    private void Awake()
    {
        MyRigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        if(MyRigidbody2D == null)
        {
            Debug.Log("Missing Rigidbody2D for Player Character");
        }
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
        IsOnGround = GroundChecking();
    }

    bool GroundChecking()
    {
        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheckPos.position, GroundCheckRadius, GroundLayer);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                return true;
            }
        }

        return false;
    }

    public void HorizontalMove(float Input,bool IsRunning)
    {
        if (IsMovable)
        {
            Vector3 targetVelocity;
            // Move the character by finding the target velocity
            if (IsRunning)
            {
                targetVelocity = new Vector2(10f * Input * RunSpeed, MyRigidbody2D.velocity.y);
            }
            else
            {
                targetVelocity = new Vector2(10f * Input * WalkSpeed, MyRigidbody2D.velocity.y);
            }

            // And then smoothing it out and applying it to the character
            MyRigidbody2D.velocity = Vector2.SmoothDamp(MyRigidbody2D.velocity, targetVelocity, ref CurrVelocity, SmoothTime);
        }
    }

    public void Jump()
    {
        if (IsMovable && IsOnGround)
        {
            // Add a vertical force to the player.
            IsOnGround = false;
            MyRigidbody2D.AddForce(new Vector2(0f, JumpSpeed));
        }
    }
}
