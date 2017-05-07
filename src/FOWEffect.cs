using System;
using UnityEngine;

[AddComponentMenu("Fog of War/Image Effect"), RequireComponent(typeof(Camera))]
public class FOWEffect : MonoBehaviour
{
	public Shader shader;

	public Color unexploredColor = new Color(0.05f, 0.05f, 0.05f, 1f);

	public Color exploredColor = new Color(0.2f, 0.2f, 0.2f, 1f);

	private FOWSystem mFog;

	private Camera mCam;

	private Matrix4x4 mInverseMVP;

	private Material mMat;

	private void OnEnable()
	{
		this.mCam = base.camera;
		this.mCam.depthTextureMode = DepthTextureMode.Depth;
		if (this.shader == null)
		{
			this.shader = Shader.Find("Image Effects/Fog of War");
		}
	}

	private void OnDisable()
	{
		if (this.mMat)
		{
			UnityEngine.Object.DestroyImmediate(this.mMat);
		}
	}

	private void Start()
	{
		if (!SystemInfo.supportsImageEffects || !this.shader || !this.shader.isSupported)
		{
			base.enabled = false;
		}
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (this.mFog == null)
		{
			this.mFog = FOWSystem.instance;
			if (this.mFog == null)
			{
				this.mFog = (UnityEngine.Object.FindObjectOfType(typeof(FOWSystem)) as FOWSystem);
			}
		}
		if (this.mFog == null || !this.mFog.enabled)
		{
			base.enabled = false;
			return;
		}
		this.mInverseMVP = (this.mCam.projectionMatrix * this.mCam.worldToCameraMatrix).inverse;
		float num = 1f / (float)this.mFog.worldSize;
		Transform transform = this.mFog.transform;
		float num2 = transform.position.x - (float)this.mFog.worldSize * 0.5f;
		float num3 = transform.position.z - (float)this.mFog.worldSize * 0.5f;
		if (this.mMat == null)
		{
			this.mMat = new Material(this.shader);
			this.mMat.hideFlags = HideFlags.HideAndDontSave;
		}
		Vector4 vector = this.mCam.transform.position;
		if (QualitySettings.antiAliasing > 0)
		{
			RuntimePlatform platform = Application.platform;
			if (platform == RuntimePlatform.WindowsEditor || platform == RuntimePlatform.WindowsPlayer || platform == RuntimePlatform.WindowsWebPlayer)
			{
				vector.w = 1f;
			}
		}
		Vector4 vector2 = new Vector4(-num2 * num, -num3 * num, num, this.mFog.blendFactor);
		this.mMat.SetColor("_Unexplored", this.unexploredColor);
		this.mMat.SetColor("_Explored", this.exploredColor);
		this.mMat.SetVector("_CamPos", vector);
		this.mMat.SetVector("_Params", vector2);
		this.mMat.SetMatrix("_InverseMVP", this.mInverseMVP);
		this.mMat.SetTexture("_FogTex0", this.mFog.texture0);
		this.mMat.SetTexture("_FogTex1", this.mFog.texture1);
		Graphics.Blit(source, destination, this.mMat);
	}
}
