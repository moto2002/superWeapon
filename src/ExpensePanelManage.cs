using System;
using System.Collections.Generic;
using UnityEngine;

public class ExpensePanelManage : FuncUIPanel
{
	public static ExpensePanelManage inst;

	public static List<string> strings = new List<string>();

	public Transform expenseBtn;

	public UILabel title;

	public GameObject content;

	public UITable contentTable;

	public UILabel btnLabel;

	public UISprite zuanShi;

	[HideInInspector]
	public static bool isCanNotBuyCoin;

	[HideInInspector]
	public static bool isCanNotBuyOil;

	[HideInInspector]
	public static bool isCanNotBuySteel;

	[HideInInspector]
	public static bool isCanNotBuyRareEarth;

	private static Action<bool, int> func;

	private static int rmbNeedNum;

	private DateTime endTime;

	private bool isUpdateByTime;

	private bool addByTime;

	private bool subByTime;

	public void OnDestroy()
	{
		ExpensePanelManage.inst = null;
	}

	public void Start()
	{
		HUDTextTool.inst.NextLuaCall("消费面板 调用Lua", new object[0]);
	}

	public override void Awake()
	{
		ExpensePanelManage.inst = this;
		this.expenseBtn = base.transform.FindChild("Camera/ExpensePanel/Container/Sprite");
		EventManager.Instance.AddEvent(EventManager.EventType.Expense_GetBtn, new EventManager.VoidDelegate(this.OnExpenseGet));
		EventManager.Instance.AddEvent(EventManager.EventType.Expense_ClosePanel, new EventManager.VoidDelegate(this.CloseThisPanel));
	}

