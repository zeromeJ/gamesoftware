using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSceneController : MonoBehaviour
{
    public void LoadScoreBoardScene()
    {
        SceneManager.LoadScene("ScoreBoard");
    }
    public void LoadStage1Scene()
    {
        LoadingSceneManager.sceneName = "Tiger";
        SceneManager.LoadScene("Loading");
    }
    public void LoadStage2Scene()
    {
        LoadingSceneManager.sceneName = "RabbitScene";
        SceneManager.LoadScene("Loading");
    }
    public void LoadStage3Scene()
    {
        LoadingSceneManager.sceneName = "Receive Toy";
        SceneManager.LoadScene("Loading");
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
