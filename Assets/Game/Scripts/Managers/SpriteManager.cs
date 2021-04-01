using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : Singleton<SpriteManager>
{
    public List<Sprite> m_CharCards;
    public List<Sprite> m_Mics;
}

public enum MiscSpriteKeys
{
    SOUND_ON = 0,
    SOUND_OFF = 1,
    MUSIC_ON = 2,
    MUSIC_OFF = 3,
}
