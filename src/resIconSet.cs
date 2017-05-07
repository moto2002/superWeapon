using System;
using UnityEngine;

public class resIconSet : MonoBehaviour
{
	[SerializeField]
	private UISprite resIcon;

	[SerializeField]
	private UILabel resNum;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void SetUpdateResIcon(string resType, string _resNum)
	{
		switch (int.Parse(resType))
		{
		case 1:
			this.resNum.text = "金币：" + _resNum;
			break;
		case 2:
			this.resNum.text = "石油：" + _resNum;
			break;
		case 3:
			this.resNum.text = "钢铁：" + _resNum;
			break;
		case 4:
			this.resNum.text = "稀矿：" + _resNum;
			break;
		}
	}
}
