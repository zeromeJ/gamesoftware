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
    public static bool countdownDone; // 타이머가 끝났는지 여부, 다른 곳에서도 공유하기 위해 스태틱으로 선언, Get함수로 사용 가능

    TextMeshProUGUI text;
    float gameTimeMultiplier = 1f; //기존 게임 배속 저장하는 변수
    int countDownTime = 3;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        StopAndGo();
    }

    public void StopAndGo(){
        GameStop();
        GameResume();
    }

    public void GameStop(){
        gameTimeMultiplier = Time.timeScale;
        Time.timeScale = 0;
    }

    public void GameResume(){
        countDownTime = 3;
        CountStart();
    }

    public void ResetTimeScale(){
        Time.timeScale = gameTimeMultiplier;
    }

    public bool GetCountDownDone(){
        return countdownDone;
    }

    public void CountStart(){
        countdownDone = false;
        this.gameObject.SetActive(true);
        text.text = countDownTime.ToString();
        StartCoroutine(CountDownStart());
    }

    IEnumerator CountDownStart(){
        while(true){
            yield return new WaitForSecondsRealtime(1f);
            countDownTime -= 1;

            if(countDownTime == 0){
                text.text = "Game Start!";
                yield return new WaitForSecondsRealtime(1f);
                countdownDone = true;
                ResetTimeScale();
                this.gameObject.SetActive(false);
                yield return null;
            }

            text.text = countDownTime.ToString();
        }
    }
}
