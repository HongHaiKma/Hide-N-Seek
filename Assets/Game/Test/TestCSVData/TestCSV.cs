using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sinbad;

public class TestCSV : MonoBehaviour
{
    public TextAsset m_GeneralData;

    void Start()
    {
        // string m_path = Application.persistentDataPath + "/GeneralData.csv";

        // Resources.Load<TextAsset>().text

        TextAsset txt = (TextAsset)Resources.Load("Datas/GeneralData", typeof(TextAsset));
        string filecontent = txt.text;

        var obj = new GeneralData();

        CsvUtil.LoadObject(filecontent, ref obj);

        // var obj = new GeneralData();
        // Sinbad.CsvUtil.LoadObject("GeneralData.csv", ref obj);

        // Helper.DebugLog("Gold: " + obj.m_Gold);
    }
}

public class GeneralData
{
    public BigNumber m_Gold;
    public int m_Level;
}
