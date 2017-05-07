using System;
using UnityEngine;

public class TestAIRun : MonoBehaviour
{
	private void Start()
	{
	}

	private void OnGUI()
	{
		if (GUI.Button(new Rect(0f, 0f, 100f, 100f), "Create Tank"))
		{
			Debug.LogError(Mathf.Sin(0.5235988f));
		}
	}
}
