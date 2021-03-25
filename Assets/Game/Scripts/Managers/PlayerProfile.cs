using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class PlayerProfile
{
    private BigNumber m_Gold = new BigNumber(0);
    public string ic_Gold = "0";

    public int m_Level;

    public void LoadLocalProfile()
    {
        m_Gold = new BigNumber(ic_Gold);

        // int a = 2;
        // a = data1["m_Gold"].To;
    }

    public void CreateNewPlayer()
    {
        string ic = "0";
        // m_Gold = new BigNumber(ic);
        m_Gold = new BigNumber(ic);
        m_Level = 1;
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

    #endregion
}