using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[DefaultExecutionOrder(-90)]
public class GUIManager : MonoBehaviour
{
    internal class GUIMap : Dictionary<int, UICanvas>
    {
        public static int m_NextID = 0;
        public int m_ID = 0;
        public GUIMap()
        {
            //Debug.Log("Create new GUI MAP " + m_NextID);
            m_ID = m_NextID;
            m_NextID++;
        }
    }

    AsyncOperation async;
    bool isLoadInitScene = false;
    private GUIMap m_GUIMap;

    private Vector3 m_CenterPos;

    private float m_OffsetTop = 0;
    public bool IsLongDevice = false;

    private UICanvas m_PreviousPopup = null;
    private UICanvas m_PreviousPanel = null;
    public GameObject m_MainCanvas;

    private bool IsHoldBackkey = false;

    public List<UICanvas> m_CurrentOpenedPopup = new List<UICanvas>();
    public List<UICanvas> m_CurrentOpenedPanel = new List<UICanvas>();

    private static GUIManager m_Instance;
    public static GUIManager Instance
    {
        get
        {
            return m_Instance;
        }
    }

    private void Awake()
    {
        if (m_Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            m_Instance = this;
            DontDestroyOnLoad(gameObject);
            this.m_GUIMap = new GUIMap();
            // if (m_SubCanvas != null)
            // {
            //     DontDestroyOnLoad(m_SubCanvas);
            // }
            FindMainCanvas();
            // float ratio = (float)Screen.height / (float)Screen.width;
            // if (ratio > 2.1f)
            // {
            //     m_OffsetTop = -50f;
            // }
            // if (ratio > 1.8f)
            // {
            //     IsLongDevice = true;
            // }
            // m_CenterPos = Vector3.zero + new Vector3(0, m_OffsetTop);
        }

        int maxScreenHeight = 1080;
        float ratio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        if (Screen.currentResolution.height > maxScreenHeight)
        {
            Screen.SetResolution(Mathf.RoundToInt(ratio * (float)maxScreenHeight), maxScreenHeight, true);
        }
    }

    void Start()
    {
        // int maxScreenHeight = 1080;
        // float ratio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        // if (Screen.currentResolution.height > maxScreenHeight)
        // {
        //     Screen.SetResolution(Mathf.RoundToInt(ratio * (float)maxScreenHeight), maxScreenHeight, true);
        // }

        LoadInitScene();
    }

    public void LoadInitScene()
    {
        StartCoroutine(LoadScreen());
    }

