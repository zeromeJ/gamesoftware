using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int bestScore { get; set; }
    public int currentScore { get; set; }

    [SerializeField] GameObject rope;
    [SerializeField] GameObject player;
    [SerializeField] GameObject gameEndPanel;
    [SerializeField] ScoreManager scoreManager;

    [SerializeField] float ropeSpeed = 0.5f;
    [SerializeField] float minRopeSpeed = 0.5f; // 줄넘기 최소 속도
    [SerializeField] float maxRopeSpeed = 1.0f; // 줄넘기 최대 속도
    [SerializeField] float changeInterval = 2.0f; // 속도 변경 주기

    private void Awake()
    {
        if(scoreManager != null)
        {
            bestScore = scoreManager.GetBestScore();
            Debug.Log(bestScore);
        }
        else
        {
            Debug.Log("Can't find ScoreManager");
        }
    }

    private void Start()
    {
        StartCoroutine(ChangeRopeSpeed());
    }

    private IEnumerator ChangeRopeSpeed()
    {
        while (true) // 무한 루프
        {
            float randomSpeed = Random.Range(minRopeSpeed, maxRopeSpeed); // 랜덤 속도 생성
            ropeSpeed = randomSpeed;
            rope.SendMessage("SetRopeSpeed", ropeSpeed); // 애니메이터의 속도 설정
            yield return new WaitForSeconds(changeInterval); // 지정한 시간 대기
        }
    }

    public void StuckRope()
    {
        gameEndPanel.SetActive(true);

        if (currentScore > scoreManager.GetBestScore())
        {
            scoreManager.SetBestScore(currentScore); 
        }
    }

    private void ScoreUp()
    {
        currentScore++;
    }
}
