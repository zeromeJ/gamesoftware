using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageName : MonoBehaviour
{
    TextMeshProUGUI stageName;
    [SerializeField] string key;
    private void Awake()
    {
        stageName = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        if (stageName != null)
        {
            stageName.text = key;
        }

        Debug.Log(key);
    }

    private void Update()
    {
        
    }
}
