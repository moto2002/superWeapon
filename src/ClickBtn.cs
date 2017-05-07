using System;
using UnityEngine;

public class ClickBtn : MonoBehaviour
{
	public static ClickBtn inst;

	public Action dele;

	public GameObject deleGa;

	private float curTime = -3.40282347E+38f;

	private bool isPressing;

	private float pressTime;

	public void Update()
	{
		if (this.isPressing && Time.time > this.pressTime + 0.5f)
		{
			this.pressTime = Time.time;
			if (this.deleGa)
			{
				this.deleGa.SendMessage("LongPress");
			}
		}
	}

	private void Awake()
	{
		ClickBtn.inst = this;
	}

	private void OnClick()
	{
		if (NewbieGuidePanel._instance && NewbieGuidePanel._instance.isShowShield)
		{
			NewbieGuidePanel._instance.HideShiele();
		}
		if (this.dele != null && this.curTime < Time.time)
		{
			this.curTime = Time.time + 0.6f;
			if (UIManager.inst)
			{
				UIManager.inst.UIInUsed(false);
			}
			NewbieGuidePanel._instance.HideSelf();
			this.dele();
			if (this.dele.Method != null)
			{
				LogManage.LogError(string.Format("引导方法名字是{0}", this.dele.Method.Name));
			}
			this.dele = null;
		}
		if (NewbieGuidePanel._instance.isZhanbao && NewbieGuidePanel._instance.isEnable)
		{
			FuncUIManager.inst.OpenFuncUI("SettlementPanel", SenceType.Island);
			NewbieGuidePanel._instance.isEnable = false;
		}
	}

	private void OnPress(bool isPress)
	{
		this.isPressing = isPress;
		if (this.isPressing)
		{
			this.pressTime = Time.time;
		}
	}
}
