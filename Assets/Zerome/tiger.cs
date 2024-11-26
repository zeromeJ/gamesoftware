using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class TigerGame : MonoBehaviour
{

    [SerializeField] CountDown countdown;

    [SerializeField] ScoreManager scoreManager;
    [SerializeField] TextMeshProUGUI bestScoreText;
    [SerializeField] TextMeshProUGUI scoreText;

    public int tigerBestScore { get; set; }
    public int tigerCurrentScore { get; set; }
    //private int score = 0;

    [SerializeField] GameObject gameEndPanel;


    public Button[] cards; // 9개의 버튼
    [SerializeField] private RectTransform heartContainer; // 하트 컨테이너
    [SerializeField] Sprite fullHeartSprite; // 채워진 하트 이미지
    [SerializeField] Sprite emptyHeartSprite; // 비어있는 하트 이미지

    [SerializeField] Sprite tigerSprite; // 호랑이 이미지
    [SerializeField] Sprite squirrelSprite; // 다람쥐 이미지
    [SerializeField] Sprite dogSprite; // 강아지 이미지

    private List<Image> heartImages = new List<Image>();
    private int lives = 3;

    private float spawnInterval = 3f; // 초기 주기
    private float originalInterval = 3f; // 원래의 주기

    private int tigerCount = 3; // 현재 배치된 호랑이 수

    private Coroutine spawnCoroutine;

    void Start()
    {
        if (scoreManager != null)
        {
            tigerBestScore = scoreManager.GetBestScore();
            bestScoreText.text = "Best Score: " + tigerBestScore;
        }
        else
        {
            Debug.LogError("ScoreManager를 찾을 수 없습니다.");
        }

        InitializeHearts();
        StartGame();
    }

    //void Update()
    //{
    //    if (!countdown.GetCountDownDone())
    //    {
    //        for (int i = 0; i < cards.Length; i++)
    //        {
    //            cards[i].GetComponent<Image>().sprite = tigerSprite;
    //        }
    //    }
    //}

    void InitializeHearts()
    {
        foreach (Transform child in heartContainer)
        {
            Destroy(child.gameObject);
        }
        heartImages.Clear();

        for (int i = 0; i < lives; i++)
        {
            GameObject heartObject = new GameObject("Heart" + i);
            heartObject.transform.SetParent(heartContainer);
            heartObject.transform.localScale = Vector3.one;

            Image heartImage = heartObject.AddComponent<Image>();
            heartImage.sprite = fullHeartSprite;
            heartImages.Add(heartImage);
        }
    }

    void StartGame()
    {
        spawnCoroutine = StartCoroutine(AnimalSpawnRoutine());
    }

    IEnumerator AnimalSpawnRoutine()
    {
        while (lives > 0)
        {
            AssignRandomAnimals();
            float timer = spawnInterval; // 타이머를 재배치 주기로 초기화

            while (timer > 0)
            {
                yield return null;
                timer -= Time.deltaTime; // 타이머 감소
                if (tigerCount == 0) break; // 호랑이 모두 클릭 시 바로 종료
            }

            if (tigerCount == 0)
            {
                spawnInterval = Mathf.Max(0.5f, spawnInterval - 0.1f); // 재배치 주기 감소
                originalInterval = spawnInterval; // 감소한 주기를 저장
            }
            else
            {
                spawnInterval = originalInterval; // 틀렸거나 시간이 끝나면 주기를 원래대로 복원
            }
        }
    }

    void AssignRandomAnimals()
    {
        // 모든 버튼의 이미지를 초기화
        foreach (var card in cards)
        {
            card.GetComponent<Image>().sprite = null;
            card.onClick.RemoveAllListeners(); // 기존 클릭 이벤트 제거
            card.interactable = true; // 버튼 다시 활성화
        }

        // 랜덤으로 호랑이, 다람쥐, 강아지 배치
        int[] indices = Enumerable.Range(0, cards.Length).OrderBy(x => Random.value).ToArray();
        tigerCount = 3;

        for (int i = 0; i < 3; i++)
        {
            int tigerIndex = indices[i];
            cards[tigerIndex].GetComponent<Image>().sprite = tigerSprite;
            cards[tigerIndex].onClick.AddListener(() => OnTigerClick(tigerIndex));
        }

        for (int i = 3; i < 6; i++)
        {
            int squirrelIndex = indices[i];
            cards[squirrelIndex].GetComponent<Image>().sprite = squirrelSprite;
            cards[squirrelIndex].onClick.AddListener(() => OnWrongClick());
        }

        for (int i = 6; i < 9; i++)
        {
            int dogIndex = indices[i];
            cards[dogIndex].GetComponent<Image>().sprite = dogSprite;
            cards[dogIndex].onClick.AddListener(() => OnWrongClick());
        }
    }

    [SerializeField] private Sprite clickedTigerSprite; // 클릭된 호랑이 이미지 추가

    void OnTigerClick(int index)
    {
        tigerCurrentScore += 10;
        tigerCount--;

        // 클릭된 호랑이 버튼의 이미지를 변경
        cards[index].GetComponent<Image>().sprite = clickedTigerSprite;

        // 클릭된 버튼을 다시 클릭하지 못하게 설정
        cards[index].interactable = false;

        if (tigerCount == 0)
        {
            StopCoroutine(spawnCoroutine); // 재배치 타이머 리셋
            spawnCoroutine = StartCoroutine(AnimalSpawnRoutine()); // 새로운 재배치 시작
        }

        UpdateUI();
    }


    void OnWrongClick()
    {
        lives--;
        UpdateLivesUI();

        if (lives == 0)
        {
            GameOver();
        }
        else
        {
            StopCoroutine(spawnCoroutine); // 재배치 타이머 리셋
            spawnInterval = originalInterval; // 주기를 원래대로 복구
            spawnCoroutine = StartCoroutine(AnimalSpawnRoutine()); // 새로운 재배치 시작
        }
    }

    void UpdateLivesUI()
    {
        for (int i = 0; i < heartImages.Count; i++)
        {
            heartImages[i].sprite = i < lives ? fullHeartSprite : emptyHeartSprite;
        }
    }

    void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + tigerCurrentScore; // UI에 점수 반영
        }
        else
        {
            Debug.LogError("ScoreText가 연결되지 않았습니다.");
        }
        Debug.Log("Score: " + tigerCurrentScore);
    }

    void GameOver()
    {

        //    StopCoroutine(spawnCoroutine);
        StopAllCoroutines();
        Debug.Log("Game Over! Final Score: " + tigerCurrentScore);
        gameEndPanel.SetActive(true);

        if (tigerCurrentScore > scoreManager.GetBestScore())
        {
            scoreManager.SetBestScore(tigerCurrentScore);
        }
    }
}
