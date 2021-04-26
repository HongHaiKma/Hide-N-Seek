using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : Singleton<TutorialManager>
{
    public void CheckTutorial(TutorialType _tutType)
    {
        switch (_tutType)
        {
            case TutorialType.SHOP_BUYBYGOLD:
                PopupCaller.OpenTutorialPopup(TutorialType.SHOP_BUYBYGOLD);
                break;
            case TutorialType.SHOP_BUYBYADS:
                PopupCaller.OpenTutorialPopup(TutorialType.SHOP_BUYBYADS);
                break;
        }
    }

    public void PassTutorial(TutorialType _tutType)
    {
        switch (_tutType)
        {
            case TutorialType.SHOP_BUYBYGOLD:
                PopupCaller.OpenTutorialPopup(TutorialType.SHOP_BUYBYGOLD);
                break;
            case TutorialType.SHOP_BUYBYADS:
                PopupCaller.OpenTutorialPopup(TutorialType.SHOP_BUYBYADS);
                break;
        }
    }
}

public enum TutorialType
{
    SHOP_BUYBYGOLD = 0,
    SHOP_BUYBYADS = 1,
}