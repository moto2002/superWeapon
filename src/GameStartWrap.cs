using DG.Tweening;
using LuaInterface;
using System;
using UnityEngine;

public class GameStartWrap
{
	public static LuaMethod[] regs = new LuaMethod[]
	{
		new LuaMethod("AngelTo", new LuaCSFunction(GameStartWrap.AngelTo)),
		new LuaMethod("MoveTo", new LuaCSFunction(GameStartWrap.MoveTo)),
		new LuaMethod("BuildTank", new LuaCSFunction(GameStartWrap.BuildTank)),
		new LuaMethod("GetCamera", new LuaCSFunction(GameStartWrap.GetCamera)),
		new LuaMethod("DisplayLoginPanel", new LuaCSFunction(GameStartWrap.DisplayLoginPanel)),
		new LuaMethod("SetGa", new LuaCSFunction(GameStartWrap.SetGa)),
		new LuaMethod("Del", new LuaCSFunction(GameStartWrap.Del))
	};

	public static LuaField[] fields = new LuaField[0];

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "GS", typeof(GameStartWrap), GameStartWrap.regs, GameStartWrap.fields, typeof(object));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int MoveTo(IntPtr L)
	{
		GameObject unityObject = LuaScriptMgr.GetUnityObject<GameObject>(L, 1);
		Vector3 vector = LuaScriptMgr.GetVector3(L, 2);
		float duration = (float)LuaDLL.lua_tonumber(L, 3);
		unityObject.transform.DOLocalMove(vector, duration, false);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int DisplayLoginPanel(IntPtr L)
	{
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int SetGa(IntPtr L)
	{
		GameObject unityObject = LuaScriptMgr.GetUnityObject<GameObject>(L, 1);
		Vector3 vector = LuaScriptMgr.GetVector3(L, 2);
		Vector3 vector2 = LuaScriptMgr.GetVector3(L, 3);
		Vector3 vector3 = LuaScriptMgr.GetVector3(L, 4);
		unityObject.transform.localPosition = vector;
		unityObject.transform.localRotation = Quaternion.Euler(vector2);
		unityObject.transform.localScale = vector3;
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int Del(IntPtr L)
	{
		GameObject unityObject = LuaScriptMgr.GetUnityObject<GameObject>(L, 1);
		UnityEngine.Object.Destroy(unityObject);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int GetCamera(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, GameStartLua.ins.cam.gameObject);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int BuildTank(IntPtr L)
	{
		Vector3 vector = LuaScriptMgr.GetVector3(L, 1);
		Vector3 vector2 = LuaScriptMgr.GetVector3(L, 2);
		Vector3 vector3 = LuaScriptMgr.GetVector3(L, 3);
		int num = (int)LuaDLL.lua_tonumber(L, 4);
		int num2 = (int)LuaDLL.lua_tonumber(L, 5);
		if (UnitConst.GetInstance().soldierConst.Length == 0)
		{
			Debug.LogError(" UnitConst.GetInstance().soldierConst.Length == 0   ");
		}
		Body_Model modelByBundleByName = PoolManage.Ins.GetModelByBundleByName(UnitConst.GetInstance().soldierConst[num].bodyId, null);
		if (modelByBundleByName == null)
		{
			Debug.LogError(" UnitConst.GetInstance().soldierConst[index].bodyId -----------   " + UnitConst.GetInstance().soldierConst[num].bodyId);
		}
		modelByBundleByName.tr.localPosition = vector;
		modelByBundleByName.tr.localRotation = Quaternion.Euler(vector2);
		modelByBundleByName.tr.localScale = vector3;
		if (num == 7)
		{
			Animation[] componentsInChildren = modelByBundleByName.GetComponentsInChildren<Animation>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Animation animation = componentsInChildren[i];
				if (animation.GetClip("Run") != null)
				{
					animation.Play("Run");
				}
			}
		}
		switch (num)
		{
		case 1:
			PoolManage.Ins.CreatEffect("huichen_S", modelByBundleByName.tr.position - new Vector3(0f, 0f, 0.4f), modelByBundleByName.tr.rotation, modelByBundleByName.tr);
			break;
		case 2:
			PoolManage.Ins.CreatEffect("huichen_S", modelByBundleByName.tr.position - new Vector3(0f, 0f, 0.4f), modelByBundleByName.tr.rotation, modelByBundleByName.tr);
			break;
		case 3:
		case 5:
		case 6:
			PoolManage.Ins.CreatEffect("huichen_M", modelByBundleByName.tr.position - new Vector3(0f, 0f, 0.4f), modelByBundleByName.tr.rotation, modelByBundleByName.tr);
			break;
		case 4:
			PoolManage.Ins.CreatEffect("huichen_L", modelByBundleByName.tr.position - new Vector3(0f, 0f, 0.4f), modelByBundleByName.tr.rotation, modelByBundleByName.tr);
			break;
		}
		if (num2 == 0)
		{
			if (modelByBundleByName.RedModel)
			{
				modelByBundleByName.RedModel.gameObject.SetActive(false);
			}
			if (modelByBundleByName.Red_DModel)
			{
				modelByBundleByName.Red_DModel.gameObject.SetActive(false);
			}
			if (modelByBundleByName.Blue_DModel)
			{
				modelByBundleByName.Blue_DModel.gameObject.SetActive(false);
			}
		}
		else if (num2 == 1)
		{
			if (modelByBundleByName.BlueModel)
			{
				modelByBundleByName.BlueModel.gameObject.SetActive(false);
			}
			if (modelByBundleByName.Blue_DModel)
			{
				modelByBundleByName.Blue_DModel.gameObject.SetActive(false);
			}
			if (modelByBundleByName.Red_DModel)
			{
				modelByBundleByName.Red_DModel.gameObject.SetActive(false);
			}
		}
		else if (num2 == 3)
		{
			if (modelByBundleByName.BlueModel)
			{
				modelByBundleByName.BlueModel.gameObject.SetActive(false);
			}
			if (modelByBundleByName.RedModel)
			{
				modelByBundleByName.RedModel.gameObject.SetActive(false);
			}
			if (modelByBundleByName.Red_DModel)
			{
				modelByBundleByName.Red_DModel.gameObject.SetActive(false);
			}
		}
		else if (num2 == 4)
		{
			if (modelByBundleByName.BlueModel)
			{
				modelByBundleByName.BlueModel.gameObject.SetActive(false);
			}
			if (modelByBundleByName.Blue_DModel)
			{
				modelByBundleByName.Blue_DModel.gameObject.SetActive(false);
			}
			if (modelByBundleByName.RedModel)
			{
				modelByBundleByName.RedModel.gameObject.SetActive(false);
			}
		}
		LuaScriptMgr.PushObject(L, modelByBundleByName.ga);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int AngelTo(IntPtr L)
	{
		GameObject unityObject = LuaScriptMgr.GetUnityObject<GameObject>(L, 1);
		Vector3 vector = LuaScriptMgr.GetVector3(L, 2);
		float duration = (float)LuaDLL.lua_tonumber(L, 3);
		unityObject.transform.DOLocalRotate(vector, duration, RotateMode.Fast);
		return 1;
	}
}
