using System;
using System.Collections.Generic;
using UnityEngine;

internal class ColliderFogRectList : List<ColliderFogRect>
{
	public FogOfWar fogOfWar
	{
		get;
		private set;
	}

	public ColliderFogRectList(FogOfWar fow)
	{
		this.fogOfWar = fow;
	}

	public void Add(params Collider[] colliders)
	{
		for (int i = 0; i < colliders.Length; i++)
		{
			this.Add(new ColliderFogRect(colliders[i], this.fogOfWar));
		}
	}

	public bool Contains(Vector2i p)
	{
		for (int i = 0; i < this.Count; i++)
		{
			if (this[i].Contains(p))
			{
				return true;
			}
		}
		return false;
	}

	public void Optimise()
	{
		base.RemoveAll(delegate(ColliderFogRect r)
		{
			for (int i = 0; i < this.Count; i++)
			{
				if (this[i] != r && this[i].Contains(r))
				{
					return true;
				}
			}
			return false;
		});
	}

	public void ExtendToCircleEdge(Vector2i p, int radius)
	{
		for (int i = 0; i < this.Count; i++)
		{
			this[i].ExtendToCircleEdge(p, radius);
		}
	}
}
