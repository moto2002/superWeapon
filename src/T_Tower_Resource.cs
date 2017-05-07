using System;
using UnityEngine;

[RequireComponent(typeof(T_Tower))]
public class T_Tower_Resource : MonoBehaviour
{
	private T_Tower t_tower;

	private T_Tower Tower
	{
		get
		{
			if (this.t_tower)
			{
				return this.t_tower;
			}
			this.t_tower = base.GetComponent<T_Tower>();
			return this.t_tower;
		}
	}

	public void Click()
	{
	}
}
