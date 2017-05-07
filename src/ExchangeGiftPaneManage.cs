using msg;
using System;
using UnityEngine;

public class ExchangeGiftPaneManage : FuncUIPanel
{
	public GameObject SendGiftKey;

	public UIInput giftInput;

	public GameObject back;

	public override void Awake()
	{
	}

	private void Start()
	{
		UIEventListener.Get(this.SendGiftKey).onClick = delegate(GameObject ga)
		{
			if (string.IsNullOrEmpty(this.giftInput.value))
			{
				HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("激活码不能为空", "others"), HUDTextTool.TextUITypeEnum.Num5);
				this.giftInput.value = this.giftInput.defaultText;
				return;
			}
			CSExchangeGift cSExchangeGift = new CSExchangeGift();
			cSExchangeGift.giftCode = this.giftInput.value;
			ClientMgr.GetNet().SendHttp(9014, cSExchangeGift, new DataHandler.OpcodeHandler(this.GetGiftCallBack), null);
		};
		UIEventListener.Get(this.back).onClick = delegate(GameObject ga)
		{
			FuncUIManager.inst.DestoryFuncUI("ExchangeGiftPanel");
		};
	}

	public new void OnEnable()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.ToptenPanel_CloseTop, new EventManager.VoidDelegate(this.ClosetTHIS));
	}

	public void ClosetTHIS(GameObject ga)
	{
		FuncUIManager.inst.DestoryFuncUI("ExchangeGiftPanel");
	}

	private void GetGiftCallBack(bool isError, Opcode code)
	{
		this.giftInput.value = this.giftInput.defaultText;
		if (!isError)
		{
			ShowAwardPanelManger.showAwardList();
		}
	}
}