	public void OnExpenseGet(GameObject o)
	{
		if (ExpensePanelManage.func != null)
		{
			ExpensePanelManage.func(true, ExpensePanelManage.rmbNeedNum);
		}
		ExpensePanelManage.ClearCache();
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public void CloseThisPanel(GameObject ga)
	{
		if (ExpensePanelManage.func != null)
		{
			ExpensePanelManage.func(false, 0);
		}
		ExpensePanelManage.ClearCache();
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public void Show(int expenseNum, DateTime timeEnd, Action<bool, int> _func)
	{
		ExpensePanelManage.func = _func;
		foreach (string current in ExpensePanelManage.strings)
		{
			this.contentTable.CreateChildren(current, true).GetComponent<UILabel>().text = current;
		}
		this.contentTable.Reposition();
		ExpensePanelManage.rmbNeedNum = expenseNum;
		this.endTime = timeEnd;
		this.zuanShi.gameObject.SetActive(true);
		this.btnLabel.gameObject.transform.localPosition = new Vector3(-5.66f, 0f, 0f);
		int rmbNum = ResourceMgr.GetRmbNum(TimeTools.DateDiffToDouble(TimeTools.GetNowTimeSyncServerToDateTime(), timeEnd));
		if (timeEnd > TimeTools.GetNowTimeSyncServerToDateTime())
		{
			this.isUpdateByTime = true;
			this.addByTime = true;
		}
		this.btnLabel.text = (expenseNum + rmbNum).ToString();
		this.content.SetActive(true);
		if (UIManager.inst != null)
		{
			UIManager.inst.UIInUsed(true);
		}
	}

	public void Show_NewByServer(int expenseNum, DateTime timeEnd, Action<bool, int> _func)
	{
		ExpensePanelManage.func = _func;
		foreach (string current in ExpensePanelManage.strings)
		{
			this.contentTable.CreateChildren(current, true).GetComponent<UILabel>().text = current;
		}
		this.contentTable.Reposition();
		ExpensePanelManage.rmbNeedNum = expenseNum;
		this.endTime = timeEnd;
		if (timeEnd > TimeTools.GetNowTimeSyncServerToDateTime())
		{
			this.isUpdateByTime = true;
		}
		if (expenseNum <= 0 && timeEnd > TimeTools.GetNowTimeSyncServerToDateTime())
		{
			this.zuanShi.gameObject.SetActive(false);
			this.btnLabel.gameObject.transform.localPosition = new Vector3(-26.49f, 0f, 0f);
			this.btnLabel.text = LanguageManage.GetTextByKey("免费", "ResIsland");
		}
		else
		{
			this.zuanShi.gameObject.SetActive(true);
			this.btnLabel.gameObject.transform.localPosition = new Vector3(-5.66f, 0f, 0f);
			this.btnLabel.text = expenseNum.ToString();
		}
		this.content.SetActive(true);
		if (UIManager.inst != null)
		{
			UIManager.inst.UIInUsed(true);
		}
	}

	public void ShowRes(string text, Action<bool, int> _func)
	{
		ExpensePanelManage.func = _func;
		this.title.text = text;
		foreach (string current in ExpensePanelManage.strings)
		{
			this.contentTable.CreateChildren(current, true).GetComponent<UILabel>().text = current;
		}
		this.contentTable.Reposition();
		this.zuanShi.gameObject.SetActive(false);
		this.btnLabel.gameObject.transform.localPosition = new Vector3(-26.49f, 0f, 0f);
		this.btnLabel.text = LanguageManage.GetTextByKey("确定", "others");
		this.content.SetActive(true);
		if (UIManager.inst != null)
		{
			UIManager.inst.UIInUsed(true);
		}
	}

	public void Show_NewByServerRes(int expenseNum, DateTime timeEnd, Action<bool, int> _func)
	{
		ExpensePanelManage.func = _func;
		foreach (string current in ExpensePanelManage.strings)
		{
			this.contentTable.CreateChildren(current, true).GetComponent<UILabel>().text = current;
		}
		this.contentTable.Reposition();
		ExpensePanelManage.rmbNeedNum = expenseNum;
		this.endTime = timeEnd;
		if (timeEnd > TimeTools.GetNowTimeSyncServerToDateTime())
		{
			this.isUpdateByTime = true;
		}
		if (expenseNum <= 0)
		{
			this.zuanShi.gameObject.SetActive(false);
			this.btnLabel.gameObject.transform.localPosition = new Vector3(-26.49f, 0f, 0f);
			this.btnLabel.text = LanguageManage.GetTextByKey("确定", "others");
		}
		else
		{
			this.zuanShi.gameObject.SetActive(true);
			this.btnLabel.gameObject.transform.localPosition = new Vector3(-5.66f, 0f, 0f);
			this.btnLabel.text = expenseNum.ToString();
		}
		this.content.SetActive(true);
		if (UIManager.inst != null)
		{
			UIManager.inst.UIInUsed(true);
		}
	}

	private void Update()
	{
		if (this.isUpdateByTime)
		{
			int num = 0;
			if (this.endTime < TimeTools.GetNowTimeSyncServerToDateTime())
			{
				if (ExpensePanelManage.strings.Count == 1)
				{
					ExpensePanelManage.ClearCache();
					UnityEngine.Object.Destroy(base.gameObject);
				}
			}
			else
			{
				num = ResourceMgr.GetRmbNum(TimeTools.DateDiffToDouble(TimeTools.GetNowTimeSyncServerToDateTime(), this.endTime));
			}
			int num2 = ExpensePanelManage.rmbNeedNum;
			if (this.addByTime)
			{
				num2 = ExpensePanelManage.rmbNeedNum + num;
			}
			if (this.subByTime)
			{
				num2 = ExpensePanelManage.rmbNeedNum - num;
			}
			if (num2 > 0)
			{
				this.btnLabel.text = num2.ToString();
			}
			else
			{
				this.btnLabel.text = LanguageManage.GetTextByKey("免费", "ResIsland");
			}
		}
	}

	public static void ClearCache()
	{
		ExpensePanelManage.isCanNotBuyCoin = false;
		ExpensePanelManage.isCanNotBuyOil = false;
		ExpensePanelManage.isCanNotBuySteel = false;
		ExpensePanelManage.isCanNotBuyRareEarth = false;
		ExpensePanelManage.strings.Clear();
	}

	public void EventBtn(ExpenseBtnType btnType)
	{
		if (btnType == ExpenseBtnType.GoBack)
		{
			if (ExpensePanelManage.func != null)
			{
				ExpensePanelManage.func(false, ExpensePanelManage.rmbNeedNum);
			}
			ExpensePanelManage.ClearCache();
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}
}
