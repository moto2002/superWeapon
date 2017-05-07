using DG.Tweening;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class NoticeMarqueeItem : MonoBehaviour
{
	public UILabel text_Conent;

	public long EndTime;

	public long interTime;

	public long ID;

	public int ID_Display;

	private void Update()
	{
	}

	public void SetInfo()
	{
		base.transform.localPosition = new Vector3((float)Screen.width / 2f + 200f, NoticeMarquee.Inst.marqeeFirstY - (float)NoticeMarquee.Inst.marqeeNum * NoticeMarquee.Inst.marqeeHeight);
		base.InvokeRepeating("Display", 1f, (float)this.interTime * 60f);
	}

	private void Display()
	{
		base.transform.localPosition = new Vector3((float)Screen.width / 2f + 200f, NoticeMarquee.Inst.marqeeFirstY - (float)NoticeMarquee.Inst.marqeeNum * NoticeMarquee.Inst.marqeeHeight);
		if (TimeTools.ConvertLongDateTime(this.EndTime) > TimeTools.GetNowTimeSyncServerToDateTime())
		{
			base.StartCoroutine(this.DoNoticeMarquee());
		}
		else
		{
			if (NoticeMarquee.Inst.marqeeNum > 0)
			{
				NoticeMarquee.Inst.marqeeNum--;
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	[DebuggerHidden]
	private IEnumerator DoNoticeMarquee()
	{
		NoticeMarqueeItem.<DoNoticeMarquee>c__Iterator72 <DoNoticeMarquee>c__Iterator = new NoticeMarqueeItem.<DoNoticeMarquee>c__Iterator72();
		<DoNoticeMarquee>c__Iterator.<>f__this = this;
		return <DoNoticeMarquee>c__Iterator;
	}

	private void TingLiu()
	{
		base.transform.DOLocalMoveX(-1f * ((float)Screen.width / 2f + (float)this.text_Conent.width + 300f), (float)this.text_Conent.width / 50f, false).SetDelay(2f).OnComplete(new TweenCallback(this.Hide)).SetUpdate(true);
	}

	private void Hide()
	{
		if (NoticeMarquee.Inst.marqeeNum > 0)
		{
			NoticeMarquee.Inst.marqeeNum--;
		}
	}
}
