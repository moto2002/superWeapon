using DG.Tweening;
using msg;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ChargeActity13 : ChargeRightPanel
{
	public UILabel tittleUIlabel;

	public UILabel timeUIlabel;

	public UILabel contentUIlabel;

	public UIScrollView scrow;

	public UIGrid grid;

	public GameObject itemPrefab;

	public void LateUpdate()
	{
		TimeSpan timeSpan = ChargeActityPanel.GetRegCharges[13][0].endTimeStr - TimeTools.GetNowTimeSyncServerToDateTime();
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
		EventManager.Instance.AddEvent(EventManager.EventType.ChargeActityPnael_Get13, new EventManager.VoidDelegate(this.ChargeActityPnael_Get13));
	}

	public override void OnEnable()
	{
		List<ActivityClass> list = ChargeActityPanel.GetRegCharges[13];
		this.scrow.ResetPosition();
		this.grid.ClearChild();
		for (int i = 0; i < list.Count; i++)
		{
			GameObject gameObject = NGUITools.AddChild(this.grid.gameObject, this.itemPrefab);
			ChargeActityItem component = gameObject.GetComponent<ChargeActityItem>();
			component.tr.localPosition = new Vector3(800f, this.grid.cellHeight * (float)(-(float)i), 0f);
			component.tr.DOLocalMoveX(0f, 0.16f, false).SetDelay(0.1f * (float)i);
			component.SetInfo(list[i]);
		}
	}

	private void ChargeActityPnael_Get13(GameObject ga)
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
