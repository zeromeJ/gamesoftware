using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FeverController : MonoBehaviour
{
    public TigerGame tigerObject;
    public UIUpdateController UIUpdateController;

    [SerializeField] private GameObject feverButtonContainer; // fever 버튼 컨테이너 
    public Button[] feverCards; // 9개의 버튼

    int afterFeverScore;
    int duringFever; // 피버 시 추가 점수 

    private Coroutine feverCoroutine;

    public void FeverTimer()
    {
        FeverUIControl(true);
        afterFeverScore = TigerGame.TigerCurrentScore;
        duringFever = TigerGame.isHardMode ? 25 : 20;
        Debug.Log(" fever timer on ");
        feverCoroutine = StartCoroutine(FiveSecondTimer());
    }

    void FeverUIControl(bool isStarted)
    {
        feverButtonContainer.SetActive(isStarted);
        feverButtonContainer.SetActive(isStarted);
    }

    public VibrationController vibrationController;
    private IEnumerator FiveSecondTimer()
    {
        Debug.Log("Fever started!"); // 타이머 시작 로그
        FeverModeCards();
        for (int i = 6; i > 0; i--)
        {
            UIUpdateController.FeverUpdateUI(i - 1);
            Debug.Log($"Timer: {i}"); // 디버그 출력
            yield return new WaitForSeconds(1f); // 1초 대기
        }
        //vibrationController.Vibrate(500);
        Debug.Log("Fever completed!");
        FeverTimeComplete(); // after fever mode, return current score
        Debug.Log("Fever score > " + TigerGame.afterFever);
        yield break; // 코루틴 종료
    }

    void FeverModeCards()
    {
        // feverMode card assign
        Debug.Log(" TIGER FEVER CALLED ");

        // 모든 버튼 초기화
        foreach (var card in feverCards)
        {
            card.onClick.RemoveAllListeners();
            card.interactable = true;
        }

        // onClick Event
        for (int i = 0; i < 9; i++) feverCards[i].onClick.AddListener(() => FeverOnClick());
    }

    void FeverOnClick()
    {
        TigerGame.TigerCurrentScore += duringFever;
        afterFeverScore = TigerGame.TigerCurrentScore;
        UIUpdateController.UpdateUI(TigerGame.isHardMode, afterFeverScore);
    }
    public void StopFeverSpawnCoroutine()
    {
        if (feverCoroutine != null)
        {
            StopCoroutine(feverCoroutine); // 코루틴 중지
            feverCoroutine = null;
        }
    }
    private void FeverTimeComplete()
    {
        FeverUIControl(false);
        Debug.Log(" count down Complete ");
        TigerGame.afterFever = TigerGame.TigerCurrentScore;
        tigerObject.ControlFeverObj(false);
        UIUpdateController.BlockClick(false);
        StopFeverSpawnCoroutine();
        tigerObject.StartTigerGame();
        TigerGame.isFever = false; // 피버 타임 종료 후 플래그 초기화
    }
}
