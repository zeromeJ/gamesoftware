using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class TigerGame : MonoBehaviour
{
    public int tigerBestScore { get; set; }
    public int tigerCurrentScore { get; set; }

    [SerializeField] Timer timer;
    [SerializeField] CountDown countdown;
    [SerializeField] TextMeshProUGUI bestScoreText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameObject gameEndPanel;
    [SerializeField] ScoreManager scoreManager;

    public Button[] cards; // 3개의 카드를 버튼으로 설정

    [SerializeField] private RectTransform heartContainer; // 하트 이미지를 표시할 UI 컨테이너
    [SerializeField] Sprite fullHeartSprite; // 하트가 채워진 이미지
    [SerializeField] Sprite emptyHeartSprite; // 하트가 비어있는 이미지
    private List<Image> heartImages = new List<Image>(); // 각 하트 UI 관리

    [SerializeField] Sprite[] tigerParts; // 호랑이 귀, 꼬리, 손 이미지 배열
    [SerializeField] Sprite[] squirrelParts; // 다람쥐 귀, 꼬리, 손 이미지 배열
    [SerializeField] Sprite[] dogParts; // 강아지 귀, 꼬리, 손 이미지 배열

    private int score = 0; // 초기 점수
    private int lives = 3; // 초기 목숨
    private int tigerIndex = -1; // 호랑이 위치 인덱스

    void Start()
    {
        if (scoreManager != null)
        {
            tigerBestScore = scoreManager.GetBestScore();
        }
        else
        {
            Debug.LogError("ScoreManager를 찾을 수 없습니다.");
        }

        InitializeHearts();
        UpdateUI();
        AssignRandom();
    }


    // 하트 UI 초기화
    void InitializeHearts()
    {
        // 기존 하트 제거
        foreach (Transform child in heartContainer)
        {
            Destroy(child.gameObject);
        }
        heartImages.Clear(); // 리스트도 초기화

        // lives 개수에 따라 하트 UI를 생성
        for (int i = 0; i < lives; i++)
        {
            GameObject heartObject = new GameObject("Heart" + i);
            heartObject.transform.SetParent(heartContainer); // heartContainer에 추가
            heartObject.transform.localScale = Vector3.one; // 스케일 초기화

            Image heartImage = heartObject.AddComponent<Image>();
            heartImage.sprite = fullHeartSprite; // 초기 상태는 채워진 하트
            heartImages.Add(heartImage);
        }
    }



    void AssignRandom()
    {
        // 손/꼬리/귀 중 랜덤 선택
        int selectedPartIndex = Random.Range(0, 3); // 0 = 귀, 1 = 꼬리, 2 = 손
        Debug.Log("Selected part index: " + selectedPartIndex);

        // 동물 배치를 랜덤화
        int[] indices = new int[] { 0, 1, 2 }.OrderBy(x => Random.value).ToArray();

        // 각 버튼에 랜덤하게 배치된 동물의 선택된 파트를 설정
        tigerIndex = indices[0]; // 첫 번째가 호랑이
        cards[tigerIndex].GetComponent<Image>().sprite = tigerParts[selectedPartIndex]; // 호랑이의 선택된 파트

        int squirrelIndex = indices[1]; // 두 번째가 다람쥐
        cards[squirrelIndex].GetComponent<Image>().sprite = squirrelParts[selectedPartIndex]; // 다람쥐의 선택된 파트

        int dogIndex = indices[2]; // 세 번째가 강아지
        cards[dogIndex].GetComponent<Image>().sprite = dogParts[selectedPartIndex]; // 강아지의 선택된 파트
    }

    // 카드 클릭 시 점수 및 목숨 업데이트
    public void OnCardClick(int index)
    {
        if (index == tigerIndex)
        {
            score += 10;
            AssignRandom();
        }
        else
        {
            lives -= 1;
            UpdateLivesUI();
            AssignRandom();

            if (lives <= 0)
            {
                GameOver();
                return;
            }
        }

        UpdateUI();
    }

    // 하트 이미지를 업데이트
    void UpdateLivesUI()
    {
        for (int i = 0; i < heartImages.Count; i++)
        {
            heartImages[i].sprite = i < lives ? fullHeartSprite : emptyHeartSprite;
        }
    }

    // UI 업데이트 함수
    void UpdateUI()
    {
        scoreText.text = "Score: " + score;
    }

    // 게임 종료 함수
    void GameOver()
    {
        StopAllCoroutines();
        Debug.Log("Game Over! Final Score: " + score);
        gameEndPanel.SetActive(true);

        if (tigerCurrentScore > scoreManager.GetBestScore())
        {
            scoreManager.SetBestScore(tigerCurrentScore);
        }
    }
}
