using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class MessagePanel : MonoBehaviour
{
	public static MessagePanel _Inst;

	private bool isFirstMessage = true;

	public static bool isHavaNotice = true;

	public static bool isOut = true;

	private Vector3 messageExtendOffPos;

	public GameObject messageBtn;

	private Vector3 messageExtendOnPos;

	public UITable messageTable;

	public UIInput messageInput;

	public GameObject systemMessageBtn;

	public GameObject familyMessageBtn;

	public GameObject wordMessageBtn;

	public static int messageState;

	public static int applyState;

	public GameObject applyItem;

	public GameObject messItem;

	public List<GameObject> applyList = new List<GameObject>();

	public void OnEnable()
	{
		T_Tower.ClickTowerSendMessage += new Action(this.ClearScree);
		DragMgr.ClickTerrSendMessage += new Action(this.ClearScree);
	}

	private void ClearScree()
	{
		if (!MessagePanel.isOut)
		{
			this.messageTween(MainUIPanelManage._instance.chatPanel.gameObject);
			MessagePanel.isOut = true;
		}
	}

	public void OnDisable()
	{
		T_Tower.ClickTowerSendMessage -= new Action(this.ClearScree);
		DragMgr.ClickTerrSendMessage -= new Action(this.ClearScree);
	}

	public void OnDestroy()
	{
		MessagePanel._Inst = null;
	}

	private void Awake()
	{
		MessagePanel._Inst = this;
		MessagePanel.messageState = 0;
		this.OngetObj();
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_MessageBtn, new EventManager.VoidDelegate(this.DisplayMessage));
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_FamilyMessage, new EventManager.VoidDelegate(this.DisplayFamilyMessage));
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_WorldMessage, new EventManager.VoidDelegate(this.DisplayWorldMessgae));
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_SystemMessage, new EventManager.VoidDelegate(this.DisplaySystemMessage));
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_SendMessage, new EventManager.VoidDelegate(this.SendChatMessage));
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_SendApply, new EventManager.VoidDelegate(this.SendApply));
	}

	private void Start()
	{
	}

	private void Validate()
	{
		if (this.messageInput.characterLimit > 0)
		{
			while (Encoding.UTF8.GetByteCount(this.messageInput.value) > this.messageInput.characterLimit)
			{
				this.messageInput.value = this.messageInput.value.Remove(this.messageInput.value.Length - 1, 1);
			}
		}
	}

	public void OngetObj()
	{
		this.messageBtn = base.transform.FindChild("MessageAni/Message").gameObject;
		this.messageTable = base.transform.FindChild("MessageAni/Message/Panel/Scroll View/Table").GetComponent<UITable>();
		this.messageInput = base.transform.FindChild("MessageAni/Message/Panel/inputField").GetComponent<UIInput>();
		this.systemMessageBtn = base.transform.FindChild("MessageAni/Message/Panel/imgBac/system").gameObject;
		this.familyMessageBtn = base.transform.FindChild("MessageAni/Message/Panel/imgBac/armyGroup").gameObject;
		this.wordMessageBtn = base.transform.FindChild("MessageAni/Message/Panel/imgBac/world").gameObject;
		this.messItem = base.transform.FindChild("MessageAni/Message/Panel/messageItem").gameObject;
		this.applyItem = base.transform.FindChild("MessageAni/Message/Panel/ApplyItem").gameObject;
	}

	private void DisplayMessage(GameObject go)
	{
		AudioManage.inst.PlayAuido("openUI", false);
		if (this.isFirstMessage)
		{
			this.messageExtendOffPos = this.messageBtn.transform.localPosition;
			this.messageExtendOnPos = this.messageExtendOffPos + new Vector3(-478f, 0f, 0f);
			this.isFirstMessage = false;
			MessagePanel.isHavaNotice = false;
		}
		this.messageTween(MainUIPanelManage._instance.chatPanel.gameObject);
		MainUIPanelManage._instance.messageRed.SetActive(false);
		if (T_CommandPanelManage._instance)
		{
			T_CommandPanelManage._instance.gameObject.SetActive(false);
			MainUIPanelManage._instance.OpenPanelMian();
		}
	}

	private void messageTween(GameObject ga)
	{
		if (Vector3.Distance(ga.transform.localPosition, this.messageExtendOffPos) > Vector3.Distance(ga.transform.localPosition, this.messageExtendOnPos))
		{
			MessagePanel.isOut = true;
			TweenPosition.Begin(ga, 0.3f, this.messageExtendOffPos).SetOnFinished(new EventDelegate(delegate
			{
				ga.transform.DOShakePosition(0.3f, new Vector3(15f, 0f, 0f), 20, 90f, false);
			}));
		}
		else
		{
			TweenPosition.Begin(ga, 0.3f, this.messageExtendOnPos).SetOnFinished(new EventDelegate(delegate
			{
				ga.transform.DOShakePosition(0.3f, new Vector3(15f, 0f, 0f), 20, 90f, false);
			}));
			this.messageTable.GetComponentInParent<UIScrollView>().ResetPosition();
			MessagePanel.isOut = false;
			MainUIPanelManage._instance.unReadChatMessage = 0;
		}
		MainUIPanelManage._instance.messageUISprite.enabled = !MessagePanel.isOut;
		MainUIPanelManage._instance.messageUISprite.GetComponent<BoxCollider>().enabled = !MessagePanel.isOut;
		MainUIPanelManage._instance.messageBackimg.enabled = !MessagePanel.isOut;
		MainUIPanelManage._instance.messageBackimg.GetComponent<BoxCollider>().enabled = !MessagePanel.isOut;
	}

	private void DisplayFamilyMessage(GameObject go)
	{
		if (HeroInfo.GetInstance().playerGroupId == 0L)
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("您还不是军团成员", "Battle"), HUDTextTool.TextUITypeEnum.Num5);
			return;
		}
		ArmyGroupHandler.CG_CSGetLegionHelpApplyList(HeroInfo.GetInstance().playerGroupId, delegate(bool isError)
		{
			if (!isError)
			{
				Debug.LogError("发请求了");
			}
		});
		this.ChangeMessageState(1);
		this.messageTable.HideAllChildren();
		foreach (messageItem current in messageItem.famliyLimit)
		{
			for (int i = 0; i < messageItem.famliyLimit.Count; i++)
			{
				if (current != null)
				{
					current.gameObject.SetActive(true);
				}
			}
		}
		this.messageTable.Reposition();
	}

	public void ChangeMessageState(int i)
	{
		switch (i)
		{
		case 0:
			this.messageInput.gameObject.SetActive(true);
			this.systemMessageBtn.GetComponent<UIButton>().normalSprite = "右未选中状态";
			this.systemMessageBtn.GetComponent<UISprite>().spriteName = "右未选中状态";
			this.systemMessageBtn.GetComponentInChildren<LangeuageLabel>().color = new Color(0.408888876f, 0.595555544f, 0.635555565f);
			this.wordMessageBtn.GetComponent<UIButton>().normalSprite = "聊天选中标签";
			this.wordMessageBtn.GetComponent<UIButton>().SetSprite("聊天选中标签");
			if (this.wordMessageBtn.GetComponentInChildren<LangeuageLabel>() != null)
			{
				this.wordMessageBtn.GetComponentInChildren<LangeuageLabel>().color = new Color(0.186666667f, 1.0844444f, 1.13333333f);
			}
			this.familyMessageBtn.GetComponent<UIButton>().normalSprite = "右未选中状态";
			this.familyMessageBtn.GetComponent<UISprite>().spriteName = "右未选中状态";
			this.familyMessageBtn.GetComponentInChildren<LangeuageLabel>().color = new Color(0.408888876f, 0.595555544f, 0.635555565f);
			this.systemMessageBtn.GetComponent<UISprite>().depth = 3;
			this.wordMessageBtn.GetComponent<UISprite>().depth = 5;
			this.familyMessageBtn.GetComponent<UISprite>().depth = 3;
			MessagePanel.messageState = i;
			MessagePanel.applyState = i;
			break;
		case 1:
			this.messageInput.gameObject.SetActive(true);
			this.systemMessageBtn.GetComponent<UIButton>().normalSprite = "右未选中状态";
			this.systemMessageBtn.GetComponent<UISprite>().spriteName = "右未选中状态";
			this.systemMessageBtn.GetComponentInChildren<LangeuageLabel>().color = new Color(0.408888876f, 0.595555544f, 0.635555565f);
			this.wordMessageBtn.GetComponent<UIButton>().normalSprite = "右未选中状态";
			this.wordMessageBtn.GetComponent<UISprite>().spriteName = "右未选中状态";
			this.wordMessageBtn.GetComponentInChildren<LangeuageLabel>().color = new Color(0.408888876f, 0.595555544f, 0.635555565f);
			this.familyMessageBtn.GetComponent<UISprite>().spriteName = "聊天选中标签";
			this.familyMessageBtn.GetComponent<UIButton>().normalSprite = "聊天选中标签";
			this.familyMessageBtn.GetComponentInChildren<LangeuageLabel>().color = new Color(0.182222217f, 1.0844444f, 1.13333333f);
			this.systemMessageBtn.GetComponent<UISprite>().depth = 2;
			this.wordMessageBtn.GetComponent<UISprite>().depth = 3;
			this.familyMessageBtn.GetComponent<UISprite>().depth = 5;
			MessagePanel.messageState = i;
			MessagePanel.applyState = i;
			break;
		case 4:
			this.messageInput.gameObject.SetActive(false);
			this.systemMessageBtn.GetComponent<UIButton>().normalSprite = "聊天选中标签";
			this.systemMessageBtn.GetComponent<UIButton>().SetSprite("聊天选中标签");
			this.systemMessageBtn.GetComponentInChildren<UILabel>().color = new Color(0.182222217f, 1.0844444f, 1.13333333f);
			this.wordMessageBtn.GetComponent<UIButton>().normalSprite = "右未选中状态";
			this.wordMessageBtn.GetComponent<UISprite>().spriteName = "右未选中状态";
			this.wordMessageBtn.GetComponentInChildren<UILabel>().color = new Color(0.408888876f, 0.595555544f, 0.635555565f);
			this.familyMessageBtn.GetComponent<UIButton>().normalSprite = "右未选中状态";
			this.familyMessageBtn.GetComponent<UISprite>().spriteName = "右未选中状态";
			this.familyMessageBtn.GetComponentInChildren<UILabel>().color = new Color(0.408888876f, 0.595555544f, 0.635555565f);
			this.systemMessageBtn.GetComponent<UISprite>().depth = 6;
			this.wordMessageBtn.GetComponent<UISprite>().depth = 2;
			this.familyMessageBtn.GetComponent<UISprite>().depth = 3;
			MessagePanel.messageState = i;
			MessagePanel.applyState = i;
			break;
		}
	}

	private void DisplayWorldMessgae(GameObject go)
	{
		this.ChangeMessageState(0);
		this.messageTable.HideAllChildren();
		foreach (messageItem current in messageItem.worldLimit)
		{
			if (current != null)
			{
				current.gameObject.SetActive(true);
			}
		}
		this.messageTable.Reposition();
	}

	private void DisplaySystemMessage(GameObject go)
	{
		this.ChangeMessageState(4);
		this.messageTable.HideAllChildren();
		foreach (messageItem current in messageItem.systemLimit)
		{
			if (current != null)
			{
				current.gameObject.SetActive(true);
			}
		}
		this.messageTable.Reposition();
	}

	private void SendChatMessage(GameObject go)
	{
		if (this.messageInput.value.Trim().Length > 0 && !this.messageInput.value.Equals(this.messageInput.defaultText))
		{
			if (this.messageInput.value.Equals("DebugGo"))
			{
				GameSetting.isEditor = true;
				return;
			}
			if (this.messageInput.value.Equals("DebugDeath"))
			{
				GameSetting.isEditor = false;
				return;
			}
			go.GetComponent<ButtonClick>().SetCDTime();
			ChatHandler.CG_Chat(this.messageInput.value, MessagePanel.messageState);
			this.messageInput.value = string.Empty;
		}
	}

	private void SendApply(GameObject ga)
	{
		bool isCan = true;
		ArmyGroupHandler.CG_CSLegionHelp(long.Parse(ga.name), delegate(bool isError)
		{
			if (!isError)
			{
				isCan = false;
				foreach (KeyValuePair<long, LegionHelpApply> current3 in HeroInfo.GetInstance().legionApply)
				{
					if (current3.Value.id == long.Parse(ga.name))
					{
						ga.GetComponentInParent<ApplyMessageItem>().endTime = current3.Value.cdTime;
					}
				}
			}
			else
			{
				if (HttpMgr.inst.Errorid == 8 || HttpMgr.inst.Errorid == 2019)
				{
					this.DestoryItem(int.Parse(ga.name));
				}
				isCan = false;
			}
		});
		if (isCan)
		{
			foreach (KeyValuePair<long, LegionHelpApply> current in HeroInfo.GetInstance().legionApply)
			{
				if (current.Value.id == (long)int.Parse(ga.name))
				{
					foreach (KeyValuePair<long, long> current2 in current.Value.helper)
					{
						if (current2.Key == HeroInfo.GetInstance().userId)
						{
							HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("你已经协助过该成员", "Battle"), HUDTextTool.TextUITypeEnum.Num5);
							return;
						}
					}
				}
			}
		}
	}

	public void ShowApplyInfo()
	{
		foreach (KeyValuePair<long, LegionHelpApply> current in HeroInfo.GetInstance().legionApply)
		{
			for (int i = 0; i < HeroInfo.GetInstance().applyIdList.Count; i++)
			{
				if (current.Value.id == HeroInfo.GetInstance().applyIdList[i])
				{
					for (int j = 0; j < MessagePanel._Inst.applyList.Count; j++)
					{
						if (this.applyList[j].GetComponent<ApplyMessageItem>().id == HeroInfo.GetInstance().applyIdList[i])
						{
							UnityEngine.Object.Destroy(this.applyList[j]);
							this.messageTable.Reposition();
							HeroInfo.GetInstance().legionApply.Remove(current.Value.id);
						}
					}
				}
			}
		}
	}

	private void DestoryItem(int id)
	{
		for (int i = 0; i < this.applyList.Count; i++)
		{
			if (this.applyList[i].GetComponent<ApplyMessageItem>().id == (long)id)
			{
				UnityEngine.Object.Destroy(this.applyList[i]);
			}
		}
		this.messageTable.Reposition();
	}
}
