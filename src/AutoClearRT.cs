using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class AutoClearRT : MonoBehaviour
{
	public bool NoClearAfterStart;

	private void Start()
	{
		base.GetComponent<Camera>().clearFlags = CameraClearFlags.Color;
	}

	private void OnPostRender()
	{
		if (!this.NoClearAfterStart)
		{
			base.GetComponent<Camera>().clearFlags = CameraClearFlags.Depth;
		}
	}
}
