using System;

public class NpcAppear : TipBase
{
	public UILabel teamName;

	public UITable table;

	public UILabel desc;

	public override void Open(ReportData thisData)
	{
		base.Open(thisData);
		if (this.teamName)
		{
			this.teamName.text = LanguageManage.GetTextByKey("发现新目标", "others");
		}
		if (this.desc)
		{
			this.desc.text = LanguageManage.GetTextByKey("指挥官，发现新目标，可以攻占该岛屿", "others");
		}
	}
}
