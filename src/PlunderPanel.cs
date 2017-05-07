using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlunderPanel : MonoBehaviour
{
	public static PlunderPanel inst;

	public List<BuildingNPC> all;

	public UILabel titleLabel;

	public UILabel coinLabel;

	public UILabel oilLabel;

	public UILabel steelLabel;

	public UILabel rareEarthLabel;

	public float coinNum;

	public float oilNum;

	public float steelNum;

	public float rareEarthNum;

	public int maxCoinNum;

	public int maxOilNum;

	public int maxSteelNum;

	public int maxRareEarthNum;

	public GameObject Exp_Ga;

	public UILabel Exp_Label;

	public GameObject ItemPrefab;

	public UIGrid ThisGrid;

	public Dictionary<int, int> BattleItemList = new Dictionary<int, int>();

	public float canshu1;

	public float canshu2;

	public float canshu3;

	public bool CanPlunderCoin
	{
		get
		{
			return this.coinNum < (float)this.maxCoinNum;
		}
	}

	public bool CanPlunderOil
	{
		get
		{
			return this.oilNum < (float)this.maxOilNum;
		}
	}

	public bool CanPlunderSteel
	{
		get
		{
			return this.steelNum < (float)this.maxSteelNum;
		}
	}

	public bool CanPlunderRareEarth
	{
		get
		{
			return this.rareEarthNum < (float)this.maxRareEarthNum;
		}
	}

	public void OnDestroy()
	{
		PlunderPanel.inst = null;
	}

	private void Awake()
	{
		PlunderPanel.inst = this;
		if (UIManager.curState != SenceState.Visit)
		{
			base.gameObject.SetActive(true);
		}
		else
		{
			base.gameObject.SetActive(false);
		}
	}

	private void Start()
	{
		this.all = SenceInfo.curMap.ResourceBuildingList.Values.ToList<BuildingNPC>();
		this.canshu1 = float.Parse(UnitConst.GetInstance().DesighConfigDic[29].value);
		this.canshu2 = float.Parse(UnitConst.GetInstance().DesighConfigDic[30].value);
		this.canshu3 = float.Parse(UnitConst.GetInstance().DesighConfigDic[31].value);
		int num = 0;
		for (int i = 0; i < SenceManager.inst.towers.Count; i++)
		{
			if (SenceManager.inst.towers[i].secType == 1)
			{
				num = SenceManager.inst.towers[i].lv;
			}
		}
		float num2 = (float)(num - HeroInfo.GetInstance().PlayerCommondLv) * this.canshu1 + this.canshu2;
		if (num2 > 1f)
		{
			num2 = 1f;
		}
		if (num2 < 0.2f)
		{
			num2 = 0.2f;
		}
		if (SpyPanelManager.inst)
		{
			this.titleLabel.text = LanguageManage.GetTextByKey("可以掠夺的资源", "Battle");
		}
		int num3 = 0;
		if (NewbieGuidePanel.isEnemyAttck && SenceInfo.curMap.baseRes.ContainsKey(ResType.金币) && (UIManager.curState == SenceState.Spy || SenceInfo.CurBattle == null))
		{
			num3++;
			this.coinLabel.transform.parent.gameObject.SetActive(true);
			this.coinLabel.transform.gameObject.SetActive(true);
			this.coinLabel.text = SenceInfo.curMap.baseRes[ResType.金币] + ((!SenceInfo.curMap.addRes.ContainsKey(ResType.金币)) ? string.Empty : ("+" + SenceInfo.curMap.addRes[ResType.金币]));
		}
		else
		{
			this.coinLabel.transform.parent.gameObject.SetActive(false);
			this.titleLabel.gameObject.SetActive(false);
		}
		if (NewbieGuidePanel.isEnemyAttck && SenceInfo.curMap.baseRes.ContainsKey(ResType.石油) && (UIManager.curState == SenceState.Spy || SenceInfo.CurBattle == null))
		{
			num3++;
			this.oilLabel.transform.parent.gameObject.SetActive(true);
			this.oilLabel.transform.gameObject.SetActive(true);
			this.oilLabel.text = SenceInfo.curMap.baseRes[ResType.石油] + ((!SenceInfo.curMap.addRes.ContainsKey(ResType.石油)) ? string.Empty : ("+" + SenceInfo.curMap.addRes[ResType.石油]));
		}
		else
		{
			this.oilLabel.transform.parent.gameObject.SetActive(false);
		}
		if (NewbieGuidePanel.isEnemyAttck && SenceInfo.curMap.baseRes.ContainsKey(ResType.钢铁) && (UIManager.curState == SenceState.Spy || SenceInfo.CurBattle == null))
		{
			num3++;
			this.steelLabel.transform.parent.gameObject.SetActive(true);
			this.steelLabel.transform.gameObject.SetActive(true);
			this.steelLabel.text = SenceInfo.curMap.baseRes[ResType.钢铁] + ((!SenceInfo.curMap.addRes.ContainsKey(ResType.钢铁)) ? string.Empty : ("+" + SenceInfo.curMap.addRes[ResType.钢铁]));
		}
		else
		{
			this.steelLabel.transform.parent.gameObject.SetActive(false);
		}
		if (NewbieGuidePanel.isEnemyAttck && SenceInfo.curMap.baseRes.ContainsKey(ResType.稀矿) && (UIManager.curState == SenceState.Spy || SenceInfo.CurBattle == null))
		{
			num3++;
			this.rareEarthLabel.transform.parent.gameObject.SetActive(true);
			this.rareEarthLabel.transform.gameObject.SetActive(true);
			this.rareEarthLabel.text = SenceInfo.curMap.baseRes[ResType.稀矿] + ((!SenceInfo.curMap.addRes.ContainsKey(ResType.稀矿)) ? string.Empty : ("+" + SenceInfo.curMap.addRes[ResType.稀矿]));
		}
		else
		{
			this.rareEarthLabel.transform.parent.gameObject.SetActive(false);
		}
		SettlementManager.maxCoin = ((!SenceInfo.curMap.baseRes.ContainsKey(ResType.金币)) ? 0 : SenceInfo.curMap.baseRes[ResType.金币]);
		SettlementManager.maxOil = ((!SenceInfo.curMap.baseRes.ContainsKey(ResType.石油)) ? 0 : SenceInfo.curMap.baseRes[ResType.石油]);
		SettlementManager.maxSteel = ((!SenceInfo.curMap.baseRes.ContainsKey(ResType.钢铁)) ? 0 : SenceInfo.curMap.baseRes[ResType.钢铁]);
		SettlementManager.maxRareth = ((!SenceInfo.curMap.baseRes.ContainsKey(ResType.稀矿)) ? 0 : SenceInfo.curMap.baseRes[ResType.稀矿]);
		int num4 = int.Parse(UnitConst.GetInstance().DesighConfigDic[75].value);
		this.coinNum = (float)num4;
		this.oilNum = (float)num4;
		this.steelNum = (float)num4;
		this.rareEarthNum = (float)num4;
		this.maxCoinNum = SettlementManager.maxCoin;
		this.maxOilNum = SettlementManager.maxOil;
		this.maxSteelNum = SettlementManager.maxSteel;
		this.maxRareEarthNum = SettlementManager.maxRareth;
		if (this.Exp_Ga)
		{
			this.Exp_Label.text = SenceInfo.SpyPlayerInfo.exp.ToString();
			switch (num3)
			{
			case 0:
				this.Exp_Ga.transform.position = this.coinLabel.transform.parent.position;
				break;
			case 1:
				this.Exp_Ga.transform.position = this.oilLabel.transform.parent.position;
				break;
			case 2:
				this.Exp_Ga.transform.position = this.steelLabel.transform.parent.position;
				break;
			case 3:
				this.Exp_Ga.transform.position = this.rareEarthLabel.transform.parent.position;
				break;
			}
			this.BattleItemList = SenceInfo.curMap.ItemList;
			foreach (KeyValuePair<int, int> current in this.BattleItemList)
			{
				GameObject gameObject = NGUITools.AddChild(this.ThisGrid.gameObject, this.ItemPrefab);
				ActivityItemPre component = gameObject.GetComponent<ActivityItemPre>();
				gameObject.transform.localScale = Vector3.one * 0.8f;
				AtlasManage.SetUiItemAtlas(component.icon, UnitConst.GetInstance().ItemConst[current.Key].IconId);
				AtlasManage.SetQuilitySpriteName(component.quality, UnitConst.GetInstance().ItemConst[current.Key].Quality);
				component.count.gameObject.SetActive(false);
				component.name.text = current.Value.ToString();
				ItemTipsShow2 component2 = component.GetComponent<ItemTipsShow2>();
				component2.Index = current.Key;
				component2.Type = 1;
			}
			base.StartCoroutine(this.ThisGrid.RepositionAfterFrame());
		}
		LogManage.LogError(string.Format("最大掠夺总量(来自于服务器)：金币{0} 石油{1} 钢铁{2} 稀矿{3}\r\n 当前敌人的资源量用于计算掠夺(来自于服务器) 金币{4} 石油{5} 钢铁{6} 稀矿{7}\r\n 客户端自己计算的最大掠夺资源量(如果不同 请截图给韩荣 松哥 赵令) 金币{8} 石油{9} 钢铁{10} 稀矿{11}  公式是:（num * canshu3 *服务器资源量)    参数1:{12}  参数2:{13}  参数3:{14} num:{15} ,当前战斗类型(如果当前资源总量为0 战斗类型必须为3):{16}", new object[]
		{
			SettlementManager.maxCoin,
			SettlementManager.maxOil,
			SettlementManager.maxSteel,
			SettlementManager.maxRareth,
			SenceInfo.curMap.curCoin,
			SenceInfo.curMap.curOil,
			SenceInfo.curMap.curSteel,
			SenceInfo.curMap.curRareEarth,
			this.maxCoinNum,
			this.maxOilNum,
			this.maxSteelNum,
			this.maxRareEarthNum,
			this.canshu1,
			this.canshu2,
			this.canshu3,
			num2,
			SenceInfo.SpyPlayerInfo.battleType
		}));
		if (FightPanelManager.inst)
		{
			this.FightingBegin();
		}
	}

	public float GetStrongRoom()
	{
		float num = 0f;
		if (SenceInfo.curMap.EnemyTech.ContainsKey(3))
		{
			num = (float)UnitConst.GetInstance().TechnologyDataConst[new Vector2(3f, (float)SenceInfo.curMap.EnemyTech[3])].Props[Technology.Enum_TechnologyProps.资源保护] * 0.01f;
		}
		if (VipConst.GetVipAddtion(100f, SenceInfo.SpyPlayerInfo.vip, VipConst.Enum_VipRight.资源保护) > 0)
		{
			num = (float)VipConst.GetVipAddtion(num, SenceInfo.SpyPlayerInfo.vip, VipConst.Enum_VipRight.资源保护);
		}
		return 1f - num;
	}

	public float GetCommand()
	{
		float num = (float)(SenceManager.inst.MainBuildingLv - HeroInfo.GetInstance().PlayerCommondLv) * this.canshu1 + this.canshu2;
		if (num > 1f)
		{
			num = 1f;
		}
		if (num < 0.2f)
		{
			num = 0.2f;
		}
		return num;
	}

	public void FightingBegin()
	{
		this.titleLabel.text = LanguageManage.GetTextByKey("已经掠夺的资源", "others");
		this.coinLabel.text = "0";
		this.oilLabel.text = "0";
		this.steelLabel.text = "0";
		this.rareEarthLabel.text = "0";
	}

	public int PluderCoin(float num)
	{
		this.coinNum += num;
		if (this.maxCoinNum == 0)
		{
			return 100;
		}
		if (this.coinNum >= (float)this.maxCoinNum)
		{
			this.coinLabel.text = this.maxCoinNum + "(100%)";
			return 100;
		}
		int num2 = Mathf.Min(100, (int)(this.coinNum / (float)this.maxCoinNum * 100f) + 1);
		this.coinLabel.text = string.Concat(new object[]
		{
			(int)this.coinNum,
			"(",
			num2,
			"%)"
		});
		return num2;
	}

	public int PluderOil(float num)
	{
		this.oilNum += num;
		if (this.maxOilNum == 0)
		{
			return 100;
		}
		if (this.oilNum >= (float)this.maxOilNum)
		{
			this.oilLabel.text = this.maxOilNum + "(100%)";
			return 100;
		}
		int num2 = Mathf.Min(100, (int)(this.oilNum / (float)this.maxOilNum * 100f) + 1);
		this.oilLabel.text = string.Concat(new object[]
		{
			(int)this.oilNum,
			"(",
			num2,
			"%)"
		});
		return num2;
	}

	public int PluderSteel(float num)
	{
		this.steelNum += num;
		if (this.maxSteelNum == 0)
		{
			return 100;
		}
		if (this.steelNum >= (float)this.maxSteelNum)
		{
			this.steelLabel.text = this.maxSteelNum + "(100%)";
			return 100;
		}
		int num2 = Mathf.Min(100, (int)(this.steelNum / (float)this.maxSteelNum * 100f) + 1);
		this.steelLabel.text = string.Concat(new object[]
		{
			(int)this.steelNum,
			"(",
			num2,
			"%)"
		});
		return num2;
	}

	public int PliderRareEarth(float num)
	{
		this.rareEarthNum += num;
		if (this.maxRareEarthNum == 0)
		{
			return 100;
		}
		if (this.rareEarthNum >= (float)this.maxRareEarthNum)
		{
			this.rareEarthLabel.text = this.maxRareEarthNum + "(100%)";
			return 100;
		}
		int num2 = Mathf.Min(100, (int)(this.rareEarthNum / (float)this.maxRareEarthNum * 100f) + 1);
		this.rareEarthLabel.text = string.Concat(new object[]
		{
			(int)this.rareEarthNum,
			"(",
			num2,
			"%)"
		});
		return num2;
	}
}
