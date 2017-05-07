using System;
using UnityEngine;

internal class FogFill
{
	public FogOfWar fogOfWar
	{
		get;
		private set;
	}

	public Vector2i position
	{
		get;
		private set;
	}

	public Vector3 worldPosition
	{
		get;
		private set;
	}

	public int xStart
	{
		get;
		private set;
	}

	public int xEnd
	{
		get;
		private set;
	}

	public int yStart
	{
		get;
		private set;
	}

	public int yEnd
	{
		get;
		private set;
	}

	public int radius
	{
		get;
		private set;
	}

	public float worldRadius
	{
		get;
		private set;
	}

	public int radiusSqr
	{
		get;
		private set;
	}

	public int innerRadius
	{
		get;
		private set;
	}

	public int innerRadiusSqr
	{
		get;
		private set;
	}

	public FogFill(FogOfWar fow, Vector3 worldpos, float worldradius)
	{
		this.fogOfWar = fow;
		this.worldPosition = worldpos;
		this.worldRadius = worldradius;
		this.position = fow.WorldPositionToFogPosition(worldpos);
		this.radius = (int)(this.worldRadius * (float)fow.mapResolution / fow.mapSize);
		this.xStart = Mathf.Clamp(this.position.x - this.radius, 1, fow.mapResolution - 1);
		this.xEnd = Mathf.Clamp(this.xStart + this.radius + this.radius, 1, fow.mapResolution - 1);
		this.yStart = Mathf.Clamp(this.position.y - this.radius, 1, fow.mapResolution - 1);
		this.yEnd = Mathf.Clamp(this.yStart + this.radius + this.radius, 1, fow.mapResolution - 1);
		this.radiusSqr = this.radius * this.radius;
		this.innerRadius = (int)((1f - fow.fogEdgeRadius) * (float)this.radius);
		this.innerRadiusSqr = this.innerRadius * this.innerRadius;
	}

	public void UnfogCircle(byte[] values)
	{
		for (int i = this.yStart; i < this.yEnd; i++)
		{
			for (int j = this.xStart; j < this.xEnd; j++)
			{
				int num = i * this.fogOfWar.mapResolution + j;
				if (values[num] != 0)
				{
					Vector2i vector2i = new Vector2i(j - this.position.x, i - this.position.y);
					int sqrMagnitude = vector2i.sqrMagnitude;
					if (sqrMagnitude <= this.innerRadiusSqr)
					{
						values[num] = 0;
					}
					else if (sqrMagnitude <= this.radiusSqr)
					{
						float t = (Mathf.Sqrt((float)sqrMagnitude) - (float)this.innerRadius) / (float)(this.radius - this.innerRadius);
						byte b = (byte)Mathf.Lerp(0f, 255f, t);
						if (b < values[num])
						{
							values[num] = b;
						}
					}
				}
			}
		}
	}

	public void UnfogCircleLineOfSight(byte[] values, ColliderFogRectList excluderects, int layermask)
	{
		for (int i = this.yStart; i < this.yEnd; i++)
		{
			for (int j = this.xStart; j < this.xEnd; j++)
			{
				int num = i * this.fogOfWar.mapResolution + j;
				if (values[num] != 0)
				{
					Vector2i vector2i = new Vector2i(j - this.position.x, i - this.position.y);
					int sqrMagnitude = vector2i.sqrMagnitude;
					if (sqrMagnitude < this.radiusSqr)
					{
						RaycastHit raycastHit;
						if (excluderects.Contains(new Vector2i(j, i)) && Physics.Raycast(this.worldPosition, new Vector3((float)vector2i.x, 0f, (float)vector2i.y), out raycastHit, Mathf.Sqrt((float)sqrMagnitude) * this.fogOfWar.pixelSize, layermask))
						{
							if (this.fogOfWar.fieldOfViewPenetration == 0f)
							{
								goto IL_1A9;
							}
							Vector2 vector = new Vector2((float)vector2i.x * this.fogOfWar.pixelSize, (float)vector2i.y * this.fogOfWar.pixelSize);
							float sqrMagnitude2 = vector.sqrMagnitude;
							float num2 = Mathf.Min(raycastHit.distance + this.fogOfWar.fieldOfViewPenetration, this.worldRadius);
							if (sqrMagnitude2 >= num2 * num2)
							{
								goto IL_1A9;
							}
						}
						if (sqrMagnitude <= this.innerRadiusSqr)
						{
							values[num] = 0;
						}
						else
						{
							float t = (Mathf.Sqrt((float)sqrMagnitude) - (float)this.innerRadius) / (float)(this.radius - this.innerRadius);
							byte b = (byte)Mathf.Lerp(0f, 255f, t);
							if (b < values[num])
							{
								values[num] = b;
							}
						}
					}
				}
				IL_1A9:;
			}
		}
	}
}
