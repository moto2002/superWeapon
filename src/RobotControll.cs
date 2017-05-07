using System;
using UnityEngine;

public class RobotControll : MonoBehaviour
{
	private string text = "Run";

	private string textFire = "Start Fire";

	public Color teamColor = Color.green;

	public Transform gunFire1;

	public Transform gunFire2;

	public AnimationClip chasisIdle;

	public AnimationClip bodyIdle;

	public AnimationClip chasisRun;

	public AnimationClip bodyRun;

	public GameObject ChildToControll;

	private void Start()
	{
		this.SetTeamColor(base.transform);
		this.RUN();
	}

	private void SetTeamColor(Transform trans)
	{
		if (trans.renderer != null)
		{
			trans.renderer.material.SetColor("_DyeColor", this.teamColor);
		}
		for (int i = 0; i < trans.transform.childCount; i++)
		{
			if (trans.GetChild(i).renderer != null)
			{
				trans.GetChild(i).renderer.material.SetColor("_DyeColor", this.teamColor);
			}
			if (trans.GetChild(i).childCount > 0)
			{
				this.SetTeamColor(trans.GetChild(i));
			}
		}
	}

	private void RUN()
	{
		if (this.chasisRun != null)
		{
			base.animation.Play(this.chasisIdle.name);
		}
		else
		{
			base.animation.Stop();
		}
		if (this.ChildToControll && this.ChildToControll.animation)
		{
			if (this.bodyIdle != null)
			{
				this.ChildToControll.animation.Play(this.bodyIdle.name);
			}
			else
			{
				this.ChildToControll.animation.Stop();
			}
		}
	}
}
