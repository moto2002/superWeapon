using System;
using UnityEngine;

public class PeopleNumUI : MonoBehaviour
{
	public UILabel peopleNumUIlabel;

	public void OnDisable()
	{
		if (ClientMgr.GetNet() != null)
		{
			ClientMgr.GetNet().NetDataHandler.DataChange -= new DataHandler.Data_Change(this.NetDataHandler_DataChange);
		}
	}

	public void OnEnable()
	{
		if (ClientMgr.GetNet() != null)
		{
			ClientMgr.GetNet().NetDataHandler.DataChange += new DataHandler.Data_Change(this.NetDataHandler_DataChange);
		}
		this.SetPeopleNum();
	}

	private void NetDataHandler_DataChange(int opcodeCMD)
	{
		if (opcodeCMD == 10006 || opcodeCMD == 10116 || opcodeCMD == 10090)
		{
			this.SetPeopleNum();
		}
	}

	private void SetPeopleNum()
	{
		this.peopleNumUIlabel.text = string.Format("{0}/{1}", HeroInfo.GetInstance().All_PeopleNum_Occupy, HeroInfo.GetInstance().All_PeopleNum);
	}
}
