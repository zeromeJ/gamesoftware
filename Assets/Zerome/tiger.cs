using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;


public class tiger : MonoBehaviour
{

    [SerializeField] Timer timer;
    [SerializeField] CountDown countdown;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] GameObject restartPanel;
    //public TMPro.TextMeshProUGUI livesText;

    //public TMPro.TextMeshProUGUI scoreText;

    public Button[] cards;       // 3개의 카드를 버튼으로 설정


    private int score = 0;       // 초기 점수
    private int lives = 3;       // 초기 목숨
    private int tigerIndex = -1;  // 위치 인덱스

    void Start()
    {
        UpdateUI();
        //StartCoroutine(ShowTiger());
        AssignRandom();
    }

    void AssignRandom()
    {

        int[] indices = new int[] { 0, 1, 2 }.OrderBy(x => Random.value).ToArray();


        tigerIndex = indices[0];
        cards[tigerIndex].GetComponent<Image>().color = Color.red;     // 빨강 카드 == tiger
        cards[indices[1]].GetComponent<Image>().color = Color.green;     // 초록 카드
        cards[indices[2]].GetComponent<Image>().color = Color.black;     // 검정 카드
    }


    //// 위치를 랜덤하게 설정하는 코루틴
    //IEnumerator ShowTiger()
    //{

    //    while (lives > 0)
    //    {
    //        // 이전 빨간색 카드 초기화
    //        if (tigerIndex >= 0)
    //        {
    //            cards[tigerIndex].GetComponent<Image>().color = Color.white;
    //        }

    //        // 카드 색상을 랜덤하게 배정 (빨강, 주황, 초록)
    //        int randomIndex = Random.Range(0, cards.Length);
    //        tigerIndex = randomIndex;

    //        // 색상 설정
    //        for (int i = 0; i < cards.Length; i++)
    //        {
    //            if (i == tigerIndex)
    //                cards[i].GetComponent<Image>().color = Color.red;     // 빨강 카드
    //            else
    //                cards[i].GetComponent<Image>().color = i % 2 == 0 ? Color.green : Color.black;  // 초록, 검( 카드
    //        }

    //        yield return new WaitForSeconds(1.0f); // 1초 동안 색상 유지 후 다시 랜덤 배정
    //    }
    //}

    // 카드 클릭 시 점수 및 목숨 업데이트
    public void OnCardClick(int index)
    {
        if (index == tigerIndex)
        {
            score += 10;
            //StartCoroutine(ShowTiger());
            AssignRandom();
        }
        else
        {
            lives -= 1;
            Debug.Log("Lives remaining: " + lives);
            if (lives <= 0)
            {
                GameOver();
                return;
            }
        }

        UpdateUI();
    }

    void Shuffle(int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int rnd = Random.Range(i, array.Length);
            int temp = array[i];
            array[i] = array[rnd];
            array[rnd] = temp;
        }
    }

    // UI 업데이트 함수
    void UpdateUI()
    {
        scoreText.text = "Score: " + score;
        livesText.text = "Lives: " + lives;
    }

    // 게임 종료 함수
    void GameOver()
    {
        StopAllCoroutines();
        livesText.text = "Game Over";
        Debug.Log("Game Over! Final Score: " + score); // 게임 종료 시 콘솔 메시지 출력
        restartPanel.SetActive(true);
    }
}
