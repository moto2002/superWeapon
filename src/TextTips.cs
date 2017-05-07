using DG.Tweening;
using System;
using UnityEngine;

public class TextTips : MonoBehaviour
{
	public UILabel Des;

	public GameObject Back;

	private Tween TwennPos;

	public void Start()
	{
		UIEventListener.Get(this.Back).onClick = delegate(GameObject a)
		{
			this.OnUp();
		};
	}

	public void OnDown(GameObject tar, string _text)
	{
		if (this.Des)
		{
			base.gameObject.SetActive(true);
			this.Des.text = _text;
			this.TwennPos.Kill(false);
			float y = (float)this.Des.height * 0.5f;
			float num = 160f;
			this.Des.transform.localPosition = new Vector3(-num, y, 0f);
			this.TwennPos = this.Des.transform.DOLocalMoveX(num, 0.2f, false);
		}
	}

	public void OnUp()
	{
		this.TwennPos.Kill(false);
		if (this.Des)
		{
			float y = (float)this.Des.height * 0.5f;
			float num = (float)(this.Des.width + 60);
			this.TwennPos = this.Des.transform.DOLocalMove(new Vector3(-num, y, 0f), 0.2f, false);
			this.TwennPos.OnComplete(delegate
			{
				this.Des.text = string.Empty;
				base.gameObject.SetActive(false);
			});
		}
	}
}
