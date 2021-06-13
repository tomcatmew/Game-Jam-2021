using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aimcontrol : MonoBehaviour
{
    SpriteRenderer render;
    void Start()
    {
        render = GetComponent<SpriteRenderer>();
    }
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
    public void default_color()
    {
        SpriteRenderer render = GetComponent<SpriteRenderer>();
        Color NewColor = new Color(1.0f, 1.0f, 1.0f,1.0f);
        render.color = NewColor;
    }
    public void connect_color()
    {
        Color NewColor = new Color(0.117f, 1.0f, 0.861f, 1.0f);
        render.color = NewColor;
    }

    public void break_color()
    {
        Color NewColor = new Color(1.0f, 0.117f, 0.535f, 1.0f);
        render.color = NewColor;
    }
    // Start is called before the first frame update

  

    // Update is called once per frame
    void Update()
    {
    }
}
