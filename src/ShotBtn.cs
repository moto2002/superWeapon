using System;
using UnityEngine;

public class ShotBtn : MonoBehaviour
{
	public static ShotBtn inst;

	public Camera cam;

	public UILabel la;

	public void OnDestroy()
	{
		ShotBtn.inst = null;
	}

	private void Start()
	{
		ShotBtn.inst = this;
	}

	private void OnClick()
	{
		LogManage.Log("截屏了--------------------");
		Application.CaptureScreenshot("Screenshot.png");
	}
}
