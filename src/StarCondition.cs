using System;
using System.Collections.Generic;

public class StarCondition
{
	public int id;

	public string name;

	public string description;

	public int conditionId;

	public Dictionary<int, int> contitions = new Dictionary<int, int>();

	private string contitionsInfo;

	public string ContitionsInfo
	{
		get
		{
			if (!string.IsNullOrEmpty(this.contitionsInfo))
			{
				return this.contitionsInfo;
			}
			if (this.conditionId == 20)
			{
				foreach (KeyValuePair<int, int> current in this.contitions)
				{
					this.contitionsInfo += string.Format("摧毁ID为{0}的{1}个建筑  ", current.Key, current.Value);
				}
			}
			else if (this.conditionId == 21)
			{
				foreach (KeyValuePair<int, int> current2 in this.contitions)
				{
					this.contitionsInfo += string.Format("携带ID为{0}，{1}个兵种    ", current2.Key, current2.Value);
				}
			}
			else if (this.conditionId == 22)
			{
				foreach (KeyValuePair<int, int> current3 in this.contitions)
				{
					this.contitionsInfo += string.Format("携带ID为{0}指挥官   ", current3.Key);
				}
			}
			else if (this.conditionId == 23)
			{
				foreach (KeyValuePair<int, int> current4 in this.contitions)
				{
					this.contitionsInfo += string.Format("兵种损失少于{0}  ", current4.Key);
				}
			}
			else if (this.conditionId == 24)
			{
				foreach (KeyValuePair<int, int> current5 in this.contitions)
				{
					this.contitionsInfo += string.Format("在{0}秒内ID为{1}的建筑物  ", current5.Key, current5.Value);
				}
			}
			return this.contitionsInfo;
		}
	}
}
