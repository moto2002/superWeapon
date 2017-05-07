using System;
using UnityEngine;

public class ActivityPrefab : MonoBehaviour
{
	public GameObject getAwardBtn;

	public GameObject haveGet;

	public GameObject watch;

	public GameObject award;

	public GameObject awardCount;

	public UILabel des;

	public UIGrid showAwardTabel;

	public int state;

	public LangeuageLabel btnDes;

	public int activityType;

	public UILabel OnLineLabel;

	public GameObject NoAward;

	public GameObject getLabelWord;

	public int ConstId;

	private void Start()
	{
	}

	private void Update()
	{
		TimeSpan timeSpan = OnLineAward.laod.time - TimeTools.GetNowTimeSyncServerToDateTime();
		if (timeSpan.TotalSeconds > 1.0 && OnLineAward.laod.step != 0)
		{
			if (timeSpan.Hours > 0)
			{
				this.OnLineLabel.text = string.Format("{0}:{1}:{2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
			}
			else if (timeSpan.Minutes > 0)
			{
				this.OnLineLabel.text = string.Format("{0}:{1}", timeSpan.Minutes, timeSpan.Seconds);
			}
			else if (timeSpan.Seconds > 1)
			{
				this.OnLineLabel.text = string.Format("{0}", timeSpan.Seconds);
			}
			else
			{
				ActivityPanelManager.ins.isRefreshOnLine = true;
			}
		}
	}
}
