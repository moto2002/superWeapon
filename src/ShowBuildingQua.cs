using System;
using UnityEngine;

public class ShowBuildingQua : MonoBehaviour
{
	public UILabel Des;

	public UILabel countLabel;

	private Action Callback1;

	private Action Callback2;

	public static ShowBuildingQua ins;

	public void Awake()
	{
		ShowBuildingQua.ins = this;
	}

	public void OnEnable()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.MessageBoxClose_btn1Click, new EventManager.VoidDelegate(this.Btn1Click));
		EventManager.Instance.AddEvent(EventManager.EventType.MessageBoxClose_btn2Click, new EventManager.VoidDelegate(this.Btn2Click));
	}

	private void Btn1Click(GameObject ga)
	{
		if (this.Callback1 != null)
		{
			this.Callback1();
			this.Callback1 = null;
		}
		base.gameObject.SetActive(false);
	}

	private void Btn2Click(GameObject ga)
	{
		if (this.Callback2 != null)
		{
			this.Callback2();
			this.Callback2 = null;
		}
		base.gameObject.SetActive(false);
	}

	public void BuyBuildingInfo(string des, int count, Action callBackone, Action callbackTwo)
	{
		this.Callback1 = callBackone;
		this.Callback2 = callbackTwo;
		this.Des.text = des;
		this.countLabel.text = count.ToString();
	}
}
