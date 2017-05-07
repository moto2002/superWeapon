using System;
using UnityEngine;

public class MilitaryRankPanel : MonoBehaviour
{
	public static MilitaryRankPanel _inst;

	public UISprite MilitaryRank_Icon;

	public UILabel MilitaryRank_Name;

	public UILabel MilitaryRank_Cup;

	public UILabel MilitaryRank_Rank;

	public UISprite MilitartRankGift_Notice;

	private int playerMilitaryRank;

	public void OnDisable()
	{
		if (ClientMgr.GetNet())
		{
			ClientMgr.GetNet().NetDataHandler.DataReadEnd -= new Action(this.NetDataHandler_DataChange);
		}
	}

	public void OnDestroy()
	{
		MilitaryRankPanel._inst = null;
	}

	private void Awake()
	{
		MilitaryRankPanel._inst = this;
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_MilitaryRank, new EventManager.VoidDelegate(this.MainPanel_MilitaryRank_CallBack));
		this.SetInfo();
	}

	private void Start()
	{
	}

	private void OnEnable()
	{
		this.SetInfo();
		if (ClientMgr.GetNet())
		{
			ClientMgr.GetNet().NetDataHandler.DataReadEnd += new Action(this.NetDataHandler_DataChange);
		}
	}

	private void NetDataHandler_DataChange()
	{
		this.SetInfo();
	}

	private void MainPanel_MilitaryRank_CallBack(GameObject ga)
	{
		Debug.Log("打开军衔奖励面板：Loading.senceType " + Loading.senceType);
		FuncUIManager.inst.OpenFuncUI("MilitaryRankGiftPanel", Loading.senceType);
	}

	public void SetInfo()
	{
		if (TimeTools.ConvertLongDateTime(HeroInfo.GetInstance().MilitaryRankGift_Time).Day != TimeTools.GetNowTimeSyncServerToDateTime().Day)
		{
			this.MilitartRankGift_Notice.gameObject.SetActive(true);
		}
		else
		{
			this.MilitartRankGift_Notice.gameObject.SetActive(false);
		}
		this.playerMilitaryRank = (int)HeroInfo.GetInstance().PlayerMilitaryRank;
		this.MilitaryRank_Icon.spriteName = UnitConst.GetInstance().MilitaryRankDataList[this.playerMilitaryRank].icon;
		this.MilitaryRank_Icon.SetDimensions(65, 65);
		if (UnitConst.GetInstance().MilitaryRankDataList.ContainsKey(this.playerMilitaryRank))
		{
			this.MilitaryRank_Name.text = UnitConst.GetInstance().MilitaryRankDataList[this.playerMilitaryRank].name.ToString();
			this.MilitaryRank_Cup.text = HeroInfo.GetInstance().playerRes.playermedal.ToString();
			if (HeroInfo.GetInstance().topScore < int.Parse(UnitConst.GetInstance().DesighConfigDic[72].value) && HeroInfo.GetInstance().topScore != 0)
			{
				this.MilitaryRank_Rank.text = LanguageManage.GetTextByKey("第", "others") + HeroInfo.GetInstance().topScore + LanguageManage.GetTextByKey("名", "others");
				this.MilitaryRank_Rank.color = new Color(0.196078435f, 0.972549f, 0.117647059f);
			}
			else
			{
				this.MilitaryRank_Rank.text = LanguageManage.GetTextByKey("未上榜", "others");
				this.MilitaryRank_Rank.color = new Color(1f, 0.1882353f, 0.101960786f);
			}
		}
		this.MilitaryRank_Icon.enabled = true;
		this.MilitaryRank_Name.enabled = true;
		this.MilitaryRank_Cup.enabled = true;
		this.MilitaryRank_Rank.enabled = true;
	}

	private void Update()
	{
	}
}
