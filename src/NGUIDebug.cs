using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("NGUI/Internal/Debug")]
public class NGUIDebug : MonoBehaviour
{
	private static bool mRayDebug = false;

	private static List<string> mLines = new List<string>();

	private static NGUIDebug mInstance = null;

	public static bool debugRaycast
	{
		get
		{
			return NGUIDebug.mRayDebug;
		}
		set
		{
			if (Application.isPlaying)
			{
				NGUIDebug.mRayDebug = value;
				if (value)
				{
					NGUIDebug.CreateInstance();
				}
			}
		}
	}

	public static void ClearLoges()
	{
		if (NGUIDebug.mLines.Count > 0)
		{
			NGUIDebug.mLines.Clear();
		}
	}

	public static void CreateInstance()
	{
		if (NGUIDebug.mInstance == null)
		{
			GameObject gameObject = new GameObject("_NGUI Debug");
			NGUIDebug.mInstance = gameObject.AddComponent<NGUIDebug>();
			UnityEngine.Object.DontDestroyOnLoad(gameObject);
		}
	}

	private static void LogString(string text)
	{
		if (NGUIDebug.mLines.Count > 20)
		{
			NGUIDebug.mLines.RemoveAt(0);
		}
		NGUIDebug.mLines.Add(text);
		NGUIDebug.CreateInstance();
	}

	public static void Log(params object[] objs)
	{
		string text = string.Empty;
		for (int i = 0; i < objs.Length; i++)
		{
			if (i == 0)
			{
				text += objs[i].ToString();
			}
			else
			{
				text = text + ", " + objs[i].ToString();
			}
		}
		NGUIDebug.LogString(text);
	}

	public static void DrawBounds(Bounds b)
	{
		Vector3 center = b.center;
		Vector3 vector = b.center - b.extents;
		Vector3 vector2 = b.center + b.extents;
		Debug.DrawLine(new Vector3(vector.x, vector.y, center.z), new Vector3(vector2.x, vector.y, center.z), Color.green);
		Debug.DrawLine(new Vector3(vector.x, vector.y, center.z), new Vector3(vector.x, vector2.y, center.z), Color.green);
		Debug.DrawLine(new Vector3(vector2.x, vector.y, center.z), new Vector3(vector2.x, vector2.y, center.z), Color.green);
		Debug.DrawLine(new Vector3(vector.x, vector2.y, center.z), new Vector3(vector2.x, vector2.y, center.z), Color.green);
	}

	private void OnGUI()
	{
		if (GameSetting.isEditor && Application.platform != RuntimePlatform.WindowsEditor)
		{
			if (NGUIDebug.mLines.Count == 0)
			{
				if (NGUIDebug.mRayDebug && UICamera.hoveredObject != null && Application.isPlaying)
				{
					GUILayout.Label("Last Hit: " + NGUITools.GetHierarchy(UICamera.hoveredObject).Replace("\"", string.Empty), new GUILayoutOption[0]);
				}
			}
			else
			{
				if (GUI.Button(new Rect(360f, 0f, 100f, 100f), "清空Debug信息"))
				{
					NGUIDebug.ClearLoges();
				}
				int i = 0;
				int count = NGUIDebug.mLines.Count;
				while (i < count)
				{
					GUILayout.Label(NGUIDebug.mLines[i], new GUILayoutOption[0]);
					i++;
				}
			}
		}
	}
}
