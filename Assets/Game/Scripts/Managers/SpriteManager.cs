using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.UI;
using UnityEngine.U2D;

public class SpriteManager : Singleton<SpriteManager>
{
    // public SpriteAtlas m_CharCard;
    public List<Sprite> m_CharCards;
    public List<Sprite> m_Mics;
}

public enum MiscSpriteKeys
{
    SOUND_ON = 0,
    SOUND_OFF = 1,
    MUSIC_ON = 2,
    MUSIC_OFF = 3,
    UI_CARD_BG = 4,
    UI_CARD_BG_LOCK = 5,
}
