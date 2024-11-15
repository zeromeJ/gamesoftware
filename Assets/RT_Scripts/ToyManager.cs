using System.Collections;
using System.Collections.Generic;
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

    void Start()
    {
        StartCoroutine(CreateToy());
    }

    void Update()
    {
        
    }

    IEnumerator CreateToy(){
        while(true){ //gameover를 플래그로 해야될지도?
            int ran = Random.Range(0, 3);
            Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(UnityEngine.Random.Range(0.05f, 0.95f), 1.1f, 10));
            pos.z = 0.0f;
            Instantiate(toys[ran], pos, Quaternion.identity);
            yield return new WaitForSeconds(1f);
        }
    }
}
