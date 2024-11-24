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

    string _nickname;
    int _characterID;

    private void Start()
    {
        CheckHasLoggedIn();
        characterImage.sprite = characterDatas[_characterID].CharacterSprite;
        WebGLInput.captureAllKeyboardInput = false;
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

    // ?????? ???????? ???? 
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
                // close()
            }
        );
    }
    
    // ?????? ???? ???? ???? ???? 
    void ProfileConfirmButton()
    {
        
        if (CheckValidNickname())
        {
            _nickname = nicknameInputField.text;
            playerInfo.Nickname = _nickname;
            playerInfo.CharacterID = _characterID;
            UpdateProfileModal();

            ModalManager.Instance.Close();
        }
    }
    
    bool CheckValidNickname()
    {
        if (nicknameInputField.text.Length == 0 || 
            nicknameInputField.text.Length >= 5)
        {
            nicknameWarningText.enabled = true;
            return false;
        }
        return true;
    }

    // ?????? ?????? Player Info ???? 
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

    // stage1~3 ???? ???? 
    public void OnClickOpenStage1()
    {
        ModalManager.Instance.Open(stage1StartModalPannel,
            OnClickConfirmButton: () =>
            {
                ModalManager.Instance.Close();
                Stage1StartButton();
            }, OnClickCancelButton: () =>
            {
                // close()
            }
        );
    }
    void Stage1StartButton()
    {
        SceneManager.LoadScene("Tiger");
    }

    public void OnClickOpenStage2()
    {
        ModalManager.Instance.Open(stage2StartModalPannel,
            OnClickConfirmButton: () =>
            {
                ModalManager.Instance.Close();
                Stage2StartButton();
            }, OnClickCancelButton: () =>
            {
                // close()
            }
        );
    }

    void Stage2StartButton()
    {
        SceneManager.LoadScene("RabbitScene");
    }
    public void OnClickOpenStage3()
    {
        ModalManager.Instance.Open(stage3StartModalPannel,
            OnClickConfirmButton: () =>
            {
                ModalManager.Instance.Close();
                Stage3StartButton();
            }, OnClickCancelButton: () =>
            {
                // close()
            }
        );
    }

    void Stage3StartButton()
    {
        SceneManager.LoadScene("Receive Toy");
    }

    #region ?????? ???? ???? ???? 
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
