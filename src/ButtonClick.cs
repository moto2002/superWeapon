using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[SerializeField]
public class ButtonClick : IMonoBehaviour
{
	public UISprite ThisUISprite;

	public EventManager.EventType eventType;

	private bool isCanDoEvent = true;

	public UISprite[] UispriteSetColor;

	public static Dictionary<EventManager.EventType, GameObject> AllButtonClick = new Dictionary<EventManager.EventType, GameObject>();

	private float endCDTime;

	public float clickTimeCD = 0.6f;

	public GameObject clickTimeCDUISprite;

	public bool newbiBtn;

	public static bool newbiLock = false;

	public bool isCDTimeByEventSet;

	private static Dictionary<EventManager.EventType, float> allButtonCD = new Dictionary<EventManager.EventType, float>();

	public bool isSendLua = true;

	public bool IsAutoSetColor = true;

	private bool isBtnOpenToSendEvent = true;

	private bool IsOpen = true;

	public ShowButtonID buttonOpen;

	protected float _lastPress = -1f;

	private bool? tmpSetColor;

	public bool IsCanDoEvent
	{
		get
		{
			return this.isCanDoEvent;
		}
		set
		{
			this.isCanDoEvent = value;
		}
	}

	public override void Awake()
	{
		base.Awake();
		this.InitEventType();
		if (this.ga.GetComponentsInChildren<UISprite>(true) != null)
		{
			this.UispriteSetColor = this.ga.GetComponentsInChildren<UISprite>(true);
		}
		if (base.GetComponent<UISprite>())
		{
			this.ThisUISprite = base.GetComponent<UISprite>();
		}
	}

	private void Start()
	{
		this.SetLabelColor();
	}

