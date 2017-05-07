using msg;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;
using UnityEngine;

public class BuildingQueue : MonoBehaviour
{
	public UILabel label;

	public GameObject showEfffect;

	public UILabel diamondLabel;

	public GameObject diamond;

	public static int MaxBuildingQueue;

	public static int[] BuildingQueuePriceConst;

	public void OnEnable()
	{
		this.RefeshBuildingCDQueue();
		ClientMgr.GetNet().NetDataHandler.DataChange += new DataHandler.Data_Change(this.DataHandler_DataChange);
	}

	private void DataHandler_DataChange(int opcodeCMD)
	{
		if (opcodeCMD == 10080 || opcodeCMD == 10007)
		{
			this.RefeshBuildingCDQueue();
		}
	}

	public void OnDisable()
	{
		if (ClientMgr.GetNet())
		{
			ClientMgr.GetNet().NetDataHandler.DataChange -= new DataHandler.Data_Change(this.DataHandler_DataChange);
		}
	}

	private void RefeshBuildingCDQueue()
	{
		this.label.text = string.Format("{0}/{1}", HeroInfo.GetInstance().BuildCD.Count, BuildingQueue.MaxBuildingQueue);
		if (BuildingQueue.MaxBuildingQueue < BuildingQueue.BuildingQueuePriceConst.Length)
		{
			this.diamondLabel.text = BuildingQueue.BuildingQueuePriceConst[BuildingQueue.MaxBuildingQueue].ToString();
		}
		else
		{
			this.diamond.SetActive(false);
		}
	}

	public static void LoadBuildingQueuePriceXML()
	{
		using (XmlTextReader xmlTextReader = new XmlTextReader(UnitXML.GetInstance().GetXMLTextByName("BuildWorkers"), XmlNodeType.Document, null))
		{
			List<int> list = new List<int>();
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName.Equals("configure"))
				{
					list.Add(int.Parse(xmlTextReader.GetAttribute("price")));
				}
			}
			BuildingQueue.BuildingQueuePriceConst = list.ToArray();
		}
	}

	private void Start()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_AddBuildingQueue, new EventManager.VoidDelegate(this.AddBuildingQueue));
	}

	private void AddBuildingQueue(GameObject ga)
	{
		if (BuildingQueue.BuildingQueuePriceConst.Length <= BuildingQueue.MaxBuildingQueue)
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("您的队列已达到上限，无法购买", "others") + "!", HUDTextTool.TextUITypeEnum.Num5);
			return;
		}
		MainUIPanelManage._instance.showBUYbuLING.gameObject.SetActive(true);
		MainUIPanelManage._instance.showBUYbuLING.GetComponent<ShowBuildingQua>().BuyBuildingInfo(string.Concat(new object[]
		{
			LanguageManage.GetTextByKey("花费", "others"),
			BuildingQueue.BuildingQueuePriceConst[BuildingQueue.MaxBuildingQueue],
			LanguageManage.GetTextByKey("钻石可以购买一个新的建造队列", "others"),
			"。"
		}), BuildingQueue.BuildingQueuePriceConst[BuildingQueue.MaxBuildingQueue], delegate
		{
			if (HeroInfo.GetInstance().playerRes.RMBCoin >= BuildingQueue.BuildingQueuePriceConst[BuildingQueue.MaxBuildingQueue])
			{
				CSBuyBuildingQueue cSBuyBuildingQueue = new CSBuyBuildingQueue();
				cSBuyBuildingQueue.id = 7758521;
				ClientMgr.GetNet().SendHttp(2016, cSBuyBuildingQueue, new DataHandler.OpcodeHandler(this.BuyBuildingQueueCallBack), null);
				base.StartCoroutine(this.ShowEffectClick());
			}
			else
			{
				HUDTextTool.inst.ShowBuyMoney();
			}
		}, null);
	}

	private void BuyBuildingQueueCallBack(bool isError, Opcode code)
	{
		HUDTextTool.inst.NextLuaCall("购买完建筑队列调用Lua", new object[0]);
	}

	[DebuggerHidden]
	public IEnumerator ShowEffectClick()
	{
		BuildingQueue.<ShowEffectClick>c__Iterator85 <ShowEffectClick>c__Iterator = new BuildingQueue.<ShowEffectClick>c__Iterator85();
		<ShowEffectClick>c__Iterator.<>f__this = this;
		return <ShowEffectClick>c__Iterator;
	}
}
