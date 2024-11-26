using UnityEngine;
using UnityEngine.SceneManagement;

public class ToMainMenuScene : MonoBehaviour
{
    [SerializeField] AudioSource clickEffect;
    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ClickButtonSoundEffect()
    {
        clickEffect.Play();
    }
}
