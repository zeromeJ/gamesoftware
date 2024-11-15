using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    PlayerInfo playerInfo;
    [SerializeField]
    TMP_Text nicknameText;

    string _nickname;
    int _characterID;

    private void Start()
    {
        UpdateNickname();
        UpdateCharacterID();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateNickname();
        UpdateCharacterID();
    }

    void UpdateNickname()
    {
        _nickname = playerInfo.Nickname;
        nicknameText.text = _nickname;
    }
    void UpdateCharacterID()
    {
        _characterID = playerInfo.CharacterID;
    }



}
