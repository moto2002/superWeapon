using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GetawardPanelShow : FuncUIPanel
{
	public GameObject UIgrid;

	public UISprite getAward;

	public GameObject UIscrollView;

	private UIGrid itemGrid;

	public GameObject getAwardPrf;

	public GameObject resourcesPanel;

	public static int DayNum;

	public static long LastResuNum;

	private int k;

	public bool isCanSign;

	private List<GameObject> go = new List<GameObject>();

	public static GetawardPanelShow _ins;

	public LangeuageLabel title;

	public GameObject closeGetAward;

	public bool isSign;

	public GetTawaedItem[] allReward = new GetTawaedItem[7];

	private UIScrollView itemScrollView;

	public static int getDay;

	private int curDaytrans;

	public static List<int> getId = new List<int>();

	public void OnDestroy()
	{
		GetawardPanelShow._ins = null;
	}

	public new void Awake()
	{
		GetawardPanelShow._ins = this;
		this.ShowSeven();
		if (SevenDayMgr.state.Contains(0))
		{
			this.isSign = true;
		}
		this.title.text = LanguageManage.GetTextByKey("7天累计登录", "others");
		this.itemGrid = this.UIgrid.GetComponent<UIGrid>();
		this.init();
		base.Awake();
	}

	public void ShowSeven()
	{
		if (this.isSign)
		{
			this.getAward.GetComponent<BoxCollider>().enabled = true;
			this.getAward.GetComponent<UIButton>().enabled = true;
			this.getAward.spriteName = "十连抽招募";
		}
		else
		{
			this.getAward.GetComponent<BoxCollider>().enabled = false;
			this.getAward.GetComponent<UIButton>().enabled = false;
			this.getAward.spriteName = "hui";
		}
		for (int i = 0; i < UnitConst.GetInstance().SevenDay.Count; i++)
		{
			this.allReward[i].name = (i + 1).ToString();
			GetTawaedItem getTawaedItem = this.allReward[i];
			getTawaedItem.state = SevenDayMgr.state[i];
			int num = i + 1;
			getTawaedItem.DayNumber.text = LanguageManage.GetTextByKey("第" + num + "天", "others");
			if (getTawaedItem.state == 0)
			{
				if (int.Parse(getTawaedItem.name) != 7)
				{
					getTawaedItem.effect = PoolManage.Ins.CreatEffect("diyitian_qiandao", getTawaedItem.bg.transform.position, getTawaedItem.bg.transform.rotation, getTawaedItem.bg.transform);
					getTawaedItem.effect.ga.AddComponent<RenderQueueEdit>();
				}
				getTawaedItem.isGetSign.gameObject.SetActive(false);
				getTawaedItem.DayNumber.GetComponent<UILabel>().color = new Color(1f, 0.8392157f, 0.08627451f);
				getTawaedItem.cantGgt.gameObject.SetActive(false);
			}
			if (getTawaedItem.state == 1)
			{
				if (getTawaedItem.effect)
				{
					UnityEngine.Object.Destroy(getTawaedItem.effect.ga);
				}
				getTawaedItem.isGetSign.gameObject.SetActive(true);
				getTawaedItem.haveGet.gameObject.SetActive(true);
				getTawaedItem.cantGgt.gameObject.SetActive(false);
				getTawaedItem.DayNumber.GetComponent<UILabel>().color = new Color(1f, 1f, 1f);
			}
			if (getTawaedItem.state == 2)
			{
				getTawaedItem.DayNumber.GetComponent<UILabel>().color = new Color(1f, 1f, 1f);
			}
			if (i == 6)
			{
				getTawaedItem.effect1 = PoolManage.Ins.CreatEffect("diqitian_qiandao", getTawaedItem.bg.transform.position, getTawaedItem.bg.transform.rotation, getTawaedItem.bg.transform);
				getTawaedItem.effect1.ga.AddComponent<RenderQueueEdit>();
				if (getTawaedItem.state == 1 && getTawaedItem.effect1)
				{
					UnityEngine.Object.Destroy(getTawaedItem.effect1.ga);
				}
				getTawaedItem.QualitSprite.gameObject.SetActive(false);
				getTawaedItem.itemName.text = LanguageManage.GetTextByKey("神秘宝箱", "others");
				getTawaedItem.itemIcon.spriteName = "lv10";
				getTawaedItem.count.gameObject.SetActive(false);
			}
			if (UnitConst.GetInstance().SevenDay[i + 1].goldBox != 1)
			{
				if (UnitConst.GetInstance().SevenDay[i + 1].type == 2)
				{
					getTawaedItem.QualitSprite.gameObject.SetActive(false);
					AtlasManage.SetResSpriteName(getTawaedItem.itemIcon, ResType.钻石);
					getTawaedItem.count.text = UnitConst.GetInstance().SevenDay[i + 1].money.ToString();
					getTawaedItem.itemName.text = LanguageManage.GetTextByKey("钻石", "others");
				}
				if (UnitConst.GetInstance().SevenDay[i + 1].type == 4)
				{
					getTawaedItem.QualitSprite.gameObject.SetActive(false);
					AtlasManage.SetResSpriteName(getTawaedItem.itemIcon, ResType.石油);
					getTawaedItem.count.text = UnitConst.GetInstance().SevenDay[i + 1].res[ResType.石油].ToString();
					getTawaedItem.itemName.text = LanguageManage.GetTextByKey("石油", "others");
				}
				if (UnitConst.GetInstance().SevenDay[i + 1].type == 1)
				{
					getTawaedItem.QualitSprite.gameObject.SetActive(false);
					AtlasManage.SetResSpriteName(getTawaedItem.itemIcon, ResType.金币);
					getTawaedItem.count.text = UnitConst.GetInstance().SevenDay[i + 1].res[ResType.金币].ToString();
					getTawaedItem.itemName.text = LanguageManage.GetTextByKey("金币", "others");
				}
				if (UnitConst.GetInstance().SevenDay[i + 1].type == 3)
				{
					LogManage.Log("test" + UnitConst.GetInstance().SevenDay[i + 1].items.Keys.ToList<int>()[0]);
					getTawaedItem.QualitSprite.gameObject.SetActive(true);
					getTawaedItem.itemIcon.width = 90;
					getTawaedItem.itemIcon.height = 80;
					getTawaedItem.itemIcon.transform.localPosition = new Vector3(-1f, 29f, 0f);
					getTawaedItem.itemIcon.gameObject.AddComponent<ItemTipsShow2>();
					getTawaedItem.itemIcon.GetComponent<ItemTipsShow2>().JianTouPostion = 4;
					getTawaedItem.itemIcon.GetComponent<ItemTipsShow2>().Type = 1;
					getTawaedItem.itemIcon.GetComponent<ItemTipsShow2>().Index = UnitConst.GetInstance().SevenDay[i + 1].items.Keys.ToList<int>()[0];
					AtlasManage.SetUiItemAtlas(getTawaedItem.itemIcon, UnitConst.GetInstance().ItemConst[UnitConst.GetInstance().SevenDay[i + 1].items.Keys.ToList<int>()[0]].IconId);
					AtlasManage.SetQuilitySpriteName(getTawaedItem.QualitSprite, UnitConst.GetInstance().ItemConst[UnitConst.GetInstance().SevenDay[i + 1].items.Keys.ToList<int>()[0]].Quality);
					getTawaedItem.count.text = UnitConst.GetInstance().SevenDay[i + 1].items[UnitConst.GetInstance().SevenDay[i + 1].items.Keys.ToList<int>()[0]].ToString();
					getTawaedItem.count.transform.localPosition = new Vector3(20.75f, -52.14f, 0f);
					getTawaedItem.itemName.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().ItemConst[UnitConst.GetInstance().SevenDay[i + 1].items.Keys.ToList<int>()[0]].Name, "item");
				}
			}
		}
	}

	private void init()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.GetAwardPanelClose, new EventManager.VoidDelegate(this.OnPanelClose));
		EventManager.Instance.AddEvent(EventManager.EventType.SignPanel_Sign, new EventManager.VoidDelegate(this.OnGetClick));
	}

	private void OnPanelClose(GameObject go)
	{
		FuncUIManager.inst.HideFuncUI("GetawardPanel");
	}

	public override void OnEnable()
	{
		if (GetawardPanelShow.getId.Count > 0)
		{
			this.getAward.GetComponent<BoxCollider>().enabled = true;
			this.getAward.GetComponent<ButtonClick>().enabled = true;
			this.getAward.spriteName = "十连抽招募";
		}
		ClientMgr.GetNet().NetDataHandler.DataChange += new DataHandler.Data_Change(this.DataHandler_DataChange);
		base.OnEnable();
	}

	private void DataHandler_DataChange(int opcodeCMD)
	{
		LogManage.Log("test" + opcodeCMD);
		this.OnGetTawardItem();
	}

	public override void OnDisable()
	{
		base.OnDisable();
		ClientMgr.GetNet().NetDataHandler.DataChange -= new DataHandler.Data_Change(this.DataHandler_DataChange);
	}

	public void OnGetTawardItem()
	{
		for (int i = 0; i < this.allReward.Length; i++)
		{
			GetTawaedItem getTawaedItem = this.allReward[i];
			getTawaedItem.state = SevenDayMgr.state[i];
			if (getTawaedItem.state == 0)
			{
				getTawaedItem.isGetSign.gameObject.SetActive(false);
				getTawaedItem.DayNumber.GetComponent<UILabel>().color = new Color(1f, 0.8392157f, 0.08627451f);
				getTawaedItem.cantGgt.gameObject.SetActive(false);
				if (getTawaedItem.effect)
				{
					getTawaedItem.effect.ga.AddComponent<RenderQueueEdit>();
				}
				else
				{
					getTawaedItem.effect = PoolManage.Ins.CreatEffect("diyitian_qiandao", getTawaedItem.bg.transform.position, getTawaedItem.bg.transform.rotation, getTawaedItem.bg.transform);
					getTawaedItem.effect.ga.AddComponent<RenderQueueEdit>();
				}
			}
			if (getTawaedItem.state == 1)
			{
				if (i != 6 && getTawaedItem.effect)
				{
					UnityEngine.Object.Destroy(getTawaedItem.effect.ga);
				}
				if (i == 6 && getTawaedItem.effect1)
				{
					UnityEngine.Object.Destroy(getTawaedItem.effect1.gameObject);
				}
				getTawaedItem.isGetSign.gameObject.SetActive(true);
				getTawaedItem.haveGet.gameObject.SetActive(true);
				getTawaedItem.cantGgt.gameObject.SetActive(false);
				getTawaedItem.DayNumber.GetComponent<UILabel>().color = new Color(1f, 1f, 1f);
			}
			if (getTawaedItem.state == 2)
			{
				getTawaedItem.DayNumber.GetComponent<UILabel>().color = new Color(1f, 1f, 1f);
			}
			int num = i + 1;
			getTawaedItem.DayNumber.text = LanguageManage.GetTextByKey("第" + num + "天", "others");
		}
	}

	public void OnGetClick(GameObject o)
	{
		SevenDayHandler.CS_SevenDay(GetawardPanelShow.getId[0], delegate(bool isError)
		{
			if (!isError)
			{
				ShowAwardPanelManger.showAwardList();
			}
		});
		GetawardPanelShow.getId.RemoveAt(0);
		if (GetawardPanelShow.getId.Count <= 0)
		{
			this.getAward.GetComponent<BoxCollider>().enabled = false;
			this.getAward.GetComponent<UIButton>().enabled = false;
			this.getAward.spriteName = "hui";
		}
	}
}
