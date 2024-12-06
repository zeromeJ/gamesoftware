using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Game/CharacterData")]
public class CharacterData : ScriptableObject
{
    [SerializeField]
    int characterID;
    [SerializeField]
    string characterName;
    [SerializeField]
    Sprite characterSprite;

    public int CharacterID
    {
        get { return characterID; }
    }
    public string CharacterName
    {
        get { return characterName; }
    }
    public Sprite CharacterSprite
    {
        get { return characterSprite; }
    }
}
