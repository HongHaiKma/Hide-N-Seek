using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LayoutFixCellSize : MonoBehaviour
{
    public void Setup(RectTransform target)
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(target.rect.width, target.rect.height);
    }
}
