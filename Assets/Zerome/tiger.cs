using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class TigerGame : MonoBehaviour
{
    [SerializeField] private GameObject modeSelectPannel; // 모드 선택 판넬
    [SerializeField] private Button easyModeBtn; // 이지 모드 버튼
    [SerializeField] private Button hardModeBtn; // hard 모드 버튼
    private bool isHardMode = false; // 모드 플래그

    [SerializeField] private CountDown countdown; // Countdown 스트립트
    public CountDown countDownObject;
    [SerializeField] private GameObject gameEndPanel; // 게임 종료 판넬

    // 점수 
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] private GameObject scoreContainer;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private TextMeshProUGUI scoreText;
    public int tigerBestScore { get; set; }
    public int tigerCurrentScore { get; set; }

    // 게임 이미지
    [SerializeField] private GameObject buttonContainer; // 버튼 컨테이너 
    public Button[] cards; // 9개의 버튼

    [SerializeField] Sprite tigerSprite; // 호랑이 이미지
    [SerializeField] private Sprite[] spriteOptions; // 이미지 배열
    [SerializeField] Sprite squirrelSprite; // 다람쥐 이미지
    [SerializeField] Sprite dogSprite; // 강아지 이미지
    [SerializeField] Sprite rabbitSprite; // rabbit 이미지
    [SerializeField] Sprite catSprite; // cat 이미지
    [SerializeField] Sprite monkeySprite; // monkey 이미지
    private int spriteOptionsLength = 2; // easy

    // 하트 이미지 및 변수
    [SerializeField] private GameObject heartContainer; // heart 컨테이너
    [SerializeField] private GameObject brokenHeart2; // 깨진 하트 이미지
    [SerializeField] private GameObject brokenHeart1; // 깨진 하트 이미지 
    [SerializeField] private GameObject brokenHeart0; // 깨진 하트 이미지
    private int lives = 3;

    // 게임 요소 (모드에 따라 변경)
    private float spawnInterval = 3f; // 초기 주기
    private float originalInterval = 3f; // 원래의 주기
    private float reduceInterval = 0.1f; // 감소 주기 
    private int tigerCount = 3; // 현재 배치된 호랑이 수
    private int score = 10; // 맞췄을 때 점수 

    // 코루틴
    private Coroutine spawnCoroutine;

    //Fever
    public FeverController feverController;
    [SerializeField] private GameObject feverContainer; // fever 컨테이너
    private int feverScore;
    public static bool isFever = false;
    [SerializeField] private GameObject feverTimeObject;
    [SerializeField] public TextMeshProUGUI feverTimeText;

    // BGM
    [SerializeField] private AudioSource gameAudioSource; // BGM AudioSource
    [SerializeField] private AudioSource endingAudioSource; // BGM AudioSource

    void Start()
    {
        endingAudioSource.Stop();
        gameAudioSource.Play();
        gameAudioSource.pitch = 1.1f;

        if (scoreManager != null)
        {
            tigerBestScore = scoreManager.GetBestScore();
            bestScoreText.text = "최고 점수 : " + tigerBestScore;
        }
        else
        {
            Debug.LogError("ScoreManager를 찾을 수 없습니다.");
        }
        SelectMode();
    }

    private bool isPaused = false;
    public int bonusScore = 0;
    private void Update()
    {
        buttonContainer.SetActive(!CountDown.isCounting);
        isPaused = Pause.isResumePaused;
        isFever = ((tigerCurrentScore - bonusScore)!=0 &&(tigerCurrentScore - bonusScore) % feverScore == 0);
    }


    void SelectMode()
    {
        countdown.GameStop();
        modeSelectPannel.SetActive(true);
        easyModeBtn.onClick.AddListener(() => InitializeGame(false));
        hardModeBtn.onClick.AddListener(() => InitializeGame(true));
    }


    private int[] spriteUsageCounts; // 각 스프라이트의 사용 횟수 추적
    public void InitializeGame(bool HardMode)
    {
        spriteUsageCounts = new int[spriteOptions.Length]; // 스프라이트 개수만큼 초기화
        isHardMode = HardMode;

        if (isHardMode)
        {
            reduceInterval = 0.2f;
            score = 15;
            Debug.Log("clicked hard button");
        }
        else
        {
            reduceInterval = 0.1f;
            score = 10;
            Debug.Log("clicked easy button");
        }

        scoreContainer.SetActive(true);
        ResetGame();
        countdown.GameResume(false);
        heartContainer.SetActive(true);

        // 카운트다운 완료 후 게임 시작 로직 연결
        countdown.OnCountdownComplete = StartGame;
    }

    private void ResetGame()
    {
        // 게임 세팅 초기화
        lives = 3;
        tigerCurrentScore = 0;
        feverScore = isHardMode ? 150 : 50;
        UpdateUI();
    }

    void StartGame()
    {
        buttonContainer.SetActive(true);
        //InitializeHearts();
        //spawnCoroutine = StartCoroutine(AnimalSpawnRoutine(false));
        StartAnimalSpawnCoroutine();
    }

    public void StartAnimalSpawnCoroutine()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine); // 기존 코루틴 중지
        }
        spawnCoroutine = StartCoroutine(AnimalSpawnRoutine(isFever));
    }

    public void StopAnimalSpawnCoroutine()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine); // 코루틴 중지
            spawnCoroutine = null;
        }
    }

    IEnumerator AnimalSpawnRoutine(bool isFever)
    {
        while (lives > 0)
        {
            if (isFever) AssignTiger();
            else AssignRandomAnimals();

            float timer = spawnInterval; // 타이머를 재배치 주기로 초기화


            while (timer > 0)
            {
                //Debug.Log($"isPaused: {isPaused}, isFever: {isFever}");
                while (isPaused || isFever)
                {
                    if (isPaused) Debug.Log(" -- 멈춤 -- ");
                    if (isFever) Debug.Log(" -- 피버로 멈춤 -- ");
                    yield return null; // 멈춘 상태 유지
                }

                yield return null;
                timer -= Time.deltaTime; // 타이머 감소
                if (tigerCount == 1) timer += 2 * Time.deltaTime;
                if (tigerCount == 0) break; // 호랑이 모두 클릭 시 바로 종료
            }

            if (timer <= 0)
            {
                // spawnInterval 감소
                spawnInterval = Mathf.Max(0.5f, spawnInterval - reduceInterval);
                Debug.Log("걍 넘어~~~" + spawnInterval);

                // 코루틴 재시작
                StartAnimalSpawnCoroutine();
                yield break; // 현재 코루틴 종료
            }
        }
    }

    Sprite SelectRandomSprite()
    {
        Sprite randomSprite = null;
        int safetyCounter = 0; // 무한 루프 방지용

        int counter = isHardMode ? 2 : 3;

        while (randomSprite == null && safetyCounter < 100)
        {
            int randomIndex = Random.Range(0, spriteOptionsLength); // 랜덤 인덱스 선택

            if (spriteUsageCounts[randomIndex] < counter)
            {
                randomSprite = spriteOptions[randomIndex]; // 스프라이트 선택
                spriteUsageCounts[randomIndex]++; // 선택된 스프라이트의 사용 횟수 증가
            }

            safetyCounter++;
        }

        if (randomSprite == null)
        {
            Debug.LogWarning("Failed to find a valid sprite within limits!");
        }

        return randomSprite;
    }


    public void AssignRandomAnimals()
    {

        Debug.Log(" RANDOM FUNC CALLED ");
        spriteUsageCounts = new int[spriteOptions.Length]; // 스프라이트 개수만큼 초기화

        // 모든 버튼 초기화
        foreach (var card in cards)
        {
            card.GetComponent<Image>().sprite = null;
            card.onClick.RemoveAllListeners();
            card.interactable = true;
        }

        // 랜덤 배치
        int[] indices = Enumerable.Range(0, cards.Length).OrderBy(x => Random.value).ToArray();
        if (isHardMode)
            {
                tigerCount = 2; // Hard 모드에서는 호랑이 4마리
                spriteOptionsLength = 5;
            }
        else
        {
            tigerCount = 3; // Easy 모드에서는 호랑이 3마리
            spriteOptionsLength = 2;
        }
        

        // 호랑이 배치
        for (int i = 0; i < tigerCount; i++)
        {
            int tigerIndex = indices[i];
            cards[tigerIndex].GetComponent<Image>().sprite = tigerSprite; // 호랑이 이미지
            cards[tigerIndex].onClick.AddListener(() => OnTigerClick(tigerIndex, false));
        }

        // 다른 스프라이트 배치
        for (int i = tigerCount; i < cards.Length; i++)
        {
            int otherIndex = indices[i];

            // 중복 방지된 랜덤 스프라이트 선택
            Sprite randomSprite = SelectRandomSprite();
            cards[otherIndex].GetComponent<Image>().sprite = randomSprite;
            //Debug.Log("Assigned sprite: " + randomSprite.name);

            cards[otherIndex].onClick.AddListener(OnWrongClick);
        }
    }

    public void AssignTiger()
    {

        Debug.Log(" TIGER FEVER CALLED ");
        // 모든 버튼 초기화
        foreach (var card in cards)
        {
            card.GetComponent<Image>().sprite = null;
            card.onClick.RemoveAllListeners();
            card.interactable = true;
        }

        tigerCount = 9;

        // 호랑이 배치
        for (int i = 0; i < tigerCount; i++)
        {
            int tigerIndex = i;
            cards[tigerIndex].GetComponent<Image>().sprite = tigerSprite; // 호랑이 이미지
            cards[tigerIndex].onClick.AddListener(() => OnTigerClick(tigerIndex, true));
        }
    }


    [SerializeField] private Sprite clickedTigerSprite; // 클릭된 호랑이 이미지 추가

    public void OnTigerClick(int index, bool feverTiger)
    {
        tigerCurrentScore += score;
        tigerCount--;

        // 클릭된 호랑이 버튼의 이미지를 변경
        cards[index].GetComponent<Image>().sprite = clickedTigerSprite;

        // 클릭된 버튼을 다시 클릭하지 못하게 설정
        if (!feverTiger) cards[index].interactable = false;

        if (tigerCount == 0)
        {
            StopCoroutine(spawnCoroutine); // 재배치 타이머 리셋
            spawnInterval = Mathf.Max(0.5f, spawnInterval - reduceInterval); // 재배치 주기 감소
            originalInterval = spawnInterval; // 감소한 주기를 저장
            Debug.Log(" -- 다 맞춰서 주기 감소 -- " + originalInterval);
            StartAnimalSpawnCoroutine();

        }

        UpdateUI();
    }


    void OnWrongClick()
    {
        lives--;
        UpdateLivesUI();

        if (lives == 0) GameOver();
        
        else
        {
            StopCoroutine(spawnCoroutine); // 재배치 타이머 리셋
            spawnInterval = originalInterval; // 주기를 원래대로 복구
            Debug.Log("< 틀려싿 > " + spawnInterval);
            //spawnCoroutine = StartCoroutine(AnimalSpawnRoutine(false)); // 새로운 재배치 시작
            //AnimalSpawnRoutine(false);
            StartAnimalSpawnCoroutine();
        }
    }

    public void ControlFeverObj(bool isFeverActive)
    {
        if (!isFeverActive)
        {
            isFever = false;
            bonusScore = tigerCurrentScore;
        }
        Debug.Log(" bonusScore > " + bonusScore);

        heartContainer.SetActive(!isFeverActive);
        feverTimeObject.SetActive(isFeverActive);
        gameAudioSource.pitch = isFeverActive ? 2f : 1f;
    }

    void UpdateLivesUI()
    {
        if (lives == 2) brokenHeart2.SetActive(true);
        if (lives == 1) brokenHeart1.SetActive(true);
        if (lives == 0) brokenHeart0.SetActive(true);
    }

    void UpdateUI()
    {
        if (scoreText != null)
        {
            if (isHardMode) scoreText.text = "(하드) 점수 : " + tigerCurrentScore; // UI에 점수 반영
            else scoreText.text = "(이지) 점수 : " + tigerCurrentScore; // UI에 점수 반영
            
            if (tigerCurrentScore > tigerBestScore) bestScoreText.text = "최고 점수 : " + tigerCurrentScore;
            
        }
        else
        {
            Debug.LogError("ScoreText가 연결되지 않았습니다.");
        }

        if (tigerCurrentScore > 0 && (tigerCurrentScore - bonusScore) % feverScore == 0 && !isFever)
        {
            isFever = true;
            Debug.Log("FEEEVEeeeeeer");
            StopAllCoroutines();
            callFever();
        }
        //Debug.Log("Current Score: " + tigerCurrentScore);
    }

    void callFever()
    {
        ControlFeverObj(true);

        feverController.FeverTimer();
    }

    void GameOver()
    {
        gameAudioSource.Stop();
        endingAudioSource.Play();
        StopAllCoroutines();
        Debug.Log("Game Over! Final Score: " + tigerCurrentScore);
        gameEndPanel.SetActive(true);

        if (tigerCurrentScore > scoreManager.GetBestScore())
        {
            scoreManager.SetBestScore(tigerCurrentScore);
        }
    }
}