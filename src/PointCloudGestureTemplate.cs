using System;
using System.Collections.Generic;
using UnityEngine;

public class PointCloudGestureTemplate : ScriptableObject
{
	[SerializeField]
	private List<int> strokeIds;

	[SerializeField]
	private List<Vector2> positions;

	[SerializeField]
	private int strokeCount;

	[SerializeField]
	private Vector2 size = Vector2.zero;

	public Vector2 Size
	{
		get
		{
			return this.size;
		}
	}

	public float Width
	{
		get
		{
			return this.size.x;
		}
	}

	public float Height
	{
		get
		{
			return this.size.y;
		}
	}

	public int PointCount
	{
		get
		{
			return this.positions.Count;
		}
	}

	public int StrokeCount
	{
		get
		{
			return this.strokeCount;
		}
	}

	public void BeginPoints()
	{
		this.positions.Clear();
		this.strokeIds.Clear();
		this.strokeCount = 0;
		this.size = Vector2.zero;
	}

	public void AddPoint(int stroke, Vector2 p)
	{
		this.strokeIds.Add(stroke);
		this.positions.Add(p);
	}

	public void AddPoint(int stroke, float x, float y)
	{
		this.AddPoint(stroke, new Vector2(x, y));
	}

	public void EndPoints()
	{
		this.Normalize();
		List<int> list = new List<int>();
		for (int i = 0; i < this.strokeIds.Count; i++)
		{
			int item = this.strokeIds[i];
			if (!list.Contains(item))
			{
				list.Add(item);
			}
		}
		this.strokeCount = list.Count;
		this.MakeDirty();
	}

	public Vector2 GetPosition(int pointIndex)
	{
		return this.positions[pointIndex];
	}

	public int GetStrokeId(int pointIndex)
	{
		return this.strokeIds[pointIndex];
	}

	public void Normalize()
	{
		Vector2 b = new Vector2(float.PositiveInfinity, float.PositiveInfinity);
		Vector2 vector = new Vector2(float.NegativeInfinity, float.NegativeInfinity);
		foreach (Vector2 current in this.positions)
		{
			b.x = Mathf.Min(b.x, current.x);
			b.y = Mathf.Min(b.y, current.y);
			vector.x = Mathf.Max(vector.x, current.x);
			vector.y = Mathf.Max(vector.y, current.y);
		}
		float num = vector.x - b.x;
		float num2 = vector.y - b.y;
		float num3 = Mathf.Max(num, num2);
		float num4 = 1f / num3;
		this.size.x = num * num4;
		this.size.y = num2 * num4;
		Vector2 b2 = -0.5f * this.size;
		for (int i = 0; i < this.positions.Count; i++)
		{
			this.positions[i] = (this.positions[i] - b) * num4 + b2;
		}
	}

	private void MakeDirty()
	{
	}
}
