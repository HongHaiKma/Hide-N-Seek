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

    //Model
    private UICharacterCardInfo m_UICharacterCardInfo;
    private int _cellIndex;

    private void Start()
    {
        // img_Char.sprite = SpriteManager.Instance.m_CharCards[m_UIChar];
        GUIManager.Instance.AddClickEvent(btn_LoadChar, OnLoadCharacter);
    }

    //This is called from the SetCell method in DataSource
    public void ConfigureCell(UICharacterCardInfo contactInfo, int cellIndex)
    {
        _cellIndex = cellIndex;
        m_UICharacterCardInfo = contactInfo;

        m_Name.text = contactInfo.m_Name;
        img_Char.sprite = SpriteManager.Instance.m_CharCards[contactInfo.m_Id - 1];
    }


    private void OnLoadCharacter()
    {
        Debug.Log("Name: " + m_UICharacterCardInfo.m_Name);
    }
}
