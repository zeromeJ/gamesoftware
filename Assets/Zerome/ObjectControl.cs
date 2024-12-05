using UnityEngine;
using System.Collections;

public class ObjectControl : MonoBehaviour
{
    [SerializeField] private GameObject buttonContainer; // 버튼 컨테이너 

    public void buttonContainerController(bool control)
    {
        buttonContainer.SetActive(control);
    }
}
