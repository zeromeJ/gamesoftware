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

    public void SaveIsFirstPlay()
    {
        PlayerPrefs.SetInt(HasLoggedIn, 1);
    }

    public bool LoadHasLoggedIn()
    {
        int _hasLoggedIn = PlayerPrefs.GetInt(HasLoggedIn);
        return (_hasLoggedIn > 0 ? true : false);
    }

    void SaveCharacterChoice(int characterID)
    {
        PlayerPrefs.SetInt(CharacterIDKey, characterID);
        PlayerPrefs.Save();
    }
    void SavePlayerNickname(string nickname)
    {
        PlayerPrefs.SetString(NicknameKey, nickname);
        PlayerPrefs.Save();
    }
    
    public int LoadCharacterChoice()
    {
        return PlayerPrefs.GetInt(CharacterIDKey, 0);
    }

    public string LoadPlayerNickname()
    {
        return PlayerPrefs.GetString(NicknameKey, "");
    }

    public void Test_ClearPlayerData()
    {
        PlayerPrefs.DeleteKey(CharacterIDKey);
        PlayerPrefs.DeleteKey(NicknameKey);
    }

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
