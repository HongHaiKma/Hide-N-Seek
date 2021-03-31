using UnityEngine;
using UnityEngine.UI;
using PolyAndCode.UI;

//Cell class for demo. A cell in Recyclable Scroll Rect must have a cell class inheriting from ICell.
//The class is required to configure the cell(updating UI elements etc) according to the data during recycling of cells.
//The configuration of a cell is done through the DataSource SetCellData method.
//Check RecyclableScrollerDemo class
public class UICharacterCard : MonoBehaviour, ICell
{
    //UI
    public Text m_Name;
    public Button btn_LoadChar;
    public Image img_Char;
    public Text txt_Price;

    public GameObject g_SelectedOutline;
    public GameObject g_Owned;
    public GameObject g_Price;
    public GameObject g_AdsClaim;

    //Model
    private UICharacterCardInfo m_UICharacterCardInfo;
    private int _cellIndex;

    private void Start()
    {
        // img_Char.sprite = SpriteManager.Instance.m_CharCards[m_UIChar];
        GUIManager.Instance.AddClickEvent(btn_LoadChar, OnLoadMiniCharacterStudio);
    }

    private void OnEnable()
    {
        StartListenToEvent();
    }

    private void Disable()
    {
        StopListenToEvent();
    }

    public void StartListenToEvent()
    {
        EventManagerWithParam<int>.AddListener(GameEvent.EQUIP_CHAR, SetEquippedChar);
    }

    public void StopListenToEvent()
    {
        EventManagerWithParam<int>.RemoveListener(GameEvent.EQUIP_CHAR, SetEquippedChar);
    }

    //This is called from the SetCell method in DataSource
    public void ConfigureCell(UICharacterCardInfo _info, int cellIndex)
    {
        _cellIndex = cellIndex;
        m_UICharacterCardInfo = _info;

        m_Name.text = _info.m_Name;

        txt_Price.text = _info.m_Price;

        img_Char.sprite = SpriteManager.Instance.m_CharCards[_info.m_Id - 1];

        int selectedChar = ProfileManager.GetSelectedChar();

        g_SelectedOutline.SetActive(ProfileManager.CheckSelectedChar(_info.m_Id));

        SetCellStatus();
    }

    public void SetCellStatus()
    {
        CharacterProfileData data = ProfileManager.GetCharacterProfileData(m_UICharacterCardInfo.m_Id);
        CharacterDataConfig config = GameData.Instance.GetCharacterDataConfig(m_UICharacterCardInfo.m_Id);

        if (data != null)
        {
            g_Owned.SetActive(true);
            g_Price.SetActive(false);
            g_AdsClaim.SetActive(false);
        }
        else
        {
            g_Owned.SetActive(false);

            bool adsCheck;

            if (config.m_AdsCheck == 1)
            {
                adsCheck = true;
            }
            else
            {
                adsCheck = false;
            }

            g_Price.SetActive(!adsCheck);
            g_AdsClaim.SetActive(adsCheck);
        }
    }

    private void OnLoadMiniCharacterStudio()
    {
        // MiniCharacterStudio.Instance.SetChar(m_UICharacterCardInfo.m_Id);
        EventManagerWithParam<int>.CallEvent(GameEvent.LOAD_OUTFIT_CHARACTER, m_UICharacterCardInfo.m_Id);
        Helper.DebugLog("ID: " + m_UICharacterCardInfo.m_Id);
        Helper.DebugLog("Name: " + m_UICharacterCardInfo.m_Name);
    }

    public void SetEquippedChar(int _id)
    {
        g_SelectedOutline.SetActive(_id == m_UICharacterCardInfo.m_Id);
    }
}

public class UICharacterCardInfo
{
    public int m_Id;
    public string m_Name;
    public string m_Price;
}