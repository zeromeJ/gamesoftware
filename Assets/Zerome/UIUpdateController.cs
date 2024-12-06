using UnityEngine;
using TMPro;

public class UIUpdateController : MonoBehaviour
{
    public ScoreManager scoreManager;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private TextMeshProUGUI scoreText;

    public void InitialScoreUI()
    {
        if (scoreManager != null)
        {
            TigerGame.TigerBestScore = scoreManager.GetBestScore();
            bestScoreText.text = "최고 점수 : " + TigerGame.TigerBestScore;
        }
        else
        {
            Debug.LogError("ScoreManager를 찾을 수 없습니다.");
        }
    }

    public void UpdateUI(bool isHardMode, int currentScore)
    {
        if (scoreText != null)
        {
            if (isHardMode) scoreText.text = "(하드) 점수 : " + currentScore; // UI에 점수 반영
            else scoreText.text = "(이지) 점수 : " + currentScore; // UI에 점수 반영

            if (currentScore > TigerGame.TigerBestScore)
            {
                bestScoreText.text = "최고 점수 : " + currentScore;
                scoreManager.SetBestScore(currentScore);
            }
        }
        else
        {
            Debug.LogError("ScoreText가 연결되지 않았습니다.");
        }
    }

    [SerializeField] private GameObject heartContainer; // heart 컨테이너]
    [SerializeField] private GameObject scoreContainer;
    [SerializeField] private GameObject buttonContainer; // 버튼 컨테이너 
    [SerializeField] private GameObject feverButtonContainer;
    [SerializeField] private GameObject feverTimerContainer;

    public void InitializeUI()
    {
        scoreContainer.SetActive(true);
        heartContainer.SetActive(true);
        buttonContainer.SetActive(true);
    }


    public void FeverUIControl(bool isStarted)
    {
        
        heartContainer.SetActive(!isStarted);
        buttonContainer.SetActive(!isStarted);
        feverButtonContainer.SetActive(isStarted);
        feverTimerContainer.SetActive(isStarted);
    }

    [SerializeField] private TextMeshProUGUI feverTimeText;
    public void FeverUpdateUI(int second)
    {
        if (feverTimeText != null)
        {
            feverTimeText.text = second.ToString(); // UI에 숫자 표시
        }
    }


    public void BlockClick(bool isBlocked)
    {
        buttonContainer.SetActive(!isBlocked);
        heartContainer.SetActive(!isBlocked);
        //foreach (var card in cards)
        //{
        //    card.interactable = !isBlocked;
        //}
    }
}
