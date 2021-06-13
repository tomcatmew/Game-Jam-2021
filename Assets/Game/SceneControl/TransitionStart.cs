using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionStart : MonoBehaviour
{

    public SceneController MySceneController;
    public GameObject transitioningGameObject;
    public ScreenFader.FadeType fadeType;

    public string NewSceneName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.transitioningGameObject.Equals(collision.gameObject))
        {
            MySceneController.TransitToScene(this);
        }
    }

}
