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

    System.Action _OnClickConfrimButton; // 확인 버튼 클릭
    System.Action _OnClickCancelButton; // 취소 버튼 클릭

    public GameObject _modal;
    public TMP_Text _modalMsg;

    private void Awake()
    {
        _modal.SetActive(false);
        DontDestroyOnLoad(this); // 씬 전환 시 파괴되지 않도록 
        _instance = this;
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
        Close();
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