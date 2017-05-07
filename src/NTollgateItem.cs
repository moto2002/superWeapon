using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class NTollgateItem : MonoBehaviour
{
	public BattleField tollagteItem;

	public UILabel name_Label;

	public UILabel commandDes;

	public UILabel commandLv;

	public UILabel consume;

	public GameObject goldIcon;

	public GameObject petroleumIcon;

	public GameObject steelIcon;

	public GameObject mineIcon;

	public UILabel goldNum;

	public UILabel petroleumNum;

	public UILabel steelNum;

	public UILabel mineNum;

	public UILabel expNum;

	public List<GameObject> starList = new List<GameObject>();

	public UITable diaoluoParent;

	public UILabel saodangNum;

	public Transform leftPos;

	public Transform rightPos;

	public Transform BG;

	public GameObject tongguan;

	public GameObject suo;

	public GameObject suo1;

	public GameObject suo2;

	public GameObject suo3;

	public GameObject suo4;

	public UILabel ArmyTokenCdTimeLabel;

	public string NowTime;

	public string CDTime;

	public string GapTime;

	private int basic_junLing;

	[HideInInspector]
	public bool isAddDataEnd;

	private void Awake()
	{
		this.Initialize();
	}

	private void Initialize()
	{
		this.name_Label = base.transform.FindChild("bg/Title/Label").GetComponent<UILabel>();
		this.commandDes = base.transform.FindChild("bg/Xinxi/HeiBeiJing/Tiaojian/NeedLvDes").GetComponent<UILabel>();
		this.commandLv = base.transform.FindChild("bg/Xinxi/HeiBeiJing/Tiaojian/NeedLv").GetComponent<UILabel>();
		this.consume = base.transform.FindChild("bg/Xinxi/HeiBeiJing/XiaoHao/Des").GetComponent<UILabel>();
		for (int i = 1; i < 4; i++)
		{
			GameObject gameObject = base.transform.FindChild("bg/Xinxi/HeiBeiJing/XingXing/Liangxing" + i).gameObject;
			this.starList.Add(gameObject);
		}
		this.goldIcon = base.transform.FindChild("bg/Huode/Ziyuan/Icon1").gameObject;
		this.petroleumIcon = base.transform.FindChild("bg/Huode/Ziyuan/Icon2").gameObject;
		this.steelIcon = base.transform.FindChild("bg/Huode/Ziyuan/Icon3").gameObject;
		this.mineIcon = base.transform.FindChild("bg/Huode/Ziyuan/Icon4").gameObject;
		this.goldNum = base.transform.FindChild("bg/Huode/Ziyuan/Icon1/Label").GetComponent<UILabel>();
		this.petroleumNum = base.transform.FindChild("bg/Huode/Ziyuan/Icon2/Label").GetComponent<UILabel>();
		this.steelNum = base.transform.FindChild("bg/Huode/Ziyuan/Icon3/Label").GetComponent<UILabel>();
		this.mineNum = base.transform.FindChild("bg/Huode/Ziyuan/Icon4/Label").GetComponent<UILabel>();
		this.expNum = base.transform.FindChild("bg/Huode/Ziyuan/EXP/Label").GetComponent<UILabel>();
		this.diaoluoParent = base.transform.FindChild("bg/Huode/diaoluo/Table").GetComponent<UITable>();
		this.saodangNum = base.transform.FindChild("bg/Saodangling/Num").GetComponent<UILabel>();
		this.leftPos = base.transform.FindChild("Left");
		this.rightPos = base.transform.FindChild("Rigt");
		this.BG = base.transform.FindChild("bg");
		this.tongguan = base.transform.FindChild("bg/Xinxi/Tongguan").gameObject;
		this.suo = base.transform.FindChild("bg/Xinxi/Suo").gameObject;
		this.ArmyTokenCdTimeLabel = base.transform.FindChild("bg/Xinxi/HeiBeiJing/XiaoHao/ArmyTokenCdTime").GetComponent<UILabel>();
	}

	private void LateUpdate()
	{
		if (this.isAddDataEnd)
		{
			if (this.leftPos.position.x >= NTollgateManage.inst.leftPos.position.x && this.rightPos.position.x <= NTollgateManage.inst.rightPos.position.x)
			{
				this.BG.localScale = Vector3.one;
				NTollgateManage.inst.nowTollagte = this.tollagteItem;
				NTollgateManage.inst.ButtonShow(this.tollagteItem);
			}
			else
			{
				this.BG.localScale = Vector3.one * 0.8f;
			}
		}
		if (this.ArmyTokenCdTimeLabel)
		{
			if (HeroInfo.GetInstance().playerRes.junLing >= int.Parse(UnitConst.GetInstance().DesighConfigDic[23].value))
			{
				this.ArmyTokenCdTimeLabel.text = "军令已达上限";
			}
			else if (SenceManager.inst.ArmyTokenCdTime > TimeTools.GetNowTimeSyncServerToDateTime())
			{
				TimeSpan timeSpan = SenceManager.inst.ArmyTokenCdTime - TimeTools.GetNowTimeSyncServerToDateTime();
				int hours = timeSpan.Hours;
				int minutes = timeSpan.Minutes;
				int seconds = timeSpan.Seconds;
				string text = string.Empty + hours;
				string text2 = string.Empty + minutes;
				string text3 = string.Empty + seconds;
				if (hours < 10)
				{
					text = "0" + text;
				}
				if (minutes < 10)
				{
					text2 = "0" + text2;
				}
				if (seconds < 10)
				{
					text3 = "0" + text3;
				}
				this.ArmyTokenCdTimeLabel.text = string.Concat(new string[]
				{
					"军令恢复倒计时 ",
					text,
					":",
					text2,
					":",
					text3
				});
			}
			else
			{
				this.ArmyTokenCdTimeLabel.text = "军令恢复！";
				if (this.basic_junLing == HeroInfo.GetInstance().playerRes.junLing)
				{
					this.basic_junLing = HeroInfo.GetInstance().playerRes.junLing + 1;
					this.consume.text = string.Format("{0}/{1}", HeroInfo.GetInstance().playerRes.junLing + 1, UnitConst.GetInstance().DesighConfigDic[23].value);
					if (this.basic_junLing >= int.Parse(UnitConst.GetInstance().DesighConfigDic[23].value))
					{
						this.ArmyTokenCdTimeLabel.text = "军令已达上限";
					}
				}
			}
		}
	}

	private void OnEnable()
	{
		ClientMgr.GetNet().NetDataHandler.DataChange += new DataHandler.Data_Change(this.DataHandler_DataChange);
	}

	private void DataHandler_DataChange(int opcodeCMD)
	{
		if (this.tollagteItem != null && opcodeCMD == 10003)
		{
			this.consume.color = ((this.tollagteItem.cost <= HeroInfo.GetInstance().playerRes.junLing) ? new Color(0.06666667f, 0.7411765f, 0f) : new Color(0.7019608f, 0.06666667f, 0f));
			this.basic_junLing = HeroInfo.GetInstance().playerRes.junLing;
			this.consume.text = string.Format("{0}/{1}", HeroInfo.GetInstance().playerRes.junLing, UnitConst.GetInstance().DesighConfigDic[23].value);
		}
		this.saodangNum.color = ((HeroInfo.GetInstance().GetItemCountById(2) <= 0) ? new Color(0.7019608f, 0.06666667f, 0f) : new Color(0.06666667f, 0.7411765f, 0f));
		this.saodangNum.text = HeroInfo.GetInstance().GetItemCountById(2).ToString();
	}

	private void OnDisable()
	{
		ClientMgr.GetNet().NetDataHandler.DataChange -= new DataHandler.Data_Change(this.DataHandler_DataChange);
	}

	[DebuggerHidden]
	public IEnumerator ShowTollagteInfo()
	{
		NTollgateItem.<ShowTollagteInfo>c__Iterator63 <ShowTollagteInfo>c__Iterator = new NTollgateItem.<ShowTollagteInfo>c__Iterator63();
		<ShowTollagteInfo>c__Iterator.<>f__this = this;
		return <ShowTollagteInfo>c__Iterator;
	}
}
