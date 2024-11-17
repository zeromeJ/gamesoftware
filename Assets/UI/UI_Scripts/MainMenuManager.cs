using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] PlayerInfo playerInfo;

    [SerializeField] Image profileImage;
    [SerializeField] Image characterImage;
    [SerializeField] TMP_Text nicknameText;
    [SerializeField] TMP_InputField nicknameInputField;

    [SerializeField] List<CharacterData> characterDatas;

    string _nickname;
    int _characterID;

    private void Start()
    {
        CheckHasLoggedIn();
    }

    void CheckHasLoggedIn()
    {
        if (!playerInfo.LoadHasLoggedIn())
        {
            OnClickOpenProfile();
            playerInfo.SaveLoggedIn();
        }
        UpdateProfileModal();
    }

    public void OnClickOpenProfile()
    {
        ModalManager.Instance.Open("My Profile",
            OnClickConfirmButton: () =>
            {
                ProfileConfirmButton();
            }, OnClickCancelButton: () =>
            {
                // close()
            }
        );
    }
    
    // 확인 버튼 클릭 
    void ProfileConfirmButton()
    {
        _nickname = nicknameInputField.text;
        playerInfo.Nickname = _nickname;
        playerInfo.CharacterID = _characterID;
        UpdateProfileModal();
    }

    // 모달에 표시될 Player Info 갱신 
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

    #region 캐릭터 선택 버튼 처리 
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
