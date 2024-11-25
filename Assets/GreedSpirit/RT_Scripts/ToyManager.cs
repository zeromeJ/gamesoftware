using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToyManager : MonoBehaviour
{
    private static ToyManager _instance;
    public static ToyManager Instance{
        get{
            if(_instance == null){
                _instance = FindObjectOfType<ToyManager>();
            }
            return _instance;
        }
    }

    [SerializeField] GameObject[] toys;
    [SerializeField] TextMeshProUGUI curScoreText;
    [SerializeField] TextMeshProUGUI bestScoreText;
    [SerializeField] ScoreManager scoreManager;

    public int curScore;

    void Start()
    {
        StartCoroutine(CreateToy());
    }

    void Update()
    {
        curScoreText.text = "현재 점수 : " + curScore;
        if(curScore > scoreManager.GetBestScore()){
            scoreManager.SetBestScore(curScore);
        }
        bestScoreText.text = "최고 점수 : " + scoreManager.GetBestScore();
    }

    IEnumerator CreateToy(){
        while(true){
            int ran = Random.Range(0, 3);
            Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(UnityEngine.Random.Range(0.05f, 0.95f), 1.1f, 10));
            pos.z = 0.0f;
            Instantiate(toys[ran], pos, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(0.4f,0.8f));
        }
    }
}
