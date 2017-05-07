using System;
using System.Collections.Generic;
using System.Linq;

public class RallyPoint : IMonoBehaviour
{
	public int rallyPointIndex;

	public void OnMouseUp()
	{
		if (UIManager.curState == SenceState.Home)
		{
			if (!Camera_FingerManager.YinDaoDianji && DragMgr.inst.BtnInUse)
			{
				return;
			}
			HUDTextTool.inst.SetText(string.Format("{0}{1}{2}", LanguageManage.GetTextByKey("司令部升级到", "Army"), UnitConst.GetInstance().HomeUpdateOpenSetDataConst.Single((KeyValuePair<int, HomeUpdateOpenSetData> a) => a.Value.rallypoint == this.rallyPointIndex).Key, LanguageManage.GetTextByKey("级解锁此集结点", "Army")), HUDTextTool.TextUITypeEnum.Num5);
		}
	}
}
