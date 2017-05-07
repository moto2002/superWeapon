using System;
using System.Collections.Generic;
using UnityEngine;

public class TechnologyUpdataTreePoint : MonoBehaviour
{
	public UILabel Level_label;

	public UISprite BG;

	public UISprite Icon;

	public UILabel Name;

	public int PointID;

	public bool UnLock;

	public int MaxLevel;

	private int base_level;

	private bool LockState;

	public string effect_name;

	public float Effect_size;

	public float effect_size;

	public void SetInfo()
	{
		this.PointID = int.Parse(this.BG.name);
		if (this.base_level == 0)
		{
			this.base_level = HeroInfo.GetInstance().PlayerTechnologyInfo[this.PointID];
		}
		for (int i = 0; i < 50; i++)
		{
			if (!UnitConst.GetInstance().TechnologyDataConst.ContainsKey(new Vector2((float)this.PointID, (float)i)))
			{
				break;
			}
			this.MaxLevel = i;
		}
		this.Level_label.text = string.Format("LV{0}", HeroInfo.GetInstance().PlayerTechnologyInfo[this.PointID]);
		if (HeroInfo.GetInstance().PlayerTechnologyInfo[this.PointID] > 0)
		{
			this.UnLock = true;
			if (this.base_level != HeroInfo.GetInstance().PlayerTechnologyInfo[this.PointID])
			{
				this.base_level = HeroInfo.GetInstance().PlayerTechnologyInfo[this.PointID];
				this.ShanGuang();
			}
		}
		else
		{
			this.UnLock = false;
			if (UnitConst.GetInstance().TechnologyDataConst[new Vector2((float)this.PointID, 0f)].needTech.Count == 0)
			{
				this.UnLock = true;
			}
			else
			{
				bool flag = true;
				foreach (KeyValuePair<int, int> current in UnitConst.GetInstance().TechnologyDataConst[new Vector2((float)this.PointID, 0f)].needTech)
				{
					if (!HeroInfo.GetInstance().PlayerTechnologyInfo.ContainsKey(current.Key))
					{
						flag = false;
						break;
					}
					if (HeroInfo.GetInstance().PlayerTechnologyInfo[current.Key] < current.Value)
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					this.UnLock = true;
					if (this.LockState)
					{
						this.ShanGuang();
					}
				}
			}
		}
		this.BG.color = Color.white;
		this.Icon.color = Color.white;
		if (this.UnLock)
		{
			this.BG.ShaderToNormal();
			this.Icon.ShaderToNormal();
			this.Name.color = Color.white;
			this.LockState = false;
		}
		else
		{
			this.BG.ShaderToGray();
			this.Icon.ShaderToGray();
			this.Name.color = Color.gray;
			this.LockState = true;
		}
		this.Icon.spriteName = this.PointID.ToString();
	}

	public void ShanGuang()
	{
		this.effect_size = 0.4f;
		this.effect_name = "peibingwancheng";
		DieBall dieBall = PoolManage.Ins.CreatEffect(this.effect_name, Vector3.zero, Quaternion.identity, base.transform);
		dieBall.tr.position = base.transform.position;
		dieBall.GetComponentInChildren<ParticleSystem>().gameObject.layer = 8;
		dieBall.GetComponentInChildren<ParticleSystem>().startSize = this.effect_size;
		dieBall.LifeTime = 1f;
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
