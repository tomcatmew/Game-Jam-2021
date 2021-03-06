using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    public void LoadNewLevel(string Name)
    {
        SceneManager.LoadSceneAsync(Name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void TransitToScene(TransitionStart transitionStart)
    {
        GameInstance.Instance.IsDialoguePlayed = false;
        SceneManager.LoadSceneAsync(transitionStart.NewSceneName);
    }

    public void ReloadLevel()
    {
        GameInstance.Instance.IsDialoguePlayed = true;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

}
