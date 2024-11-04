using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// 화면 중앙 상단에 타이머를 만드는 기능
// Timer 프리팹을 캔버스에 넣은 후 시작하면 작동
public class Timer : MonoBehaviour
{
    public static bool timerIsDone; // 타이머가 끝났는지 여부, 다른 곳에서도 사용하기 편하게 스태틱으로 선언
    [SerializeField] float maxTime; // 제한시간
    [SerializeField] GameObject restartPanel;

    TextMeshProUGUI timerText;
    float remainTime;
    CountDown countDown;

    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        countDown = FindObjectOfType<CountDown>();
        GameStart();
    }


    void Update()
    {
        if(countDown.GetCountDownDone()){
            countDown.ResetTimeScale();
        }
        if(!timerIsDone){
            remainTime -= Time.deltaTime;
            timerText.text = Mathf.RoundToInt(remainTime).ToString();
        }
        if(remainTime <= 0){
            GameOver();
        }        
    }

    void GameStart(){
        remainTime = maxTime;
        timerIsDone = false;
    }

    void GameOver(){
        timerIsDone = true;
        countDown.GameStop();
        restartPanel.SetActive(true);
        //timescale로 멈추거나 작동 중이던 함수 정지 후 모달 띄우기
    }

    public void ReStart(){
        remainTime = maxTime;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public bool GetTimerIsDone(){
        return timerIsDone;
    }
}
