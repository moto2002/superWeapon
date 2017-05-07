using System;
using System.Collections.Generic;
using UnityEngine;

public class ChangeArmyIconPanel : MonoBehaviour
{
	public UIScrollView iconScroll;

	public UITable iconTabel;

	public GameObject iconPrefab;

	public GameObject btnSure;

	public UISprite ChooseBMP;

	public UISprite NowChooseIcon;

	public int id = 1;

	private GameObject ChooseIconGa;

	public void OnEnable()
	{
		this.init();
		this.ShowAllIcon();
	}

	private void init()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.ArmyPeopleManager_SureChangeIcon, new EventManager.VoidDelegate(this.SureChangeIcon));
		EventManager.Instance.AddEvent(EventManager.EventType.ArmyPeopleManager_CancleChangeIcon, new EventManager.VoidDelegate(this.CancleChangeIcon));
		EventManager.Instance.AddEvent(EventManager.EventType.ArmyGroupManager_ChooseArmyIcon, new EventManager.VoidDelegate(this.ArmyGroupManager_ChooseArmyIcon));
	}

	private void ArmyGroupManager_ChooseArmyIcon(GameObject ga)
	{
		this.ChooseBMP.transform.parent = ga.transform;
		this.ChooseBMP.transform.localPosition = Vector3.zero;
		this.ChooseBMP.gameObject.GetComponent<UISprite>().enabled = true;
		this.ChooseBMP.gameObject.GetComponent<UISprite>().depth = 65;
		this.NowChooseIcon.spriteName = ga.GetComponent<UISprite>().spriteName;
		this.ChooseIconGa = ga;
	}

	private void ShowAllIcon()
	{
		this.DestoyIocn();
		this.iconTabel.DestoryChildren(true);
		this.iconTabel.Reposition();
		bool flag = true;
		foreach (KeyValuePair<int, ArmyIconClass> current in UnitConst.GetInstance().armyIcon)
		{
			GameObject prefab = NGUITools.AddChild(this.iconTabel.gameObject, this.iconPrefab);
			prefab.name = current.Key.ToString();
			if (HeroInfo.GetInstance().playerGroupId != 0L)
			{
				if (ArmyPeopleManager.ins && current.Value.name.ToString() == ArmyPeopleManager.ins.legionIcon.spriteName)
				{
					this.ChooseBMP.transform.parent = prefab.transform;
					this.ChooseBMP.transform.localPosition = Vector3.zero;
					this.ChooseBMP.gameObject.GetComponent<UISprite>().enabled = true;
					this.ChooseBMP.gameObject.GetComponent<UISprite>().depth = 65;
					this.ChooseIconGa = prefab.gameObject;
					this.NowChooseIcon.spriteName = current.Value.name.ToString();
				}
			}
			else if (flag)
			{
				flag = false;
				this.ChooseBMP.transform.parent = prefab.transform;
				this.ChooseBMP.transform.localPosition = Vector3.zero;
				this.ChooseBMP.gameObject.GetComponent<UISprite>().enabled = true;
				this.ChooseBMP.gameObject.GetComponent<UISprite>().depth = 65;
				this.ChooseIconGa = prefab.gameObject;
				this.NowChooseIcon.spriteName = current.Value.name.ToString();
			}
			ChangeArmyIconPrefab component = prefab.GetComponent<ChangeArmyIconPrefab>();
			component.iconName.spriteName = current.Value.name.ToString();
			UIEventListener.Get(prefab.gameObject).onClick = delegate(GameObject ga)
			{
				this.btnSure.name = prefab.name;
			};
		}
		this.iconTabel.Reposition();
	}

	private void SureChangeIcon(GameObject ga)
	{
		if (HeroInfo.GetInstance().playerGroupId != 0L)
		{
			ArmyGroupHandler.CG_CSModifyLegionInfo(HeroInfo.GetInstance().playerGroupId, 4, this.ChooseIconGa.name, delegate(bool isError)
			{
				if (!isError)
				{
					if (ArmyPeopleManager.ins)
					{
						ArmyPeopleManager.ins.legionIcon.spriteName = this.ChooseIconGa.GetComponent<UISprite>().spriteName;
					}
					base.gameObject.SetActive(false);
				}
			});
		}
		else
		{
			CreatArmyPanel.ins.armyIcon.spriteName = this.ChooseIconGa.GetComponent<UISprite>().spriteName;
			CreatArmyPanel.ins.armyIcon.name = this.ChooseIconGa.name;
			base.gameObject.SetActive(false);
		}
	}

	private void CancleChangeIcon(GameObject ga)
	{
		base.gameObject.SetActive(false);
	}

	private void DestoyIocn()
	{
		GameObject gameObject = this.iconTabel.gameObject;
		Transform[] componentsInChildren = gameObject.GetComponentsInChildren<Transform>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Transform transform = componentsInChildren[i];
			if (gameObject.transform.parent == this.iconTabel.transform)
			{
				gameObject.transform.parent = this.iconScroll.transform;
				UnityEngine.Object.Destroy(transform.gameObject);
			}
		}
	}
}
