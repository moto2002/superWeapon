using System;
using System.Collections.Generic;
using UnityEngine;

public class ArmyIconSet : MonoBehaviour
{
	public UIScrollView iconScrollview;

	public UITable iconTabel;

	public GameObject iconPrefab;

	public void OnEnable()
	{
		this.SetIcon();
	}

	private void SetIcon()
	{
		this.DestoryIconPrefab();
		this.iconTabel.Reposition();
		foreach (KeyValuePair<int, ArmyIconClass> current in UnitConst.GetInstance().armyIcon)
		{
			GameObject prefab = NGUITools.AddChild(this.iconTabel.gameObject, this.iconPrefab);
			prefab.name = current.Value.id.ToString();
			ArmyIconPrefab pre = prefab.GetComponent<ArmyIconPrefab>();
			pre.iconName.spriteName = current.Value.name.ToString();
			UIEventListener.Get(prefab.gameObject).onClick = delegate(GameObject ga)
			{
				CreatArmyPanel.ins.armyIcon.spriteName = pre.iconName.spriteName;
				CreatArmyPanel.ins.armyIcon.name = prefab.name;
				this.gameObject.SetActive(false);
			};
		}
		this.iconTabel.Reposition();
	}

	private void DestoryIconPrefab()
	{
		GameObject gameObject = this.iconTabel.gameObject;
		Transform[] componentsInChildren = gameObject.GetComponentsInChildren<Transform>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Transform transform = componentsInChildren[i];
			if (transform.transform.parent == gameObject.transform)
			{
				transform.transform.parent = this.iconScrollview.transform;
				UnityEngine.Object.Destroy(transform.gameObject);
			}
		}
	}
}
