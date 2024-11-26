using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class ScoreBoardManager : MonoBehaviour
{
    [SerializeField] List<CharacterData> characterDatas;
    [SerializeField] PlayerInfo playerInfo;

    [SerializeField] TMP_Text nicknameText;
    [SerializeField] Image characterImage;

    string _nickname;
    int _characterID;

    private void Awake()
    {
        Test_SaveBestScores();
        SetProfile();
    }

    private void Start()
    {
        LoadBestScores();
    }

    void SetProfile()
    {
        _characterID = playerInfo.CharacterID;
        _nickname = playerInfo.Nickname;
        characterImage.sprite = characterDatas[_characterID].CharacterSprite;
        nicknameText.text = _nickname;
    }

    private void LoadBestScores()
    {
        for (int i = 0; i < 3; i++)
        {
            int bestScore = PlayerPrefs.GetInt($"bestScore{i + 1}", 0);
        }
    }

    #region Test_Code
    private void Test_SaveBestScores()
    {
        PlayerPrefs.SetInt("bestScore1", 1);
        PlayerPrefs.SetInt("bestScore2", 2);
        PlayerPrefs.SetInt("bestScore3", 3);
    }
    #endregion
}
