using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;

public class GameTools
{
	public enum Distance_No_X_Y_Z
	{
		No = 1,
		NoX,
		NoY,
		NoZ
	}

	public static string ErrorTittle = "出错！！！ ";

	private static List<int> allIndex = new List<int>();

	public static bool NetAvailable
	{
		get
		{
			return Application.internetReachability != NetworkReachability.NotReachable;
		}
	}

	public static bool IsWifi
	{
		get
		{
			return Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork;
		}
	}

	public static void DelayMethod(float time, Action Method)
	{
		GameObject ga = CoroutineInstance.DoJob(GameTools.Do(time, Method));
		GameTools.DontDestroyOnLoad(ga);
	}

	[DebuggerHidden]
	private static IEnumerator Do(float time, Action Method)
	{
		GameTools.<Do>c__Iterator98 <Do>c__Iterator = new GameTools.<Do>c__Iterator98();
		<Do>c__Iterator.time = time;
		<Do>c__Iterator.Method = Method;
		<Do>c__Iterator.<$>time = time;
		<Do>c__Iterator.<$>Method = Method;
		return <Do>c__Iterator;
	}

	public static void RemoveComponent<T>(GameObject tar) where T : Component
	{
		if (tar.GetComponent<T>() != null)
		{
			UnityEngine.Object.Destroy(tar.GetComponent<T>());
		}
	}

