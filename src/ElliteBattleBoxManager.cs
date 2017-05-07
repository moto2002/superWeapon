using msg;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ElliteBattleBoxManager : MonoBehaviour
{
	public static ElliteBattleBoxManager _inst;

	public int ElliteBattleBoxMax;

	public UISprite[] EB_Box;

	public UISprite[] EB_RedLine;

	public DieBall[] EB_Box_Effect;

	public GameObject Notice;

	public GameObject Notice_Back;

	public UILabel Notice_Title;

	public UILabel Notice_Label;

	public UISprite ElliteBattleShow_BJ;

	public UIGrid ElliteBattleShow_Grid;

	public GameObject ElliteBattleShow_ResGa;

	public GameObject ElliteBattleShow_ItemGa;

	private int basic_box_id;

	public void OnDestroy()
	{
		ElliteBattleBoxManager._inst = null;
	}

	private void Awake()
	{
		ElliteBattleBoxManager._inst = this;
		this.Init();
	}

	public void CloseElliteBattleShow()
	{
		this.OpenElliteBattleShow(this.basic_box_id);
	}

	public void OpenElliteBattleShow(int box_id)
	{
		if (this.basic_box_id != box_id)
		{
			this.ElliteBattleShow_BJ.gameObject.SetActive(true);
			this.basic_box_id = box_id;
			int num = 0;
			float num2 = 0.4f;
			this.ElliteBattleShow_Grid.ClearChild();
			box_id = (int)(from a in UnitConst.GetInstance().ElliteBattleBoxList
			where a.Value.level == HeroInfo.GetInstance().playerlevel && a.Value.star == box_id
			select a).ToList<KeyValuePair<int, ElliteBattleBox>>().First<KeyValuePair<int, ElliteBattleBox>>().Value.id;
			if (UnitConst.GetInstance().ElliteBattleBoxList.ContainsKey(box_id))
			{
				float num3 = 0.6f;
				if (UnitConst.GetInstance().ElliteBattleBoxList[box_id].diamond > 0)
				{
					GameObject gameObject = NGUITools.AddChild(this.ElliteBattleShow_Grid.gameObject, this.ElliteBattleShow_ResGa);
					AtlasManage.SetResSpriteName(gameObject.GetComponent<resPrefabScript>().resName, ResType.钻石);
					gameObject.GetComponent<resPrefabScript>().resName.SetDimensions((int)(num3 * 42f), (int)(num3 * 42f));
					gameObject.GetComponent<resPrefabScript>().bg.SetDimensions((int)(num3 * 96f), (int)(num3 * 96f));
					gameObject.GetComponent<resPrefabScript>().name_Client.text = string.Empty;
					gameObject.GetComponent<resPrefabScript>().resCount.text = UnitConst.GetInstance().ElliteBattleBoxList[box_id].diamond.ToString();
					gameObject.GetComponent<resPrefabScript>().name_Client.text = LanguageManage.GetTextByKey("钻石", "ResIsland");
					num++;
					gameObject.transform.localScale = Vector3.zero;
					TweenScale.Begin(gameObject, num2, Vector3.one);
				}
				foreach (KeyValuePair<ResType, int> current in UnitConst.GetInstance().ElliteBattleBoxList[box_id].res)
				{
					GameObject gameObject2 = NGUITools.AddChild(this.ElliteBattleShow_Grid.gameObject, this.ElliteBattleShow_ResGa);
					AtlasManage.SetResSpriteName(gameObject2.GetComponent<resPrefabScript>().resName, current.Key);
					gameObject2.GetComponent<resPrefabScript>().resName.SetDimensions((int)(num3 * 42f), (int)(num3 * 42f));
					gameObject2.GetComponent<resPrefabScript>().bg.SetDimensions((int)(num3 * 96f), (int)(num3 * 96f));
					gameObject2.GetComponent<resPrefabScript>().name_Client.text = string.Empty;
					gameObject2.GetComponent<resPrefabScript>().resCount.text = current.Value.ToString();
					switch (current.Key)
					{
					case ResType.金币:
						gameObject2.GetComponent<resPrefabScript>().name_Client.text = LanguageManage.GetTextByKey("金币", "ResIsland");
						break;
					case ResType.石油:
						gameObject2.GetComponent<resPrefabScript>().name_Client.text = LanguageManage.GetTextByKey("石油", "ResIsland");
						break;
					case ResType.钢铁:
						gameObject2.GetComponent<resPrefabScript>().name_Client.text = LanguageManage.GetTextByKey("钢铁", "ResIsland");
						break;
					case ResType.稀矿:
						gameObject2.GetComponent<resPrefabScript>().name_Client.text = LanguageManage.GetTextByKey("稀矿", "ResIsland");
						break;
					case ResType.钻石:
						gameObject2.GetComponent<resPrefabScript>().name_Client.text = LanguageManage.GetTextByKey("钻石", "ResIsland");
						break;
					case ResType.军令:
						gameObject2.GetComponent<resPrefabScript>().name_Client.text = LanguageManage.GetTextByKey("军令", "ResIsland");
						break;
					}
					IL_3C0:
					num++;
					gameObject2.transform.localScale = Vector3.zero;
					TweenScale.Begin(gameObject2, num2, Vector3.one);
					continue;
					goto IL_3C0;
				}
				foreach (KeyValuePair<int, int> current2 in UnitConst.GetInstance().ElliteBattleBoxList[box_id].item)
				{
					GameObject gameObject3 = NGUITools.AddChild(this.ElliteBattleShow_Grid.gameObject, this.ElliteBattleShow_ItemGa);
					AtlasManage.SetUiItemAtlas(gameObject3.GetComponent<itemPrefabScript>().itemName, UnitConst.GetInstance().ItemConst[current2.Key].IconId);
					AtlasManage.SetQuilitySpriteName(gameObject3.GetComponent<itemPrefabScript>().itemQuality, UnitConst.GetInstance().ItemConst[current2.Key].Quality);
					gameObject3.GetComponent<itemPrefabScript>().itemName.SetDimensions((int)(num3 * 72f), (int)(num3 * 72f));
					gameObject3.GetComponent<itemPrefabScript>().itemQuality.SetDimensions((int)(num3 * 96f), (int)(num3 * 96f));
					gameObject3.GetComponent<itemPrefabScript>().itemCount.text = current2.Value.ToString();
					gameObject3.GetComponent<itemPrefabScript>().des.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().ItemConst[current2.Key].Name, "item");
					num++;
					gameObject3.transform.localScale = Vector3.zero;
					TweenScale.Begin(gameObject3, num2, Vector3.one);
				}
				if (Loading.senceType == SenceType.WorldMap)
				{
					string b = string.Empty;
					switch (UnitConst.GetInstance().ElliteBattleBoxList[box_id].star)
					{
					case 1:
						b = "一星精英副本";
						break;
					case 2:
						b = "二星精英副本";
						break;
					case 3:
						b = "三星精英副本";
						break;
					case 4:
						b = "四星精英副本";
						break;
					case 5:
						b = "五星精英副本";
						break;
					}
					foreach (KeyValuePair<int, T_Island> current3 in T_WMap.inst.islandList)
					{
						if (current3.Value.ownerName == b)
						{
							if (CameraSmoothMove.inst)
							{
								CameraSmoothMove.inst.MovePosition(new Vector3(current3.Value.tr.position.x - 0.3f, 0f, current3.Value.tr.position.z - 0.3f), null);
							}
							break;
						}
					}
				}
			}
			this.ElliteBattleShow_BJ.width = 0;
			TweenWidth.Begin(this.ElliteBattleShow_BJ, num2 - 0.1f, (num - 1) * 100 + 20);
			base.StartCoroutine(this.ElliteBattleShow_Grid.RepositionAfterFrame());
		}
		else
		{
			this.basic_box_id = 0;
			this.ElliteBattleShow_Grid.ClearChild();
			this.ElliteBattleShow_BJ.gameObject.SetActive(false);
		}
	}

	private void OnEnable()
	{
		this.Init();
		this.ElliteBattleShow_BJ.gameObject.SetActive(false);
	}

	private void Start()
	{
		for (int i = 0; i < 5; i++)
		{
			this.EB_Box[i].GetComponent<ButtonClick>().eventType = EventManager.EventType.EliteBattleBox;
		}
		this.Notice_Back.GetComponent<ButtonClick>().eventType = EventManager.EventType.EliteBattleBox;
		EventManager.Instance.AddEvent(EventManager.EventType.EliteBattleBox, new EventManager.VoidDelegate(this.OpenEliteBattleBox));
		this.Notice.gameObject.SetActive(false);
		this.Init();
	}

	private void Update()
	{
		if (this.ElliteBattleBoxMax != SenceManager.inst.ElliteBallteBoxMax)
		{
			this.ElliteBattleBoxMax = SenceManager.inst.ElliteBallteBoxMax;
			this.Init();
		}
	}

	private void OpenEliteBattleBox(GameObject ga)
	{
		if (ga.name == "bg")
		{
			this.Notice.gameObject.SetActive(false);
			return;
		}
		if (ga.name == "Box")
		{
			return;
		}
		if (ga.name.Substring(0, 1) == "_")
		{
			this.OpenElliteBattleShow(int.Parse(ga.name.Replace("_", string.Empty)));
		}
		else if (ga.name.Substring(0, 1) != "i")
		{
			Debug.Log("打开精英关卡宝箱：" + ga.name);
			ElliteBattleBoxManager.CS_OpenEliteBox(int.Parse(ga.name));
		}
	}

	public static void CS_OpenEliteBox(int box_id)
	{
		CSOpenEliteBox cSOpenEliteBox = new CSOpenEliteBox();
		cSOpenEliteBox.boxId = box_id;
		Debug.Log("打开精英关卡宝箱ID：" + cSOpenEliteBox.boxId);
		ClientMgr.GetNet().SendHttp(5032, cSOpenEliteBox, new DataHandler.OpcodeHandler(ElliteBattleBoxManager.SCOpenEliteBoxCallBack), null);
	}

	public static void SCOpenEliteBoxCallBack(bool Error, Opcode func)
	{
		Debug.Log("打开精英关卡宝箱回调");
		if (!Error)
		{
			ShowAwardPanelManger.showAwardList();
		}
	}

	public void Init()
	{
		if (SenceManager.inst.ShowNotice)
		{
			SenceManager.inst.ShowNotice = false;
			this.Notice.gameObject.SetActive(true);
		}
		for (int i = 0; i < 5; i++)
		{
			this.EB_Box[i].name = "_" + (i + 1);
			this.EB_Box[i].spriteName = "精英" + (i + 1) + "关";
			if (i < this.ElliteBattleBoxMax)
			{
				this.EB_Box[i].color = Color.white;
				this.EB_RedLine[i].enabled = true;
			}
			else
			{
				float num = 0.55f;
				this.EB_Box[i].color = new Color(num, num, num, 1f);
				this.EB_RedLine[i].enabled = false;
			}
			if (this.EB_Box_Effect[i] != null)
			{
				UnityEngine.Object.Destroy(this.EB_Box_Effect[i].ga);
			}
		}
		foreach (KeyValuePair<int, ElliteBattleBox> current in SenceManager.inst.ElliteBallteBoxes)
		{
			if (current.Value.state == 0)
			{
				int num2 = UnitConst.GetInstance().ElliteBattleBoxList[(int)current.Value.id].star - 1;
				this.EB_Box[num2].name = string.Empty + current.Value.id;
				string resName = string.Empty;
				switch (UnitConst.GetInstance().ElliteBattleBoxList[(int)current.Value.id].star)
				{
				case 1:
					resName = "yiji";
					break;
				case 2:
					resName = "liangji";
					break;
				case 3:
					resName = "sanji";
					break;
				case 4:
					resName = "sanji";
					break;
				case 5:
					resName = "sanji";
					break;
				}
				this.EB_Box_Effect[num2] = PoolManage.Ins.CreatEffect(resName, this.EB_Box[num2].transform.position, Quaternion.identity, this.EB_Box[num2].transform);
				Transform[] componentsInChildren = this.EB_Box_Effect[num2].GetComponentsInChildren<Transform>();
				for (int j = 0; j < componentsInChildren.Length; j++)
				{
					Transform transform = componentsInChildren[j];
					transform.gameObject.layer = 8;
				}
			}
			else if (current.Value.state == 1)
			{
				int num3 = UnitConst.GetInstance().ElliteBattleBoxList[(int)current.Value.id].star - 1;
				this.EB_Box[num3].spriteName = "精英" + (num3 + 1) + "开";
				this.EB_Box[num3].name = "isRecetive_" + current.Value.id;
			}
		}
	}
}
