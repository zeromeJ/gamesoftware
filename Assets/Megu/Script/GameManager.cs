using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    string rabbitGameKey = "Rabbit";
    public int bestScore { get; set; }
    public int currentScore { get; set; }
    //public bool isGameActive = false;
    public GameObject rope;
    public GameObject player;
    public GameObject gameEndPanel;
    public float ropeSpeed = 0.5f;
    public float minRopeSpeed = 0.5f; // 줄넘기 최소 속도
    public float maxRopeSpeed = 1.0f; // 줄넘기 최대 속도
    public float changeInterval = 2.0f; // 속도 변경 주기

    private void Awake()
    {
        bestScore = GetBestScore();
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

    private void StuckRope()
    {
        Debug.Log("StuckRope!");
        gameEndPanel.SetActive(true);
        //isGameActive = false;

        SetBestScore();
    }

    private void ScoreUp()
    {
        currentScore++;
    }

    private int GetBestScore()
    {
        int best = PlayerPrefs.GetInt(rabbitGameKey);
        return best;
    }

    private void SetBestScore()
    {
        if (currentScore > GetBestScore())
        {
            PlayerPrefs.SetInt(rabbitGameKey, currentScore);
        }
    }
}
