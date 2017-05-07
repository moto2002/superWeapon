using System;

public class ShowTimeTip : IMonoBehaviour
{
	public UISprite speendProcess;

	public T_Tower tar;

	public T_Res res;

	public UILabel timetext;

	public DateTime beginTime;

	public DateTime endTime;

	public Action CallBack;

	public int cdType;

	public int posIndex;

	public long id;

	public int itemid;

	public int btnType;

	public UISprite updatingEnum;

	private bool isEnd;

	public void OnEnable()
	{
		this.beginTime = TimeTools.GetNowTimeSyncServerToDateTime();
	}

	public void SetUpdatingEum(int updateEnum)
	{
		switch (updateEnum)
		{
		case 1:
			this.updatingEnum.spriteName = "建筑升级";
			break;
		case 2:
			this.updatingEnum.spriteName = "兵种升级";
			break;
		case 3:
			this.updatingEnum.spriteName = "科技升级";
			break;
		default:
			this.updatingEnum.spriteName = string.Empty;
			break;
		}
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (TimeTools.DateDiffToDouble(TimeTools.GetNowTimeSyncServerToDateTime(), this.endTime) > 0.0)
		{
			if (this.speendProcess)
			{
				this.speendProcess.fillAmount = (float)(TimeTools.DateDiffToDouble(this.beginTime, TimeTools.GetNowTimeSyncServerToDateTime()) / TimeTools.DateDiffToDouble(this.beginTime, this.endTime));
			}
			this.timetext.text = TimeTools.DateDiffToString(TimeTools.GetNowTimeSyncServerToDateTime(), this.endTime);
		}
		else
		{
			this.TimeEnd();
		}
	}

	public void TimeEnd()
	{
		if (this.isEnd)
		{
			return;
		}
		this.isEnd = true;
		if (this.CallBack != null)
		{
			this.CallBack();
			this.CallBack = null;
		}
		if (this.speendProcess)
		{
			this.speendProcess.fillAmount = 0f;
		}
		this.timetext.text = string.Empty;
	}
}
