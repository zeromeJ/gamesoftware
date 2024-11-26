using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject resumePanel;
    public CountDown countDownObject;
    
    void Start()
    {
        //countDownObject = FindObjectOfType<CountDown>(); 비활성화된 오브젝트를 찾을 수 없어서 변경
        countDownObject = GameObject.Find("Canvas").transform.Find("CountDown").gameObject.GetComponent<CountDown>();
    }

    public void PauseClick(){
        if(!countDownObject.GetCountDownDone()){
            return;
        }
        countDownObject.GameStop();
        resumePanel.SetActive(true);
    }

    public void ResumeClick(){
        countDownObject.GameResume();
        resumePanel.SetActive(false);
    }
}
