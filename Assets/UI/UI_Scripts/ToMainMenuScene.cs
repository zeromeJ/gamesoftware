using UnityEngine;
using UnityEngine.SceneManagement;

public class ToMainMenuScene : MonoBehaviour
{
    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
