using System;
using UnityEngine;

public class MachineShader : MonoBehaviour
{
	private Shader shader;

	private Transform tr;

	private void Start()
	{
		this.tr = base.transform;
		this.shader = Shader.Find("NMG/MachineShader");
		if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
		{
			this.tr.renderer.material.shader = this.shader;
		}
		UnityEngine.Object.Destroy(this);
	}
}
