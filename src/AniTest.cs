using System;
using System.Collections.Generic;
using UnityEngine;

public class AniTest : MonoBehaviour
{
	public bool animationCanUse = true;

	public Dictionary<string, AnimData> anis = new Dictionary<string, AnimData>();

	private Transform tr;

	public List<Animation> AllAnimation;

	private void Awake()
	{
		this.tr = base.transform;
		this.SetAnimaInfo();
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
	}

	private void OnGUI()
	{
		if (GUI.Button(new Rect(0f, 0f, 100f, 100f), "Play Idle"))
		{
			this.AnimPlay("Idle");
		}
		if (GUI.Button(new Rect(0f, 100f, 100f, 100f), "Play Run"))
		{
			this.AnimPlay("Run");
		}
		if (GUI.Button(new Rect(0f, 200f, 100f, 100f), "Play Attack1"))
		{
			this.AnimPlay("Attack1");
		}
	}

	public bool AnimPlay(string aniName)
	{
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
}
