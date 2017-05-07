using System;
using UnityEngine;

public class ParticlesShader : MonoBehaviour
{
	public string shaderUrl;

	private Shader shader;

	private Transform tr;

	private void Start()
	{
		UnityEngine.Object.Destroy(this);
	}
}
