using System;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

[AddComponentMenu("Fog of War/System")]
public class FOWSystem : MonoBehaviour
{
	public enum LOSChecks
	{
		None,
		OnlyOnce,
		EveryUpdate
	}

	public class Revealer
	{
		public bool isActive;

		public FOWSystem.LOSChecks los;

		public Vector3 pos = Vector3.zero;

		public float inner;

		public float outer;

		public bool[] cachedBuffer;

		public int cachedSize;

		public int cachedX;

		public int cachedY;
	}

	public enum State
	{
		Blending,
		NeedUpdate,
		UpdateTexture0,
		UpdateTexture1
	}

	public static FOWSystem instance;

	protected int[,] mHeights;

	protected Transform mTrans;

	protected Vector3 mOrigin = Vector3.zero;

	protected Vector3 mSize = Vector3.one;

	private static BetterList<FOWSystem.Revealer> mRevealers = new BetterList<FOWSystem.Revealer>();

	private static BetterList<FOWSystem.Revealer> mAdded = new BetterList<FOWSystem.Revealer>();

	private static BetterList<FOWSystem.Revealer> mRemoved = new BetterList<FOWSystem.Revealer>();

	protected Color32[] mBuffer0;

	protected Color32[] mBuffer1;

	protected Color32[] mBuffer2;

	protected Texture2D mTexture0;

	protected Texture2D mTexture1;

	protected float mBlendFactor;

	protected float mNextUpdate;

	protected int mScreenHeight;

	protected FOWSystem.State mState;

	private Thread mThread;

	public int worldSize = 256;

	public int textureSize = 128;

	public float updateFrequency = 0.1f;

	public float textureBlendTime = 0.5f;

	public int blurIterations = 2;

	public Vector2 heightRange = new Vector2(0f, 10f);

	public LayerMask raycastMask = -1;

	public float raycastRadius;

	public float margin = 0.4f;

	public bool debug;

	private float mElapsed;

	public Texture2D texture0
	{
		get
		{
			return this.mTexture0;
		}
	}

	public Texture2D texture1
	{
		get
		{
			return this.mTexture1;
		}
	}

	public float blendFactor
	{
		get
		{
			return this.mBlendFactor;
		}
	}

	public static FOWSystem.Revealer CreateRevealer()
	{
		FOWSystem.Revealer revealer = new FOWSystem.Revealer();
		revealer.isActive = false;
		BetterList<FOWSystem.Revealer> obj = FOWSystem.mAdded;
		lock (obj)
		{
			FOWSystem.mAdded.Add(revealer);
		}
		return revealer;
	}

	public static void DeleteRevealer(FOWSystem.Revealer rev)
	{
		BetterList<FOWSystem.Revealer> obj = FOWSystem.mRemoved;
		lock (obj)
		{
			FOWSystem.mRemoved.Add(rev);
		}
	}

	private void Awake()
	{
		FOWSystem.instance = this;
	}

	private void Start()
	{
		this.mTrans = base.transform;
		this.mHeights = new int[this.textureSize, this.textureSize];
		this.mSize = new Vector3((float)this.worldSize, this.heightRange.y - this.heightRange.x, (float)this.worldSize);
		this.mOrigin = this.mTrans.position;
		this.mOrigin.x = this.mOrigin.x - (float)this.worldSize * 0.5f;
		this.mOrigin.z = this.mOrigin.z - (float)this.worldSize * 0.5f;
		int num = this.textureSize * this.textureSize;
		this.mBuffer0 = new Color32[num];
		this.mBuffer1 = new Color32[num];
		this.mBuffer2 = new Color32[num];
		this.CreateGrid();
		this.UpdateBuffer();
		this.UpdateTexture();
		this.mNextUpdate = Time.time + this.updateFrequency;
		this.mThread = new Thread(new ThreadStart(this.ThreadUpdate));
		this.mThread.Start();
	}

	private void OnDestroy()
	{
		if (this.mThread != null)
		{
			this.mThread.Abort();
			while (this.mThread.IsAlive)
			{
				Thread.Sleep(1);
			}
			this.mThread = null;
		}
	}

	private void Update()
	{
		if (this.textureBlendTime > 0f)
		{
			this.mBlendFactor = Mathf.Clamp01(this.mBlendFactor + Time.deltaTime / this.textureBlendTime);
		}
		else
		{
			this.mBlendFactor = 1f;
		}
		if (this.mState == FOWSystem.State.Blending)
		{
			float time = Time.time;
			if (this.mNextUpdate < time)
			{
				this.mNextUpdate = time + this.updateFrequency;
				this.mState = FOWSystem.State.NeedUpdate;
			}
		}
		else if (this.mState != FOWSystem.State.NeedUpdate)
		{
			this.UpdateTexture();
		}
	}

