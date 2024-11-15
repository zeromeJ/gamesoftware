using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartSceneLoader : MonoBehaviour
{
    [SerializeField]
    PlayerInfo playerInfo;

    public void LoadMainMenuScene()
    {
        if (playerInfo.LoadHasLoggedIn())
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            SceneManager.LoadScene("MyInfo");
        }
    }
}
