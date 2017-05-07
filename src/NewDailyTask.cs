using DicForUnity;
using System;
using System.Collections.Generic;
using UnityEngine;

public class NewDailyTask : MonoBehaviour
{
	public int id;

	public GameObject Btn;

	public GameObject lineTaskGa;

	public GameObject dailyTaskGa;

	public UILabel nameLabel;

	public UILabel lineDescriptionUILabel;

	public UILabel dailyDescriptionUILabel;

	public UISprite nameLabel_BMP;

	public UITable resTable;

	public LangeuageLabel btnSkipLabel;

	public DailyTask taskPlanning;

	private void Awake()
	{
	}

	public void IsCanReceive(bool isCan)
	{
		if (isCan)
		{
			this.btnSkipLabel.gameObject.SetActive(false);
			this.Btn.SetActive(true);
		}
		else
		{
			if (this.taskPlanning.type == 0)
			{
				this.btnSkipLabel.gameObject.SetActive(false);
			}
			else
			{
				this.btnSkipLabel.gameObject.SetActive(true);
			}
			this.Btn.SetActive(false);
		}
	}

	public void InitData()
	{
		this.IsCanReceive(this.taskPlanning.isCanRecieved);
		this.Btn.name = this.taskPlanning.id.ToString();
		this.btnSkipLabel.gameObject.name = this.taskPlanning.id.ToString();
		if (this.taskPlanning.type == 1)
		{
			this.dailyTaskGa.SetActive(true);
			this.lineTaskGa.SetActive(false);
			this.dailyDescriptionUILabel.text = LanguageManage.GetTextByKey(this.taskPlanning.description, "Task");
			UILabel expr_9B = this.dailyDescriptionUILabel;
			expr_9B.text += string.Format(" ({1}/{0})", this.taskPlanning.step, this.taskPlanning.StepRecord);
		}
		else
		{
			this.lineTaskGa.SetActive(true);
			this.dailyTaskGa.SetActive(false);
			if (this.taskPlanning.type == 0)
			{
				this.nameLabel_BMP.spriteName = "主线任务";
			}
			else if (this.taskPlanning.type == 2)
			{
				this.nameLabel_BMP.spriteName = "支线任务";
			}
			this.lineDescriptionUILabel.text = LanguageManage.GetTextByKey(this.taskPlanning.description, "Task");
		}
		List<int> list = new List<int>();
		List<ResType> list2 = new List<ResType>();
		DicForU.GetValues<ResType, int>(this.taskPlanning.rewardRes, list);
		DicForU.GetKeys<ResType, int>(this.taskPlanning.rewardRes, list2);
		for (int i = 0; i < list.Count; i++)
		{
			GameObject gameObject = GameTools.CreateChildrenInTable(this.resTable, NewTaskPanelManager.ins.resPrefab, list2[i].ToString());
			ResourcesPrefab compentIfNoAddOne = GameTools.GetCompentIfNoAddOne<ResourcesPrefab>(gameObject);
			AtlasManage.SetResSpriteName(compentIfNoAddOne.resIcon, list2[i]);
			compentIfNoAddOne.recNum.text = list[i].ToString();
			ItemTipsShow2 itemTipsShow = gameObject.AddComponent<ItemTipsShow2>();
			itemTipsShow.Index = (int)list2[i];
			itemTipsShow.Type = 2;
			itemTipsShow.JianTouPostion = 4;
		}
		DicForU.GetKeys<int, int>(this.taskPlanning.rewardItems, list);
		for (int j = 0; j < list.Count; j++)
		{
			GameObject gameObject2 = GameTools.CreateChildrenInTable(this.resTable, NewTaskPanelManager.ins.itemPrefab, UnitConst.GetInstance().ItemConst[list[j]].Name);
			TaskItemPre compentIfNoAddOne2 = GameTools.GetCompentIfNoAddOne<TaskItemPre>(gameObject2);
			ItemTipsShow2 itemTipsShow2 = gameObject2.AddComponent<ItemTipsShow2>();
			itemTipsShow2.Index = list[j];
			itemTipsShow2.Type = 1;
			itemTipsShow2.JianTouPostion = 4;
			AtlasManage.SetUiItemAtlas(compentIfNoAddOne2.itemIcon, UnitConst.GetInstance().ItemConst[list[j]].IconId);
			compentIfNoAddOne2.num.text = this.taskPlanning.rewardItems[list[j]].ToString();
		}
		DicForU.GetKeys<int, int>(this.taskPlanning.skillAward, list);
		for (int k = 0; k < list.Count; k++)
		{
			GameObject gameObject3 = GameTools.CreateChildrenInTable(this.resTable, NewTaskPanelManager.ins.skillPrefab, list[k].ToString());
			TaskItemPre component = gameObject3.GetComponent<TaskItemPre>();
			AtlasManage.SetSkillSpritName(component.itemIcon, UnitConst.GetInstance().skillList[list[k]].Ficon);
			AtlasManage.SetQuilitySpriteName(component.quality, UnitConst.GetInstance().skillList[list[k]].skillQuality);
			component.num.text = this.taskPlanning.skillAward[list[k]].ToString();
			ItemTipsShow2 itemTipsShow3 = gameObject3.AddComponent<ItemTipsShow2>();
			itemTipsShow3.Index = list[k];
			itemTipsShow3.Type = 3;
			itemTipsShow3.JianTouPostion = 4;
		}
		if (this.taskPlanning.rewardNum > 0)
		{
			GameObject gameObject4 = GameTools.CreateChildrenInTable(this.resTable, NewTaskPanelManager.ins.resPrefab, "7");
			ResourcesPrefab compentIfNoAddOne3 = GameTools.GetCompentIfNoAddOne<ResourcesPrefab>(gameObject4);
			AtlasManage.SetResSpriteName(compentIfNoAddOne3.resIcon, ResType.钻石);
			compentIfNoAddOne3.recNum.text = this.taskPlanning.rewardNum.ToString();
			ItemTipsShow2 itemTipsShow4 = gameObject4.AddComponent<ItemTipsShow2>();
			itemTipsShow4.Index = 7;
			itemTipsShow4.Type = 2;
			itemTipsShow4.JianTouPostion = 4;
		}
		this.resTable.Reposition();
	}

	public void RefreshData()
	{
		this.IsCanReceive(this.taskPlanning.isCanRecieved);
	}
}
