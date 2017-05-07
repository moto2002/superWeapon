using System;
using UnityEngine;

[Serializable]
public struct Vector2i
{
	public int x;

	public int y;

	public int this[int idx]
	{
		get
		{
			return (idx != 0) ? this.y : this.x;
		}
		set
		{
			if (idx != 0)
			{
				this.y = value;
			}
			else
			{
				this.x = value;
			}
		}
	}

	public Vector2 vector2
	{
		get
		{
			return new Vector2((float)this.x, (float)this.y);
		}
	}

	public Vector2i perp
	{
		get
		{
			return new Vector2i(this.y, this.x);
		}
	}

	public float magnitude
	{
		get
		{
			return Mathf.Sqrt((float)(this.x * this.x + this.y * this.y));
		}
	}

	public int sqrMagnitude
	{
		get
		{
			return this.x * this.x + this.y * this.y;
		}
	}

	public int manhattanMagnitude
	{
		get
		{
			return Mathf.Abs(this.x) + Mathf.Abs(this.y);
		}
	}

	public static Vector2i zero
	{
		get
		{
			return new Vector2i(0, 0);
		}
	}

	public static Vector2i one
	{
		get
		{
			return new Vector2i(1, 1);
		}
	}

	public Vector2i(int x, int y)
	{
		this.x = x;
		this.y = y;
	}

	public Vector2i(Vector2 v)
	{
		this.x = Mathf.RoundToInt(v.x);
		this.y = Mathf.RoundToInt(v.y);
	}

	public override bool Equals(object obj)
	{
		if (obj == null)
		{
			return false;
		}
		Vector2i vector2i = (Vector2i)obj;
		return vector2i != null && this.x == vector2i.x && this.y == vector2i.y;
	}

	public bool Equals(Vector2i p)
	{
		return p != null && this.x == p.x && this.y == p.y;
	}

	public override int GetHashCode()
	{
		return this.x ^ this.y;
	}

	public override string ToString()
	{
		return string.Concat(new object[]
		{
			"(",
			this.x,
			", ",
			this.y,
			")"
		});
	}

	public static Vector2i operator +(Vector2i c1, Vector2i c2)
	{
		return new Vector2i(c1.x + c2.x, c1.y + c2.y);
	}

	public static Vector2 operator +(Vector2i c1, Vector2 c2)
	{
		return new Vector3((float)c1.x + c2.x, (float)c1.y + c2.y);
	}

	public static Vector2 operator +(Vector2 c1, Vector2i c2)
	{
		return new Vector3(c1.x + (float)c2.x, c1.y + (float)c2.y);
	}

	public static Vector2i operator -(Vector2i c1, Vector2i c2)
	{
		return new Vector2i(c1.x - c2.x, c1.y - c2.y);
	}

	public static Vector2i operator *(Vector2i c1, int c2)
	{
		return new Vector2i(c1.x * c2, c1.y * c2);
	}

	public static Vector2 operator *(Vector2i c1, float c2)
	{
		return new Vector2((float)c1.x * c2, (float)c1.y * c2);
	}

	public static Vector2i operator *(int c1, Vector2i c2)
	{
		return new Vector2i(c1 * c2.x, c1 * c2.y);
	}

	public static Vector2 operator *(float c1, Vector2i c2)
	{
		return new Vector2(c1 * (float)c2.x, c1 * (float)c2.y);
	}

	public static bool operator ==(Vector2i a, Vector2i b)
	{
		return object.ReferenceEquals(a, b) || (a != null && b != null && a.x == b.x && a.y == b.y);
	}

	public static bool operator !=(Vector2i a, Vector2i b)
	{
		return !(a == b);
	}
}