	public static void RemoveComponentsInChildren<T>(GameObject tar) where T : Component
	{
		T[] componentsInChildren = tar.GetComponentsInChildren<T>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			UnityEngine.Object.Destroy(componentsInChildren[i]);
		}
	}

	public static void RemoveComponentsInParent<T>(GameObject tar) where T : Component
	{
		T[] componentsInParent = tar.GetComponentsInParent<T>();
		for (int i = 0; i < componentsInParent.Length; i++)
		{
			UnityEngine.Object.Destroy(componentsInParent[i]);
		}
	}

	public static List<T> RemoveNullInList<T>(List<T> TS) where T : class
	{
		GameTools.allIndex.Clear();
		for (int i = TS.Count - 1; i >= 0; i--)
		{
			if (TS[i] == null)
			{
				GameTools.allIndex.Add(i);
			}
		}
		for (int j = 0; j < GameTools.allIndex.Count; j++)
		{
			TS.RemoveAt(GameTools.allIndex[j]);
		}
		return TS;
	}

	public static Transform GetTranformChildByName(Transform parent, string name)
	{
		if (parent.name.ToUpper() == name.ToUpper())
		{
			return parent;
		}
		for (int i = 0; i < parent.childCount; i++)
		{
			Transform tranformChildByName = GameTools.GetTranformChildByName(parent.GetChild(i), name);
			if (tranformChildByName != null)
			{
				return tranformChildByName;
			}
		}
		return null;
	}

	public static Transform GetTranformChildByName(GameObject parent, string name)
	{
		if (parent.name.ToUpper() == name.ToUpper())
		{
			return parent.transform;
		}
		for (int i = 0; i < parent.transform.childCount; i++)
		{
			Transform tranformChildByName = GameTools.GetTranformChildByName(parent.transform.GetChild(i), name);
			if (tranformChildByName != null)
			{
				return tranformChildByName;
			}
		}
		return null;
	}

	public static void RemoveChilderns(Transform tran)
	{
		int childCount = tran.GetChildCount();
		for (int i = 0; i < childCount; i++)
		{
			UnityEngine.Object.Destroy(tran.GetChild(i).gameObject);
		}
	}

	public static Transform GetTranformActiveChildByName(Transform parent, string name)
	{
		if (parent.gameObject.activeInHierarchy && parent.name.ToUpper().Equals(name.ToUpper()))
		{
			return parent;
		}
		for (int i = 0; i < parent.childCount; i++)
		{
			Transform tranformActiveChildByName = GameTools.GetTranformActiveChildByName(parent.GetChild(i), name);
			if (tranformActiveChildByName != null)
			{
				return tranformActiveChildByName;
			}
		}
		return null;
	}

	public static float GetDistance(Vector3 x, Vector3 y, GameTools.Distance_No_X_Y_Z disType)
	{
		switch (disType)
		{
		case GameTools.Distance_No_X_Y_Z.No:
			return Vector3.Distance(x, y);
		case GameTools.Distance_No_X_Y_Z.NoX:
			return Vector2.Distance(new Vector2(x.y, x.z), new Vector2(y.y, y.z));
		case GameTools.Distance_No_X_Y_Z.NoY:
			return Vector2.Distance(new Vector2(x.x, x.z), new Vector2(y.x, y.z));
		case GameTools.Distance_No_X_Y_Z.NoZ:
			return Vector2.Distance(new Vector2(x.x, x.y), new Vector2(y.x, y.y));
		default:
			return Vector3.Distance(x, y);
		}
	}

	public static float GetDistance(List<Vector3> vec3es)
	{
		float num = 0f;
		for (int i = 1; i < vec3es.Count; i++)
		{
			num += Vector3.Distance(vec3es[i - 1], vec3es[i]);
		}
		return num;
	}

	public static Transform GetTranformActiveChildByName(GameObject parent, string name)
	{
		if (parent.activeInHierarchy && parent.name.ToUpper().Equals(name.ToUpper()))
		{
			return parent.transform;
		}
		for (int i = 0; i < parent.transform.childCount; i++)
		{
			Transform tranformActiveChildByName = GameTools.GetTranformActiveChildByName(parent.transform.GetChild(i), name);
			if (tranformActiveChildByName != null)
			{
				return tranformActiveChildByName;
			}
		}
		return null;
	}

	public static GameObject CreateChildrenInGrid(UIGrid grid, GameObject itemPrefab, string gaName)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(itemPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		gameObject.transform.parent = grid.transform;
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localScale = Vector3.one;
		gameObject.SetActive(true);
		gameObject.name = gaName;
		return gameObject;
	}

	public static GameObject CreateChildrenInTable(UITable table, GameObject itemPrefab, string gaName)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(itemPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		gameObject.transform.parent = table.transform;
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localScale = Vector3.one;
		gameObject.SetActive(true);
		gameObject.name = gaName;
		return gameObject;
	}

	public static T CreateChildren<T>(Transform tr) where T : Component
	{
		return new GameObject("child")
		{
			transform = 
			{
				parent = tr,
				localPosition = Vector3.zero
			}
		}.AddComponent<T>();
	}

	public static T GetCompentIfNoAddOne<T>(GameObject ga) where T : Component
	{
		T t = ga.GetComponent<T>();
		if (t == null)
		{
			t = ga.AddComponent<T>();
		}
		return t;
	}

	public static T GetCompentByParent<T>(GameObject ga) where T : Component
	{
		T componentInParent = ga.GetComponentInParent<T>();
		if (componentInParent)
		{
			return componentInParent;
		}
		return GameTools.GetCompentByParent<T>(ga.transform.parent);
	}

	public static T GetCompentByParent<T>(Transform ga) where T : Component
	{
		if (!ga)
		{
			return (T)((object)null);
		}
		T componentInParent = ga.GetComponentInParent<T>();
		if (componentInParent)
		{
			return componentInParent;
		}
		return GameTools.GetCompentByParent<T>(ga.transform.parent);
	}

	public static Material GetMaterial(Renderer render)
	{
		return render.sharedMaterial;
	}

	public static void SetMaterial(Renderer render, Material material)
	{
		render.sharedMaterial = material;
	}

	public static void DontDestroyOnLoad(GameObject ga)
	{
		GameStartLoadSync.AllNeedDelInReStartGame.Add(ga);
		UnityEngine.Object.DontDestroyOnLoad(ga);
	}

	public static float GetXPosInCenter(int i, float offset, int count)
	{
		return (float)(1 + i * 2 - count) * offset * 0.5f;
	}

	public static void CreateFile(string path, string name, string info)
	{
		if (!Directory.Exists(path))
		{
			Directory.CreateDirectory(path);
		}
		if (File.Exists(path + name))
		{
			File.Delete(path + name);
		}
		FileInfo fileInfo = new FileInfo(path + name);
		StreamWriter streamWriter;
		if (!fileInfo.Exists)
		{
			streamWriter = fileInfo.CreateText();
		}
		else
		{
			streamWriter = fileInfo.AppendText();
		}
		streamWriter.WriteLine(info);
		streamWriter.Close();
		streamWriter.Dispose();
	}

	public static void AppendFile(string path, string name, string info)
	{
		if (!Directory.Exists(path))
		{
			Directory.CreateDirectory(path);
		}
		FileInfo fileInfo = new FileInfo(path + name);
		StreamWriter streamWriter;
		if (!fileInfo.Exists)
		{
			streamWriter = fileInfo.CreateText();
		}
		else
		{
			streamWriter = fileInfo.AppendText();
		}
		streamWriter.WriteLine(info);
		streamWriter.Close();
		streamWriter.Dispose();
	}

	public static FileInfo[] GetFileInfos(string path)
	{
		DirectoryInfo directoryInfo = new DirectoryInfo(path);
		return directoryInfo.GetFiles();
	}

	public static void CheckString(string str, out int digitCount, out int leterCount, out int spaceCount, out int chineseLeterCount, out int ortherLeterCount)
	{
		digitCount = 0;
		leterCount = 0;
		spaceCount = 0;
		chineseLeterCount = 0;
		ortherLeterCount = 0;
		for (int i = 0; i < str.Length; i++)
		{
			if (char.IsDigit(str, i))
			{
				digitCount++;
			}
			else if (char.IsWhiteSpace(str, i))
			{
				spaceCount++;
			}
			else if (char.ConvertToUtf32(str, i) >= Convert.ToInt32("4e00", 16) && char.ConvertToUtf32(str, i) <= Convert.ToInt32("9fff", 16))
			{
				chineseLeterCount++;
			}
			else if (char.IsLetter(str, i))
			{
				leterCount++;
			}
			else
			{
				ortherLeterCount++;
			}
		}
	}

	public static int CheckStringlength(string str)
	{
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < str.Length; i++)
		{
			if (char.ConvertToUtf32(str, i) >= Convert.ToInt32("4e00", 16) && char.ConvertToUtf32(str, i) <= Convert.ToInt32("9fff", 16))
			{
				num++;
			}
			else
			{
				num2++;
			}
		}
		return num * 2 + num2;
	}
}
