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

        CharacterDataConfig charrr = GetCharacterDataConfig(CharacterType.ASTRONAUS);

        Helper.DebugLog(charrr.m_Id);
        Helper.DebugLog(charrr.m_Name);
        Helper.DebugLog(charrr.m_RunSpeed);
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

            CharacterDataConfig character = new CharacterDataConfig();
            character.Init(id, name, runSpeed);
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