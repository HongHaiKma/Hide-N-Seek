using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class PanelInGame : MonoBehaviour
{
    public GameObject gameLoseUI;
    public GameObject gameWinUI;
    bool gameIsOver;

    public GameObject ui_Keys;
    public Text txt_Keys;

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
        EventManager.AddListener(GameEvent.CHAR_SPOTTED, ShowGameLoseUI);
        EventManager.AddListener(GameEvent.CHAR_WIN, ShowGameWinUI);
        EventManager.AddListener(GameEvent.GAME_START, ShowKeys);
        EventManagerWithParam<int>.AddListener(GameEvent.CHAR_CLAIM_KEYKEY, UpdateCurrentKey);
    }

    public void StopListenToEvent()
    {
        EventManager.RemoveListener(GameEvent.CHAR_SPOTTED, ShowGameLoseUI);
        EventManager.RemoveListener(GameEvent.CHAR_WIN, ShowGameWinUI);
        EventManager.RemoveListener(GameEvent.GAME_START, ShowKeys);
        EventManagerWithParam<int>.RemoveListener(GameEvent.CHAR_CLAIM_KEYKEY, UpdateCurrentKey);
    }

    void Update()
    {
        if (gameIsOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    public void Restart()
    {
        if (gameIsOver)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void ShowKeys()
    {
        ui_Keys.SetActive(true);

        int keys = InGameObjectsManager.Instance.m_Map.m_Keys.Count;
        txt_Keys.text = 0.ToString() + "/" + keys.ToString();
    }

    public void UpdateCurrentKey(int _value)
    {
        RectTransform rect = txt_Keys.GetComponent<RectTransform>();
        int keys = InGameObjectsManager.Instance.m_Map.m_Keys.Count;

        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(rect.DOScale(new Vector3(2f, 2f, 2f), 0.25f));
        Tween tween = rect.DOScale(new Vector3(1f, 1f, 1f), 0.25f).OnComplete
        (
            () => txt_Keys.text = _value.ToString() + "/" + keys.ToString()
        );
        mySequence.Append(tween);
    }

    void ShowGameWinUI()
    {
        OnGameOver(gameWinUI);
    }

    void ShowGameLoseUI()
    {
        OnGameOver(gameLoseUI);
    }

    void OnGameOver(GameObject gameOverUI)
    {
        gameOverUI.SetActive(true);
        gameIsOver = true;
    }
}
