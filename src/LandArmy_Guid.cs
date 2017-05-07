using System;
using UnityEngine;

public class LandArmy_Guid : MonoBehaviour
{
	public void OnMouseUp()
	{
		if (FightPanelManager.inst != null && FightPanelManager.inst.curSelectUIItem != null)
		{
			if (FightPanelManager.inst.curSelectUIItem.GetComponent<SoldierUIITem>())
			{
				if (!Camera_FingerManager.inst.IsCanSendSolider(base.transform.position))
				{
					HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("此处不可放置兵种", "others"), HUDTextTool.TextUITypeEnum.Num5);
					return;
				}
				SoldierUIITem soldierUIITem = FightPanelManager.inst.curSelectUIItem as SoldierUIITem;
				if (soldierUIITem.soliderType == SoliderType.Normal)
				{
					armyData armyToInBattle = soldierUIITem.GetArmyToInBattle();
					if (soldierUIITem.soliderNum <= 0)
					{
						FightPanelManager.inst.CurSelectUIItem = null;
					}
					Container.CreateContainer(base.transform.position, armyToInBattle.buildingID, SenceManager.inst.SoldierId, armyToInBattle.index, armyToInBattle.lv, true, false, CommanderType.None);
					HUDTextTool.inst.NextLuaCall("上兵", new object[0]);
				}
				else
				{
					soldierUIITem.soliderNum = 0;
					soldierUIITem.ResetArmyNum();
					if (soldierUIITem.soliderNum <= 0)
					{
						FightPanelManager.inst.CurSelectUIItem = null;
					}
					if (NewbieGuidePanel.curGuideIndex == -1)
					{
						Container.CreateCommander(base.transform.position, 0L, 1L, HeroInfo.GetInstance().gameStart.soliderIndex, HeroInfo.GetInstance().gameStart.soliderlV, 1, 1, true);
					}
					else
					{
						Container.CreateCommander(base.transform.position, HeroInfo.GetInstance().PlayerCommandoBuildingID, HeroInfo.GetInstance().Commando_Fight.id, HeroInfo.GetInstance().Commando_Fight.index, HeroInfo.GetInstance().Commando_Fight.level, HeroInfo.GetInstance().Commando_Fight.star, HeroInfo.GetInstance().Commando_Fight.skillLevel, true);
					}
					HUDTextTool.inst.NextLuaCall("上兵", new object[0]);
				}
			}
			else if (FightPanelManager.inst.curSelectUIItem.GetComponent<SkillUIITem>())
			{
				FightPanelManager.inst.curSelectUIItem.GetComponent<SkillUIITem>().True_UseCard(base.transform.position);
				FightPanelManager.inst.curSelectUIItem = null;
			}
		}
	}

	private void LongPress()
	{
		if (FightPanelManager.inst != null && FightPanelManager.inst.curSelectUIItem != null && FightPanelManager.inst.curSelectUIItem.GetComponent<SoldierUIITem>())
		{
			if (!Camera_FingerManager.inst.IsCanSendSolider(base.transform.position))
			{
				HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("此处不可放置兵种", "others"), HUDTextTool.TextUITypeEnum.Num5);
				return;
			}
			SoldierUIITem soldierUIITem = FightPanelManager.inst.curSelectUIItem as SoldierUIITem;
			if (soldierUIITem.soliderType == SoliderType.Normal)
			{
				armyData armyToInBattle = soldierUIITem.GetArmyToInBattle();
				if (soldierUIITem.soliderNum <= 0)
				{
					FightPanelManager.inst.CurSelectUIItem = null;
				}
				Container.CreateContainer(base.transform.position, armyToInBattle.buildingID, SenceManager.inst.SoldierId, armyToInBattle.index, armyToInBattle.lv, true, false, CommanderType.None);
				HUDTextTool.inst.NextLuaCall("上兵", new object[0]);
			}
			else if (NewbieGuidePanel.curGuideIndex != -1)
			{
				soldierUIITem.soliderNum = 0;
				soldierUIITem.ResetArmyNum();
				if (soldierUIITem.soliderNum <= 0)
				{
					FightPanelManager.inst.CurSelectUIItem = null;
				}
				Container.CreateCommander(base.transform.position, HeroInfo.GetInstance().PlayerCommandoBuildingID, HeroInfo.GetInstance().Commando_Fight.id, HeroInfo.GetInstance().Commando_Fight.index, HeroInfo.GetInstance().Commando_Fight.level, HeroInfo.GetInstance().Commando_Fight.star, HeroInfo.GetInstance().Commando_Fight.skillLevel, true);
			}
		}
	}
}
