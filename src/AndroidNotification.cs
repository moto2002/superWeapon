using System;
using System.Collections.Generic;

public class AndroidNotification : INotification
{
	public AndroidNotification()
	{
		this.ClearAll();
	}

	public void Unregister(int id)
	{
	}

	public void ClearAll()
	{
	}

	public void ScheduleNotificationRepeating(int firstTriggerInSeconds, int intervalSeconds, string title, string text, int id, IDictionary<string, string> userData, string notificationProfile)
	{
	}

	public void ScheduleNotificationRepeating(DateTime firstTriggerDateTime, int intervalSeconds, string title, string text, int id, IDictionary<string, string> userData, string notificationProfile)
	{
	}

	public void ScheduleNotification(int triggerInSeconds, string title, string text, int id, IDictionary<string, string> userData, string notificationProfile)
	{
	}

	public void ScheduleNotification(DateTime triggerDateTime, string title, string text, int id, IDictionary<string, string> userData, string notificationProfile)
	{
	}
}
