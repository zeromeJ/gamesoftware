using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSceneLoader : MonoBehaviour
{
    public void LoadMyInfoScene()
    {
        SceneManager.LoadScene("MyInfo");
    }
    public void LoadScoreBoardScene()
    {
        SceneManager.LoadScene("ScoreBoard");
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
