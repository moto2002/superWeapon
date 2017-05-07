using System;
using UnityEngine;

public class EmailItem : MonoBehaviour
{
	public GameObject obj;

	public UISprite emailFlag;

	public UILabel lblTime;

	public UILabel title;

	public UILabel content;

	public GameObject btnReward;

	public Transform bottomTrains;

	public UITexture emailBG;

	public UILabel showState;

	public GameObject bottom;

	public static EmailItem _instance;

	public UILabel helpInfo;

	public Transform expend;

	public UITable resTablePrf;

	public UITable itemTablePrf;

	public GameObject xulz;

	[HideInInspector]
	public bool IsExpend;

	public long emailID;

	public void OnDestroy()
	{
		EmailItem._instance = null;
	}

	private void Awake()
	{
		EmailItem._instance = this;
		this.ShowEmail();
	}

	public void ShowEmail()
	{
		this.obj = base.gameObject;
		this.emailFlag = base.transform.FindChild("left/icon").GetComponent<UISprite>();
		this.lblTime = base.transform.FindChild("left/time").GetComponent<UILabel>();
		this.title = base.transform.FindChild("right/title").GetComponent<UILabel>();
		this.content = base.transform.FindChild("bottom/Scroll View/context").GetComponent<UILabel>();
		this.showState = base.transform.FindChild("bottom/record/Background/Label").GetComponent<UILabel>();
		this.btnReward = base.transform.FindChild("bottom/record/Background").gameObject;
		this.btnReward.AddComponent<EmailBtn>();
		EmailBtn component = this.btnReward.GetComponent<EmailBtn>();
		component.type = EmailBtnType.getReward;
		this.btnReward.AddComponent<ButtonClick>();
		ButtonClick component2 = this.btnReward.GetComponent<ButtonClick>();
		component2.eventType = EventManager.EventType.none;
		this.bottomTrains = base.transform.FindChild("bottom");
		this.emailBG = base.transform.FindChild("bg").GetComponent<UITexture>();
		this.bottom = base.transform.FindChild("bottom").gameObject;
		this.helpInfo = base.transform.FindChild("helpInfo").GetComponent<UILabel>();
		this.expend = base.transform.FindChild("expend");
		this.expend.gameObject.AddComponent<EmailBtn>();
		EmailBtn component3 = this.expend.GetComponent<EmailBtn>();
		component3.type = EmailBtnType.expend;
		this.expend.gameObject.AddComponent<ButtonClick>();
		ButtonClick component4 = this.expend.GetComponent<ButtonClick>();
		component4.eventType = EventManager.EventType.none;
		this.resTablePrf = base.transform.FindChild("bottom/ResTable").GetComponent<UITable>();
		this.itemTablePrf = base.transform.FindChild("bottom/ItemTable").GetComponent<UITable>();
		this.xulz = base.transform.FindChild("bottom/record/Background/Sprite").gameObject;
	}
}
