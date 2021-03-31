using System.Collections.Generic;
using UnityEngine;
using PolyAndCode.UI;
using UnityEngine.UI;

/// <summary>
/// Demo controller class for Recyclable Scroll Rect. 
/// A controller class is responsible for providing the scroll rect with datasource. Any class can be a controller class. 
/// The only requirement is to inherit from IRecyclableScrollRectDataSource and implement the interface methods
/// </summary>

//Dummy Data model for demostraion

public class UICharacterOutfit : MonoBehaviour, IRecyclableScrollRectDataSource
{
    [SerializeField]
    RecyclableScrollRect _recyclableScrollRect;

    [SerializeField]
    private int _dataLength;

    //Dummy data List
    private List<UICharacterCardInfo> _contactList = new List<UICharacterCardInfo>();

    // public PopupOutfit

    //Recyclable scroll rect's data source must be assigned in Awake.
    private void Awake()
    {
        InitData();
        MiniCharacterStudio.Instance.SetChar(ProfileManager.GetSelectedCharacter());
        _recyclableScrollRect.DataSource = this;
    }

    //Initialising _contactList with dummy data 
    private void InitData()
    {
        if (_contactList != null) _contactList.Clear();

        Dictionary<int, CharacterDataConfig> characterDataConfig = GameData.Instance.GetCharacterDataConfig();
        int len = characterDataConfig.Count;
        _dataLength = len;

        Dictionary<int, CharacterDataConfig> charConfig = GameData.Instance.GetCharacterDataConfig();

        for (int i = 0; i < len; i++)
        {
            UICharacterCardInfo obj = new UICharacterCardInfo();
            obj.m_Id = charConfig[i + 1].m_Id;
            obj.m_Name = charConfig[i + 1].m_Name;
            obj.m_Price = charConfig[i + 1].m_Price.ToString();
            obj.m_AdsNumber = charConfig[i + 1].m_AdsNumber;
            _contactList.Add(obj);
        }
    }

    #region DATA-SOURCE

    /// <summary>
    /// Data source method. return the list length.
    /// </summary>
    public int GetItemCount()
    {
        return _contactList.Count;
    }

    /// <summary>
    /// Data source method. Called for a cell every time it is recycled.
    /// Implement this method to do the necessary cell configuration.
    /// </summary>
    public void SetCell(ICell cell, int index)
    {
        //Casting to the implemented Cell
        var item = cell as UICharacterCard;
        item.ConfigureCell(_contactList[index], index);
    }

    #endregion
}