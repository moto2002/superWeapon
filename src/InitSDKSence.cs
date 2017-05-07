using System;
using UnityEngine;

public class InitSDKSence : MonoBehaviour
{
	private void Awake()
	{
	}

	public void Load()
	{
		Application.LoadLevel(1);
	}
}
