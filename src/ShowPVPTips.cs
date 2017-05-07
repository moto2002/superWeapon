using System;
using UnityEngine;

public class ShowPVPTips : MonoBehaviour
{
	public UILabel Des;

	public UILabel countLabel;

	private Action CallbackOne;

	private Action CallbackTwo;

	public static ShowPVPTips ins;

	public void Awake()
	{
		ShowPVPTips.ins = this;
	}

	public void OnEnable()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.MessageBoxClose_btn1Click, new EventManager.VoidDelegate(this.Btn1Click));
		EventManager.Instance.AddEvent(EventManager.EventType.MessageBoxClose_btn2Click, new EventManager.VoidDelegate(this.Btn2Click));
	}

	private void Btn1Click(GameObject ga)
	{
		try
		{
			if (this.CallbackOne != null)
			{
				this.CallbackOne();
			}
		}
		catch (Exception var_0_1B)
		{
		}
		base.gameObject.SetActive(false);
	}

	private void Btn2Click(GameObject ga)
	{
		try
		{
			if (this.CallbackTwo != null)
			{
				this.CallbackTwo();
			}
		}
		catch (Exception var_0_1B)
		{
		}
		base.gameObject.SetActive(false);
	}

	public void showPVPTips(string des, Action callBack_one, Action callBack_two)
	{
		this.CallbackOne = callBack_one;
		this.CallbackTwo = callBack_two;
		this.Des.text = des;
	}
}
