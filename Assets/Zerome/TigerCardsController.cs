using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class TigerCardsController : MonoBehaviour
{
    public UIUpdateController uIUpdateController;
    public HeartController heartController;
    public TigerGame tigerGame; // TigerGame controller
    private Coroutine spawnCoroutine; // 코루틴

    // 게임 이미지
    public Button[] cards; // 9개의 버튼
    [SerializeField] Sprite tigerSprite; // 호랑이 이미지
    [SerializeField] private Sprite clickedTigerSprite; // 클릭된 호랑이 이미지 추가
    [SerializeField] private Sprite[] tigerSpriteOptions; // 이미지 배열

    public int spriteOptionsLength; // easy
    public int[] spriteUsageCounts; // 각 스프라이트의 사용 횟수 추적

    public bool isStopped = false;


    float spawnInterval = 3f; // 초기 주기
    float originalInterval = 3f; // 원래의 주기
    float reduceInterval = 0.1f; // 감소 주기 
    int tigerCount; // 현재 배치된 호랑이 수
    int score; // 맞췄을 때 점수

    public void SetSprite(bool isHard)
    {
        Debug.Log("setting");
        spriteUsageCounts = new int[tigerSpriteOptions.Length]; // 스프라이트 개수만큼 초기화
        reduceInterval = isHard ? 0.2f : 0.1f;
        score = isHard ? 15 : 10;
    }

    public void StartAnimalSpawnCoroutine()
    {
        if (spawnCoroutine != null) StopCoroutine(spawnCoroutine); // 기존 코루틴 중지
        spawnCoroutine = StartCoroutine(AnimalSpawnRoutine()); // new coroutine
    }

    public void StopAnimalSpawnCoroutine()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine); // 코루틴 중지
            spawnCoroutine = null;
        }
    }

    public void SetStoppedFlag(bool stopped)
    {
        isStopped = stopped; // 플래그 업데이트
    }

    IEnumerator AnimalSpawnRoutine()
    {
        while (TigerGame.tigerLife > 0)
        {
            AssignRandomAnimals();
            float timer = spawnInterval; // 타이머를 재배치 주기로 초기화

            while (isStopped)
            {
                Debug.Log("random 코루틴 일시정지...");
                yield return null; // 상태가 변경될 때까지 대기
            }

            while (timer > 0)
            { 
                yield return null;
                timer -= Time.deltaTime; // 타이머 감소
                if (tigerCount == 1) timer += 2 * Time.deltaTime;
                if (tigerCount == 0) break; // 호랑이 모두 클릭 시 바로 종료
            }

            if (timer <= 0)
            {
                // spawnInterval 감소 
                spawnInterval = Mathf.Max(0.5f, spawnInterval - reduceInterval);
                //Debug.Log("걍 넘어~~~" + spawnInterval);
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

        int counter = TigerGame.isHardMode ? 2 : 3;

        while (randomSprite == null && safetyCounter < 100)
        {
            int randomIndex = Random.Range(0, spriteOptionsLength); // 배열 크기와 동기화
            //Debug.Log("random > " + spriteUsageCounts[randomIndex]);
            if (spriteUsageCounts[randomIndex] < counter)
            {
                randomSprite = tigerSpriteOptions[randomIndex]; // 스프라이트 선택
                spriteUsageCounts[randomIndex]++; // 선택된 스프라이트의 사용 횟수 증가
            }
            safetyCounter++;
        }

        if (randomSprite == null) Debug.LogWarning("Failed to find a valid sprite within limits!");
        return randomSprite;
    }

    public void AssignRandomAnimals()
    {
        //Debug.Log(" RANDOM FUNC CALLED ");
        spriteUsageCounts = new int[tigerSpriteOptions.Length]; // 스프라이트 개수만큼 초기화

        // 모든 버튼 초기화
        foreach (var card in cards)
        {
            card.GetComponent<Image>().sprite = null;
            card.onClick.RemoveAllListeners();
            card.interactable = true;
        }

        // 랜덤 배치
        int[] indices = Enumerable.Range(0, cards.Length).OrderBy(x => Random.value).ToArray();
        if (TigerGame.isHardMode)
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
            cards[tigerIndex].onClick.AddListener(() => OnTigerClick(tigerIndex));
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

    //int feverScore = TigerGame.isHardMode ? 150 : 200;
    //public void CheckFever()
    //{

    //    int check = TigerGame.TigerCurrentScore - TigerGame.afterFever;
    //    if (check != 0 && check % feverScore == 0 && !TigerGame.isFever)
    //    {
    //        Debug.Log("fever check >>> " + check);
    //        TigerGame.isFever = true;
    //        tigerGame.CallFever();
    //    }
    //    //Debug.Log(">>> " + check);
    //}

    public void OnTigerClick(int index)
    {
        TigerGame.TigerCurrentScore += score;
        tigerCount--;

        // 클릭된 호랑이 버튼의 이미지를 변경
        cards[index].GetComponent<Image>().sprite = clickedTigerSprite;
        //Debug.Log("clicked" + index);
        // 클릭된 버튼을 다시 클릭하지 못하게 설정

        if (tigerCount == 0) ReCallCoroutine();
        //CheckFever();
        if (TigerGame.TigerCurrentScore - TigerGame.afterFever == score*20 )
        {
            Debug.Log("clicked ㄹㄷㅍㄷ" + TigerGame.TigerCurrentScore);
            TigerGame.isFever = true;
            tigerGame.CallFever();
        }
        uIUpdateController.UpdateUI(TigerGame.isHardMode,TigerGame.TigerCurrentScore);
    }

    void ReCallCoroutine()
    {
        StopCoroutine(spawnCoroutine); // 재배치 타이머 리셋
        spawnInterval = Mathf.Max(0.7f, spawnInterval - reduceInterval); // 재배치 주기 감소
        originalInterval = spawnInterval; // 감소한 주기를 저장
        //Debug.Log(" -- 다 맞춰서 주기 감소 -- " + TigerGame.originalInterval);
        StartAnimalSpawnCoroutine();
    }

    public VibrationController vibrationController;
    [SerializeField] private AudioSource effectGetWrong;
    [SerializeField] private GameObject redBG;
    int curLife = 3;
    void OnWrongClick()
    {
        effectGetWrong.Play();
        StartCoroutine(ToggleBG());
        TigerGame.tigerLife--;
        curLife = TigerGame.tigerLife;
        if (curLife <= 0) tigerGame.GameOver();
        heartController.ControllLivesUI(TigerGame.tigerLife);
        StopCoroutine(spawnCoroutine); // 재배치 타이머 리셋
        spawnInterval = originalInterval; // 주기를 원래대로 복구
        Debug.Log("< 틀리 > " + spawnInterval);

        StartAnimalSpawnCoroutine();
    }

    private IEnumerator ToggleBG()
    {
        redBG.SetActive(true);
        //vibrationController.Vibrate(200);
        yield return new WaitForSeconds(0.18f);
        redBG.SetActive(false);
    }

}
