using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void TransitToScene(TransitionStart transitionStart)
    {
        GameInstance.Instance.MyScreenFader.FadeSceneOut(transitionStart.fadeType);
        SceneManager.LoadSceneAsync(transitionStart.NewSceneName);
        GameInstance.Instance.MyScreenFader.FadeSceneIn();
    }

}
