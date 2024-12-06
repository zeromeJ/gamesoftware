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

    // ù ��° �������� Ȯ�� 
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

    // ������ ĳ���� ���� 
    void SaveCharacterChoice(int characterID)
    {
        PlayerPrefs.SetInt(CharacterIDKey, characterID);
        PlayerPrefs.Save();
    }
    public int LoadCharacterChoice()
    {
        return PlayerPrefs.GetInt(CharacterIDKey, 0);
    }

    // �г��� ���� 
    void SavePlayerNickname(string nickname)
    {
        PlayerPrefs.SetString(NicknameKey, nickname);
        PlayerPrefs.Save();
    }
    public string LoadPlayerNickname()
    {
        return PlayerPrefs.GetString(NicknameKey, "");
    }

    // �÷��̾� ���� �ʱ�ȭ 
    public void ClearPlayerData()
    {
        SaveNotLoggedIn();
        PlayerPrefs.DeleteKey(CharacterIDKey);
        PlayerPrefs.DeleteKey(NicknameKey);
        PlayerPrefs.DeleteKey("Tiger");
        PlayerPrefs.DeleteKey("RabbitScene");
        PlayerPrefs.DeleteKey("Receive Toy");
    }

    // Get/Set �޼ҵ� 
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
