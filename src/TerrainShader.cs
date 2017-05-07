using System;
using UnityEngine;

public class TerrainShader : MonoBehaviour
{
	private Shader shader;

	private Transform tr;

	private void Start()
	{
		this.tr = base.transform;
		this.shader = Shader.Find("T4MShaders/T4M 4 Textures for Mobile");
		if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
		{
			this.tr.renderer.material.shader = this.shader;
		}
		UnityEngine.Object.Destroy(this);
	}
}
