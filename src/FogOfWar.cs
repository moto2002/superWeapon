using System;
using UnityEngine;

public class FogOfWar : MonoBehaviour
{
	public int mapResolution = 128;

	public float mapSize = 128f;

	public Vector2 mapOffset = Vector2.zero;

	public FilterMode filterMode = FilterMode.Bilinear;

	public Color fogColor = Color.black;

	public float fieldOfViewPenetration = 1f;

	[Range(0f, 1f)]
	public float fogEdgeRadius = 0.2f;

	[Range(0f, 1f)]
	public float partialFogAmount = 0.5f;

	private Shader _shader;

	private Material _material;

	private byte[] _values;

	public float updateFrequency = 0.1f;

	private float _nextUpdate;

	private Transform _transform;

	private Camera _camera;

	public static FogOfWar current;

	public float pixelSize
	{
		get
		{
			return this.mapSize / (float)this.mapResolution;
		}
	}

	public Texture2D texture
	{
		get;
		private set;
	}

	private void Awake()
	{
		FogOfWar.current = this;
		this.texture = new Texture2D(this.mapResolution, this.mapResolution, TextureFormat.Alpha8, false);
		this._shader = Resources.Load<Shader>("FogOfWarShader");
		if (this._shader == null)
		{
			Debug.LogError("Failed to find FogOfWarShader in Resouces folder!");
		}
		this._material = new Material(this._shader);
		this._material.name = "FogMaterial";
		this._material.SetTexture("_FogTex", this.texture);
		this._material.SetInt("_FogTextureSize", this.mapResolution);
		this._material.SetFloat("_mapSize", this.mapSize);
		this._material.SetVector("_mapOffset", this.mapOffset);
		this._values = new byte[this.mapResolution * this.mapResolution];
		this.SetAll(255);
	}

	private void Start()
	{
		this._transform = base.transform;
		this._camera = base.GetComponent<Camera>();
		this._camera.depthTextureMode |= DepthTextureMode.Depth;
		this.UpdateTexture();
	}

	public void SetAll(byte value)
	{
		for (int i = 0; i < this._values.Length; i++)
		{
			this._values[i] = value;
		}
	}

	public Vector2i WorldPositionToFogPosition(Vector3 position)
	{
		float d = (float)this.mapResolution / this.mapSize;
		Vector2i vector2i = new Vector2i((new Vector2(position.x, position.z) - this.mapOffset) * d);
		vector2i += new Vector2i(this.mapResolution >> 1, this.mapResolution >> 1);
		return vector2i;
	}

	public Vector2 WorldPositionToFogPositionNormalized(Vector3 position)
	{
		return (new Vector2(position.x, position.z) - this.mapOffset) / this.mapSize + new Vector2(0.5f, 0.5f);
	}

	public byte GetFogValue(Vector3 position)
	{
		Vector2i vector2i = this.WorldPositionToFogPosition(position);
		return this._values[vector2i.y * this.mapResolution + vector2i.x];
	}

	public bool IsInCompleteFog(Vector3 position)
	{
		return this.GetFogValue(position) > 240;
	}

	public bool IsInPartialFog(Vector3 position)
	{
		return this.GetFogValue(position) > 20;
	}

	private ColliderFogRectList GetExtendedColliders(FogFill fogfill, int layermask)
	{
		if (layermask == 0)
		{
			return null;
		}
		Collider[] array = Physics.OverlapSphere(fogfill.worldPosition, fogfill.worldRadius, layermask);
		if (array.Length == 0)
		{
			return null;
		}
		ColliderFogRectList colliderFogRectList = new ColliderFogRectList(this);
		colliderFogRectList.Add(array);
		colliderFogRectList.ExtendToCircleEdge(fogfill.position, fogfill.radius);
		colliderFogRectList.Optimise();
		return (colliderFogRectList.Count != 0) ? colliderFogRectList : null;
	}

