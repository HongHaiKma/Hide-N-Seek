using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : Singleton<TutorialManager>
{
    public void OpenTutorial(TutorialType _tutType)
    {
        // switch (_tutType)
        // {
        //     case TutorialType.SHOP_BUYBYGOLD:
        //         PopupCaller.OpenTutorialPopup(TutorialType.SHOP_BUYBYGOLD);
        //         break;
        //     case TutorialType.SHOP_BUYBYADS:
        //         PopupCaller.OpenTutorialPopup(TutorialType.SHOP_BUYBYADS);
        //         break;
        // }
    }

    public void PassTutorial(TutorialType _tutType)
    {
        switch (_tutType)
        {
            case TutorialType.SHOP_BUYBYGOLD:
                PlayerPrefs.SetInt(TutorialType.SHOP_BUYBYGOLD.ToString(), 1);
                break;
            case TutorialType.SHOP_BUYBYADS:
                PlayerPrefs.SetInt(TutorialType.SHOP_BUYBYADS.ToString(), 1);
                break;
        }
    }

    public bool CheckTutorial(TutorialType _tutType)
    {
        if (_tutType == TutorialType.SHOP_BUYBYGOLD)
        {
            if (ProfileManager.GetGold2() >= 500 && (ProfileManager.GetLevel()) >= 2)
            {
                Helper.DebugLog("Gold and level");
                return PlayerPrefs.GetInt(TutorialType.SHOP_BUYBYGOLD.ToString()) == 0;
            }
            else
            {
                return false;
            }
        }
        if (_tutType == TutorialType.SHOP_BUYBYADS)
        {
            return PlayerPrefs.GetInt(TutorialType.SHOP_BUYBYADS.ToString()) == 0;
        }

        return false;
    }
}

public enum TutorialType
{
    SHOP_BUYBYGOLD = 0,
    SHOP_BUYBYADS = 1,
}