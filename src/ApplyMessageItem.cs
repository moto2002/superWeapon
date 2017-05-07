using System;
using UnityEngine;

public class ApplyMessageItem : messageItem
{
	public UILabel desLabel;

	public UISprite fowardSprite;

	public GameObject btnApply;

	public UILabel showTime;

	public DateTime endTime;

	public DateTime beginTime;

	public GameObject backSprite;

	public long buidingId;

	public long userId;

	public int state = 1;

	public long id;

	public bool isCan;

	private LegionHelpApply ThisApply;

	private bool Finish;

	public void showInfo(LegionHelpApply apply)
	{
		this.ThisApply = apply;
		this.btnApply.name = apply.id.ToString();
		if (HeroInfo.GetInstance().userId == apply.userId)
		{
			this.btnApply.SetActive(false);
		}
		this.desLabel.text = apply.userName + " " + LanguageManage.GetTextByKey("请求协助升级", "build") + LanguageManage.GetTextByKey(UnitConst.GetInstance().buildingConst[apply.buildingIndex].name, "build");
		this.endTime = apply.cdTime;
		this.beginTime = apply.time;
		this.buidingId = apply.buildingId;
		this.userId = apply.userId;
		this.id = apply.id;
		if (MessagePanel.applyState != 1)
		{
			base.gameObject.SetActive(false);
		}
	}

	public void Update()
	{
		if (this.isCan && HeroInfo.GetInstance().legionApply.Count > 0)
		{
			if (TimeTools.DateDiffToDouble(TimeTools.GetNowTimeSyncServerToDateTime(), this.endTime) > 0.0)
			{
				if (this.fowardSprite)
				{
					this.fowardSprite.fillAmount = (float)(TimeTools.DateDiffToDouble(this.beginTime, TimeTools.GetNowTimeSyncServerToDateTime()) / TimeTools.DateDiffToDouble(this.beginTime, this.endTime));
				}
				this.showTime.text = TimeTools.DateDiffToString(TimeTools.GetNowTimeSyncServerToDateTime(), this.endTime);
			}
			else if (!this.Finish)
			{
				this.Finish = true;
				HeroInfo.GetInstance().legionApply.Remove(this.ThisApply.id);
				if (MessagePanel._Inst)
				{
					MessagePanel._Inst.ShowApplyInfo();
				}
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}
}
