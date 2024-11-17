using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    private const string HasLoggedIn = "HasLoggedIn";
    private const string CharacterIDKey = "Character";
    private const string NicknameKey = "Nickname";

    private bool hasLoggedIn;
    private int characterID;
    private string nickname;

    // 첫 번째 접속인지 확인 
    public void SaveLoggedIn()
    {
        PlayerPrefs.SetInt(HasLoggedIn, 1);
    }
    public void SaveNotLoggedIn()
    {
        PlayerPrefs.SetInt(HasLoggedIn, 0);
    }
    public bool LoadHasLoggedIn()
    {
        int _hasLoggedIn = PlayerPrefs.GetInt(HasLoggedIn);
        return (_hasLoggedIn > 0 ? true : false);
    }

    // 선택한 캐릭터 관리 
    void SaveCharacterChoice(int characterID)
    {
        PlayerPrefs.SetInt(CharacterIDKey, characterID);
        PlayerPrefs.Save();
    }
    public int LoadCharacterChoice()
    {
        return PlayerPrefs.GetInt(CharacterIDKey, 0);
    }

    // 닉네임 관리 
    void SavePlayerNickname(string nickname)
    {
        PlayerPrefs.SetString(NicknameKey, nickname);
        PlayerPrefs.Save();
    }
    public string LoadPlayerNickname()
    {
        return PlayerPrefs.GetString(NicknameKey, "");
    }

    // 플레이어 정보 초기화 
    public void ClearPlayerData()
    {
        SaveNotLoggedIn();
        PlayerPrefs.DeleteKey(CharacterIDKey);
        PlayerPrefs.DeleteKey(NicknameKey);
    }

    // Get/Set 메소드 
    public int CharacterID
    {
        get
        {
            characterID = LoadCharacterChoice();
            return characterID;
        }
        set
        {
            if (value >= 0)
            {
                characterID = value;
                SaveCharacterChoice(characterID);
            }
        }
    }

    public string Nickname
    {
        get
        {
            nickname = LoadPlayerNickname();
            return nickname;
        }
        set
        { 
            nickname = value;
            SavePlayerNickname(nickname);
        }
    }
}
