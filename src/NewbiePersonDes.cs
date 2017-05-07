using System;
using UnityEngine;

public class NewbiePersonDes : MonoBehaviour
{
	public GameObject des;

	public TypewriterEffect desType;

	public static bool IsTeXiaoShow;

	private void OnEnable()
	{
		NewbiePersonDes.IsTeXiaoShow = true;
	}

	private void OnDisable()
	{
		NewbiePersonDes.IsTeXiaoShow = false;
	}

	public void DesShow()
	{
		this.des.SetActive(true);
	}
}
