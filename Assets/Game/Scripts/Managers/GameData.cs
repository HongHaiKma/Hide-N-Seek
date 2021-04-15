using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

[DefaultExecutionOrder(-95)]
public class GameData : Singleton<GameData>
{
    public List<TextAsset> m_DataText = new List<TextAsset>();

    private Dictionary<int, CharacterDataConfig> m_CharacterDataConfigs = new Dictionary<int, CharacterDataConfig>();

    private void Awake()
    {
        LoadCharacterConfig();

        // CharacterDataConfig charrr = GetCharacterDataConfig(CharacterType.FIREMAN);

        // Helper.DebugLog("m_Id: " + charrr.m_Id);
        // Helper.DebugLog("m_Name: " + charrr.m_Name);
        // Helper.DebugLog("m_RunSpeed: " + charrr.m_RunSpeed);
        // Helper.DebugLog("m_Price: " + charrr.m_Price);
        // Helper.DebugLog("m_AdsCheck: " + charrr.m_AdsCheck);
        // Helper.DebugLog("m_AdsNumber: " + charrr.m_AdsNumber);

        // Dictionary<int, CharacterDataConfig> characterDataConfig = GameData.Instance.GetCharacterDataConfig();

        // Helper.DebugLog("Name: " + characterDataConfig[0].m_Name);
    }

    public void LoadCharacterConfig()
    {
        m_CharacterDataConfigs.Clear();
        TextAsset ta = GetDataAssets(GameDataType.DATA_CHAR);
        var js1 = JSONNode.Parse(ta.text);
        for (int i = 0; i < js1.Count; i++)
        {
            JSONNode iNode = JSONNode.Parse(js1[i].ToString());

            int id = int.Parse(iNode["ID"]);

            string name = "";
            if (iNode["Name"].ToString().Length > 0)
            {
                name = iNode["Name"];
            }

            string colName = "";

            float runSpeed = 0f;
            colName = "RunSpeed";
            if (iNode[colName].ToString().Length > 0)
            {
                runSpeed = float.Parse(iNode[colName]);
            }

            BigNumber price = 0;
            colName = "Price";
            if (iNode[colName].ToString().Length > 0)
            {
                price = new BigNumber(iNode[colName]) + 0;
            }

            int adsCheck = 0;
            colName = "AdsCheck";
            if (iNode[colName].ToString().Length > 0)
            {
                adsCheck = int.Parse(iNode[colName]);
            }

            int adsNumber = 0;
            colName = "AdsNumber";
            if (iNode[colName].ToString().Length > 0)
            {
                adsNumber = int.Parse(iNode[colName]);
            }

            CharacterDataConfig character = new CharacterDataConfig();
            character.Init(id, name, runSpeed, price, adsCheck, adsNumber);
            m_CharacterDataConfigs.Add(id, character);
        }
    }

    public TextAsset GetDataAssets(GameDataType _id)
    {
        return m_DataText[(int)_id];
    }

    public CharacterDataConfig GetCharacterDataConfig(int charID)
    {
        return m_CharacterDataConfigs[charID];
    }
    public CharacterDataConfig GetCharacterDataConfig(CharacterType characterType)
    {
        return m_CharacterDataConfigs[(int)characterType];
    }
    public Dictionary<int, CharacterDataConfig> GetCharacterDataConfig()
    {
        return m_CharacterDataConfigs;
    }

    public enum GameDataType
    {
        DATA_CHAR = 0,
    }
}