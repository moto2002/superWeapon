using System;
using UnityEngine;

public class ShowBuyKey : MonoBehaviour
{
	public static ShowBuyKey ins;

	public UILabel Des;

	public UILabel countLabel;

	private Action Callback1;

	private Action Callback2;

	public void OnDestroy()
	{
		ShowBuyKey.ins = null;
	}

	public void Awake()
	{
		ShowBuyKey.ins = this;
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
		}
		base.gameObject.SetActive(false);
	}

	private void Btn2Click(GameObject ga)
	{
		if (this.Callback2 != null)
		{
			this.Callback2();
		}
		base.gameObject.SetActive(false);
	}

	public void BuyKeyInfo(string des, int count, Action callBackone, Action callbackTwo)
	{
		this.Callback1 = callBackone;
		this.Callback2 = callbackTwo;
		this.Des.text = des;
		this.countLabel.text = count.ToString();
	}
}
