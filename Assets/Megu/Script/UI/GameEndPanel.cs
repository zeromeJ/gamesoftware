using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEndPanel : MonoBehaviour
{
    public void RestartClick()
    {
        gameObject.SetActive(false);
        Debug.Log("reeee");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
