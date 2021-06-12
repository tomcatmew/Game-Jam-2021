using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void TransitToScene(TransitionStart transitionStart)
    {
        SceneManager.LoadSceneAsync(transitionStart.NewSceneName);
    }

}
