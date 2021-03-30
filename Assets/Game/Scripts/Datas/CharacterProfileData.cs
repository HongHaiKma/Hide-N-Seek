using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterProfileData
{
    public CharacterType m_Cid;
    public string m_Name;
    public float m_RunSpeed;

    public void Init(CharacterType _id)
    {
        m_Cid = _id;
    }

    public void Load()
    {
        CharacterDataConfig cdc = GameData.Instance.GetCharacterDataConfig(m_Cid);
        m_Name = cdc.m_Name;
        m_RunSpeed = cdc.m_RunSpeed;
    }
}

public enum CharacterType
{
    BLUEBOY = 1,
    STREETBOY = 2,
    GREENBOY = 3,
    BUSINESSMAN = 4,
    FIREMAN = 5,
    ASTRONAUS = 6,
    CLOWN = 7,
    WIZARD = 8,
    SUPERBOY = 9,
    BATBOY = 10,
    VIKING = 11,
    MOHAWKBOY = 12,
    COWBOY = 13,
}