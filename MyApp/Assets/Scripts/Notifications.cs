using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;

public class Notifications : MonoBehaviour
{
    public enum Importance
    {
        normal,
        high,
    }

    private void Start()
    {
        // Remove Notifications That Have Already Been Displayed
        AndroidNotificationCenter.CancelAllDisplayedNotifications();

        // Create The Android Notification Channel To Send Messages Through
        var defaultChannel = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Notifications Channel",
            Importance = Unity.Notifications.Android.Importance.Default,
            Description = "Reminder notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(defaultChannel);
    }

    public static void SendNotification(string title, string text, System.DateTime sendDateTime, bool repeats, System.TimeSpan intervalTime)
    {
        var notification = new AndroidNotification();
        notification.Title = title;
        notification.Text = text;
        notification.LargeIcon = "Icon";
        notification.SmallIcon = "Icon";
        notification.FireTime = sendDateTime;
        if (repeats) notification.RepeatInterval = intervalTime;

        var id = AndroidNotificationCenter.SendNotification(notification, "channel_id");

        if (AndroidNotificationCenter.CheckScheduledNotificationStatus(id) == NotificationStatus.Scheduled)
        {
            AndroidNotificationCenter.CancelAllNotifications();
            AndroidNotificationCenter.SendNotification(notification, "channel_id");
        }
    }
}
