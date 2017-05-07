using DG.Tweening;
using msg;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ChargeActity12 : ChargeRightPanel
{
	public int NewActity;

	public UILabel tittleUIlabel;

	public UILabel timeUIlabel;

	public UILabel contentUIlabel;

	public UILabel NewTitleLabel;

	public UILabel NewContentLabel;

	public UIScrollView scrow;

	public UIGrid grid;

	public GameObject itemPrefab;

	public void SetNewActity(int type)
	{
		Debug.Log("设置type:" + type);
		this.NewActity = type;
	}

	public void Update()
	{
		if (this.NewActity != 0)
		{
			return;
		}
		TimeSpan timeSpan = ChargeActityPanel.GetRegCharges[12][0].endTimeStr - TimeTools.GetNowTimeSyncServerToDateTime();
		this.timeUIlabel.text = string.Format("{0}: {1}{2}{3}:{4}:{5}", new object[]
		{
			LanguageManage.GetTextByKey("活动倒计时", "Activities"),
			timeSpan.Days,
			LanguageManage.GetTextByKey("天", "Activities"),
			timeSpan.Hours,
			timeSpan.Minutes,
			timeSpan.Seconds
		});
	}

	public void Awake()
	{
		this.grid.isRespositonOnStart = false;
		EventManager.Instance.AddEvent(EventManager.EventType.ChargeActityPnael_Get12, new EventManager.VoidDelegate(this.ChargeActityPnael_Get12));
		EventManager.Instance.AddEvent(EventManager.EventType.ActivityPanelManager_GetAward, new EventManager.VoidDelegate(this.GetSevenAward));
	}

	private void GetSevenAward(GameObject ga)
	{
		int num = int.Parse(ga.name);
		int coin = 0;
		int oil = 0;
		int steel = 0;
		int earth = 0;
		foreach (KeyValuePair<ResType, int> current in UnitConst.GetInstance().SevenDay[num].res)
		{
			switch (current.Key)
			{
			case ResType.金币:
				coin = current.Value;
				break;
			case ResType.石油:
				oil = current.Value;
				break;
			case ResType.钢铁:
				steel = current.Value;
				break;
			case ResType.稀矿:
				earth = current.Value;
				break;
			}
		}
		if (SenceManager.inst.NoResSpace(coin, oil, steel, earth, true))
		{
			return;
		}
		HUDTextTool.isGetActivitiesAward = true;
		SevenDayHandler.CS_SevenDay(num, delegate(bool isError)
		{
			if (!isError)
			{
				ShowAwardPanelManger.showAwardList();
			}
		});
	}

	public override void OnEnable()
	{
		List<ActivityClass> list = null;
		if (this.NewActity == 0)
		{
			list = ChargeActityPanel.GetRegCharges[12];
		}
		else
		{
			list = ChargeActityPanel.GetRegCharges[this.NewActity];
		}
		this.scrow.ResetPosition();
		this.grid.ClearChild();
		for (int i = 0; i < list.Count; i++)
		{
			if (this.NewActity == 1)
			{
				int num = 0;
				foreach (KeyValuePair<int, SevenDay> current in UnitConst.GetInstance().SevenDay)
				{
					GameObject gameObject = NGUITools.AddChild(this.grid.gameObject, this.itemPrefab);
					ChargeActityItem component = gameObject.GetComponent<ChargeActityItem>();
					component.tr.localPosition = new Vector3(800f, this.grid.cellHeight * (float)(-(float)num), 0f);
					component.tr.DOLocalMoveX(0f, 0.16f, false).SetDelay(0.1f * (float)num);
					component.SetInfoBySevenDay(current.Value);
					num++;
				}
			}
			else
			{
				GameObject gameObject2 = NGUITools.AddChild(this.grid.gameObject, this.itemPrefab);
				ChargeActityItem component2 = gameObject2.GetComponent<ChargeActityItem>();
				component2.tr.localPosition = new Vector3(800f, this.grid.cellHeight * (float)(-(float)i), 0f);
				component2.tr.DOLocalMoveX(0f, 0.16f, false).SetDelay(0.1f * (float)i);
				component2.SetInfo(list[i]);
				if (this.NewActity != 0)
				{
					if (list[i].rewardCount > 0)
					{
						component2.GetComponent<ChargeActity12Item>().NumLabel.text = LanguageManage.GetTextByKey("奖励数量：", "Activities") + list[i].AwardCount.ToString();
					}
					else
					{
						component2.GetComponent<ChargeActity12Item>().NumLabel.text = string.Empty;
					}
				}
			}
		}
	}

	private void ChargeActityPnael_Get12(GameObject ga)
	{
		ActivityClass curActity = ga.GetComponentInParent<ChargeActityItem>().curActity;
		if (ChargeActityPanel.ins.isCanRecieveActityRes(curActity))
		{
			CSgetActivityPrize cSgetActivityPrize = new CSgetActivityPrize();
			cSgetActivityPrize.activityId = curActity.activityId;
			ClientMgr.GetNet().SendHttp(2112, cSgetActivityPrize, delegate(bool isError, Opcode opcode)
			{
				ShowAwardPanelManger.showAwardList();
			}, null);
		}
	}
}
