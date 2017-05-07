using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class buildingItem : MonoBehaviour
{
	public UILabel nameLabel;

	public UIGrid resGrid;

	public GameObject oilLabel;

	public GameObject steelLabel;

	public GameObject rareEarthLabel;

	public UILabel timerLabel;

	public UILabel buildedLabel;

	public UILabel lockText;

	public UITexture bacImg;

	public GameObject lockSp;

	public GameObject lockLabel;

	public GameObject buildlabel;

	public Transform itemBuildingRoot;

	public UILabel lockinfo;

	public GameObject infobtn;

	public GameObject notionShow;

	public string BodyId;

	public int battleid;

	public int Index;

	public bool isNoticon;

	public GameObject LeftEffet;

	public GameObject RightEffect;

	public GameObject LightEffect;

	public Body_Model Building;

	[HideInInspector]
	public int buildingId;

	public bool isCanBuilidMore;

	public bool isMaxNum;

	private List<NewBuildingInfo> allBuildingStore;

	public int sortNum;

	[HideInInspector]
	public int itemType;

	[HideInInspector]
	public Transform tr;

	public NewBuildingInfo buildingConstInfo;

	private Tween tween;

	private void Awake()
	{
		this.tr = base.transform;
	}

	private void Start()
	{
	}

	public void OnSetBuilding()
	{
		Transform[] componentsInChildren = this.Building.GetComponentsInChildren<Transform>();
		if (this.buildingConstInfo.TowerQuaternion.Length > 1)
		{
			this.Building.tr.localRotation = Quaternion.Euler(new Vector3((float)this.buildingConstInfo.TowerQuaternion[0], (float)this.buildingConstInfo.TowerQuaternion[1], (float)this.buildingConstInfo.TowerQuaternion[2]));
		}
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].gameObject.layer = 5;
		}
		this.Building.tr.localScale = new Vector3(this.buildingConstInfo.TowerSize * 100f, this.buildingConstInfo.TowerSize * 100f, this.buildingConstInfo.TowerSize * 100f);
		if (this.Building.GetComponentsInChildren<ParticleSystem>() != null)
		{
			ParticleSystem[] componentsInChildren2 = this.Building.GetComponentsInChildren<ParticleSystem>();
			if (this.buildingConstInfo.particleSizeArr != null)
			{
				int num = (componentsInChildren2.Length <= this.buildingConstInfo.particleSizeArr.Length) ? componentsInChildren2.Length : this.buildingConstInfo.particleSizeArr.Length;
				for (int j = 0; j < num; j++)
				{
					componentsInChildren2[j].startSize.GetType();
					componentsInChildren2[j].startSize = this.buildingConstInfo.particleSizeArr[j];
				}
			}
			else
			{
				for (int k = 0; k < componentsInChildren2.Length; k++)
				{
					componentsInChildren2[k].gameObject.SetActive(false);
				}
			}
		}
	}

	public void Ani(float width, int i)
	{
		if (this.tween != null)
		{
			this.tween.Kill(false);
		}
		this.tr.localPosition = new Vector3(width * (float)i, 600f, 0f);
		this.tween = this.tr.DOLocalMoveY(0f, 0.16f, false).SetDelay(0.05f * (float)i).OnComplete(delegate
		{
			this.itemBuildingRoot.DOScale(Vector3.one * 1.2f, 0.18f).OnComplete(delegate
			{
				this.itemBuildingRoot.DOScale(Vector3.one, 0.08f);
			});
		});
	}

	public void SetEffectActive(bool active)
	{
		if (this.LeftEffet && this.LeftEffet.activeSelf != active)
		{
			this.LeftEffet.SetActive(active);
		}
		if (this.RightEffect && this.RightEffect.activeSelf != active)
		{
			this.RightEffect.SetActive(active);
		}
		if (this.LightEffect && this.LightEffect.activeSelf != active)
		{
			this.LightEffect.SetActive(active);
		}
	}

	public void SetTexture(UITexture texture, string tesName, string path)
	{
		texture.mainTexture = (Resources.Load(path + tesName) as Texture);
	}
}
