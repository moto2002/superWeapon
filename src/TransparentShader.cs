using System;
using UnityEngine;

public class TransparentShader : MonoBehaviour
{
	private Shader shader;

	private Transform tr;

	private void Start()
	{
		this.tr = base.transform;
		this.shader = Shader.Find("Transparent/Cutout/Soft Edge Unlit");
		if ((Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor) && this.tr.GetComponent<Renderer>() != null)
		{
			this.tr.renderer.material.shader = this.shader;
		}
		UnityEngine.Object.Destroy(this);
	}
}
