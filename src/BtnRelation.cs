using System;
using UnityEngine;

public class BtnRelation : MonoBehaviour
{
	public enum BtnType
	{
		Close,
		FriendView,
		ChouRenView,
		Share,
		FuChou,
		ZhenCha
	}

	public BtnRelation.BtnType type;

	private void OnClick()
	{
		switch (this.type)
		{
		case BtnRelation.BtnType.Close:
			RelationPanel.ins.obj.SetActive(false);
			break;
		case BtnRelation.BtnType.FriendView:
			RelationPanel.ins.ShowFriend();
			break;
		case BtnRelation.BtnType.ChouRenView:
			RelationPanel.ins.ShowEnemy();
			break;
		case BtnRelation.BtnType.Share:
			LogManage.Log("Share");
			break;
		case BtnRelation.BtnType.FuChou:
			LogManage.Log("FuChou");
			break;
		case BtnRelation.BtnType.ZhenCha:
			LogManage.Log("ZhenCha");
			break;
		default:
			LogManage.Log("事件不存在啊");
			break;
		}
	}
}
