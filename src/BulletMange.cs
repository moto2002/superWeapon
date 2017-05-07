using System;
using System.Collections.Generic;
using UnityEngine;

public class BulletMange : MonoBehaviour
{
	public static BulletMange _instance;

	private List<GameObject> bulletList = new List<GameObject>();

	public void OnDestroy()
	{
		BulletMange._instance = null;
	}

	private void Awake()
	{
		BulletMange._instance = this;
	}

	public void AddBullet(GameObject bullet)
	{
		this.bulletList.Add(bullet);
	}

	public GameObject GetBullet(string name)
	{
		GameObject result = null;
		for (int i = 0; i < this.bulletList.Count; i++)
		{
			if (this.bulletList[i].name == name && !this.bulletList[i].activeSelf)
			{
				result = this.bulletList[i];
				break;
			}
		}
		return result;
	}

	public void RemoveBullet()
	{
		for (int i = 0; i < this.bulletList.Count; i++)
		{
			UnityEngine.Object.Destroy(this.bulletList[i]);
		}
		this.bulletList.Clear();
	}
}
