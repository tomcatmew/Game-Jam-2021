using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class indicontrol : MonoBehaviour
{
    public void moveObject(Vector3 newpos)
    {
        transform.position = newpos;
    }
    public void render_enable()
    {
        SpriteRenderer render = GetComponent<SpriteRenderer>();
        render.enabled = true;
    }

    public void render_disable()
    {
        SpriteRenderer render = GetComponent<SpriteRenderer>();
        render.enabled = false;
    }
}
