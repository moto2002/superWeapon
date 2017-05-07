using msg;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CollectResourcesHandler
{
	private static Action func;

	public static CSTakeResource TakeResource = new CSTakeResource();

	public static void CG_CollectResources()
	{
		if (CollectResourcesHandler.TakeResource != null)
		{
			AudioManage.inst.PlayAuido("getres", false);
			ClientMgr.GetNet().SendHttp(2006, CollectResourcesHandler.TakeResource, new DataHandler.OpcodeHandler(CollectResourcesHandler.CollectResCallBack), null);
		}
	}

	private static void CollectResCallBack(bool isError, Opcode code)
	{
		if (!isError)
		{
			List<SCTakeResShow> list = code.Get<SCTakeResShow>(10095);
			if (list.Count > 0)
			{
				foreach (SCTakeResShow current in list)
				{
					if (current.id.ToString().Length > 6)
					{
						current.value = Mathf.Max(current.value, 1);
						foreach (T_Tower current2 in SenceManager.inst.towers)
						{
							if (current2.id == current.id)
							{
								switch (UnitConst.GetInstance().buildingConst[current2.index].outputType)
								{
								case ResType.金币:
									HUDTextTool.inst.SetResIconText(LanguageManage.GetTextByKey("获得金币", "ResIsland") + current.value, current2.tr, 1.5f, ResType.金币);
									break;
								case ResType.石油:
									HUDTextTool.inst.SetResIconText(LanguageManage.GetTextByKey("获得石油", "ResIsland") + current.value, current2.tr, 1.5f, ResType.石油);
									break;
								case ResType.钢铁:
									HUDTextTool.inst.SetResIconText(LanguageManage.GetTextByKey("获得钢铁", "ResIsland") + current.value, current2.tr, 1.5f, ResType.钢铁);
									break;
								case ResType.稀矿:
									HUDTextTool.inst.SetResIconText(LanguageManage.GetTextByKey("获得稀矿", "ResIsland") + current.value, current2.tr, 1.5f, ResType.稀矿);
									break;
								}
							}
						}
					}
					else
					{
						GameObject gameObject = new GameObject();
						current.value = Mathf.Max(current.value, 1);
						long id = current.id;
						if (id >= 1L && id <= 4L)
						{
							switch ((int)(id - 1L))
							{
							case 0:
								gameObject.transform.position = new Vector3(FuncUIManager.inst.coin.transform.position.x, 0f, FuncUIManager.inst.coin.transform.position.z);
								HUDTextTool.inst.SetResIconText(LanguageManage.GetTextByKey("获得金币", "ResIsland") + current.value, gameObject.transform, 1.5f, ResType.金币);
								break;
							case 1:
								gameObject.transform.position = new Vector3(FuncUIManager.inst.oil.transform.position.x, 0f, FuncUIManager.inst.oil.transform.position.z);
								HUDTextTool.inst.SetResIconText(LanguageManage.GetTextByKey("获得石油", "ResIsland") + current.value, gameObject.transform, 1.5f, ResType.石油);
								break;
							case 2:
								gameObject.transform.position = new Vector3(FuncUIManager.inst.steel.transform.position.x, 0f, FuncUIManager.inst.steel.transform.position.z);
								HUDTextTool.inst.SetResIconText(LanguageManage.GetTextByKey("获得钢铁", "ResIsland") + current.value, gameObject.transform, 1.5f, ResType.钢铁);
								break;
							case 3:
								gameObject.transform.position = new Vector3(FuncUIManager.inst.rareEarth.transform.position.x, 0f, FuncUIManager.inst.rareEarth.transform.position.z);
								HUDTextTool.inst.SetResIconText(LanguageManage.GetTextByKey("获得稀矿", "ResIsland") + current.value, gameObject.transform, 1.5f, ResType.稀矿);
								break;
							}
						}
						UnityEngine.Object.Destroy(gameObject, 5f);
					}
				}
			}
		}
	}
}
