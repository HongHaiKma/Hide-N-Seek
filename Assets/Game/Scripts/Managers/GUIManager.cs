using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
            // this.m_GUIMap = new GUIMap();
            // if (m_SubCanvas != null)
            // {
            //     DontDestroyOnLoad(m_SubCanvas);
            // }
            // FindMainCanvas();
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
    }

    void Start()
    {
        int maxScreenHeight = 1080;

        float ratio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        if (Screen.currentResolution.height > maxScreenHeight)
        {
            Screen.SetResolution(Mathf.RoundToInt(ratio * (float)maxScreenHeight), maxScreenHeight, true);
        }

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
    }

    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        if (!isLoadInitScene)
        {
            isLoadInitScene = true;
            GameManager.Instance.ChangeToStartMenu();
        }
    }

    public void AddClickEvent(Button _bt, UnityAction _callback, bool _isFlip = false)
    {
        int tx = 1;
        if (_isFlip) tx = -1;
        _bt.onClick.AddListener(() =>
        {
            // SoundManager.Instance.PlayButtonClick(_bt.transform.position);
            if (_callback != null)
            {
                _callback();
            }
        });
    }
}
