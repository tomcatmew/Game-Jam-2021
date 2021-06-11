using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerCharacter ControlledCharacter;

    public bool AllowPlayerControl = true;


    private bool IsRunning;
    private float HorizontalInput;

    //Accept Input
    private void Update()
    {
        if (ControlledCharacter != null && AllowPlayerControl)
        {
            HorizontalInput = Input.GetAxisRaw("Horizontal");
            if (Input.GetAxisRaw("Shift") == 1f)
            {
                IsRunning = true;
            }
            else
            {
                IsRunning = false;
            }

            if (Input.GetAxisRaw("Jump") == 1f)
            {
                ControlledCharacter.Jump();
            }
        }
    }

    //Apply Control using the input accepted
    private void FixedUpdate()
    {
        if (ControlledCharacter != null && AllowPlayerControl)
        {
            ControlledCharacter.HorizontalMove(HorizontalInput * Time.fixedDeltaTime, IsRunning);
        }
    }
}
