using System;
using UnityEngine;

public class InfoPrograme : MonoBehaviour
{
	public UISlider uis;

	private GameObject tr;

	private void Awake()
	{
		this.tr = base.gameObject;
	}

	public void Show(bool s)
	{
		this.tr.SetActive(s);
	}
}
