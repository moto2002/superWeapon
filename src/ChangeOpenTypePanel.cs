using System;
using UnityEngine;

public class ChangeOpenTypePanel : MonoBehaviour
{
	public GameObject btnSure;

	public GameObject btnCancle;

	public GameObject back;

	public GameObject popback;

	public UILabel armyType;

	public int id;

	public void Start()
	{
		this.SureChangeType();
		this.BGClick();
		this.CancleClick();
	}

	public void OnEnable()
	{
		this.Init();
	}

	private void Init()
	{
		this.popback.GetComponent<UIPopupList>().items.Clear();
		this.popback.GetComponent<UIPopupList>().items.Add(LanguageManage.GetTextByKey("向所有人开放", "Battle"));
		this.popback.GetComponent<UIPopupList>().items.Add(LanguageManage.GetTextByKey("不向所有人开放", "Battle"));
		EventDelegate.Add(this.popback.GetComponent<UIPopupList>().onChange, new EventDelegate.Callback(this.GetValueChange));
	}

	private void GetValueChange()
	{
		this.armyType.text = this.popback.GetComponent<UIPopupList>().value;
		for (int i = 0; i < this.popback.GetComponent<UIPopupList>().items.Count; i++)
		{
			if (this.armyType.text.Equals(this.popback.GetComponent<UIPopupList>().items[i]))
			{
				this.id = i + 1;
			}
		}
	}

	private void SureChangeType()
	{
		UIEventListener.Get(this.btnSure).onClick = delegate(GameObject ga)
		{
			ArmyGroupHandler.CG_CSModifyLegionInfo(HeroInfo.GetInstance().playerGroupId, 8, this.id.ToString(), delegate(bool isError)
			{
				if (!isError)
				{
					base.gameObject.SetActive(false);
				}
			});
		};
	}

	private void BGClick()
	{
		UIEventListener.Get(this.back).onClick = delegate(GameObject ga)
		{
			base.gameObject.SetActive(false);
		};
	}

	private void CancleClick()
	{
		UIEventListener.Get(this.btnCancle).onClick = delegate(GameObject ga)
		{
			base.gameObject.SetActive(false);
		};
	}
}
