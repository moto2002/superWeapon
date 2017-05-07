using System;
using UnityEngine;

public class ModelByBundle : IMonoBehaviour
{
	[SerializeField]
	protected bool isActive = true;

	public bool IsActive
	{
		get
		{
			return this.isActive;
		}
		set
		{
			this.isActive = value;
		}
	}

	public virtual void SetActive(bool isActiveSelf)
	{
	}

	public virtual void DesInsInPool()
	{
	}
}