	public void SetLabelColor()
	{
		if (this.ThisUISprite)
		{
			if (!this.ThisUISprite.atlas)
			{
				return;
			}
			if (this.ThisUISprite.atlas.name != "NewBtnAtlas")
			{
				return;
			}
			if (this.ThisUISprite.spriteName == "十连抽招募" || this.ThisUISprite.spriteName == "十连抽招募选中" || this.ThisUISprite.spriteName == "升级、确定按钮" || this.ThisUISprite.spriteName == "升级、确定按钮选中" || this.ThisUISprite.spriteName == "hui")
			{
				if (this.ThisUISprite.spriteName == "升级、确定按钮")
				{
					this.ThisUISprite.spriteName = "blue";
				}
				else if (this.ThisUISprite.spriteName == "升级、确定按钮选中")
				{
					this.ThisUISprite.spriteName = "blued";
				}
				else if (this.ThisUISprite.spriteName == "十连抽招募")
				{
					this.ThisUISprite.spriteName = "yellow";
				}
				else if (this.ThisUISprite.spriteName == "十连抽招募选中")
				{
					this.ThisUISprite.spriteName = "yellowed";
				}
			}
			Color white = Color.white;
			Color white2 = Color.white;
			float num = 1f;
			if (this.ThisUISprite.spriteName == "yellow" || this.ThisUISprite.spriteName == "yellowed")
			{
				white = new Color(0.6666667f, 0.403921574f, 0.0784313753f, 1f);
				white2 = new Color(0.992156863f, 0.9843137f, 0.784313738f, 1f);
				num = 1f;
			}
			else if (this.ThisUISprite.spriteName == "blue" || this.ThisUISprite.spriteName == "blued")
			{
				white = new Color(0f, 0.368627459f, 0.843137264f, 1f);
				num = 2f;
			}
			else if (this.ThisUISprite.spriteName == "hui")
			{
				white = new Color(0.3764706f, 0.3764706f, 0.3764706f, 1f);
				white2 = new Color(0.7019608f, 0.7019608f, 0.7019608f, 1f);
				num = 1f;
			}
			UILabel[] componentsInChildren = base.GetComponentsInChildren<UILabel>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				UILabel uILabel = componentsInChildren[i];
				uILabel.color = white2;
				uILabel.effectStyle = UILabel.Effect.Outline;
				uILabel.effectDistance = new Vector2(num, num);
				uILabel.effectColor = white;
				uILabel.applyGradient = false;
			}
		}
	}

	private void InitEventType()
	{
		if (this.eventType != EventManager.EventType.none)
		{
			if (!ButtonClick.AllButtonClick.ContainsKey(this.eventType))
			{
				ButtonClick.AllButtonClick.Add(this.eventType, this.ga);
			}
			else
			{
				ButtonClick.AllButtonClick[this.eventType] = this.ga;
			}
		}
	}

	public virtual void OnClick()
	{
		if (PoolManage.Ins && Loading.senceType != SenceType.Login)
		{
			Vector3 mousePosition = Input.mousePosition;
			Vector3 position = HUDTextTool.inst.hudtextCamera.ScreenToWorldPoint(mousePosition);
			DieBall dieBall = PoolManage.Ins.CreatEffect("dianji_fankui", position, Quaternion.identity, HUDTextTool.inst.noticeMarqee.transform);
			GameTools.GetCompentIfNoAddOne<RenderQueueEdit>(dieBall.ga);
			Transform[] componentsInChildren = dieBall.GetComponentsInChildren<Transform>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Transform transform = componentsInChildren[i];
				transform.gameObject.layer = 8;
			}
		}
		if (CameraControl.inst && CameraControl.inst.cameraBuildingStateEnum == CameraControl.CameraBuildingStateEnum.BuildingMovingOrInBuilding && Camera_FingerManager.inst && Camera_FingerManager.inst.dragCamera != null)
		{
			UnityEngine.Object.Destroy(Camera_FingerManager.inst.dragCamera.gameObject);
		}
		if (!this.IsOpen)
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("按钮尚未开放", "others"), HUDTextTool.TextUITypeEnum.Num5);
			return;
		}
		if (!this.isBtnOpenToSendEvent)
		{
			return;
		}
		if (!this.newbiBtn && ButtonClick.newbiLock)
		{
			return;
		}
		if (base.GetComponent<UISprite>() && base.GetComponent<UISprite>().atlas && base.GetComponent<UISprite>().atlas.name == "NewBtnAtlas" && (base.GetComponent<UISprite>().spriteName == "blue" || base.GetComponent<UISprite>().spriteName == "blued" || base.GetComponent<UISprite>().spriteName == "yellow" || base.GetComponent<UISprite>().spriteName == "yellowed"))
		{
			string resName = string.Empty;
			if (base.GetComponent<UISprite>().spriteName == "yellow" || base.GetComponent<UISprite>().spriteName == "yellowed")
			{
				resName = "k_yellow";
			}
			else if (base.GetComponent<UISprite>().spriteName == "blue" || base.GetComponent<UISprite>().spriteName == "blued")
			{
				resName = "k_blue";
			}
			DieBall dieBall2 = PoolManage.Ins.CreatEffect(resName, base.transform.position, Quaternion.identity, base.transform);
			if (dieBall2)
			{
				dieBall2.LifeTime = 1f;
				float num = base.GetComponent<BoxCollider>().size.x / 218f;
				dieBall2.tr.localPosition = Vector3.zero + new Vector3(114.2f * (1f - num), 0f, 0f);
				dieBall2.tr.localScale = Vector3.one;
				dieBall2.ga.AddComponent<RenderQueueEdit>();
				Transform[] componentsInChildren2 = dieBall2.GetComponentsInChildren<Transform>();
				for (int j = 0; j < componentsInChildren2.Length; j++)
				{
					Transform transform2 = componentsInChildren2[j];
					transform2.gameObject.layer = 8;
					if (transform2.GetComponent<ParticleSystem>())
					{
						transform2.GetComponent<ParticleSystem>().startSize *= num * 0.95f;
					}
				}
			}
			base.StartCoroutine(this.ButtonCreateLightCallBack());
			return;
		}
		this.DoClick();
	}

	[DebuggerHidden]
	private IEnumerator ButtonCreateLightCallBack()
	{
		ButtonClick.<ButtonCreateLightCallBack>c__Iterator75 <ButtonCreateLightCallBack>c__Iterator = new ButtonClick.<ButtonCreateLightCallBack>c__Iterator75();
		<ButtonCreateLightCallBack>c__Iterator.<>f__this = this;
		return <ButtonCreateLightCallBack>c__Iterator;
	}

	public void DoClick()
	{
		if (base.enabled && this.isCanDoEvent && Time.time > this.endCDTime)
		{
			if (!this.isCDTimeByEventSet)
			{
				this.SetCDTime();
			}
			else
			{
				this.endCDTime = Time.time + 0.6f;
			}
			if (EventManager.Instance != null)
			{
				EventManager.Instance.DispatchEvent(this.eventType, base.gameObject);
			}
			if (this.ga.layer != 13 && this.eventType != EventManager.EventType.SkillExtractPanel_One && this.isSendLua)
			{
				if (NewbieGuidePanel._instance && NewbieGuidePanel._instance.gameObject.activeSelf)
				{
					NewbieGuidePanel._instance.HideSelf();
				}
				if (HUDTextTool.inst)
				{
					HUDTextTool.inst.NextLuaCall(this.eventType.ToString(), new object[]
					{
						base.gameObject
					});
				}
			}
		}
		else
		{
			LogManage.LogError(string.Format("isCanDoEvent :{0} enable:{1} Time.time>endCDTime :{2}", this.isCanDoEvent, base.enabled, Time.time > this.endCDTime));
		}
	}

	public void SetCDTime()
	{
		this.endCDTime = Time.time + this.clickTimeCD;
		if (ButtonClick.allButtonCD.ContainsKey(this.eventType))
		{
			ButtonClick.allButtonCD[this.eventType] = this.endCDTime;
		}
		else
		{
			ButtonClick.allButtonCD.Add(this.eventType, this.endCDTime);
		}
		if (this.clickTimeCDUISprite)
		{
			TweenFillAmount tweenFillAmount = UITweener.Begin<TweenFillAmount>(this.clickTimeCDUISprite, this.clickTimeCD);
			tweenFillAmount.from = 1f;
			tweenFillAmount.to = 0f;
			tweenFillAmount.PlayForward();
		}
	}

	protected void OnEnable()
	{
		this.InitEventType();
		if (ButtonClick.allButtonCD.ContainsKey(this.eventType))
		{
			this.endCDTime = ButtonClick.allButtonCD[this.eventType];
			if (Time.time < this.endCDTime)
			{
				float num = this.endCDTime - Time.time;
				if (this.clickTimeCDUISprite && num > 0f)
				{
					TweenFillAmount tweenFillAmount = UITweener.Begin<TweenFillAmount>(this.clickTimeCDUISprite, num);
					tweenFillAmount.from = num / this.clickTimeCD;
					tweenFillAmount.to = 0f;
					tweenFillAmount.PlayForward();
				}
			}
		}
		this.IsCanDoEvent = true;
		if (this.buttonOpen != ShowButtonID.none)
		{
			this.isBtnOpenToSendEvent = false;
			if (NewbieGuideManage.btnIDList.Contains((int)this.buttonOpen) || NewbieGuideManage.isAllOpenbtn)
			{
				this.btnOpen(true);
			}
			else
			{
				this.btnOpen(false);
			}
		}
		else if (this.IsAutoSetColor)
		{
			this.OnSetSpColor(false);
		}
		this.SetLabelColor();
		if (ClientMgr.GetNet())
		{
			ClientMgr.GetNet().NetDataHandler.DataChange += new DataHandler.Data_Change(this.DataHandler_DataChange);
		}
	}

	private void OnDisable()
	{
		if (ClientMgr.GetNet())
		{
			ClientMgr.GetNet().NetDataHandler.DataChange -= new DataHandler.Data_Change(this.DataHandler_DataChange);
		}
	}

	private void DataHandler_DataChange(int opcodeCMD)
	{
		if (opcodeCMD == 10063 && this.buttonOpen != ShowButtonID.none && !this.isBtnOpenToSendEvent && (NewbieGuideManage.btnIDList.Contains((int)this.buttonOpen) || NewbieGuideManage.isAllOpenbtn))
		{
			this.btnOpen(true);
		}
	}

	private void btnOpen(bool isOpen)
	{
		if (this.buttonOpen != ShowButtonID.none)
		{
			if (UnitConst.GetInstance().btnUpSet[(int)this.buttonOpen][0].Equals("2"))
			{
				if (isOpen)
				{
					TweenAlpha tween = TweenAlpha.Begin(this.ga, 0.6f, 1f);
					UIButton component = this.ga.GetComponent<UIButton>();
					if (component)
					{
						component.defaultColor = new Color(component.defaultColor.r, component.defaultColor.g, component.defaultColor.b, 1f);
					}
					tween.SetOnFinished(new EventDelegate(delegate
					{
						UnityEngine.Object.DestroyImmediate(tween);
					}));
				}
				else
				{
					UISprite component2 = base.GetComponent<UISprite>();
					if (component2)
					{
						component2.color = new Color(component2.color.r, component2.color.g, component2.color.b, 0f);
					}
				}
			}
			else if (this.UispriteSetColor.Length != 0)
			{
				this.OnSetSpColor(!isOpen);
			}
		}
		if (isOpen && !this.isBtnOpenToSendEvent && NewbieGuideManage.btnID.Contains((int)this.buttonOpen))
		{
			DieBall dieBall = PoolManage.Ins.CreatEffect(UnitConst.GetInstance().btnUpSet[(int)this.buttonOpen][1], this.tr.position, Quaternion.identity, this.tr);
			if (dieBall)
			{
				dieBall.tr.localPosition = new Vector3(dieBall.tr.localPosition.x, dieBall.tr.localPosition.y, -100f);
			}
			if (!string.IsNullOrEmpty(UnitConst.GetInstance().btnUpSet[(int)this.buttonOpen][2]) && TaskByNewBieManager._inst && UnitConst.GetInstance().btnUpSet[(int)this.buttonOpen][2] != string.Empty)
			{
				TaskByNewBieManager._inst.SetNewbieGroup(UnitConst.GetInstance().btnUpSet[(int)this.buttonOpen][2]);
			}
			NewbieGuideManage.btnID.Remove((int)this.buttonOpen);
			this.buttonOpen = ShowButtonID.none;
		}
		this.IsOpen = isOpen;
		this.isBtnOpenToSendEvent = isOpen;
	}

	protected void OnPress(bool pressed)
	{
		SenceType senceType = Loading.senceType;
		if (senceType != SenceType.Island)
		{
			if (senceType == SenceType.WorldMap)
			{
				if (WMap_DragManager.inst)
				{
					WMap_DragManager.inst.btnInUse = pressed;
				}
				if (DragMgr.inst)
				{
					DragMgr.inst.BtnInUse = false;
				}
			}
		}
		else
		{
			if (DragMgr.inst)
			{
				DragMgr.inst.BtnInUse = pressed;
			}
			if (WMap_DragManager.inst)
			{
				WMap_DragManager.inst.btnInUse = false;
			}
		}
	}

	public void OnSetSpColor(bool isSet)
	{
		if (this.tmpSetColor == isSet)
		{
			return;
		}
		this.tmpSetColor = new bool?(isSet);
		for (int i = 0; i < this.UispriteSetColor.Length; i++)
		{
			if (this.UispriteSetColor[i] != null)
			{
				if (isSet)
				{
					if (this.UispriteSetColor[i].gameObject.GetComponent<UIButton>() != null)
					{
						this.UispriteSetColor[i].gameObject.GetComponent<UIButton>().enabled = false;
						this.UispriteSetColor[i].ShaderToGray();
					}
					else
					{
						this.UispriteSetColor[i].ShaderToGray();
					}
				}
				else if (this.UispriteSetColor[i].gameObject.GetComponent<UIButton>() != null)
				{
					this.UispriteSetColor[i].gameObject.GetComponent<UIButton>().enabled = true;
					this.UispriteSetColor[i].ShaderToNormal();
				}
				else
				{
					this.UispriteSetColor[i].ShaderToNormal();
				}
			}
		}
	}
}
