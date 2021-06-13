using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerCharacter ControlledCharacter;

    public bool AllowPlayerControl = true;

    public aimcontrol MyAim;
    public indicontrol right_arrow;
    public indicontrol left_arrow;
    public indicontrol top_arrow;
    public indicontrol bot_arrow;

    public de_indicontrol right_demop;
    public de_indicontrol left_demop;
    public de_indicontrol top_demop;
    public de_indicontrol bot_demop;

    [SerializeField] private LayerMask MagicObjectLayer;
    [SerializeField] private float InputIntervalTime = 0.05f;

    private float TimeSinceLastInput = 0f;

    private float HorizontalInput;
    private float VerticalInput;
    private bool InDragMode = false;
    private bool InConnectMode = false;
    private bool InDisconnectMode = false;
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

            top_arrow.render_disable();
            bot_arrow.render_disable();
            right_arrow.render_disable();
            left_arrow.render_disable();
            top_arrow.render_disable();
            bot_arrow.render_disable();
            right_arrow.render_disable();
            left_arrow.render_disable();
            right_demop.render_disable();
            left_demop.render_disable();
            top_demop.render_disable();
            bot_demop.render_disable();

            RaycastHit2D Hit = Physics2D.Raycast(ControlledCharacter.transform.position, ControlledCharacter.GetFacingDir(), GameInstance.Instance.TileSize, MagicObjectLayer);
            if (Hit.collider != null && Hit.collider.tag == "MagicObject")
            {
                Vector3 tempt = new Vector3(Hit.collider.gameObject.transform.position.x, Hit.collider.gameObject.transform.position.y, Hit.collider.gameObject.transform.position.z);
                MyAim.render_enable();
                MyAim.moveObject(tempt);
                MagicObject mynear = Hit.collider.gameObject.GetComponent<MagicObject>();


                if (mynear.DefaultColor == GameInstance.MagicColor.GREEN || mynear.DefaultColor == GameInstance.MagicColor.PURPLE || mynear.DefaultColor == GameInstance.MagicColor.ORANGE)
                {

                    List<Vector3> NearByColors = mynear.CheckNearDecompMagic();

                    foreach (Vector3 Magic in NearByColors)
                    {
                        Vector3 temp = Hit.collider.gameObject.transform.position + Magic * GameInstance.Instance.TileSize;
                        if (Magic == transform.up)
                        {
                            top_demop.render_enable();
                            top_demop.moveObject(temp);
                        }
                        else if(Magic == -transform.up)
                        {
                            bot_demop.render_enable();
                            bot_demop.moveObject(temp);
                        }
                        else if (Magic == transform.right)
                        {
                            right_demop.render_enable();
                            right_demop.moveObject(temp);
                        }
                        else if (-Magic == transform.right)
                        {
                            left_demop.render_enable();
                            left_demop.moveObject(temp);
                        }
                    }
                }
                else
                {

                    List<Vector3> NearByColors = mynear.CheckNearMagic();
                    foreach(Vector3 Magic in NearByColors)
                    {
                        Vector3 temp = Hit.collider.gameObject.transform.position + Magic * GameInstance.Instance.TileSize;
                        if (Magic == transform.up)
                        {
                            top_arrow.render_enable();
                            top_arrow.moveObject(temp);
                        }
                        else if (Magic == -transform.up)
                        {
                            bot_arrow.render_enable();
                            bot_arrow.moveObject(temp);
                        }
                        else if (Magic == transform.right)
                        {
                            right_arrow.render_enable();
                            right_arrow.moveObject(temp);
                        }
                        else if (Magic == -transform.right)
                        {
                            left_arrow.render_enable();
                            left_arrow.moveObject(temp);
                        }


                    }
                }
            }
            else
            {
                MyAim.render_disable();
                top_arrow.render_disable();
                bot_arrow.render_disable();
                right_arrow.render_disable();
                left_arrow.render_disable();
                right_demop.render_disable();
                left_demop.render_disable();
                top_demop.render_disable();
                bot_demop.render_disable();
            }

            if (Input.GetAxisRaw("Drag") == 1f)
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
                        Vector3 tempt = new Vector3(Hit.collider.gameObject.transform.position.x, Hit.collider.gameObject.transform.position.y, Hit.collider.gameObject.transform.position.z);
                        MyAim.moveObject(tempt);
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
