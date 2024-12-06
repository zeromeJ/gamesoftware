using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableUI : MonoBehaviour
{
    [SerializeField] GameObject gameEndPanel;
    [SerializeField] GameObject resumePanel;
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject currentScore;
    [SerializeField] GameObject bestScore;

    void Update()
    {
        if(gameEndPanel.activeSelf)
        {
            currentScore.SetActive(false);
            bestScore.SetActive(false);
            pauseButton.SetActive(false);
        }

        if(resumePanel.activeSelf)
        {
            currentScore.SetActive(false);
            bestScore.SetActive(false);
            pauseButton.SetActive(false);
        }
        else 
        {
            currentScore.SetActive(true);
            bestScore.SetActive(true);
            pauseButton.SetActive(true);
        }
    }
}
