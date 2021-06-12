using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerCharacter ControlledCharacter;

    public bool AllowPlayerControl = true;

    private float HorizontalInput;
    private float VerticalInput;
    private bool InDragMode;
    private bool InConnectMode;
    private bool InDisconnectMode;
    private bool ChangeOrder;

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

        }

    }

    //Apply Control using the input accepted
    private void FixedUpdate()
    {
        if (ControlledCharacter != null && AllowPlayerControl)
        {

            if (InConnectMode)
            {

            }
            else if (InDisconnectMode)
            {
                if (ChangeOrder)
                {
                    //Change Order
                }
            }
            else
            {
                ControlledCharacter.Move(new Vector2(HorizontalInput, VerticalInput), InDragMode);
            }
        }
    }
}