	public void Unfog(Vector3 position, float radius, int layermask = 0)
	{
		FogFill fogFill = new FogFill(this, position, radius);
		ColliderFogRectList extendedColliders = this.GetExtendedColliders(fogFill, layermask);
		if (extendedColliders == null)
		{
			fogFill.UnfogCircle(this._values);
			return;
		}
		fogFill.UnfogCircleLineOfSight(this._values, extendedColliders, layermask);
	}

	public void Unfog(Rect rect)
	{
		Vector2i vector2i = this.WorldPositionToFogPosition(rect.min);
		Vector2i vector2i2 = this.WorldPositionToFogPosition(rect.max);
		for (int i = vector2i.y; i < vector2i2.y; i++)
		{
			for (int j = vector2i.x; j < vector2i2.x; j++)
			{
				this._values[i * this.mapResolution + j] = 0;
			}
		}
	}

	private void UpdateTexture()
	{
		this.texture.LoadRawTextureData(this._values);
		this.texture.filterMode = this.filterMode;
		this.texture.Apply();
		byte b = (byte)(this.partialFogAmount * 255f);
		for (int i = 0; i < this.mapResolution; i++)
		{
			for (int j = 0; j < this.mapResolution; j++)
			{
				int num = i * this.mapResolution + j;
				if (this._values[num] < b)
				{
					this._values[num] = b;
				}
			}
		}
	}

	private void Update()
	{
		this._nextUpdate -= Time.deltaTime;
		if (this._nextUpdate > 0f)
		{
			return;
		}
		this._nextUpdate = this.updateFrequency;
		this.UpdateTexture();
	}

	private Matrix4x4 CalculateCameraFrustumCorners()
	{
		float nearClipPlane = this._camera.nearClipPlane;
		float farClipPlane = this._camera.farClipPlane;
		float fieldOfView = this._camera.fieldOfView;
		float aspect = this._camera.aspect;
		Matrix4x4 identity = Matrix4x4.identity;
		float num = fieldOfView * 0.5f;
		Vector3 b = this._transform.right * nearClipPlane * Mathf.Tan(num * 0.0174532924f) * aspect;
		Vector3 b2 = this._transform.up * nearClipPlane * Mathf.Tan(num * 0.0174532924f);
		Vector3 vector = this._transform.forward * nearClipPlane - b + b2;
		float d = vector.magnitude * farClipPlane / nearClipPlane;
		vector.Normalize();
		vector *= d;
		Vector3 vector2 = this._transform.forward * nearClipPlane + b + b2;
		vector2.Normalize();
		vector2 *= d;
		Vector3 vector3 = this._transform.forward * nearClipPlane + b - b2;
		vector3.Normalize();
		vector3 *= d;
		Vector3 vector4 = this._transform.forward * nearClipPlane - b - b2;
		vector4.Normalize();
		vector4 *= d;
		identity.SetRow(0, vector);
		identity.SetRow(1, vector2);
		identity.SetRow(2, vector3);
		identity.SetRow(3, vector4);
		return identity;
	}

	[ImageEffectOpaque]
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		this._material.SetColor("_fogColor", this.fogColor);
		this._material.SetMatrix("_FrustumCornersWS", this.CalculateCameraFrustumCorners());
		this._material.SetVector("_CameraWS", this._transform.position);
		FogOfWar.CustomGraphicsBlit(source, destination, this._material);
	}

	private static void CustomGraphicsBlit(RenderTexture source, RenderTexture dest, Material material)
	{
		RenderTexture.active = dest;
		material.SetTexture("_MainTex", source);
		GL.PushMatrix();
		GL.LoadOrtho();
		material.SetPass(0);
		GL.Begin(7);
		GL.MultiTexCoord2(0, 0f, 0f);
		GL.Vertex3(0f, 0f, 3f);
		GL.MultiTexCoord2(0, 1f, 0f);
		GL.Vertex3(1f, 0f, 2f);
		GL.MultiTexCoord2(0, 1f, 1f);
		GL.Vertex3(1f, 1f, 1f);
		GL.MultiTexCoord2(0, 0f, 1f);
		GL.Vertex3(0f, 1f, 0f);
		GL.End();
		GL.PopMatrix();
	}
}
