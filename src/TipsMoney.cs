using System;
using UnityEngine;

public class TipsMoney : MonoBehaviour
{
	public UILabel money;

	private void Start()
	{
	}

	private void OnEnable()
	{
		this.money.text = string.Empty;
	}
}
