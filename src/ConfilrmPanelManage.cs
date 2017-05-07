using System;
using UnityEngine;

public class ConfilrmPanelManage : MonoBehaviour
{
	public int id;

	public GameObject sureBtn;

	public GameObject cancleBtn;

	private void Awake()
	{
		this.sureBtn = base.transform.FindChild("ConfilrmBtn").gameObject;
		this.sureBtn.AddComponent<ButtonClick>();
		ButtonClick component = this.sureBtn.GetComponent<ButtonClick>();
		component.eventType = EventManager.EventType.ConfirmalPanel_Suer;
		this.cancleBtn = base.transform.FindChild("CancelBtn").gameObject;
		this.cancleBtn.AddComponent<ButtonClick>();
		ButtonClick component2 = this.cancleBtn.GetComponent<ButtonClick>();
		component2.eventType = EventManager.EventType.ConfirmalPanel_Canel;
		EventManager.Instance.AddEvent(EventManager.EventType.ConfirmalPanel_Canel, new EventManager.VoidDelegate(this.CancelBtnClick));
		EventManager.Instance.AddEvent(EventManager.EventType.ConfirmalPanel_Suer, new EventManager.VoidDelegate(this.ConfilrmBtnClick));
	}

	public void ConfilrmBtnClick(GameObject ga)
	{
		ArmsDealerHandler.CG_CSBuyItemFromArmsDealer(this.id);
		HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("购买成功！", "others"), HUDTextTool.TextUITypeEnum.Num5);
		base.gameObject.SetActive(false);
	}

	public void CancelBtnClick(GameObject ga)
	{
		base.gameObject.SetActive(false);
	}
}
