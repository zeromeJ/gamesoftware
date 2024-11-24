using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableUI : MonoBehaviour
{
    [SerializeField] GameObject gameEndPanel;
    void Update()
    {
        if(gameEndPanel.activeSelf)
        {
            this.gameObject.SetActive(false);
        }
    }
}
