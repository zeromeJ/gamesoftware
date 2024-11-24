using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
class SceneScore
{
    public string key;
    public int score;
}

public class ScoreManager : MonoBehaviour
{
    [SerializeField] SceneScore tiger;
    [SerializeField] SceneScore rabbit;
    [SerializeField] SceneScore kitty;
    private string currentScene;
    private Dictionary<string, SceneScore> sceneScores;

    private void Awake()
    {
        InitializeSceneScores();
        SelectScene(); // 현재 씬 이름 저장
        RefreshScore(); // 최고기록 초기화
    }

    // 다른 게임 매니저에서 최고점수 불러오는 함수
    public int GetBestScore()
    {
        int best = PlayerPrefs.GetInt(currentScene, 0);
        return best;
    }

    public int GetBestScore(string key)
    {
        int best = PlayerPrefs.GetInt(key, 0);
        return best;
    }

    // 다른 게임 매니저에서 최고점수 저장하는 함수
    public void SetBestScore(int newBest)
    {
        PlayerPrefs.SetInt(currentScene, newBest);
        PlayerPrefs.Save();

        RefreshScore();
    }
    
    // class 내부의 function

    private void InitializeSceneScores()
    {
        sceneScores = new Dictionary<string, SceneScore>
        {
            { tiger.key, tiger },
            { rabbit.key, rabbit },
            { kitty.key, kitty }
        };
    }

    // 현재 활성화중인 Scene이름 가져오기
    private string GetKeyOfScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        return scene.name;
    }
    // currentScene에 현재 씬의 Key값 넣어주기
    private void SelectScene()
    {
        string sceneName = GetKeyOfScene();
        if (sceneName == tiger.key) { currentScene = tiger.key; }
        else if (sceneName == rabbit.key) { currentScene = rabbit.key; }
        else if (sceneName == kitty.key) { currentScene = kitty.key; }
        else { }
        Debug.Log(currentScene);
    }

    // 모든 최고기록 값 갱신
    private void RefreshScore()
    {
        foreach (var sceneScore in sceneScores.Values)
        {
            sceneScore.score = GetBestScore(sceneScore.key);
        }
    }
}
