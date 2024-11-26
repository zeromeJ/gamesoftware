using TMPro;
using UnityEngine;

public class ModalManager : MonoBehaviour
{
    // �̱���
    private static ModalManager _instance;
    public static ModalManager Instance 
    {
        get
        {
            return _instance;
        }
    }

    System.Action _OnClickConfrimButton; // Ȯ�� ��ư Ŭ�� �� �޼���
    System.Action _OnClickCancelButton; // ��� ��ư Ŭ�� �� �޼��� 

    GameObject _modal;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
            return;
        }
        else
        {
            _instance = this;
        }
    }

    public void Open(GameObject pannel, System.Action OnClickConfirmButton, System.Action OnClickCancelButton)
    {
        _modal = pannel;
        _modal.SetActive(true);
        _OnClickConfrimButton = OnClickConfirmButton;
        _OnClickCancelButton = OnClickCancelButton;
    }

    public void Close()
    {
        _modal.SetActive(false);
    }

    // ��ư�� On Click()�� �Ҵ� 
    public void OnClickConfirmButton()
    {
        if (_OnClickConfrimButton != null)
        {
            _OnClickConfrimButton();
        }
    }

    public void OnClickCancelButton()
    {
        if (_OnClickCancelButton != null)
        {
            _OnClickCancelButton();
        }
        Close();
        _modal = null;
    }

}