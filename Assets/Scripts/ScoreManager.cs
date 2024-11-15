using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    private const string ScoreStage1Key = "ScoreStage1";
    private const string ScoreStage2Key = "ScoreStage2";
    private const string ScoreStage3Key = "ScoreStage3";

    int scoreStage1;
    int scoreStage2;
    int scoreStage3;

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateScores();
    }

    void ShowScores()
    {

    }

    void UpdateScores()
    {
        scoreStage1 = PlayerPrefs.GetInt(ScoreStage1Key, 0);
        scoreStage2 = PlayerPrefs.GetInt(ScoreStage2Key, 0);
        scoreStage3 = PlayerPrefs.GetInt(ScoreStage3Key, 0);
    }

}
