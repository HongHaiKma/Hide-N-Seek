using UnityEngine;
using System;

public class CoolDown
{
    protected CdType cdType;
    protected bool noti;

    protected double cdTime;
    protected double maxCdTime;
    protected string cdTimeData;
    protected string oldDate;

    protected string cdTimeOutEventStr;
    protected string cdRunningEventStr;
    protected string cdTimeTextEventStr;

    protected int canClaim;

    public CoolDown()
    {

    }

    public CoolDown(string cdTimeOutEventStr, string cdRunningEventStr, string cdTimeTextEventStr, string cdTimeData, string oldDate, double maxCdTime, CdType cdType, bool noti = true)
    {
        this.cdTimeOutEventStr = cdTimeOutEventStr;
        this.cdRunningEventStr = cdRunningEventStr;
        this.cdTimeTextEventStr = cdTimeTextEventStr;

        this.cdTimeData = cdTimeData;
        this.oldDate = oldDate;
        this.maxCdTime = maxCdTime;

        this.cdType = cdType;
        this.noti = noti;
    }

    public CoolDown(string cdTimeOutEventStr, string cdRunningEventStr, string cdTimeData, string oldDate, double maxCdTime, CdType cdType, bool noti = false)
    {
        this.cdTimeOutEventStr = cdTimeOutEventStr;
        this.cdRunningEventStr = cdRunningEventStr;
        this.cdTimeTextEventStr = "";

        this.cdTimeData = cdTimeData;
        this.oldDate = oldDate;
        this.maxCdTime = maxCdTime;

        this.cdType = cdType;
        this.noti = noti;
        // this.noti = noti;
    }

    public CoolDown(string cdTimeOutEventStr, string cdRunningEventStr, string cdTimeData, string oldDate, double maxCdTime, bool noti = false)
    {
        this.cdTimeOutEventStr = cdTimeOutEventStr;
        this.cdRunningEventStr = cdRunningEventStr;
        this.cdTimeTextEventStr = "";

        this.cdTimeData = cdTimeData;
        this.oldDate = oldDate;
        this.maxCdTime = maxCdTime;

        this.cdType = cdType;
        this.noti = noti;
        // this.noti = noti;
    }

    public void Awake()
    {
        if (!PlayerPrefs.HasKey(cdTimeData)) //====> first time set cdTime = 0
        {
            cdTime = 0;
            canClaim = 0;
        }
        else  //====> first time set cdTime = 0
        {
            cdTime = double.Parse(PlayerPrefs.GetString(cdTimeData));

            long oldDateTemp = Convert.ToInt64(PlayerPrefs.GetString(this.oldDate));

            DateTime oldDate = DateTime.FromBinary(oldDateTemp);

            TimeSpan timePassed = DateTime.Now - oldDate;

            if (cdTime > 0)
            {
                cdTime -= timePassed.TotalSeconds;
            }

            if (cdTime > 0)
            {
                canClaim = 0;
                EventManager2.TriggerEvent(cdRunningEventStr);
            }
            else
            {
                canClaim = 1;
                EventManager2.TriggerEvent(cdTimeOutEventStr);
            }

            // if (cdTime > 2 && noti)
            // {

            //     NotificationManager.Instance.SendNoti((int)GetCdTimeByDigits(), cdType);
            // }
        }
    }

    public void OnUpdate()
    {
        CountingCdTime();
    }

    public void CountingCdTime()
    {
        if (cdTime > 0)
        {
            cdTime -= Time.deltaTime;
            EventManager2.TriggerEvent(cdTimeTextEventStr);
        }
        else //if cdTime <= 0 => Set canClaim = 1 => if canClaim = 0
        {
            if (canClaim == 0)
            {
                EventManager2.TriggerEvent(cdTimeOutEventStr);

                canClaim = 1;
            }
        }

        Save();
    }

    public string GetCdTimeRemaining()
    {
        return Helper.ConvertSecondsToDateTimeFormat(3, cdTime);
    }

    public string GetCdTimeRemaining(int _convertType)
    {
        return Helper.ConvertSecondsToDateTimeFormat(_convertType, cdTime);
    }

    public double GetCdTimeByDigits()
    {
        return cdTime;
    }

    public bool IsCooldown()
    {
        return (cdTime > 0);
    }

    public void Save()
    {
        PlayerPrefs.SetString(cdTimeData, cdTime.ToString());
        PlayerPrefs.SetString(oldDate, DateTime.Now.ToBinary().ToString());
    }

    public virtual void BeginCoolDown()
    {
        cdTime = maxCdTime;
        canClaim = 0;

        if (noti)
        {
            NotificationManager.Instance.SendNoti((int)GetCdTimeByDigits(), cdType);
        }
    }

    public void StopCoolDown()
    {
        cdTime = 0;
    }
}