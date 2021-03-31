using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
// using Newtonsoft.Json;

public class PlayerProfile
{
    private BigNumber m_Gold = new BigNumber(0);
    public string ic_Gold = "0";

    public int m_Level;

    public int m_SelectedCharacter = 0;
    public List<CharacterProfileData> m_CharacterData = new List<CharacterProfileData>();


    public void LoadLocalProfile()
    {
        m_Gold = new BigNumber(ic_Gold);

        LoadCharacterData();

        // if (GetCharacterProfile(CharacterType.BATMAN) != null)
        // {
        //     Helper.DebugLog("Batman existed!!!");
        // }
        // int a = 2;
        // a = data1["m_Gold"].To;
    }

    public void CreateNewPlayer()
    {
        string ic = "0";
        m_Gold = new BigNumber(ic);
        m_Level = 1;
        UnlockCharacter(CharacterType.BLUEBOY);
        SetSelectedCharacter(CharacterType.BLUEBOY);
        LoadCharacterData();
    }

    public void SaveDataToLocal()
    {
        string piJson = this.ObjectToJsonString();
        ProfileManager.Instance.SaveDataText(piJson);
    }

    public string ObjectToJsonString()
    {
        return JsonMapper.ToJson(this);
    }

    public JsonData StringToJsonObject(string _data)
    {
        return JsonMapper.ToObject(_data);
    }

    #region GOLD
    public BigNumber GetGold()
    {
        return m_Gold;
    }

    public string GetGold(bool a = false)
    {
        return (m_Gold + 1).ToString();
    }

    public bool IsEnoughGold(BigNumber _value)
    {
        return (m_Gold >= _value);
    }

    public void AddGold(BigNumber _value)
    {
        m_Gold += _value;
        ic_Gold = m_Gold.ToString();
        // ProfileManager.Instance.SaveData();
        SaveDataToLocal();
        // EventManager.TriggerEvent("UpdateGold");
    }

    #endregion

    #region LEVEL

    public void PassLevel()
    {
        m_Level++;
        SaveDataToLocal();
    }

    public int GetLevel()
    {
        return m_Level;
    }

    public void SetLevel(int _level)
    {
        m_Level = _level;
        SaveDataToLocal();
    }

    #endregion

    #region CHARACTER

    public int GetSelectedCharacter()
    {
        Helper.DebugLog("Selected character: " + m_SelectedCharacter);
        return m_SelectedCharacter;
    }

    public void SetSelectedCharacter(int _id)
    {
        // public int GetSelectedCharacter()
        //     {
        //         return m_SelectedCharacter;
        //     }
    }

    public void LoadCharacterData()
    {
        for (int i = 0; i < m_CharacterData.Count; i++)
        {
            CharacterProfileData cpd = m_CharacterData[i];
            cpd.Load();
        }
    }

    public void UnlockCharacter(CharacterType characterType)
    {
        CharacterProfileData newCharacter = new CharacterProfileData();
        newCharacter.Init(characterType);
        newCharacter.Load();
        m_CharacterData.Add(newCharacter);
    }

    public void SetSelectedCharacter(CharacterType characterType)
    {
        m_SelectedCharacter = (int)characterType;
    }

    public CharacterProfileData GetCharacterProfile(int characterType)
    {
        return GetCharacterProfile((CharacterType)characterType);
    }

    public CharacterProfileData GetCharacterProfile(CharacterType characterType)
    {
        for (int i = 0; i < m_CharacterData.Count; i++)
        {
            CharacterProfileData cpd = m_CharacterData[i];
            if (cpd.m_Cid == characterType)
            {
                return cpd;
            }
        }
        return null;
    }

    public bool CheckSelectedChar(int _id)
    {
        if (_id == m_SelectedCharacter)
        {
            return true;
        }

        return false;
    }

    #endregion
}