	private void ThreadUpdate()
	{
		Stopwatch stopwatch = new Stopwatch();
		while (true)
		{
			if (this.mState == FOWSystem.State.NeedUpdate)
			{
				stopwatch.Reset();
				stopwatch.Start();
				this.UpdateBuffer();
				stopwatch.Stop();
				if (this.debug)
				{
					UnityEngine.Debug.Log(stopwatch.ElapsedMilliseconds);
				}
				this.mElapsed = 0.001f * (float)stopwatch.ElapsedMilliseconds;
				this.mState = FOWSystem.State.UpdateTexture0;
			}
			Thread.Sleep(1);
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.matrix = base.transform.localToWorldMatrix;
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireCube(new Vector3(0f, (this.heightRange.x + this.heightRange.y) * 0.5f, 0f), new Vector3((float)this.worldSize, this.heightRange.y - this.heightRange.x, (float)this.worldSize));
	}

	private bool IsVisible(int sx, int sy, int fx, int fy, float outer, int sightHeight, int variance)
	{
		int num = Mathf.Abs(fx - sx);
		int num2 = Mathf.Abs(fy - sy);
		int num3 = (sx >= fx) ? -1 : 1;
		int num4 = (sy >= fy) ? -1 : 1;
		int num5 = num - num2;
		float to = (float)sightHeight;
		float from = (float)this.mHeights[fx, fy];
		float num6 = 1f / outer;
		while (sx != fx || sy != fy)
		{
			int num7 = fx - sx;
			int num8 = fy - sy;
			float t = num6 * Mathf.Sqrt((float)(num7 * num7 + num8 * num8));
			if ((float)this.mHeights[sx, sy] > Mathf.Lerp(from, to, t) + (float)variance)
			{
				return false;
			}
			int num9 = num5 << 1;
			if (num9 > -num2)
			{
				num5 -= num2;
				sx += num3;
			}
			if (num9 < num)
			{
				num5 += num;
				sy += num4;
			}
		}
		return true;
	}

	public int WorldToGridHeight(float height)
	{
		int value = Mathf.RoundToInt(height / this.mSize.y * 255f);
		return Mathf.Clamp(value, 0, 255);
	}

	protected virtual void CreateGrid()
	{
		Vector3 origin = this.mOrigin;
		origin.y += this.mSize.y;
		float num = (float)this.worldSize / (float)this.textureSize;
		bool flag = this.raycastRadius > 0f;
		for (int i = 0; i < this.textureSize; i++)
		{
			origin.z = this.mOrigin.z + (float)i * num;
			int j = 0;
			while (j < this.textureSize)
			{
				origin.x = this.mOrigin.x + (float)j * num;
				if (flag)
				{
					RaycastHit raycastHit;
					if (!Physics.SphereCast(new Ray(origin, Vector3.down), this.raycastRadius, out raycastHit, this.mSize.y, this.raycastMask))
					{
						goto IL_13E;
					}
					this.mHeights[j, i] = this.WorldToGridHeight(origin.y - raycastHit.distance - this.raycastRadius);
				}
				else
				{
					RaycastHit raycastHit;
					if (!Physics.Raycast(new Ray(origin, Vector3.down), out raycastHit, this.mSize.y, this.raycastMask))
					{
						goto IL_13E;
					}
					this.mHeights[j, i] = this.WorldToGridHeight(origin.y - raycastHit.distance);
				}
				IL_14D:
				j++;
				continue;
				IL_13E:
				this.mHeights[j, i] = 0;
				goto IL_14D;
			}
		}
	}

	private void UpdateBuffer()
	{
		if (FOWSystem.mAdded.size > 0)
		{
			BetterList<FOWSystem.Revealer> obj = FOWSystem.mAdded;
			lock (obj)
			{
				while (FOWSystem.mAdded.size > 0)
				{
					int num = FOWSystem.mAdded.size - 1;
					FOWSystem.mRevealers.Add(FOWSystem.mAdded.buffer[num]);
					FOWSystem.mAdded.RemoveAt(num);
				}
			}
		}
		if (FOWSystem.mRemoved.size > 0)
		{
			BetterList<FOWSystem.Revealer> obj2 = FOWSystem.mRemoved;
			lock (obj2)
			{
				while (FOWSystem.mRemoved.size > 0)
				{
					int num2 = FOWSystem.mRemoved.size - 1;
					FOWSystem.mRevealers.Remove(FOWSystem.mRemoved.buffer[num2]);
					FOWSystem.mRemoved.RemoveAt(num2);
				}
			}
		}
		float t = (this.textureBlendTime <= 0f) ? 1f : Mathf.Clamp01(this.mBlendFactor + this.mElapsed / this.textureBlendTime);
		int i = 0;
		int num3 = this.mBuffer0.Length;
		while (i < num3)
		{
			this.mBuffer0[i] = Color32.Lerp(this.mBuffer0[i], this.mBuffer1[i], t);
			this.mBuffer1[i].r = 0;
			i++;
		}
		float worldToTex = (float)this.textureSize / (float)this.worldSize;
		for (int j = 0; j < FOWSystem.mRevealers.size; j++)
		{
			FOWSystem.Revealer revealer = FOWSystem.mRevealers[j];
			if (revealer.isActive)
			{
				if (revealer.los == FOWSystem.LOSChecks.None)
				{
					this.RevealUsingRadius(revealer, worldToTex);
				}
				else if (revealer.los == FOWSystem.LOSChecks.OnlyOnce)
				{
					this.RevealUsingCache(revealer, worldToTex);
				}
				else
				{
					this.RevealUsingLOS(revealer, worldToTex);
				}
			}
		}
		for (int k = 0; k < this.blurIterations; k++)
		{
			this.BlurVisibility();
		}
		this.RevealMap();
	}

	private void RevealUsingRadius(FOWSystem.Revealer r, float worldToTex)
	{
		Vector3 vector = r.pos - this.mOrigin;
		int num = Mathf.RoundToInt((vector.x - r.outer) * worldToTex);
		int num2 = Mathf.RoundToInt((vector.z - r.outer) * worldToTex);
		int num3 = Mathf.RoundToInt((vector.x + r.outer) * worldToTex);
		int num4 = Mathf.RoundToInt((vector.z + r.outer) * worldToTex);
		int num5 = Mathf.RoundToInt(vector.x * worldToTex);
		int num6 = Mathf.RoundToInt(vector.z * worldToTex);
		num5 = Mathf.Clamp(num5, 0, this.textureSize - 1);
		num6 = Mathf.Clamp(num6, 0, this.textureSize - 1);
		int num7 = Mathf.RoundToInt(r.outer * r.outer * worldToTex * worldToTex);
		for (int i = num2; i < num4; i++)
		{
			if (i > -1 && i < this.textureSize)
			{
				int num8 = i * this.textureSize;
				for (int j = num; j < num3; j++)
				{
					if (j > -1 && j < this.textureSize)
					{
						int num9 = j - num5;
						int num10 = i - num6;
						int num11 = num9 * num9 + num10 * num10;
						if (num11 < num7)
						{
							this.mBuffer1[j + num8].r = 255;
						}
					}
				}
			}
		}
	}

	private void RevealUsingLOS(FOWSystem.Revealer r, float worldToTex)
	{
		Vector3 vector = r.pos - this.mOrigin;
		int num = Mathf.RoundToInt((vector.x - r.outer) * worldToTex);
		int num2 = Mathf.RoundToInt((vector.z - r.outer) * worldToTex);
		int num3 = Mathf.RoundToInt((vector.x + r.outer) * worldToTex);
		int num4 = Mathf.RoundToInt((vector.z + r.outer) * worldToTex);
		int num5 = Mathf.RoundToInt(vector.x * worldToTex);
		int num6 = Mathf.RoundToInt(vector.z * worldToTex);
		num5 = Mathf.Clamp(num5, 0, this.textureSize - 1);
		num6 = Mathf.Clamp(num6, 0, this.textureSize - 1);
		int num7 = Mathf.RoundToInt(r.inner * r.inner * worldToTex * worldToTex);
		int num8 = Mathf.RoundToInt(r.outer * r.outer * worldToTex * worldToTex);
		int sightHeight = this.WorldToGridHeight(r.pos.y);
		int variance = Mathf.RoundToInt(Mathf.Clamp01(this.margin / (this.heightRange.y - this.heightRange.x)) * 255f);
		Color32 color = new Color32(255, 255, 255, 255);
		for (int i = num2; i < num4; i++)
		{
			if (i > -1 && i < this.textureSize)
			{
				for (int j = num; j < num3; j++)
				{
					if (j > -1 && j < this.textureSize)
					{
						int num9 = j - num5;
						int num10 = i - num6;
						int num11 = num9 * num9 + num10 * num10;
						int num12 = j + i * this.textureSize;
						if (num11 < num7 || (num5 == j && num6 == i))
						{
							this.mBuffer1[num12] = color;
						}
						else if (num11 < num8)
						{
							Vector2 a = new Vector2((float)num9, (float)num10);
							a.Normalize();
							a *= r.inner;
							int num13 = num5 + Mathf.RoundToInt(a.x);
							int num14 = num6 + Mathf.RoundToInt(a.y);
							if (num13 > -1 && num13 < this.textureSize && num14 > -1 && num14 < this.textureSize && this.IsVisible(num13, num14, j, i, Mathf.Sqrt((float)num11), sightHeight, variance))
							{
								this.mBuffer1[num12] = color;
							}
						}
					}
				}
			}
		}
	}

	private void RevealUsingCache(FOWSystem.Revealer r, float worldToTex)
	{
		if (r.cachedBuffer == null)
		{
			this.RevealIntoCache(r, worldToTex);
		}
		Color32 color = new Color32(255, 255, 255, 255);
		int i = r.cachedY;
		int num = r.cachedY + r.cachedSize;
		while (i < num)
		{
			if (i > -1 && i < this.textureSize)
			{
				int num2 = i * this.textureSize;
				int num3 = (i - r.cachedY) * r.cachedSize;
				int j = r.cachedX;
				int num4 = r.cachedX + r.cachedSize;
				while (j < num4)
				{
					if (j > -1 && j < this.textureSize)
					{
						int num5 = j - r.cachedX + num3;
						if (r.cachedBuffer[num5])
						{
							this.mBuffer1[j + num2] = color;
						}
					}
					j++;
				}
			}
			i++;
		}
	}

	private void RevealIntoCache(FOWSystem.Revealer r, float worldToTex)
	{
		Vector3 vector = r.pos - this.mOrigin;
		int num = Mathf.RoundToInt((vector.x - r.outer) * worldToTex);
		int num2 = Mathf.RoundToInt((vector.z - r.outer) * worldToTex);
		int num3 = Mathf.RoundToInt((vector.x + r.outer) * worldToTex);
		int num4 = Mathf.RoundToInt((vector.z + r.outer) * worldToTex);
		int num5 = Mathf.RoundToInt(vector.x * worldToTex);
		int num6 = Mathf.RoundToInt(vector.z * worldToTex);
		num5 = Mathf.Clamp(num5, 0, this.textureSize - 1);
		num6 = Mathf.Clamp(num6, 0, this.textureSize - 1);
		int num7 = Mathf.RoundToInt((float)(num3 - num));
		r.cachedBuffer = new bool[num7 * num7];
		r.cachedSize = num7;
		r.cachedX = num;
		r.cachedY = num2;
		int i = 0;
		int num8 = num7 * num7;
		while (i < num8)
		{
			r.cachedBuffer[i] = false;
			i++;
		}
		int num9 = Mathf.RoundToInt(r.inner * r.inner * worldToTex * worldToTex);
		int num10 = Mathf.RoundToInt(r.outer * r.outer * worldToTex * worldToTex);
		int variance = Mathf.RoundToInt(Mathf.Clamp01(this.margin / (this.heightRange.y - this.heightRange.x)) * 255f);
		int sightHeight = this.WorldToGridHeight(r.pos.y);
		for (int j = num2; j < num4; j++)
		{
			if (j > -1 && j < this.textureSize)
			{
				for (int k = num; k < num3; k++)
				{
					if (k > -1 && k < this.textureSize)
					{
						int num11 = k - num5;
						int num12 = j - num6;
						int num13 = num11 * num11 + num12 * num12;
						if (num13 < num9 || (num5 == k && num6 == j))
						{
							r.cachedBuffer[k - num + (j - num2) * num7] = true;
						}
						else if (num13 < num10)
						{
							Vector2 a = new Vector2((float)num11, (float)num12);
							a.Normalize();
							a *= r.inner;
							int num14 = num5 + Mathf.RoundToInt(a.x);
							int num15 = num6 + Mathf.RoundToInt(a.y);
							if (num14 > -1 && num14 < this.textureSize && num15 > -1 && num15 < this.textureSize && this.IsVisible(num14, num15, k, j, Mathf.Sqrt((float)num13), sightHeight, variance))
							{
								r.cachedBuffer[k - num + (j - num2) * num7] = true;
							}
						}
					}
				}
			}
		}
	}

	private void BlurVisibility()
	{
		for (int i = 0; i < this.textureSize; i++)
		{
			int num = i * this.textureSize;
			int num2 = i - 1;
			if (num2 < 0)
			{
				num2 = 0;
			}
			int num3 = i + 1;
			if (num3 == this.textureSize)
			{
				num3 = i;
			}
			num2 *= this.textureSize;
			num3 *= this.textureSize;
			for (int j = 0; j < this.textureSize; j++)
			{
				int num4 = j - 1;
				if (num4 < 0)
				{
					num4 = 0;
				}
				int num5 = j + 1;
				if (num5 == this.textureSize)
				{
					num5 = j;
				}
				int num6 = j + num;
				int num7 = (int)this.mBuffer1[num6].r;
				num7 += (int)this.mBuffer1[num4 + num].r;
				num7 += (int)this.mBuffer1[num5 + num].r;
				num7 += (int)this.mBuffer1[j + num2].r;
				num7 += (int)this.mBuffer1[j + num3].r;
				num7 += (int)this.mBuffer1[num4 + num2].r;
				num7 += (int)this.mBuffer1[num5 + num2].r;
				num7 += (int)this.mBuffer1[num4 + num3].r;
				num7 += (int)this.mBuffer1[num5 + num3].r;
				Color32 color = this.mBuffer2[num6];
				color.r = (byte)(num7 / 9);
				this.mBuffer2[num6] = color;
			}
		}
		Color32[] array = this.mBuffer1;
		this.mBuffer1 = this.mBuffer2;
		this.mBuffer2 = array;
	}

	private void RevealMap()
	{
		for (int i = 0; i < this.textureSize; i++)
		{
			int num = i * this.textureSize;
			for (int j = 0; j < this.textureSize; j++)
			{
				int num2 = j + num;
				Color32 color = this.mBuffer1[num2];
				if (color.g < color.r)
				{
					color.g = color.r;
					this.mBuffer1[num2] = color;
				}
			}
		}
	}

	private void UpdateTexture()
	{
		if (this.mScreenHeight != Screen.height || this.mTexture0 == null)
		{
			this.mScreenHeight = Screen.height;
			if (this.mTexture0 != null)
			{
				UnityEngine.Object.Destroy(this.mTexture0);
			}
			if (this.mTexture1 != null)
			{
				UnityEngine.Object.Destroy(this.mTexture1);
			}
			this.mTexture0 = new Texture2D(this.textureSize, this.textureSize, TextureFormat.ARGB32, false);
			this.mTexture1 = new Texture2D(this.textureSize, this.textureSize, TextureFormat.ARGB32, false);
			this.mTexture0.wrapMode = TextureWrapMode.Clamp;
			this.mTexture1.wrapMode = TextureWrapMode.Clamp;
			this.mTexture0.SetPixels32(this.mBuffer0);
			this.mTexture0.Apply();
			this.mTexture1.SetPixels32(this.mBuffer1);
			this.mTexture1.Apply();
			this.mState = FOWSystem.State.Blending;
		}
		else if (this.mState == FOWSystem.State.UpdateTexture0)
		{
			this.mTexture0.SetPixels32(this.mBuffer0);
			this.mTexture0.Apply();
			this.mState = FOWSystem.State.UpdateTexture1;
			this.mBlendFactor = 0f;
		}
		else if (this.mState == FOWSystem.State.UpdateTexture1)
		{
			this.mTexture1.SetPixels32(this.mBuffer1);
			this.mTexture1.Apply();
			this.mState = FOWSystem.State.Blending;
		}
	}

	public bool IsVisible(Vector3 pos)
	{
		if (this.mBuffer0 == null)
		{
			return false;
		}
		pos -= this.mOrigin;
		float num = (float)this.textureSize / (float)this.worldSize;
		int num2 = Mathf.RoundToInt(pos.x * num);
		int num3 = Mathf.RoundToInt(pos.z * num);
		num2 = Mathf.Clamp(num2, 0, this.textureSize - 1);
		num3 = Mathf.Clamp(num3, 0, this.textureSize - 1);
		int num4 = num2 + num3 * this.textureSize;
		return this.mBuffer1[num4].r > 0 || this.mBuffer0[num4].r > 0;
	}

	public bool IsExplored(Vector3 pos)
	{
		if (this.mBuffer0 == null)
		{
			return false;
		}
		pos -= this.mOrigin;
		float num = (float)this.textureSize / (float)this.worldSize;
		int num2 = Mathf.RoundToInt(pos.x * num);
		int num3 = Mathf.RoundToInt(pos.z * num);
		num2 = Mathf.Clamp(num2, 0, this.textureSize - 1);
		num3 = Mathf.Clamp(num3, 0, this.textureSize - 1);
		return this.mBuffer0[num2 + num3 * this.textureSize].g > 0;
	}
}
