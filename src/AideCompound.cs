using DicForUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class AideCompound : MonoBehaviour
{
	public static AideCompound _instance;

	public UITable aideTable;

	public UILabel haveSoilderCount;

	public LangeuageLabel info;

	public UIButton aide1;

	public UIButton aide2;

	public UIButton aide3;

	public GameObject resInfo;

	public List<AideDateUI> allAideUI = new List<AideDateUI>();

	public int timeId;

	public List<Aide> aideList = new List<Aide>();

	public GameObject itemWrite;

	public UILabel itemLabel;

	public GameObject[] itemAray = new GameObject[10];

	public GameObject back;

	public GameObject aideItem;

	public GameObject close;

	public void OnDestroy()
	{
		AideCompound._instance = null;
	}

	private void Awake()
	{
		AideCompound._instance = this;
		this.ShowAideComp();
	}

	public void ShowAideComp()
	{
		this.back = base.transform.FindChild("backImg").gameObject;
		this.back.AddComponent<ButtonClick>();
		ButtonClick component = this.back.GetComponent<ButtonClick>();
		component.eventType = EventManager.EventType.none;
		this.aideItem = base.transform.FindChild("aideItem").gameObject;
		this.aideItem.AddComponent<AideDateUI>();
		this.close = base.transform.FindChild("close").gameObject;
		this.close.AddComponent<ButtonClick>();
		ButtonClick component2 = this.close.GetComponent<ButtonClick>();
		component2.eventType = EventManager.EventType.AdjutantPanle_Close;
		this.aideTable = base.transform.FindChild("Scroll View/aideTable").GetComponent<UITable>();
		this.haveSoilderCount = base.transform.FindChild("Adjutant/AdjutantCount").GetComponent<UILabel>();
		this.info = base.transform.FindChild("Label").GetComponent<LangeuageLabel>();
		this.aide1 = base.transform.FindChild("1").GetComponent<UIButton>();
		this.aide2 = base.transform.FindChild("2").GetComponent<UIButton>();
		this.aide3 = base.transform.FindChild("3").GetComponent<UIButton>();
		this.resInfo = base.transform.FindChild("resInfo").gameObject;
		this.itemWrite = base.transform.FindChild("101").gameObject;
		this.itemLabel = base.transform.FindChild("101/Label").GetComponent<UILabel>();
		this.itemAray[0] = base.transform.FindChild("ItemList/35").gameObject;
		this.itemAray[1] = base.transform.FindChild("ItemList/38").gameObject;
		this.itemAray[2] = base.transform.FindChild("ItemList/41").gameObject;
		this.itemAray[3] = base.transform.FindChild("ItemList/36").gameObject;
		this.itemAray[4] = base.transform.FindChild("ItemList/39").gameObject;
		this.itemAray[5] = base.transform.FindChild("ItemList/42").gameObject;
		this.itemAray[6] = base.transform.FindChild("ItemList/37").gameObject;
		this.itemAray[7] = base.transform.FindChild("ItemList/40").gameObject;
		this.itemAray[8] = base.transform.FindChild("ItemList/43").gameObject;
		this.itemAray[9] = base.transform.FindChild("ItemList/101").gameObject;
	}

	private void Start()
	{
		this.ShowAideCompound();
		UIEventListener.Get(this.aide1.gameObject).onClick = delegate(GameObject a)
		{
			this.SortAide(int.Parse(a.name));
		};
		UIEventListener.Get(this.aide2.gameObject).onClick = delegate(GameObject a)
		{
			this.SortAide(int.Parse(a.name));
		};
		UIEventListener.Get(this.aide3.gameObject).onClick = delegate(GameObject a)
		{
			this.SortAide(int.Parse(a.name));
		};
	}

	public void OnEnable()
	{
		this.SortAide(1);
	}

	private void PlayTextTip(Transform tar, string text)
	{
		if (!this.resInfo.activeSelf)
		{
			this.resInfo.SetActive(true);
		}
		this.resInfo.transform.parent = tar;
		this.resInfo.transform.localPosition = Vector3.zero;
		this.resInfo.GetComponentInChildren<UILabel>().text = text;
		if (!this.resInfo.activeSelf)
		{
			this.resInfo.SetActive(true);
		}
		TweenScale component = this.resInfo.GetComponent<TweenScale>();
		if (!component.enabled)
		{
			component.enabled = true;
		}
		component.ResetToBeginning();
	}

	public void SortAide(int i)
	{
		foreach (AideDateUI current in this.allAideUI)
		{
			if (current.curAide.type == i)
			{
				current.gameObject.SetActive(true);
				this.info.text = LanguageManage.GetTextByKey(current.curAide.description, "officer");
			}
			else
			{
				current.gameObject.SetActive(false);
			}
		}
		switch (i)
		{
		case 1:
			this.aide1.SetSprite("选中状态按钮");
			this.aide1.normalSprite = "选中状态按钮";
			this.aide1.transform.GetComponentInChildren<LangeuageLabel>().color = new Color(1f, 1f, 1f);
			this.aide2.SetSprite("未选中状态");
			this.aide2.normalSprite = "未选中状态";
			this.aide2.transform.GetComponentInChildren<LangeuageLabel>().color = new Color(0f, 0.5921569f, 0.6509804f);
			this.aide3.SetSprite("未选中状态");
			this.aide3.normalSprite = "未选中状态";
			this.aide3.transform.GetComponentInChildren<LangeuageLabel>().color = new Color(0f, 0.5921569f, 0.6509804f);
			break;
		case 2:
			this.aide1.SetSprite("未选中状态");
			this.aide1.normalSprite = "未选中状态";
			this.aide1.transform.GetComponentInChildren<LangeuageLabel>().color = new Color(0f, 0.5921569f, 0.6509804f);
			this.aide2.SetSprite("选中状态按钮");
			this.aide2.normalSprite = "选中状态按钮";
			this.aide2.transform.GetComponentInChildren<LangeuageLabel>().color = new Color(1f, 1f, 1f);
			this.aide3.SetSprite("未选中状态");
			this.aide3.normalSprite = "未选中状态";
			this.aide3.transform.GetComponentInChildren<LangeuageLabel>().color = new Color(0f, 0.5921569f, 0.6509804f);
			break;
		case 3:
			this.aide1.SetSprite("未选中状态");
			this.aide1.normalSprite = "未选中状态";
			this.aide1.transform.GetComponentInChildren<LangeuageLabel>().color = new Color(0f, 0.5921569f, 0.6509804f);
			this.aide2.SetSprite("未选中状态");
			this.aide2.normalSprite = "未选中状态";
			this.aide2.transform.GetComponentInChildren<LangeuageLabel>().color = new Color(0f, 0.5921569f, 0.6509804f);
			this.aide3.SetSprite("选中状态按钮");
			this.aide3.normalSprite = "选中状态按钮";
			this.aide3.transform.GetComponentInChildren<LangeuageLabel>().color = new Color(1f, 1f, 1f);
			break;
		}
		this.aideTable.Reposition();
	}

	public void OnItemClick()
	{
		this.PlayTextTip(this.itemWrite.transform, UnitConst.GetInstance().ItemConst[int.Parse(this.itemWrite.name)].Name);
	}

	public void ShowAideCompound()
	{
		this.aideTable.DestoryChildren(true);
		this.allAideUI.Clear();
		DicForU.GetValues<int, Aide>(UnitConst.GetInstance().AideConst, this.aideList);
		base.StartCoroutine(this.showAideC());
		this.aideTable.Reposition();
		this.SortAide(1);
	}

	[DebuggerHidden]
	private IEnumerator showAideC()
	{
		AideCompound.<showAideC>c__Iterator65 <showAideC>c__Iterator = new AideCompound.<showAideC>c__Iterator65();
		<showAideC>c__Iterator.<>f__this = this;
		return <showAideC>c__Iterator;
	}
}
