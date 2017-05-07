using System;
using UnityEngine;

public class EnemyScoreItem : MonoBehaviour
{
	public UILabel num;

	public UISprite icon;

	public string _name;

	public UILabel des;

	private void Awake()
	{
		this.des.gameObject.SetActive(false);
	}

	public void ShowThisDes()
	{
		this.des.gameObject.SetActive(true);
		this.des.text = this._name;
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0) && this.des.gameObject.activeSelf)
		{
			this.des.gameObject.SetActive(false);
		}
	}
}
