using SimpleFramework;
using System;
using UnityEngine;

public class Client : MonoBehaviour
{
	private LuaScriptMgr luaMgr;

	private void Start()
	{
		if (!Util.CheckEnvironment())
		{
			return;
		}
		this.luaMgr = new LuaScriptMgr();
		this.luaMgr.Start();
		this.luaMgr.DoFile("System.Test");
	}

	private void Update()
	{
		if (this.luaMgr != null)
		{
			this.luaMgr.Update();
		}
	}

	private void LateUpdate()
	{
		if (this.luaMgr != null)
		{
			this.luaMgr.LateUpate();
		}
	}

	private void FixedUpdate()
	{
		if (this.luaMgr != null)
		{
			this.luaMgr.FixedUpdate();
		}
	}

	private void OnGUI()
	{
		if (GUI.Button(new Rect(10f, 10f, 120f, 50f), "Test"))
		{
			float realtimeSinceStartup = Time.realtimeSinceStartup;
			Vector3 vector = Vector3.one;
			for (int i = 0; i < 200000; i++)
			{
				vector = base.transform.position;
				base.transform.position = Vector3.one;
			}
			LogManage.Log("c# cost time: " + (Time.realtimeSinceStartup - realtimeSinceStartup));
			base.transform.position = Vector3.zero;
			this.luaMgr.CallLuaFunction("Test", new object[0]);
		}
		if (GUI.Button(new Rect(10f, 70f, 120f, 50f), "Test2"))
		{
			float realtimeSinceStartup2 = Time.realtimeSinceStartup;
			for (int j = 0; j < 200000; j++)
			{
				base.transform.Rotate(Vector3.up, 1f);
			}
			LogManage.Log("c# cost time: " + (Time.realtimeSinceStartup - realtimeSinceStartup2));
			this.luaMgr.CallLuaFunction("Test2", new object[]
			{
				base.transform
			});
		}
		if (GUI.Button(new Rect(10f, 130f, 120f, 50f), "Test3"))
		{
			float realtimeSinceStartup3 = Time.realtimeSinceStartup;
			Vector3 one = Vector3.one;
			for (int k = 0; k < 200000; k++)
			{
				one = new Vector3((float)k, (float)k, (float)k);
			}
			LogManage.Log("c# cost time: " + (Time.realtimeSinceStartup - realtimeSinceStartup3));
			this.luaMgr.CallLuaFunction("Test3", new object[]
			{
				base.transform
			});
		}
		if (GUI.Button(new Rect(10f, 190f, 120f, 50f), "Test4"))
		{
			float realtimeSinceStartup4 = Time.realtimeSinceStartup;
			for (int l = 0; l < 200000; l++)
			{
				GameObject gameObject = new GameObject();
			}
			LogManage.Log("c# cost time: " + (Time.realtimeSinceStartup - realtimeSinceStartup4));
			this.luaMgr.CallLuaFunction("Test4", new object[]
			{
				base.transform
			});
		}
		if (GUI.Button(new Rect(10f, 250f, 120f, 50f), "Test5"))
		{
			float realtimeSinceStartup5 = Time.realtimeSinceStartup;
			for (int m = 0; m < 20000; m++)
			{
				GameObject gameObject2 = new GameObject();
				gameObject2.AddComponent<SkinnedMeshRenderer>();
				SkinnedMeshRenderer component = gameObject2.GetComponent<SkinnedMeshRenderer>();
				component.castShadows = false;
				component.receiveShadows = false;
			}
			LogManage.Log("c# cost time: " + (Time.realtimeSinceStartup - realtimeSinceStartup5));
			this.luaMgr.CallLuaFunction("Test5", new object[]
			{
				base.transform
			});
		}
	}
}
