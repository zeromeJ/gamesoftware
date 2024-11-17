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
        CurScoreText.text = "Current Score : 0";
    }

    private void Update()
    {
        CurScoreText.text = "Current Score : " + manager.currentScore;
    }
}
