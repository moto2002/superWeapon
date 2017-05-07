using System;
using UnityEngine;

public class InfoLife : MonoBehaviour
{
	public UISlider uis;

	public UISprite uisp;

	public UISprite headBg;

	public UILabel lvtext;

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
