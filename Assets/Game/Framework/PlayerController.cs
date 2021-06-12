using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerCharacter ControlledCharacter;

    public bool AllowPlayerControl = true;

    private float HorizontalInput;
    private float VerticalInput;

    //Accept Input
    private void Update()
    {
        if (ControlledCharacter != null && AllowPlayerControl)
        {
            HorizontalInput = Input.GetAxisRaw("Horizontal");
            VerticalInput = Input.GetAxisRaw("Vertical");
        }

    }

    //Apply Control using the input accepted
    private void FixedUpdate()
    {
        if (ControlledCharacter != null && AllowPlayerControl)
        {
            ControlledCharacter.Move(new Vector2(HorizontalInput,VerticalInput));
        }
    }
}
