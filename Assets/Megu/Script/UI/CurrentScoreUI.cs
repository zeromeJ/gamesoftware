using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrentScoreUI : MonoBehaviour
{
    public GameManager manager;
    TextMeshProUGUI CurScoreText;

    private void Awake()
    {
        CurScoreText = GetComponent<TextMeshProUGUI>();
        CurScoreText.text = "현재 점수 : 0";
    }

    private void Update()
    {
        CurScoreText.text = "현재 점수 : " + manager.currentScore;
    }
}
