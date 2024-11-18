using TMPro;
using UnityEngine;

public class ModalManager : MonoBehaviour
{
    // 싱글톤
    private static ModalManager _instance;
    public static ModalManager Instance 
    {
        get
        {
            return _instance;
        }
    }

    System.Action _OnClickConfrimButton; // 확인 버튼 클릭 시 메서드
    System.Action _OnClickCancelButton; // 취소 버튼 클릭 시 메서드 

    public GameObject _modal;
    public TMP_Text _modalMsg;

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
            _modal.SetActive(false);
        }
    }

    public void Open(string msg, System.Action OnClickConfirmButton, System.Action OnClickCancelButton)
    {
        _modal.SetActive(true);
        _modalMsg.text = msg;
        _OnClickConfrimButton = OnClickConfirmButton;
        _OnClickCancelButton = OnClickCancelButton;
    }

    public void Close()
    {
        _modal.SetActive(false);
    }

    // 버튼에 On Click()에 할당 
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
    }

}