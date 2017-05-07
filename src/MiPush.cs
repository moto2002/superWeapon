using System;
using System.Collections.Generic;
using UnityEngine;

public class MiPush : MonoBehaviour
{
	private static AndroidJavaObject plugin;

	static MiPush()
	{
		AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		MiPush.plugin = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
	}

	public static void registerPush(string appId, string appKey)
	{
		MiPush.plugin.Call("registerPush", new object[]
		{
			appId,
			appKey
		});
	}

	public static void unregisterPush()
	{
		MiPush.plugin.Call("unregisterPush", new object[0]);
	}

	public static void setAlias(string alias)
	{
		AndroidJavaObject arg_14_0 = MiPush.plugin;
		string arg_14_1 = "setAlias";
		object[] expr_10 = new object[2];
		expr_10[0] = alias;
		arg_14_0.Call(arg_14_1, expr_10);
	}

	public static void unsetAlias(string alias)
	{
		AndroidJavaObject arg_14_0 = MiPush.plugin;
		string arg_14_1 = "unsetAlias";
		object[] expr_10 = new object[2];
		expr_10[0] = alias;
		arg_14_0.Call(arg_14_1, expr_10);
	}

	public static void subscribe(string topic)
	{
		AndroidJavaObject arg_14_0 = MiPush.plugin;
		string arg_14_1 = "subscribe";
		object[] expr_10 = new object[2];
		expr_10[0] = topic;
		arg_14_0.Call(arg_14_1, expr_10);
	}

	public static void unsubscribe(string topic)
	{
		AndroidJavaObject arg_14_0 = MiPush.plugin;
		string arg_14_1 = "unsubscribe";
		object[] expr_10 = new object[2];
		expr_10[0] = topic;
		arg_14_0.Call(arg_14_1, expr_10);
	}

	public static void pausePush()
	{
		MiPush.plugin.Call("pausePush", null);
	}

	public static void resumePush()
	{
		MiPush.plugin.Call("resumePush", null);
	}

	public static void setAcceptTime(int startHour, int startMin, int endHour, int endMin)
	{
		AndroidJavaObject arg_34_0 = MiPush.plugin;
		string arg_34_1 = "setAcceptTime";
		object[] expr_10 = new object[5];
		expr_10[0] = startHour;
		expr_10[1] = startMin;
		expr_10[2] = endHour;
		expr_10[3] = endMin;
		arg_34_0.Call(arg_34_1, expr_10);
	}

	public static void reportMessageClicked(string msgid)
	{
		MiPush.plugin.Call("reportMessageClicked", new object[]
		{
			msgid
		});
	}

	public static void checkManifest()
	{
		MiPush.plugin.Call("checkManifest", new object[0]);
	}

	public static void clearNotification()
	{
		MiPush.plugin.Call("clearNotification", new object[0]);
	}

	public static void clearNotification(int notifyId)
	{
		MiPush.plugin.Call("clearNotification", new object[]
		{
			notifyId
		});
	}

	public static void setLocalNotificationType(int type)
	{
		MiPush.plugin.Call("setLocalNotificationType", new object[]
		{
			type
		});
	}

	public static void clearLocalNotificationType()
	{
		MiPush.plugin.Call("clearLocalNotificationType", new object[0]);
	}

	public static string getRegId()
	{
		return MiPush.plugin.Call<string>("getRegId", new object[0]);
	}

	public static List<string> getAllAlias()
	{
		AndroidJavaObject objects = MiPush.plugin.Call<AndroidJavaObject>("getAllAlias", new object[0]);
		return MiPush.convetToList(objects);
	}

	public static List<string> getAllTopic()
	{
		AndroidJavaObject objects = MiPush.plugin.Call<AndroidJavaObject>("getAllTopic", new object[0]);
		return MiPush.convetToList(objects);
	}

	private static List<string> convetToList(AndroidJavaObject objects)
	{
		List<string> list = new List<string>();
		IntPtr rawObject = objects.GetRawObject();
		IntPtr rawClass = objects.GetRawClass();
		IntPtr methodID = AndroidJNI.GetMethodID(rawClass, "get", "(I)Ljava/lang/Object;");
		IntPtr methodID2 = AndroidJNI.GetMethodID(rawClass, "size", "()I");
		jvalue[] args = new jvalue[1];
		int num = AndroidJNI.CallIntMethod(rawObject, methodID2, args);
		for (int i = 0; i < num; i++)
		{
			object[] args2 = new object[]
			{
				i
			};
			jvalue[] array = AndroidJNIHelper.CreateJNIArgArray(args2);
			string item = AndroidJNI.CallStringMethod(rawObject, methodID, array);
			list.Add(item);
			AndroidJNIHelper.DeleteJNIArgArray(args2, array);
		}
		return list;
	}
}
