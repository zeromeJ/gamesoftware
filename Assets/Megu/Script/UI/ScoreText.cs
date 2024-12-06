using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    TextMeshProUGUI scoreText;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] string key;

    private void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (scoreManager != null)
        {
            Debug.Log(scoreManager.GetBestScore(key));
            scoreText.text = scoreManager.GetBestScore(key).ToString();
        }
    }
}
