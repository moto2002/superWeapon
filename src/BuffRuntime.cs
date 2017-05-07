using System;
using System.Collections.Generic;
using UnityEngine;

public class BuffRuntime : MonoBehaviour
{
	public T_TankAbstract myTank;

	public T_Tower myTower;

	public float StarUP_Bufftime;

	private BaseFightInfo fightInfo;

	[SerializeField]
	private Buff.BuffType Buffes;

	private Dictionary<Buff.BuffType, BuffEffect> AllBuff = new Dictionary<Buff.BuffType, BuffEffect>();

	private BuffEffect buffEffect;

	private Shader tank_shader;

	private Material tank_material;

	private bool ContainBuff(int buff)
	{
		return (this.Buffes & (Buff.BuffType)buff) != (Buff.BuffType)0;
	}

	public void AddBuff(Buff.BuffType buff)
	{
		if (this.AllBuff.ContainsKey(buff))
		{
			this.AllBuff[buff].Destory();
			this.AllBuff.Remove(buff);
		}
		this.Buffes |= buff;
	}

	public void RemoveBuff(Buff.BuffType buff)
	{
		if (this.AllBuff.ContainsKey(buff))
		{
			this.AllBuff[buff].Destory();
			this.AllBuff.Remove(buff);
		}
		if (this.ContainBuff((int)buff))
		{
			this.Buffes ^= buff;
		}
	}

	public void AddBuffIndex(int SkillID, Character BuffSender, params int[] buffIndex)
	{
		for (int i = 0; i < buffIndex.Length; i++)
		{
			int num = buffIndex[i];
			if (UnitConst.GetInstance().BuffConst.ContainsKey(num))
			{
				Body_Model body_Model = PoolManage.Ins.GetEffectByName(UnitConst.GetInstance().BuffConst[num].effect, base.transform);
				if (body_Model == null)
				{
					body_Model = GameTools.GetCompentIfNoAddOne<Body_Model>(new GameObject("buffEffect"));
					body_Model.tr.parent = base.transform;
				}
				this.buffEffect = GameTools.GetCompentIfNoAddOne<BuffEffect>(body_Model.ga);
				this.buffEffect.buffSkillID = SkillID;
				this.buffEffect.myTank = this.myTank;
				this.buffEffect.myTower = this.myTower;
				this.buffEffect.buffIndex = num;
				this.buffEffect.buffRuntime = this;
				this.buffEffect.BuffSender = BuffSender;
				this.buffEffect.ApplyBuff();
				this.AddBuff(UnitConst.GetInstance().BuffConst[num].buffType);
				this.AllBuff.Add(UnitConst.GetInstance().BuffConst[num].buffType, this.buffEffect);
			}
		}
	}

	public bool IsCanShoot()
	{
		return (this.Buffes & Buff.BuffType.Halo) == (Buff.BuffType)0 && (this.Buffes & Buff.BuffType.SmokeBomb) == (Buff.BuffType)0;
	}

	public bool IsCanBedShoot()
	{
		return (this.Buffes & Buff.BuffType.SmokeBomb) == (Buff.BuffType)0;
	}

	public void Stealth(GameObject Yan, bool stealth)
	{
		if (this.myTank)
		{
			SkinnedMeshRenderer[] componentsInChildren = this.myTank.GetComponentsInChildren<SkinnedMeshRenderer>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				SkinnedMeshRenderer skinnedMeshRenderer = componentsInChildren[i];
				if (stealth && Yan != null)
				{
					this.tank_shader = skinnedMeshRenderer.transform.renderer.material.shader;
					this.tank_material = skinnedMeshRenderer.transform.renderer.material;
					skinnedMeshRenderer.transform.renderer.material = Yan.transform.GetComponentInChildren<ParticleSystem>().renderer.material;
					skinnedMeshRenderer.transform.renderer.material.shader = this.tank_shader;
					skinnedMeshRenderer.transform.renderer.material.color = Vector4.one * 0.3f;
				}
				else
				{
					skinnedMeshRenderer.transform.renderer.material = this.tank_material;
					skinnedMeshRenderer.transform.renderer.material.color = Vector4.one * 0.5f;
				}
			}
		}
		else if (this.myTower)
		{
			SkinnedMeshRenderer[] componentsInChildren2 = this.myTower.GetComponentsInChildren<SkinnedMeshRenderer>();
			for (int j = 0; j < componentsInChildren2.Length; j++)
			{
				SkinnedMeshRenderer skinnedMeshRenderer2 = componentsInChildren2[j];
				if (skinnedMeshRenderer2.transform.renderer.material.shader.name == "Outline_/OutLineHighLightSelfIllumVirtualLightDir")
				{
					if (stealth && Yan != null)
					{
						this.tank_shader = skinnedMeshRenderer2.transform.renderer.material.shader;
						this.tank_material = skinnedMeshRenderer2.transform.renderer.material;
						skinnedMeshRenderer2.transform.renderer.material = Yan.transform.GetComponentInChildren<ParticleSystem>().renderer.material;
						skinnedMeshRenderer2.transform.renderer.material.shader = this.tank_shader;
						skinnedMeshRenderer2.transform.renderer.material.color = Vector4.one * 0.3f;
					}
					else
					{
						skinnedMeshRenderer2.transform.renderer.material = this.tank_material;
						skinnedMeshRenderer2.transform.renderer.material.color = Vector4.one * 0.5f;
					}
				}
			}
			MeshRenderer[] componentsInChildren3 = this.myTower.GetComponentsInChildren<MeshRenderer>();
			for (int k = 0; k < componentsInChildren3.Length; k++)
			{
				MeshRenderer meshRenderer = componentsInChildren3[k];
				if (meshRenderer.transform.renderer.material.shader.name == "Outline_/OutLineHighLightSelfIllumVirtualLightDir")
				{
					if (stealth && Yan != null)
					{
						this.tank_shader = meshRenderer.transform.renderer.material.shader;
						this.tank_material = meshRenderer.transform.renderer.material;
						meshRenderer.transform.renderer.material = Yan.transform.GetComponentInChildren<ParticleSystem>().renderer.material;
						meshRenderer.transform.renderer.material.shader = this.tank_shader;
						meshRenderer.transform.renderer.material.color = Vector4.one * 0.3f;
					}
					else
					{
						meshRenderer.transform.renderer.material = this.tank_material;
						meshRenderer.transform.renderer.material.color = Vector4.one * 0.5f;
					}
				}
			}
		}
	}
}
