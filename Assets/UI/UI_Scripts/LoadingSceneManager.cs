using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour
{
    [SerializeField] Sprite[] backgrounds;
    [SerializeField] Image background;

    int backgroundIndex;

    public Slider progressBarSlider;
    public static string sceneName;


    private void Start()
    {
        if (sceneName == null)
        {
            sceneName = "MainMenu";
        }
        SetBackground();
        StartCoroutine(LoadSceneProgress());
    }

    IEnumerator LoadSceneProgress()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        op.allowSceneActivation = false;

        float timer = 0f;
        while (!op.isDone)
        {
            yield return null;
            if (op.progress < 0.3f)
            {
                progressBarSlider.value = op.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                progressBarSlider.value = Mathf.Lerp(0.3f, 1f, timer);
                if (progressBarSlider.value >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }

    void SetBackground()
    {
        if (sceneName == "Tiger")
        {
            backgroundIndex = 0;
        }
        else if (sceneName == "RabbitScene")
        {
            backgroundIndex = 1;
        }
        else if (sceneName == "Receive Toy")
        {
            backgroundIndex = 2;
        }
        else
        {
            backgroundIndex = 0;
        }
        background.sprite = backgrounds[backgroundIndex];
    }



}