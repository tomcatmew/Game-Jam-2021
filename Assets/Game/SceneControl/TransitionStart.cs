using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionStart : MonoBehaviour
{

    public SceneController MySceneController;

    public string NewSceneName;

    private void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.05f);
        foreach (Collider2D coll in colliders)
        {
            if (coll.gameObject.tag == "Player")
            {
                MySceneController.TransitToScene(this);
            }
        }
    }
}
