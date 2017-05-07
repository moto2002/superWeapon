using System;
using UnityEngine;

public class AideData_Server : MonoBehaviour
{
	[HideInInspector]
	public AdjutantPanelData.AideData curAideData;

	public UILabel nowCount;

	public UILabel addCount;

	public UILabel haveUpCount;

	public UILabel coldTime;

	public UILabel info;

	public UILabel name_Client;

	public UISprite needUpSprite;

	public UISprite peopleBG;

	public UITexture peopleTexture;

	public GameObject CallBack;

	public GameObject backSprite;

	[HideInInspector]
	public static AideData_Server _ins;

	public bool isCanIntensify = true;

	public int data;

	public void OnDestroy()
	{
		AideData_Server._ins = null;
	}

	private void Awake()
	{
		AideData_Server._ins = this;
		this.ShowT();
	}

	public void ShowT()
	{
		this.nowCount = base.transform.FindChild("info/nowCount").GetComponent<UILabel>();
		this.addCount = base.transform.FindChild("info/addCount").GetComponent<UILabel>();
		this.haveUpCount = base.transform.FindChild("强化剂/Sprite/remainLabel").GetComponent<UILabel>();
		this.coldTime = base.transform.FindChild("强化剂/Label").GetComponent<UILabel>();
		this.info = base.transform.FindChild("info").GetComponent<UILabel>();
		this.name_Client = base.transform.FindChild("name/Label").GetComponent<UILabel>();
		this.needUpSprite = base.transform.FindChild("强化剂/Sprite").GetComponent<UISprite>();
		this.peopleBG = base.transform.FindChild("icon/Sprite").GetComponent<UISprite>();
		this.CallBack = base.transform.FindChild("召回").gameObject;
		this.CallBack.AddComponent<AdjutantBtn>();
		AdjutantBtn component = this.CallBack.GetComponent<AdjutantBtn>();
		component.btnType = AdjutantPanel.btnType.OpenCallBack;
		this.backSprite = base.transform.FindChild("强化剂").gameObject;
		if (!this.backSprite.GetComponent<AdjutantBtn>())
		{
			this.backSprite.AddComponent<AdjutantBtn>();
		}
		AdjutantBtn component2 = this.backSprite.GetComponent<AdjutantBtn>();
		component2.btnType = AdjutantPanel.btnType.Intensify;
		this.peopleTexture = base.transform.FindChild("icon/people").GetComponent<UITexture>();
	}

	private void Start()
	{
	}

	public void OnEnable()
	{
		ClientMgr.GetNet().NetDataHandler.DataChange += new DataHandler.Data_Change(this.DataHandler_DataChange);
	}

	private void DataHandler_DataChange(int opcodeCMD)
	{
		if (opcodeCMD == 10028)
		{
			this.OnAideData(this.data);
		}
	}

	private void OnDisable()
	{
		ClientMgr.GetNet().NetDataHandler.DataChange -= new DataHandler.Data_Change(this.DataHandler_DataChange);
	}

	private void Update()
	{
		if (this.curAideData != null)
		{
			if (TimeTools.DateDiffToDouble(TimeTools.GetNowTimeSyncServerToDateTime(), TimeTools.ConvertLongDateTime(this.curAideData.time)) > 0.0)
			{
				if (this.isCanIntensify)
				{
					this.coldTime.gameObject.SetActive(true);
					this.needUpSprite.gameObject.SetActive(false);
					this.backSprite.GetComponent<BoxCollider>().enabled = false;
					this.coldTime.text = TimeTools.DateDiffToString(TimeTools.GetNowTimeSyncServerToDateTime(), TimeTools.ConvertLongDateTime(this.curAideData.time));
				}
				this.addCount.text = "+" + UnitConst.GetInstance().AideAbilityConst[this.data].value * 2 + "%";
				this.isCanIntensify = false;
			}
			else
			{
				this.needUpSprite.gameObject.SetActive(true);
				this.backSprite.GetComponent<BoxCollider>().enabled = true;
				this.coldTime.gameObject.SetActive(false);
				if (HeroInfo.GetInstance().PlayerItemInfo.ContainsKey(int.Parse(UnitConst.GetInstance().DesighConfigDic[17].value)) && HeroInfo.GetInstance().PlayerItemInfo[int.Parse(UnitConst.GetInstance().DesighConfigDic[17].value)] > 0)
				{
					this.isCanIntensify = true;
				}
				else
				{
					this.isCanIntensify = false;
				}
			}
		}
	}

	public void OnAideData(int i)
	{
		this.data = i;
	}
}
