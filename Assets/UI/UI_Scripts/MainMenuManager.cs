using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] PlayerInfo playerInfo;
    [SerializeField] List<CharacterData> characterDatas;
    
    [SerializeField] Image profileImage;
    [SerializeField] Image characterImage;
    [SerializeField] TMP_Text nicknameText;
    [SerializeField] TMP_InputField nicknameInputField;
    [SerializeField] TMP_Text nicknameWarningText;
    [SerializeField] GameObject profileModalPannel;
    [SerializeField] GameObject stage1StartModalPannel;
    [SerializeField] GameObject stage2StartModalPannel;
    [SerializeField] GameObject stage3StartModalPannel;
    [SerializeField] AudioSource clickEffect;

    string _nickname;
    int _characterID;

    private void Start()
    {
        CheckHasLoggedIn();
        characterImage.sprite = characterDatas[_characterID].CharacterSprite;
#if UNITY_WEBGL && !UNITY_EDITOR
    WebGLInput.mobileKeyboardSupport = true;
#endif
    }

    void CheckHasLoggedIn()
    {
        if (!playerInfo.LoadHasLoggedIn())
        {
            OnClickOpenProfile();
        }
        
        UpdateProfileModal();
    }

    // 프로필 수정 모달 열기 
    public void OnClickOpenProfile()
    {
        nicknameWarningText.enabled = false;
        nicknameInputField.text = "";
        characterImage.sprite = characterDatas[playerInfo.CharacterID].CharacterSprite;
        ModalManager.Instance.Open(profileModalPannel,
            OnClickConfirmButton: () =>
            {
                ProfileConfirmButton();
            }, OnClickCancelButton: () =>
            {
                ProfileCancelButton();
            }
        );
    }
    
    // 프로필 수정 확인 버튼 클릭 
    void ProfileConfirmButton()
    {
        
        if (CheckValidNickname())
        {
            _nickname = nicknameInputField.text;
            playerInfo.Nickname = _nickname;
            playerInfo.CharacterID = _characterID;
            UpdateProfileModal();

            ModalManager.Instance.Close();

            if (!playerInfo.LoadHasLoggedIn())
            {
                playerInfo.SaveLoggedIn();
            }
        }
    }
    
    bool CheckValidNickname()
    {
        if (nicknameInputField.text.Length == 0 || 
            nicknameInputField.text.Length > 5)
        {
            nicknameWarningText.enabled = true;
            return false;
        }
        return true;
    }

    void ProfileCancelButton()
    {
        if ( !playerInfo.LoadHasLoggedIn() && (_nickname == null || CheckValidNickname() ))
        {
            nicknameWarningText.enabled = true;
        }
        else if ( playerInfo.LoadHasLoggedIn())
        {
            ModalManager.Instance.Close();
            ModalManager.Instance._modal = null;
        }
    }


    // PlayerInfo.cs 갱신 
    void UpdateNicknameModal()
    {
        _nickname = playerInfo.Nickname;
        nicknameText.text = _nickname;
    }
    void UpdateCharacterIDModal()
    {
        if( _characterID < 0 || _characterID > characterDatas.Count)
        {
            _characterID = 0;
        }
        else
        {
            _characterID = playerInfo.CharacterID;
        }
        if (characterDatas.Count > 0)
        {
            profileImage.sprite = characterDatas[_characterID].CharacterSprite;
        }
    }
    void UpdateProfileModal()
    {
        UpdateNicknameModal();
        UpdateCharacterIDModal();
    }

    // stage1~3 모달 열기  
    public void OnClickOpenStage1()
    {
        ModalManager.Instance.Open(stage1StartModalPannel,
            OnClickConfirmButton: () =>
            {
                ModalManager.Instance.Close();
            }, OnClickCancelButton: () =>
            {
                ModalManager.Instance.Close();
                ModalManager.Instance._modal = null;
            }
        );
    }

    public void OnClickOpenStage2()
    {
        ModalManager.Instance.Open(stage2StartModalPannel,
            OnClickConfirmButton: () =>
            {
                ModalManager.Instance.Close();
            }, OnClickCancelButton: () =>
            {
                ModalManager.Instance.Close();
                ModalManager.Instance._modal = null;
            }
        );
    }

    public void OnClickOpenStage3()
    {
        ModalManager.Instance.Open(stage3StartModalPannel,
            OnClickConfirmButton: () =>
            {
                ModalManager.Instance.Close();
            }, OnClickCancelButton: () =>
            {
                ModalManager.Instance.Close();
                ModalManager.Instance._modal = null;
            }
        );
    }

    public void ClickEffectPlay()
    {
        clickEffect.Play();
    }

    #region 캐릭터 id 저장
    public void Character0Button()
    {
        _characterID = 0;
        characterImage.sprite = characterDatas[_characterID].CharacterSprite;
    }
    public void Character1Button()
    {
        _characterID = 1;
        characterImage.sprite = characterDatas[_characterID].CharacterSprite;
    }
    public void Character2Button()
    {
        _characterID = 2;
        characterImage.sprite = characterDatas[_characterID].CharacterSprite;
    }
    public void Character3Button()
    {
        _characterID = 3;
        characterImage.sprite = characterDatas[_characterID].CharacterSprite;
    }
    public void Character4Button()
    {
        _characterID = 4;
        characterImage.sprite = characterDatas[_characterID].CharacterSprite;
    }
    public void Character5Button()
    {
        _characterID = 5;
        characterImage.sprite = characterDatas[_characterID].CharacterSprite;
    }
    #endregion
}
