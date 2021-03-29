using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDataConfig : MonoBehaviour
{
    public int m_Id;
    public string m_Name;
    public float m_RunSpeed;

    public void Init(int _id, string _name, float _runSpeed)
    {
        m_Id = _id;
        m_Name = _name;
        m_RunSpeed = _runSpeed;
    }
}
