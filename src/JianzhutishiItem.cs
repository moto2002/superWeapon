using System;
using UnityEngine;

public class JianzhutishiItem : MonoBehaviour
{
	public UILabel name_Client;

	public UILabel des;

	public UISprite icon;

	private void Awake()
	{
		this.name_Client = base.transform.FindChild("Name").GetComponent<UILabel>();
		this.des = base.transform.FindChild("officename_lable").GetComponent<UILabel>();
		this.icon = base.transform.FindChild("Sprite").GetComponent<UISprite>();
	}
}
