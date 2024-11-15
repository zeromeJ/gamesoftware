using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject resumePanel;
    [SerializeField] Image fadeImage;
    CountDown countDownObject;
    
    // Start is called before the first frame update
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
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 0.5f);
    }

    public void ResumeClick(){
        countDownObject.GameResume();
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 0f);
        resumePanel.SetActive(false);
    }
}
