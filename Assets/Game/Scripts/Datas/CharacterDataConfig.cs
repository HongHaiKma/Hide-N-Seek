using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDataConfig : MonoBehaviour
{
    public int m_Id;
    public string m_Name;
    public float m_RunSpeed;
    public BigNumber m_Price;
    public int m_AdsCheck;
    public int m_AdsNumber;

    public void Init(int _id, string _name, float _runSpeed, BigNumber _price, int _adsCheck, int _adsNumber)
    {
        m_Id = _id;
        m_Name = _name;
        m_RunSpeed = _runSpeed;
        m_Price = _price;
        m_AdsCheck = _adsCheck;
        m_AdsNumber = _adsNumber;
    }

    public bool CheckAds()
    {
        if (m_AdsCheck == 1)
        {
            return true;
        }

        return false;
    }
}
