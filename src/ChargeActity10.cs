using msg;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ChargeActity10 : ChargeRightPanel
{
	public UILabel thatDayLabel;

	public UILabel allDayLabel;

	public UIScrollView thatDayScrollView;

	public UIScrollView allDayScroView;

	public UIGrid thatDayGrid;

	public UIGrid allDayGrid;

	public ButtonClick btnClick;

	public UILabel btnUIlabel;

	public UISprite btnUisprite;

	public UIGrid xiangziTable;

	public GameObject xiangziPrefab;

	private List<Transform> Xiangzis;

	private ActivityClass cueActivety;

	private ActivityClass maxDayActivety;

	private int CurActityIndex;

	private int curDisplayIndex = -1;

	private GameObject curXiangzi;

	private void Awake()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.ChargeActityPnael_Get10, new EventManager.VoidDelegate(this.ChargeActityPnael_Get10));
		EventManager.Instance.AddEvent(EventManager.EventType.ChargeActityPnael_Get10All, new EventManager.VoidDelegate(this.ChargeActityPnael_Get10All));
		this.Xiangzis = new List<Transform>();
	}

	public override void OnEnable()
	{
		this.allDayGrid.ClearChild();
		this.xiangziTable.ClearChild();
		this.Xiangzis.Clear();
		for (int i = 0; i < ChargeActityPanel.GetRegCharges[10].Count; i++)
		{
			GameObject gameObject = NGUITools.AddChild(this.xiangziTable.gameObject, this.xiangziPrefab);
			if (ChargeActityPanel.GetRegCharges[10][i].isReceived)
			{
				gameObject.GetComponent<UISprite>().spriteName = "精英4开";
			}
			else if (ChargeActityPanel.GetRegCharges[10][i].isCanGetAward)
			{
				gameObject.GetComponent<UISprite>().spriteName = "精英4关";
				DieBall dieBall = PoolManage.Ins.CreatEffect("siji", Vector3.zero, Quaternion.identity, gameObject.transform);
				dieBall.tr.localPosition = Vector3.zero;
				GameTools.GetCompentIfNoAddOne<RenderQueueEdit>(dieBall.ga);
				Transform[] componentsInChildren = dieBall.GetComponentsInChildren<Transform>();
				for (int j = 0; j < componentsInChildren.Length; j++)
				{
					Transform transform = componentsInChildren[j];
					transform.gameObject.layer = 5;
				}
			}
			else
			{
				gameObject.GetComponent<UISprite>().spriteName = "精英4关";
			}
			if (HeroInfo.GetInstance().Activety_DayOfDay_HavedID == ChargeActityPanel.GetRegCharges[10][i].activityId)
			{
				if ((TimeTools.GetNowTimeSyncServerToDateTime() - HeroInfo.GetInstance().Activety_DayOfDay_HavedDatetime).Days == 0)
				{
					this.CurActityIndex = i;
				}
				else
				{
					this.CurActityIndex = i + 1;
					if (ChargeActityPanel.GetRegCharges[10].Count == this.CurActityIndex)
					{
						this.CurActityIndex = 0;
					}
				}
			}
			gameObject.name = i.ToString();
			this.Xiangzis.Add(gameObject.transform);
			UIEventListener.Get(gameObject).onClick = delegate(GameObject g)
			{
				int num = int.Parse(g.name);
				if (ChargeActityPanel.GetRegCharges[10][num].isCanGetAward && num != this.CurActityIndex)
				{
					ActivityClass activityClass = ChargeActityPanel.GetRegCharges[10][num];
					if (ChargeActityPanel.ins.isCanRecieveActityRes(activityClass))
					{
						CSgetActivityPrize cSgetActivityPrize = new CSgetActivityPrize();
						cSgetActivityPrize.activityId = activityClass.activityId;
						ClientMgr.GetNet().SendHttp(2112, cSgetActivityPrize, delegate(bool isError, Opcode opcode)
						{
							ShowAwardPanelManger.showAwardList();
						}, null);
					}
				}
			};
		}
		this.xiangziTable.StartCoroutine(this.xiangziTable.RepositionAfterFrame());
		this.maxDayActivety = ChargeActityPanel.GetRegCharges[10][ChargeActityPanel.GetRegCharges[10].Count - 1];
		ChargeActityPanel.ins.CreateRes(this.allDayGrid.gameObject, this.maxDayActivety.totalActivityResReward);
		ChargeActityPanel.ins.CreateItem(this.allDayGrid.gameObject, this.maxDayActivety.totalActivityItemReward);
		ChargeActityPanel.ins.CreateSkill(this.allDayGrid.gameObject, this.maxDayActivety.totalActivitySkillReward);
		base.StartCoroutine(this.allDayGrid.RepositionAfterFrame());
		this.allDayLabel.text = LanguageManage.GetTextByKey(this.maxDayActivety.totalDiscription, "Activities");
		this.DisplayCurActity(this.CurActityIndex);
		this.thatDayScrollView.ResetPosition();
		this.allDayScroView.ResetPosition();
	}

	private void DisplayCurActity(int index)
	{
		if (ChargeActityPanel.GetRegCharges[10].Count <= index)
		{
			return;
		}
		if (this.curXiangzi)
		{
			this.curXiangzi.transform.FindChild("bac").gameObject.SetActive(false);
		}
		this.curDisplayIndex = index;
		this.thatDayGrid.ClearChild();
		this.cueActivety = ChargeActityPanel.GetRegCharges[10][index];
		ChargeActityPanel.ins.CreateRes(this.thatDayGrid.gameObject, this.cueActivety.curActivityResReward);
		ChargeActityPanel.ins.CreateItem(this.thatDayGrid.gameObject, this.cueActivety.curActivityItemReward);
		ChargeActityPanel.ins.CreateSkill(this.thatDayGrid.gameObject, this.cueActivety.curActivitySkillReward);
		base.StartCoroutine(this.thatDayGrid.RepositionAfterFrame());
		this.thatDayLabel.text = LanguageManage.GetTextByKey(this.cueActivety.conditionName, "Activities");
		ChargeActityPanel.ins.SetBtnState(this.cueActivety, this.btnClick, this.btnUisprite, this.btnUIlabel, true, EventManager.EventType.ChargeActityPnael_Get10, "充值", EventManager.EventType.ChargeActityPnael_Charge);
		this.Xiangzis[index].FindChild("bac").gameObject.SetActive(true);
	}

	private void ChargeActityPnael_Get10(GameObject ga)
	{
		if (ChargeActityPanel.ins.isCanRecieveActityRes(this.cueActivety))
		{
			CSgetActivityPrize cSgetActivityPrize = new CSgetActivityPrize();
			cSgetActivityPrize.activityId = this.cueActivety.activityId;
			ClientMgr.GetNet().SendHttp(2112, cSgetActivityPrize, delegate(bool isError, Opcode opcode)
			{
				ShowAwardPanelManger.showAwardList();
			}, null);
		}
	}

	private void ChargeActityPnael_Get10All(GameObject ga)
	{
	}
}
