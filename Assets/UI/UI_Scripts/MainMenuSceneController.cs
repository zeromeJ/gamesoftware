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
        SceneManager.LoadScene("Tiger");
    }
    public void LoadStage2Scene()
    {
        SceneManager.LoadScene("RabbitScene");
    }
    public void LoadStage3Scene()
    {
        SceneManager.LoadScene("Receive Toy");
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
