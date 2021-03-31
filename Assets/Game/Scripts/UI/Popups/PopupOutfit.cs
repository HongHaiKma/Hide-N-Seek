using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupOutfit : UICanvas
{
    public int m_SelectedCharacter;

    public Text txt_CharName;
    public Button btn_Equip;

    private void Awake()
    {
        m_ID = UIID.POPUP_OUTFIT;
        Init();
        SetChar(ProfileManager.GetSelectedCharacter());

        GUIManager.Instance.AddClickEvent(btn_Equip, OnEquip);
    }

    private void OnEnable()
    {
        m_SelectedCharacter = ProfileManager.GetSelectedCharacter();

        StartListenToEvent();
    }

    private void Disable()
    {
        StopListenToEvent();
    }

    public void StartListenToEvent()
    {
        EventManagerWithParam<int>.AddListener(GameEvent.LOAD_OUTFIT_CHARACTER, SetChar);
    }

    public void StopListenToEvent()
    {
        EventManagerWithParam<int>.RemoveListener(GameEvent.LOAD_OUTFIT_CHARACTER, SetChar);
    }

    public void SetChar(int _id)
    {
        Helper.DebugLog("PopupOutfits Id: " + _id);
        CharacterDataConfig config = GameData.Instance.GetCharacterDataConfig(_id);
        txt_CharName.text = config.m_Name;
        m_SelectedCharacter = _id;
    }

    public void OnEquip()
    {
        EventManagerWithParam<int>.CallEvent(GameEvent.EQUIP_CHAR, m_SelectedCharacter);
    }
}
