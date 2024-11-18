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

    public int curScore;

    void Start()
    {
        StartCoroutine(CreateToy());
    }

    void Update()
    {
        curScoreText.text = "Score : " + curScore;
        if(curScore > PlayerPrefs.GetInt("RT_BestScore", 0)){
            PlayerPrefs.SetInt("RT_BestScore", curScore);
        }
        bestScoreText.text = "Best Score : " + PlayerPrefs.GetInt("RT_BestScore", 0);
    }

    IEnumerator CreateToy(){
        while(true){ //gameover를 플래그로 해야될지도?
            int ran = Random.Range(0, 3);
            Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(UnityEngine.Random.Range(0.05f, 0.95f), 1.1f, 10));
            pos.z = 0.0f;
            Instantiate(toys[ran], pos, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(0.4f,0.8f));
        }
    }
}
