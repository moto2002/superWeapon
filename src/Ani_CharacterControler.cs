using System;
using System.Collections.Generic;
using UnityEngine;

public class Ani_CharacterControler : MonoBehaviour
{
	public bool animationCanUse = true;

	public Dictionary<string, AnimData> anis = new Dictionary<string, AnimData>();

	public List<GameObject> colorbody = new List<GameObject>();

	public ScrollUV uvbody;

	public Animation bodyAni;

	public Animation headAni;

	public Animation ani2;

	private Transform tr;

	public List<Animation> AllAnimation;

	public ParticleSystem[] AllParticleSystem;

	public UVAnimation[] AllUVAnimation;

	private void Awake()
	{
		this.tr = base.transform;
		this.SetAnimaInfo();
		this.AllParticleSystem = base.GetComponentsInChildren<ParticleSystem>(true);
		this.AllUVAnimation = base.GetComponentsInChildren<UVAnimation>(true);
		Character component = base.GetComponent<Character>();
		if (component)
		{
			SenceManager.ElectricityEnum mapElectricity = component.MapElectricity;
			if (mapElectricity == SenceManager.ElectricityEnum.电力瘫痪)
			{
				this.CloseAllAnimation();
			}
		}
		this.FindUVInChildren(this.tr, "UV");
	}

	public void SetAnimaInfo()
	{
		if (this.AllAnimation == null)
		{
			this.AllAnimation = new List<Animation>();
		}
		else
		{
			this.AllAnimation.Clear();
		}
		this.AllAnimation.AddRange(base.GetComponentsInChildren<Animation>(true));
		Character component = base.GetComponent<Character>();
		if (component)
		{
			SenceManager.ElectricityEnum mapElectricity = component.MapElectricity;
			if (mapElectricity != SenceManager.ElectricityEnum.电力瘫痪)
			{
				for (int i = 0; i < this.AllAnimation.Count; i++)
				{
					if (this.AllAnimation[i] && !this.AllAnimation[i].enabled)
					{
						this.AllAnimation[i].enabled = true;
					}
				}
			}
			else
			{
				this.CloseAllAnimation();
			}
		}
	}

	public void CloseAllAnimation()
	{
		for (int i = 0; i < this.AllAnimation.Count; i++)
		{
			if (this.AllAnimation[i])
			{
				this.AllAnimation[i].enabled = false;
			}
		}
		for (int j = 0; j < this.AllParticleSystem.Length; j++)
		{
			if (this.AllParticleSystem[j])
			{
				this.AllParticleSystem[j].Pause();
			}
		}
		for (int k = 0; k < this.AllUVAnimation.Length; k++)
		{
			if (this.AllUVAnimation[k])
			{
				this.AllUVAnimation[k].enabled = false;
			}
		}
	}

	private void FindUVInChildren(Transform trans, string name)
	{
		if (trans.name == name)
		{
			ScrollUV scrollUV = trans.gameObject.AddComponent<ScrollUV>();
			this.uvbody = scrollUV;
			scrollUV.enabled = false;
		}
		else
		{
			for (int i = 0; i < trans.childCount; i++)
			{
				if (this.uvbody == null)
				{
					this.FindUVInChildren(trans.GetChild(i), name);
				}
			}
		}
	}

	private void GetUV()
	{
		foreach (Transform transform in this.tr)
		{
			if (transform.name == "UV")
			{
				ScrollUV scrollUV = transform.gameObject.AddComponent<ScrollUV>();
				this.uvbody = scrollUV;
				scrollUV.enabled = false;
			}
		}
	}

	public bool AnimPlay(string aniName)
	{
		if (aniName == "run" || aniName == "walk")
		{
			if (this.uvbody != null)
			{
				this.uvbody.enabled = true;
			}
		}
		else if (this.uvbody != null)
		{
			this.uvbody.enabled = false;
		}
		bool result = false;
		for (int i = 0; i < this.AllAnimation.Count; i++)
		{
			if (this.AllAnimation[i] != null && this.AllAnimation[i].GetClip(aniName) != null)
			{
				if (!this.AllAnimation[i].enabled)
				{
					this.AllAnimation[i].enabled = true;
				}
				if (this.AllAnimation[i].isPlaying)
				{
					this.AllAnimation[i].CrossFade(aniName);
				}
				else
				{
					this.AllAnimation[i].Stop();
					this.AllAnimation[i].Play(aniName);
				}
				result = true;
			}
		}
		return result;
	}

	public void AnimPlayQuened(string aniName)
	{
		for (int i = 0; i < this.AllAnimation.Count; i++)
		{
			if (this.AllAnimation[i] != null && this.AllAnimation[i].GetClip(aniName) != null)
			{
				if (!this.AllAnimation[i].enabled)
				{
					this.AllAnimation[i].enabled = true;
				}
				this.AllAnimation[i].PlayQueued(aniName);
			}
		}
	}

	public void AnimPlayCrocessQuened(string aniName)
	{
		for (int i = 0; i < this.AllAnimation.Count; i++)
		{
			if (this.AllAnimation[i] != null && this.AllAnimation[i].GetClip(aniName) != null)
			{
				if (!this.AllAnimation[i].enabled)
				{
					this.AllAnimation[i].enabled = true;
				}
				this.AllAnimation[i].CrossFadeQueued(aniName);
			}
		}
	}

	public void StopAllAni()
	{
	}
}
