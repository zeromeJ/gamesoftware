using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreBoardManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI[] bestScoreTexts;

    private void Awake()
    {
        Test_SaveBestScores();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LoadBestScores();
    }

    private void LoadBestScores()
    {
        for (int i = 0; i < bestScoreTexts.Length; i++)
        {
            int bestScore = PlayerPrefs.GetInt($"bestScore{i + 1}", 0);
            bestScoreTexts[i].text = $"{bestScore}";
        }
    }

    #region Test_Code
    private void Test_SaveBestScores()
    {
        PlayerPrefs.SetInt("bestScore1", 100);
        PlayerPrefs.SetInt("bestScore2", 100);
        PlayerPrefs.SetInt("bestScore3", 100);
    }
    #endregion
}
