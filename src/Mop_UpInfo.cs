using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Mop_UpInfo : FuncUIPanel
{
	public static int Battlefield;

	public static int BattlefieNum;

	public static List<SweepReward> RewardData = new List<SweepReward>();

	private GameObject[] itemArray;

	public static int Num;

	private Mop_upItem ItemInfo;

	private Transform[] itemTransfrom;

	public GameObject ItemGrid;

	private UIGrid ItemClear;

	private ResTips restips;

	public GameObject closeBtn;

	public override void Awake()
	{
		this.ItemClear = this.ItemGrid.GetComponent<UIGrid>();
		this.ItemInfo = Mop_upItem.inst;
	}

	private new void OnEnable()
	{
		this.RefreshData();
		base.OnEnable();
	}

	private new void OnDisable()
	{
		base.OnDisable();
	}

	public void RefreshData()
	{
		this.closeBtn.SetActive(false);
		this.ItemClear.ClearChild();
		this.OnBattleOne();
		this.ItemClear.Reposition();
	}

	private void DataHandler_DataChange(int opcodeCMD)
	{
	}

	public void OnBattleOne()
	{
		base.StartCoroutine(this.HadePanel());
	}

	[DebuggerHidden]
	private IEnumerator HadePanel()
	{
		Mop_UpInfo.<HadePanel>c__Iterator82 <HadePanel>c__Iterator = new Mop_UpInfo.<HadePanel>c__Iterator82();
		<HadePanel>c__Iterator.<>f__this = this;
		return <HadePanel>c__Iterator;
	}

	public void OnClick(GameObject o)
	{
		this.restips = HUDTextTool.inst.restip;
		this.restips.PlayTextTip(o.gameObject.transform, UnitConst.GetInstance().ItemConst[int.Parse(o.name)].Name);
	}

	public void OnClickEquip(GameObject o)
	{
		this.restips = HUDTextTool.inst.restip;
		this.restips.PlayTextTip(o.gameObject.transform, UnitConst.GetInstance().equipList[int.Parse(o.name)].name);
	}

	public void OnRestips()
	{
		if (this.restips != null)
		{
			this.restips.gameObject.SetActive(false);
		}
	}
}
