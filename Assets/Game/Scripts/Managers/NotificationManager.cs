using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;

public class NotificationManager : Singleton<NotificationManager>
{
    public Dictionary<CdType, string> m_NotiDict = new Dictionary<CdType, string>();

    private void Awake()
    {
        CreateFreeNormalBoxNotiChannel();
    }

    public void CreateFreeNormalBoxNotiChannel()
    {
        var c = new AndroidNotificationChannel()
        {
            Id = "Default Channel",
            Name = "Default Noti Channel",
            Importance = Importance.High,
            Description = "Generic notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(c);
    }

    public void SendNoti(int seconds, CdType cdType)
    {
        var notification = new AndroidNotification();
        notification.Title = "Hide and Seek 3D: Monster Escape";

        notification.Text = m_NotiDict[cdType];

        notification.FireTime = System.DateTime.Now.AddSeconds(seconds);
        notification.SmallIcon = "icon";
        notification.LargeIcon = "icon_0";

        // Debug.Log(m_NotiDict[cdType]);

        AndroidNotificationCenter.SendNotification(notification, "Default Channel");

        // m_NotiDict.
    }
}

public enum CdType
{
    NOTI_DAILY,
}