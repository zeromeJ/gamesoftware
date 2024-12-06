using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] ScreenOrientation orient;

    private void Awake()
    {
        Screen.orientation = orient;
    }
}