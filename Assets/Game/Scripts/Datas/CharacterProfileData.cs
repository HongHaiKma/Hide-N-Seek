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
    BOIZZ = 1,
    FIRE_FIGHTER = 2,
    BATMAN = 3,
    SIR = 4,
    ASTRONAUS = 5,
}