using System;
using System.Collections.Generic;
using UnityEngine;

public class NotificationManager
{
	private class DefaultNotification : INotification
	{
		public void ScheduleNotificationRepeating(int firstTriggerInSeconds, int intervalSeconds, string title, string text, int id, IDictionary<string, string> userData = null, string notificationProfile = null)
		{
			Debug.Log("此平台不支持推送");
		}

		public void ScheduleNotificationRepeating(DateTime firstTriggerDateTime, int intervalSeconds, string title, string text, int id, IDictionary<string, string> userData = null, string notificationProfile = null)
		{
			Debug.Log("此平台不支持推送");
		}

		public void ScheduleNotification(int triggerInSeconds, string title, string text, int id, IDictionary<string, string> userData = null, string notificationProfile = null)
		{
			Debug.Log("此平台不支持推送");
		}

		public void ScheduleNotification(DateTime triggerDateTime, string title, string text, int id, IDictionary<string, string> userData = null, string notificationProfile = null)
		{
			Debug.Log("此平台不支持推送");
		}

		public void Unregister(int id)
		{
			Debug.Log("此平台不支持推送");
		}

		public void ClearAll()
		{
			Debug.Log("此平台不支持推送");
		}
	}

	private INotification m_notification;

	private static NotificationManager s_instance;

	public static NotificationManager Instance
	{
		get
		{
			if (NotificationManager.s_instance == null)
			{
				NotificationManager.s_instance = new NotificationManager();
			}
			return NotificationManager.s_instance;
		}
	}

	private NotificationManager()
	{
		this.m_notification = this.CreateObj();
	}

	private INotification CreateObj()
	{
		return new AndroidNotification();
	}

	public void ScheduleNotificationRepeating(int firstTriggerInSeconds, int intervalSeconds, string title, string text, int id, IDictionary<string, string> userData, string notificationProfile)
	{
		this.m_notification.ScheduleNotificationRepeating(firstTriggerInSeconds, intervalSeconds, title, text, id, userData, notificationProfile);
	}

	public void ScheduleNotificationRepeating(DateTime firstTriggerDateTime, int intervalSeconds, string title, string text, int id, IDictionary<string, string> userData, string notificationProfile)
	{
		this.m_notification.ScheduleNotificationRepeating(firstTriggerDateTime, intervalSeconds, title, text, id, userData, notificationProfile);
	}

	public void ScheduleNotification(int triggerInSeconds, string title, string text, int id, IDictionary<string, string> userData, string notificationProfile)
	{
		this.m_notification.ScheduleNotification(triggerInSeconds, title, text, id, userData, notificationProfile);
	}

	public void ScheduleNotification(DateTime triggerDateTime, string title, string text, int id, IDictionary<string, string> userData, string notificationProfile)
	{
		this.m_notification.ScheduleNotification(triggerDateTime, title, text, id, userData, notificationProfile);
	}

	public void Unregister(int id)
	{
		this.m_notification.Unregister(id);
	}

	public void Unregister(NotificationType type)
	{
		this.Unregister((int)type);
	}

	public void ClearAll()
	{
		this.m_notification.ClearAll();
	}

	public void UpdateWhenLoginComplete()
	{
	}
}
