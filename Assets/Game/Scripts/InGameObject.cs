using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameObject : MonoBehaviour
{
    public ObjectType m_ObjectType;
    public Transform tf_Owner;
    public Animator anim_Owner;
}

public enum ObjectType
{
    CHAR = 0,
    ENEMY = 1,
    KEY = 2,
    DOOR = 3,
    GOLD = 4,
}