using System;
using UnityEngine;

[AddComponentMenu("Image Effects/Mobile Bloom V2"), ExecuteInEditMode, RequireComponent(typeof(Camera))]
[Serializable]
public class MobileBloom : MonoBehaviour
{
	public float intensity;

	public float threshhold;

	public float blurWidth;

	public bool extraBlurry;

	public Material bloomMaterial;

	private bool supported;

	private RenderTexture tempRtA;

	private RenderTexture tempRtB;

	public MobileBloom()
	{
		this.intensity = 0.7f;
		this.threshhold = 0.75f;
		this.blurWidth = 1f;
	}

	public override bool Supported()
	{
		bool arg_45_0;
		if (this.supported)
		{
			arg_45_0 = true;
		}
		else
		{
			bool arg_23_0;
			if (arg_23_0 = SystemInfo.supportsImageEffects)
			{
				arg_23_0 = SystemInfo.supportsRenderTextures;
			}
			bool arg_3A_1;
			if (arg_3A_1 = arg_23_0)
			{
				arg_3A_1 = this.bloomMaterial.shader.isSupported;
			}
			this.supported = arg_3A_1;
			arg_45_0 = this.supported;
		}
		return arg_45_0;
	}

	public override void CreateBuffers()
	{
		if (!this.tempRtA)
		{
			this.tempRtA = new RenderTexture(Screen.width / 4, Screen.height / 4, 0);
			this.tempRtA.hideFlags = HideFlags.DontSave;
		}
		if (!this.tempRtB)
		{
			this.tempRtB = new RenderTexture(Screen.width / 4, Screen.height / 4, 0);
			this.tempRtB.hideFlags = HideFlags.DontSave;
		}
	}

	public override void OnDisable()
	{
		if (this.tempRtA)
		{
			UnityEngine.Object.DestroyImmediate(this.tempRtA);
			this.tempRtA = null;
		}
		if (this.tempRtB)
		{
			UnityEngine.Object.DestroyImmediate(this.tempRtB);
			this.tempRtB = null;
		}
	}

	public override bool EarlyOutIfNotSupported(RenderTexture source, RenderTexture destination)
	{
		bool arg_20_0;
		if (!this.Supported())
		{
			this.enabled = false;
			Graphics.Blit(source, destination);
			arg_20_0 = true;
		}
		else
		{
			arg_20_0 = false;
		}
		return arg_20_0;
	}

	public override void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		this.CreateBuffers();
		if (!this.EarlyOutIfNotSupported(source, destination))
		{
			this.bloomMaterial.SetVector("_Parameter", new Vector4((float)0, (float)0, this.threshhold, this.intensity / (1f - this.threshhold)));
			float num = 1f / ((float)source.width * 1f);
			float num2 = 1f / ((float)source.height * 1f);
			this.bloomMaterial.SetVector("_OffsetsA", new Vector4(1.5f * num, 1.5f * num2, -1.5f * num, 1.5f * num2));
			this.bloomMaterial.SetVector("_OffsetsB", new Vector4(-1.5f * num, -1.5f * num2, 1.5f * num, -1.5f * num2));
			Graphics.Blit(source, this.tempRtB, this.bloomMaterial, 1);
			num *= 4f * this.blurWidth;
			num2 *= 4f * this.blurWidth;
			this.bloomMaterial.SetVector("_OffsetsA", new Vector4(1.5f * num, (float)0, -1.5f * num, (float)0));
			this.bloomMaterial.SetVector("_OffsetsB", new Vector4(0.5f * num, (float)0, -0.5f * num, (float)0));
			Graphics.Blit(this.tempRtB, this.tempRtA, this.bloomMaterial, 2);
			this.bloomMaterial.SetVector("_OffsetsA", new Vector4((float)0, 1.5f * num2, (float)0, -1.5f * num2));
			this.bloomMaterial.SetVector("_OffsetsB", new Vector4((float)0, 0.5f * num2, (float)0, -0.5f * num2));
			Graphics.Blit(this.tempRtA, this.tempRtB, this.bloomMaterial, 2);
			if (this.extraBlurry)
			{
				this.bloomMaterial.SetVector("_OffsetsA", new Vector4(1.5f * num, (float)0, -1.5f * num, (float)0));
				this.bloomMaterial.SetVector("_OffsetsB", new Vector4(0.5f * num, (float)0, -0.5f * num, (float)0));
				Graphics.Blit(this.tempRtB, this.tempRtA, this.bloomMaterial, 2);
				this.bloomMaterial.SetVector("_OffsetsA", new Vector4((float)0, 1.5f * num2, (float)0, -1.5f * num2));
				this.bloomMaterial.SetVector("_OffsetsB", new Vector4((float)0, 0.5f * num2, (float)0, -0.5f * num2));
				Graphics.Blit(this.tempRtA, this.tempRtB, this.bloomMaterial, 2);
			}
			this.bloomMaterial.SetTexture("_Bloom", this.tempRtB);
			Graphics.Blit(source, destination, this.bloomMaterial, 0);
		}
	}

	public override void Main()
	{
	}
}
