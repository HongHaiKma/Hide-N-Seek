using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public enum UIID
{
    PANEL_INGAME = 0,
}

public class UICanvas : MonoBehaviour
{
    public UIID m_ID;
    protected CanvasGroup m_CanvasGroup;

    protected void Init(bool isActive = false)
    {
        // m_RectTransform = GetComponent<RectTransform>();
        m_CanvasGroup = GetComponent<CanvasGroup>();
        if (m_CanvasGroup == null)
        {
            m_CanvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
        // if (GUIManager.Instance == null)
        // {
        //     StartCoroutine(OnWaitingRegister());
        // }
        // else
        // {
        //     GUIManager.Instance.RegisterUI(this);
        //     gameObject.SetActive(isActive);
        // }
        // float ratio = (float)Screen.height / (float)Screen.width;
        // if (ratio > 2.1f)
        // {
        //     Vector2 leftBottom = m_RectTransform.offsetMin;
        //     Vector2 rightTop = m_RectTransform.offsetMax;
        //     rightTop.y = -100f;
        //     m_RectTransform.offsetMax = rightTop;
        //     leftBottom.y = 0f;
        //     m_RectTransform.offsetMin = leftBottom;
        //     m_OffsetY = 100f;
        // }
        // m_IsInit = true;
    }

    private void FadeOut()
    {
        m_CanvasGroup.DOFade(0, 0.2f).SetEase(Ease.Flash).SetUpdate(UpdateType.Late, true); ;
        transform.DOScale(1.05f, 0.2f).SetEase(Ease.Flash).OnComplete(() => { gameObject.SetActive(false); }).SetUpdate(UpdateType.Late, true); ;
    }
    private void FadeIn()
    {
        if (m_CanvasGroup != null)
        {
            m_CanvasGroup.alpha = 0;
            transform.localScale = new Vector3(1.05f, 1.05f, 1.05f);
            m_CanvasGroup.DOFade(1, 0.2f).SetEase(Ease.Flash).SetUpdate(UpdateType.Late, true); ;
            transform.DOScale(1, 0.2f).SetEase(Ease.Flash).SetUpdate(UpdateType.Late, true); ;
        }
    }
}
