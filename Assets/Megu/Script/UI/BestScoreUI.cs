using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BestScoreUI : MonoBehaviour
{
    public GameManager manager;
    TextMeshProUGUI bestScoreText;

    private void Awake()
    {
        bestScoreText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        bestScoreText.text = "Best Score : " + manager.bestScore;
    }
}
