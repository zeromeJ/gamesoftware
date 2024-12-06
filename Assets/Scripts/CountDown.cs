using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//일시정지 혹은 게임 시작 혹은 다시 시작할 때 3초의 유예 기간을 주고 시작하게 만드는 코드
//CountDown 프리팹을 Canvas에 넣고 게임을 시작하거나 Restart 버튼에 StopAndGo를 할당하여 사용가능
//일시 정지 버튼에 GameStop을 재개 버튼에 GameResume을 사용하여 정지 재개도 가능

public class CountDown : MonoBehaviour
{
    private static bool countdownDone; // 타이머가 끝났는지 여부, 다른 곳에서도 공유하기 위해 스태틱으로 선언

    [SerializeField] public int countdownTime = 3; // 시간 설정

    TextMeshProUGUI text;
    float gameTimeMultiplier = 1f; //게임 배속

    int curCount;

    public System.Action OnCountdownComplete; // 카운트다운 완료 시 실행될 콜백

    public bool isResume = false;

    void Start()
    {
        //isResume = false;
        //Debug.Log("before key >> " + isResume);
        text = GetComponent<TextMeshProUGUI>();
        StopAndGo();
    }

    public void StopAndGo(){
        GameStop();
        //isResume = false;
        //Debug.Log("ing key >> " + isResume);
        GameResume(false);
    }

    public void GameStop(){
        //countdownDone = false;
        Time.timeScale = 0;
    }

    public void GameResume(bool isResumeGR)
    {
        isResume = isResumeGR;
        curCount = countdownTime; // 타이머 값 초기화
        text.text = curCount.ToString(); // UI 갱신
        CountStart(isResumeGR);
    }


    public void ResetTimeScale(){
        Time.timeScale = gameTimeMultiplier;
    }

    public bool GetCountDownDone(){
        return countdownDone;
    }

    public static bool isCounting = false; // 중복 방지 플래그

    public void CountStart(bool isResumeCS)
    {
        if (isCounting) return; // 이미 카운트다운 중이면 무시
        isCounting = true;

        this.gameObject.SetActive(true);
        text.text = curCount.ToString();
        StartCoroutine(CountDownStart(isResumeCS));
        isResume = false;
        //Debug.Log("after key >> " + isResume);

    }

    IEnumerator CountDownStart(bool isResumeS)
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(1f);
            curCount--;

            if (curCount <= 0)
            {
                text.text = isResumeS ? "다시집중!" : "게임시작!" ;
                yield return new WaitForSecondsRealtime(1f);
                countdownDone = true;
                Pause.isResumePaused = false;
                Debug.Log(" 게임 재개 ");
                ResetTimeScale();
                this.gameObject.SetActive(false);
                isCounting = false; // 완료 후 플래그 해제
                OnCountdownComplete?.Invoke();
                yield break; // 코루틴 종료
            }

            text.text = curCount.ToString();
        }
    }
}
