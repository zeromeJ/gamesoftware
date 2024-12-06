using UnityEngine;

public class TigerGame : MonoBehaviour
{
    // 스크립트
    public ModeController modeController; //모드 스크립트 
    public CountDown countdown; // Countdown 스트립트
    public UIUpdateController uIUpdateController; // UI스트립트
    public TigerCardsController tigerCardsController; // 
    public HeartController heartController;
    public FeverController feverController;
    public TigerBGMController tigerBGMController;

    public static bool isHardMode = false; // 모드 플래그

    // 점수 
    public static int TigerBestScore { get; set; }
    public static int TigerCurrentScore { get; set; }
    public static int tigerLife = 3;

    public static bool isFever = false;
    public static int afterFever;
    [SerializeField] private GameObject gameEndPanel; // 게임 종료 판넬

    void Start()
    {
        tigerBGMController.StartTigerGame();
        uIUpdateController.InitialScoreUI();
        modeController.SelectMode();
    }

    private void Update()
    {
        uIUpdateController.BlockClick(Pause.isResumePaused || CountDown.isCounting || isFever);
        //buttonContainer.SetActive(!CountDown.isCounting);
        tigerCardsController.SetStoppedFlag(Pause.isResumePaused);
        if (isFever)
        {
            tigerCardsController.SetStoppedFlag(true);
        }
    }

    public void InitializeGame(bool HardMode)
    {
        tigerCardsController.SetSprite(HardMode);
        ResetGame();
        countdown.GameResume(false);
        uIUpdateController.InitializeUI();
        // 카운트다운 완료 후 게임 시작 로직 연결
        countdown.OnCountdownComplete = StartTigerGame;
    }

    private void ResetGame()
    {
        // 게임 세팅 초기화
        tigerLife = 3;
        TigerCurrentScore = 0;
        afterFever = 0;
        UpdateUI();
    }

    public void StartTigerGame()
    {
        tigerCardsController.StartAnimalSpawnCoroutine();
    }
    
    public void ControlFeverObj(bool isFeverActive)
    {
        uIUpdateController.FeverUIControl(isFeverActive);
        tigerBGMController.DuringTigerFever(isFeverActive);
        if (!isFeverActive)
        {
            afterFever = TigerCurrentScore;
        }
        Debug.Log(" isFeverActive > " + isFeverActive);
    }

    public void CallFever()
    {
        ControlFeverObj(true);
        feverController.FeverTimer();
    }

    void UpdateUI()
    {
        uIUpdateController.UpdateUI(isHardMode, TigerCurrentScore);
        //Debug.Log("cur > " + tigerCurrentScore + " / afeter > " + afterFever + " / fever > " + feverScore);
    }

    public void GameOver()
    {
        tigerBGMController.OnEndingPannel();
        gameEndPanel.SetActive(true);
        StopAllCoroutines();
        Debug.Log("Game Over! Final Score: " + TigerCurrentScore);
    }
}