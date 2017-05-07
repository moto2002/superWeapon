using System;
using UnityEngine;

public class CameraZhedang : MonoBehaviour
{
	public static CameraZhedang inst;

	public GameObject uiInUseBox;

	public void OnDestroy()
	{
		CameraZhedang.inst = null;
	}

	private void Awake()
	{
		CameraZhedang.inst = this;
	}
}
