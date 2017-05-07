using System;
using UnityEngine;

public abstract class IMonoBehaviour : MonoBehaviour
{
	[HideInInspector]
	public Transform tr;

	[HideInInspector]
	public GameObject ga;

	public virtual void Awake()
	{
		this.tr = base.transform;
		this.ga = base.gameObject;
	}
}
