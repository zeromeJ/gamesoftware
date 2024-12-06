using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// 화면 중앙 상단에 타이머를 만드는 기능
// Timer 프리팹을 캔버스에 넣은 후 시작하면 작동
public class Timer : MonoBehaviour
{
    private static bool timerIsDone; // 타이머가 끝났는지 여부, 다른 곳에서도 공유하기 위해 스태틱으로 선언
    [SerializeField] float maxTime; // 제한시간
    [SerializeField] GameObject restartPanel;

    TextMeshProUGUI timerText;
    float remainTime;
    CountDown countDown;

    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        countDown = FindAnyObjectByType<CountDown>();
        GameStart();
    }


    void Update()
    {
        /*if(countDown.GetCountDownDone()){
            countDown.ResetTimeScale();
        }*/
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
        //countDown.GameStop();
        Time.timeScale = 0;
        restartPanel.SetActive(true);
    }

    public void ReStart(){
        remainTime = maxTime;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public bool GetTimerIsDone(){
        return timerIsDone;
    }
}
