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
        countDownObject = FindObjectOfType<CountDown>();
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
