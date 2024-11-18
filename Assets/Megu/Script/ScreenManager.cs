using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    ScreenOrientation orient;

    void Start()
    {
        orient = Screen.orientation;
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    private void OnDestroy()
    {
        Screen.orientation = orient;
    }
}