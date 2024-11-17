using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSceneController : MonoBehaviour
{

    public void LoadMyInfoScene()
    {
        SceneManager.LoadScene("MyInfo");
    }
    public void LoadScoreBoardScene()
    {
        SceneManager.LoadScene("ScoreBoard");
    }
    public void LoadStage1Scene()
    {

    }
    public void LoadStage2Scene()
    {

    }
    public void LoadStage3Scene()
    {

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
