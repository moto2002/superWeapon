using System;
using UnityEngine;

public class AideDateUI : MonoBehaviour
{
	public UISprite selectUISprite;

	public UISprite lvSprite;

	public UISprite needItemIcon;

	public UILabel needItemCount;

	public UILabel comPoundTime;

	public UITexture peopleTexture;

	[HideInInspector]
	public Aide curAide;

	private void Start()
	{
	}

	private void Awake()
	{
		this.ShowAideD();
	}

	public void ShowAideD()
	{
		this.selectUISprite = base.transform.FindChild("QualitySprite").GetComponent<UISprite>();
		this.needItemIcon = base.transform.FindChild("bottom/count/Sprite").GetComponent<UISprite>();
		this.needItemCount = base.transform.FindChild("bottom/count").GetComponent<UILabel>();
		this.comPoundTime = base.transform.FindChild("bottom/Time").GetComponent<UILabel>();
		this.peopleTexture = base.transform.FindChild("people").GetComponent<UITexture>();
	}

	private void Update()
	{
	}

	private void OnClick()
	{
		if (!HeroInfo.GetInstance().PlayerItemInfo.ContainsKey(this.curAide.needItemId) || HeroInfo.GetInstance().PlayerItemInfo[this.curAide.needItemId] < this.curAide.needItemNum)
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("道具不足~~", "item"), HUDTextTool.TextUITypeEnum.Num5);
		}
	}
}
