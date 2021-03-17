using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ConfigKeys
{
    public static string p_Idle = "Idle";
    public static string p_Run = "Run";
    public static string p_Die = "Die";
    public static string p_Win = "Win";

    public static string e_Idle = "Idle";
    public static string e_Run = "Run";
    public static string e_Catch = "Catch";

    #region CHARACTER_PREFAB

    public static string Char1 = "Char1 - Boy";
    public static string Char2 = "Char2 - BadBoy";

    #endregion

    #region ENEMY_PREFAB

    public static string Enemy1 = "Enemy1 - Zombie";
    public static string Enemy2 = "Enemy2 - Bat";
    public static string Enemy3 = "Enemy3 - Robot";

    #endregion
}

public enum EnemyKeys
{
    Enemy1 = 0,
    Enemy2,
    Enemy3,
    Enemy4,
    Enemy5,
    Enemy6
}