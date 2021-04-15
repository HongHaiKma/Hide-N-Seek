using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

[DefaultExecutionOrder(-94)]
public class ProfileManager : MonoBehaviour
{
    private static ProfileManager m_Instance;
    public static ProfileManager Instance
    {
        get
        {
            return m_Instance;
        }
    }

    public static PlayerProfile MyProfile
    {
        get
        {
            return m_Instance.m_LocalProfile;
        }
    }
    private PlayerProfile m_LocalProfile;

    public BigNumber m_Gold;
    public BigNumber m_Gold2 = new BigNumber(0);

    private void Awake()
    {
        if (m_Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            m_Instance = this;
            InitProfile();
            DontDestroyOnLoad(gameObject);
        }

        // MyProfile.AddGold(5f);
    }

    private void OnEnable()
    {
        StartListenToEvent();
    }

    private void OnDisable()
    {
        StopListenToEvent();
    }

    public void StartListenToEvent()
    {
        EventManager.AddListener(GameEvent.CHAR_WIN, PassLevel);
        EventManagerWithParam<int>.AddListener(GameEvent.EQUIP_CHAR, EquipChar);
    }

    public void StopListenToEvent()
    {
        EventManager.RemoveListener(GameEvent.CHAR_WIN, PassLevel);
        EventManagerWithParam<int>.RemoveListener(GameEvent.EQUIP_CHAR, EquipChar);
    }

    public void InitProfile()
    {
        CreateOrLoadLocalProfile();
    }

    private void CreateOrLoadLocalProfile()
    {
        Debug.Log("Create Or Load Data");
        LoadDataFromPref();
    }

    private void LoadDataFromPref()
    {
        Debug.Log("Load Data");
        string dataText = PlayerPrefs.GetString("SuperFetch", "");
        //Debug.Log("Data " + dataText);
        if (string.IsNullOrEmpty(dataText))
        {
            // Dont have -> create new player and save;
            CreateNewPlayer();
        }
        else
        {
            // Have -> Load data
            LoadDataToPlayerProfile(dataText);
        }
    }

    private void CreateNewPlayer()
    {
        m_LocalProfile = new PlayerProfile();
        m_LocalProfile.CreateNewPlayer();
        SaveData();
    }

    private void LoadDataToPlayerProfile(string data)
    {
        m_LocalProfile = JsonMapper.ToObject<PlayerProfile>(data);
        m_LocalProfile.LoadLocalProfile();
        m_Gold = m_LocalProfile.GetGold();
    }

    public void SaveData()
    {
        m_LocalProfile.SaveDataToLocal();
    }

    public void SaveDataText(string data)
    {
        if (!string.IsNullOrEmpty(data))
        {
            PlayerPrefs.SetString("SuperFetch", data);
        }
    }
    public void TestDisplayGold()
    {
        // string a = 
        // Helper.DebugLog("Profile Gold: " + MyProfile.GetGold());
        // Helper.DebugLog("Profile Level: " + MyProfile.m_Level);
    }




    public static void PassLevel()
    {
        MyProfile.PassLevel();
    }
    public static int GetLevel()
    {
        return MyProfile.GetLevel();
    }

    public static string GetLevel2()
    {
        return MyProfile.GetLevel().ToString();
    }

    public static void SetLevel(int _level)
    {
        MyProfile.SetLevel(_level);
    }

    #region GENERAL
    public static string GetGold()
    {
        return MyProfile.GetGold().ToString();
    }

    public static BigNumber GetGold2()
    {
        return MyProfile.GetGold();
    }

    public static void AddGold(BigNumber _gold)
    {
        MyProfile.AddGold(_gold);
    }

    public static void ConsumeGold(BigNumber _gold)
    {
        MyProfile.ConsumeGold(_gold);
    }

    public static bool IsEnoughGold(BigNumber _gold)
    {
        return MyProfile.IsEnoughGold(_gold);
    }

    #endregion

    #region CHARACTER

    public static int GetSelectedCharacter()
    {
        return MyProfile.GetSelectedCharacter();
    }

    public static void SetSelectedCharacter(int _id)
    {
        MyProfile.SetSelectedCharacter(_id);
    }

    public static CharacterProfileData GetCharacterProfileData(int _id)
    {
        return MyProfile.GetCharacterProfile(_id);
    }

    public static CharacterProfileData GetCharacterProfileData(CharacterType _id)
    {
        return MyProfile.GetCharacterProfile(_id);
    }

    public static bool IsOwned(int _id)
    {
        return MyProfile.IsOwned(_id);
    }

    public void EquipChar(int _id)
    {
        MyProfile.SetSelectedCharacter(_id);
    }

    public static void UnlockNewCharacter(int _id)
    {
        MyProfile.UnlockCharacter((CharacterType)_id);
    }

    public static void UnlockNewCharacter(CharacterType _id)
    {
        MyProfile.UnlockCharacter(_id);
    }

    #endregion

    public static int GetSelectedChar()
    {
        return MyProfile.m_SelectedCharacter;
    }

    public static bool CheckSelectedChar(int _id)
    {
        return MyProfile.CheckSelectedChar(_id);
    }

    public void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveData();
        }
    }
    public void OnApplicationQuit()
    {
        SaveData();
    }
}