    IEnumerator LoadScreen()
    {
        Debug.Log("Start Load");
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
        // async = SceneManager.LoadSceneAsync("InitScene", LoadSceneMode.Single);
        async = SceneManager.LoadSceneAsync("InitScene", LoadSceneMode.Single);
        async.allowSceneActivation = false;
        //float _loadProgress = 0;
        //while(_loadProgress <= 0.3f) {
        //    _loadProgress += 0.02f;
        //    yield return new WaitForSeconds(Time.deltaTime);
        //}

        while (async.progress < 0.9f)
        {
            yield return null;
        }

        async.allowSceneActivation = true;

        FindMainCanvas();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !IsHoldBackkey)
        {
            // if (GameManager.Instance.GetTutorialMode()) return;
            UICanvas _UICanvas = GUIManager.Instance.GetCurrentPopup();
            if (_UICanvas != null && !_UICanvas.IsAvoidBackKey)
            {
                _UICanvas.OnBack();
            }
        }
    }

    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        if (!isLoadInitScene)
        {
            isLoadInitScene = true;
            GameManager.Instance.ChangeToStartMenu();
        }
    }

    public void AddClickEvent(Button _bt, UnityAction _callback)
    {
        _bt.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlayButtonClick(_bt.transform.position);
            if (_callback != null)
            {
                _callback();
            }
        });
    }

    public void RegisterUI(UICanvas uicanvas)
    {
        int id = (int)uicanvas.ID();

        UICanvas rUI = null;
        if (m_GUIMap.TryGetValue(id, out rUI))
        {
            m_GUIMap[id] = uicanvas;
            //Debug.Log(m_GUIMap.m_ID + " Register overlap " + uicanvas.ID());
        }
        else
        {
            m_GUIMap.Add((int)uicanvas.ID(), uicanvas);
            //Debug.Log(m_GUIMap.m_ID + " Register add " + uicanvas.ID());
        }
    }

    public UICanvas GetUICanvasByID(UIID uid)
    {
        return GetUICanvasByID((int)uid);
    }

    public void FindMainCanvas()
    {
        m_MainCanvas = GameObject.Find("MainCanvas");
    }

    public UICanvas GetUICanvasByID(int id)
    {
        if (m_MainCanvas == null)
        {
            FindMainCanvas();
        }
        UICanvas rUI = null;
        m_GUIMap.TryGetValue(id, out rUI);
        if (rUI == null)
        {
            string name = GetUIName((UIID)id);
            GameObject go = FindObject(m_MainCanvas, name);
            if (go != null)
            {
                go.gameObject.SetActive(true);
                return go.GetComponent<UICanvas>();
            }
            else
            {
                GameObject prefab = GetPrefab((UIID)id);
                if (prefab == null) return null;
                GameObject goUICanvas = GameObject.Instantiate(prefab, m_MainCanvas.transform.position, Quaternion.identity, m_MainCanvas.transform);
                RectTransform rect = goUICanvas.GetComponent<RectTransform>();
                rect.SetParent(m_MainCanvas.GetComponent<RectTransform>());
                rect.localPosition = new Vector3(0, 0, 0);
                rect.localScale = new Vector3(1, 1, 1);
                return goUICanvas.GetComponent<UICanvas>();
            }
        }
        else
        {
            return rUI;
        }
    }

    public void HideUIPopup(UICanvas popup, bool isClosed, bool isOpenPrevious = true)
    {
        if (popup == null) return;
        popup.HidePopup();

        if (isClosed || isOpenPrevious)
        {
            if (m_CurrentOpenedPopup.Contains(popup))
            {
                m_CurrentOpenedPopup.Remove(popup);
                CleanOpenPopup();
            }
        }
        if (isOpenPrevious)
        {
            if (m_CurrentOpenedPopup.Count > 0)
            {
                UICanvas previusPopup = m_CurrentOpenedPopup[m_CurrentOpenedPopup.Count - 1];
                ShowUIPopup(previusPopup, false);
            }
        }
    }

    public void ShowUIPopup(UICanvas popup, bool isClosePrevious = true)
    {
        //Debug.Log("AAAA" + popup.ID());
        ShowUIPopup(popup, GetCenterPosition(), isClosePrevious);
    }
    public void ShowUIPopup(UICanvas popup, Vector3 position, bool isClosePreviousPopup = true)
    {
        if (popup == null) return;
        if (m_CurrentOpenedPopup.Count > 0)
        {
            m_PreviousPopup = m_CurrentOpenedPopup[m_CurrentOpenedPopup.Count - 1];
            if (isClosePreviousPopup)
            {
                HideUIPopup(m_PreviousPopup, m_PreviousPopup.IsAutoRemove, false);
            }
        }
        popup.ShowPopup();
        popup.SetLocalPosition(position);
        if (!m_CurrentOpenedPopup.Contains(popup))
        {
            m_CurrentOpenedPopup.Add(popup);
            CleanOpenPopup();
        }
    }
    public Vector3 GetCenterPosition()
    {
        return m_CenterPos;
    }

    public void ShowUIPanel(UIID id)
    {
        //Debug.Log(id.ToString());
        UICanvas panel = GetUICanvasByID((int)id);
        ShowUIPanel(panel, m_MainCanvas.transform.position);
    }
    public void ShowUIPanel(UIID id, Vector3 position)
    {
        UICanvas panel = GetUICanvasByID((int)id);
        ShowUIPanel(panel, position);
    }
    public void ShowUIPanel(UICanvas panel)
    {
        ShowUIPanel(panel, m_MainCanvas.transform.position);
    }
    public void ShowUIPanel(UICanvas panel, Vector3 position, bool isLocalPositon = false)
    {
        if (panel == null)
        {
            //Debug.Log(" NULL");
            return;
        }
        if (m_CurrentOpenedPanel.Count > 0)
        {
            m_PreviousPanel = m_CurrentOpenedPanel[m_CurrentOpenedPanel.Count - 1];
        }
        panel.ShowPanel();

        Vector3 v = Vector3.zero + new Vector3(0, m_OffsetTop);
        v.z = 0;
        panel.RectTransform.localPosition = v;
        if (!m_CurrentOpenedPanel.Contains(panel))
        {
            m_CurrentOpenedPanel.Add(panel);
        }
        //Debug.Log(" Complete Show");
    }

    public void CleanOpenPopup()
    {
        int num = 0;
        while (num < m_CurrentOpenedPopup.Count)
        {
            if (m_CurrentOpenedPopup[num] == null)
            {
                m_CurrentOpenedPopup.RemoveAt(num);
                continue;
            }
            num++;
        }
    }

    public GameObject GetPrefab(UIID uid)
    {
        string prefabName = "";
        GameObject prefab = null;
        switch (uid)
        {
            case UIID.PANEL_INGAME:
                prefabName = "PANEL_INGAME";
                break;
            case UIID.POPUP_1:
                prefabName = "Popup1";
                break;
            case UIID.POPUP_11:
                prefabName = "Popup11";
                break;
            case UIID.POPUP_WIN:
                prefabName = "PopupWin";
                break;
            case UIID.POPUP_LOSE:
                prefabName = "PopupLose";
                break;
            case UIID.POPUP_PAUSE:
                prefabName = "PopupPause";
                break;
            case UIID.POPUP_OUTFIT:
                prefabName = "PopupOutfit";
                break;
            case UIID.POPUP_LEVELREWARD:
                prefabName = "PopupLevelReward";
                break;
        }
        prefab = GetPopupPrefabByName(prefabName);
        return prefab;
    }

    public UICanvas GetCurrentPopup()
    {
        int num = m_CurrentOpenedPopup.Count;
        if (num > 0)
            return m_CurrentOpenedPopup[num - 1];
        return null;
    }

    public GameObject GetPopupPrefabByName(string name)
    {
        GameObject go = Resources.Load<GameObject>("UI/Popups/" + name);
        return go;
    }

    public string GetUIName(UIID uiid)
    {
        string name = "";
        switch (uiid)
        {

        }
        return name;
    }

    public GameObject FindObject(GameObject parent, string name)
    {
        if (parent == null) return null;
        foreach (Transform t in parent.transform)
        {
            if (t.name == name)
            {
                return t.gameObject;
            }
        }
        return null;
    }
}

public enum UIID
{
    PANEL_INGAME = 0,
    POPUP_1 = 1,
    POPUP_11 = 2,
    POPUP_WIN = 3,
    POPUP_LOSE = 4,
    POPUP_PAUSE = 5,
    POPUP_OUTFIT = 6,
    POPUP_LEVELREWARD = 7,
}