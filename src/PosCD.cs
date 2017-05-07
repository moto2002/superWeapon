using System;
using UnityEngine;

public class PosCD : MonoBehaviour
{
	public static PosCD inst;

	private UILabel uil;

	private float time;

	public void OnDestroy()
	{
		PosCD.inst = null;
	}

	private void Awake()
	{
		PosCD.inst = this;
		this.uil = base.GetComponent<UILabel>();
		TweenScale.Begin(base.gameObject, 0.2f, Vector3.one * 2.6f).style = UITweener.Style.PingPong;
		base.gameObject.SetActive(false);
	}

	private void Update()
	{
		if (this.time > 0f)
		{
			this.time -= Time.deltaTime;
			if (this.time > 0f)
			{
				this.uil.text = (Mathf.RoundToInt(this.time) + 1).ToString();
			}
			else
			{
				this.time = 0f;
				this.uil.text = string.Empty;
			}
		}
	}

	public void NewTime()
	{
		this.time = HeroInfo.GetInstance().posCD;
	}
}
