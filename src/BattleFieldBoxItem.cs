using msg;
using System;
using System.Linq;
using UnityEngine;

public class BattleFieldBoxItem : MonoBehaviour
{
	public int Quilaity;

	public void OnMouseUp()
	{
		if (Camera_FingerManager.inst.IsNotCanDragOrClickTerrInIsland())
		{
			return;
		}
		BattleFieldBoxPanel.OpenBoxQuality = this.Quilaity;
		if (BattleFieldBox.battleFieldBoxes.ContainsKey(this.Quilaity) && BattleFieldBox.battleFieldBoxes[this.Quilaity].Count > 0)
		{
			KVStruct kVStruct = (from a in BattleFieldBox.battleFieldBoxes[this.Quilaity]
			orderby BattleFieldBox.BattleFieldBox_PlannerData[(int)a.key].level descending
			select a).FirstOrDefault<KVStruct>();
			if (kVStruct != null)
			{
				int boxId = (int)kVStruct.key;
				BattleFieldBoxPanel.ShowBattleFieldBox(boxId);
			}
		}
	}
}
