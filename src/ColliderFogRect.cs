using System;
using UnityEngine;

internal class ColliderFogRect
{
	public Vector2i position;

	public Vector2i size;

	public Collider collider
	{
		get;
		private set;
	}

	public int xMin
	{
		get
		{
			return this.position.x;
		}
		set
		{
			this.size.x = this.size.x - (value - this.position.x);
			this.position.x = value;
		}
	}

	public int yMin
	{
		get
		{
			return this.position.y;
		}
		set
		{
			this.size.y = this.size.y - (value - this.position.y);
			this.position.y = value;
		}
	}

	public int xMax
	{
		get
		{
			return this.position.x + this.size.x;
		}
		set
		{
			this.size.x = value - this.position.x;
		}
	}

	public int yMax
	{
		get
		{
			return this.position.y + this.size.y;
		}
		set
		{
			this.size.y = value - this.position.y;
		}
	}

	public ColliderFogRect(Collider c, FogOfWar fow)
	{
		Bounds bounds = c.bounds;
		this.position = fow.WorldPositionToFogPosition(bounds.min);
		this.size = fow.WorldPositionToFogPosition(bounds.max) - this.position;
		this.collider = c;
	}

	public bool Contains(Vector2i p)
	{
		return p.x >= this.xMin && p.x <= this.xMax && p.y >= this.yMin && p.y <= this.yMax;
	}

	public bool Contains(ColliderFogRect other)
	{
		return other.xMin >= this.xMin && other.xMax <= this.xMax && other.yMin >= this.yMin && other.yMax <= this.yMax;
	}

	public bool ContainsCircle(Vector2i p, int r)
	{
		return p.x - r >= this.xMin && p.x + r <= this.xMax && p.y - r >= this.yMin && p.y + r <= this.yMax;
	}

	public void ExtendToCircleEdge(Vector2i p, int radius)
	{
		if (this.xMin < p.x)
		{
			this.xMin = p.x - radius;
		}
		if (this.xMax > p.x)
		{
			this.xMax = p.x + radius;
		}
		if (this.yMin < p.y)
		{
			this.yMin = p.y - radius;
		}
		if (this.yMax > p.y)
		{
			this.yMax = p.y + radius;
		}
	}
}
