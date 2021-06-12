using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerCharacter ControlledCharacter;

    public bool AllowPlayerControl = true;

    [SerializeField] private LayerMask MagicObjectLayer;
    [SerializeField] private float InputIntervalTime = 0.05f;

    private float TimeSinceLastInput = 0f;

    private float HorizontalInput;
    private float VerticalInput;
    private bool InDragMode;
    private bool InConnectMode;
    private bool InDisconnectMode;
    private bool ChangeOrder;

    private void Start()
    {
        TimeSinceLastInput = InputIntervalTime;
    }

    //Accept Input
    private void Update()
    {
        if (ControlledCharacter != null && AllowPlayerControl)
        {
            HorizontalInput = Input.GetAxisRaw("Horizontal");
            VerticalInput = Input.GetAxisRaw("Vertical");
            if(Input.GetAxisRaw("Drag") == 1f)
            {
                InDragMode = true;
            }
            else
            {
                InDragMode = false;
            }

            if(Input.GetAxisRaw("Connect") == 1f)
            {
                InConnectMode = true;
            }
            else
            {
                InConnectMode = false;
            }

            if (Input.GetAxisRaw("Disconnect") == 1f)
            {
                InDisconnectMode = true;
                if(Input.GetAxisRaw("Change") == 1f)
                {
                    ChangeOrder = true;
                }
                else
                {
                    ChangeOrder = false;
                }
            }
            else
            {
                InDisconnectMode = false;
            }

            if(TimeSinceLastInput < InputIntervalTime) TimeSinceLastInput += Time.deltaTime;

        }

    }

    private int CurrIdx = 0;

    //Apply Control using the input accepted
    private void FixedUpdate()
    {
        if (ControlledCharacter != null && AllowPlayerControl)
        {
            if (InConnectMode)
            {
                //Debug.Log(ControlledCharacter.GetFacingDir().ToString());
                if (TimeSinceLastInput >= InputIntervalTime)
                {
                    RaycastHit2D Hit = Physics2D.Raycast(ControlledCharacter.transform.position, ControlledCharacter.GetFacingDir(), GameInstance.Instance.TileSize, MagicObjectLayer);
                    if (Hit.collider != null && Hit.collider.tag == "MagicObject")
                    {
                        if (Mathf.Abs(HorizontalInput) == 1f || Mathf.Abs(VerticalInput) == 1f)
                        {
                            Hit.collider.gameObject.GetComponent<MagicObject>().ComposeMagicColor(new Vector2(HorizontalInput, VerticalInput));
                            TimeSinceLastInput = 0f;
                        }
                    }
                }
            }
            else if (InDisconnectMode)
            {
                if (TimeSinceLastInput >= InputIntervalTime)
                {
                    RaycastHit2D Hit = Physics2D.Raycast(ControlledCharacter.transform.position, ControlledCharacter.GetFacingDir(), GameInstance.Instance.TileSize, MagicObjectLayer);
                    if (Hit.collider != null && Hit.collider.tag == "MagicObject")
                    {
                        List<GameInstance.MagicColor> SplitingColors = Hit.collider.gameObject.GetComponent<MagicObject>().GetCurrentColors();
                        if (SplitingColors.Count == 2) {
                            if (ChangeOrder)
                            {
                                //Change Order
                                if (CurrIdx == 0) CurrIdx = 1;
                                else CurrIdx = 0;
                                TimeSinceLastInput = 0f;
                            }

                            if (Mathf.Abs(HorizontalInput) == 1f || Mathf.Abs(VerticalInput) == 1f)
                            {
                                Hit.collider.gameObject.GetComponent<MagicObject>().DecomposeMagicObject(new Vector2(HorizontalInput, VerticalInput), SplitingColors[CurrIdx]);
                                TimeSinceLastInput = 0f;
                            }
                        }
                        else
                        {
                            //cannot split base colors
                        }
                    }
                }
            }
            else
            {
                if (Mathf.Abs(HorizontalInput) == 1f || Mathf.Abs(VerticalInput) == 1f)
                {
                    ControlledCharacter.Move(new Vector2(HorizontalInput, VerticalInput), InDragMode);
                }
            }
        }
    }
}
