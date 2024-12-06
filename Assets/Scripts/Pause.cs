using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject resumePanel;
    public CountDown countDownObject;
    public static bool isResumePaused = false;
    
    void Start()
    {
        //countDownObject = FindObjectOfType<CountDown>(); 비활성화된 오브젝트를 찾을 수 없어서 변경
        countDownObject = GameObject.Find("Canvas").transform.Find("CountDown").gameObject.GetComponent<CountDown>();
    }

    public void PauseClick(){
        if(Time.timeScale == 0){
            return;
        }
        isResumePaused = true;
        countDownObject.GameStop();
        resumePanel.SetActive(true);
    }

    public void ResumeClick(){
        countDownObject.GameResume(true);
        resumePanel.SetActive(false);
    }
}
