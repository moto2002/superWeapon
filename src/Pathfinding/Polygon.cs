using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
	public static class Polygon
	{
		public static List<Vector3> hullCache = new List<Vector3>();

		public static long TriangleArea2(Int3 a, Int3 b, Int3 c)
		{
			return (long)(b.x - a.x) * (long)(c.z - a.z) - (long)(c.x - a.x) * (long)(b.z - a.z);
		}

		public static float TriangleArea2(Vector3 a, Vector3 b, Vector3 c)
		{
			return (b.x - a.x) * (c.z - a.z) - (c.x - a.x) * (b.z - a.z);
		}

		[Obsolete("Use TriangleArea2 instead to avoid confusion regarding the factor 2")]
		public static long TriangleArea(Int3 a, Int3 b, Int3 c)
		{
			return Polygon.TriangleArea2(a, b, c);
		}

		[Obsolete("Use TriangleArea2 instead to avoid confusion regarding the factor 2")]
		public static float TriangleArea(Vector3 a, Vector3 b, Vector3 c)
		{
			return Polygon.TriangleArea2(a, b, c);
		}

		public static bool ContainsPoint(Vector3 a, Vector3 b, Vector3 c, Vector3 p)
		{
			return Polygon.IsClockwiseMargin(a, b, p) && Polygon.IsClockwiseMargin(b, c, p) && Polygon.IsClockwiseMargin(c, a, p);
		}

		public static bool ContainsPoint(Int2 a, Int2 b, Int2 c, Int2 p)
		{
			return Polygon.IsClockwiseMargin(a, b, p) && Polygon.IsClockwiseMargin(b, c, p) && Polygon.IsClockwiseMargin(c, a, p);
		}

		public static bool ContainsPoint(Int3 a, Int3 b, Int3 c, Int3 p)
		{
			return Polygon.IsClockwiseMargin(a, b, p) && Polygon.IsClockwiseMargin(b, c, p) && Polygon.IsClockwiseMargin(c, a, p);
		}

		public static bool ContainsPoint(Vector2[] polyPoints, Vector2 p)
		{
			int num = polyPoints.Length - 1;
			bool flag = false;
			int i = 0;
			while (i < polyPoints.Length)
			{
				if (((polyPoints[i].y <= p.y && p.y < polyPoints[num].y) || (polyPoints[num].y <= p.y && p.y < polyPoints[i].y)) && p.x < (polyPoints[num].x - polyPoints[i].x) * (p.y - polyPoints[i].y) / (polyPoints[num].y - polyPoints[i].y) + polyPoints[i].x)
				{
					flag = !flag;
				}
				num = i++;
			}
			return flag;
		}

		public static bool ContainsPoint(Vector3[] polyPoints, Vector3 p)
		{
			int num = polyPoints.Length - 1;
			bool flag = false;
			int i = 0;
			while (i < polyPoints.Length)
			{
				if (((polyPoints[i].z <= p.z && p.z < polyPoints[num].z) || (polyPoints[num].z <= p.z && p.z < polyPoints[i].z)) && p.x < (polyPoints[num].x - polyPoints[i].x) * (p.z - polyPoints[i].z) / (polyPoints[num].z - polyPoints[i].z) + polyPoints[i].x)
				{
					flag = !flag;
				}
				num = i++;
			}
			return flag;
		}

		public static bool LeftNotColinear(Vector3 a, Vector3 b, Vector3 p)
		{
			return (b.x - a.x) * (p.z - a.z) - (p.x - a.x) * (b.z - a.z) < -1.401298E-45f;
		}

		public static bool Left(Vector3 a, Vector3 b, Vector3 p)
		{
			return (b.x - a.x) * (p.z - a.z) - (p.x - a.x) * (b.z - a.z) <= 0f;
		}

		public static bool Left(Vector2 a, Vector2 b, Vector2 p)
		{
			return (b.x - a.x) * (p.y - a.y) - (p.x - a.x) * (b.y - a.y) <= 0f;
		}

		public static bool Left(Int3 a, Int3 b, Int3 c)
		{
			return (long)(b.x - a.x) * (long)(c.z - a.z) - (long)(c.x - a.x) * (long)(b.z - a.z) <= 0L;
		}

		public static bool LeftNotColinear(Int3 a, Int3 b, Int3 c)
		{
			return (long)(b.x - a.x) * (long)(c.z - a.z) - (long)(c.x - a.x) * (long)(b.z - a.z) < 0L;
		}

		public static bool Left(Int2 a, Int2 b, Int2 c)
		{
			return (long)(b.x - a.x) * (long)(c.y - a.y) - (long)(c.x - a.x) * (long)(b.y - a.y) <= 0L;
		}

		public static bool IsClockwiseMargin(Vector3 a, Vector3 b, Vector3 c)
		{
			return (b.x - a.x) * (c.z - a.z) - (c.x - a.x) * (b.z - a.z) <= 1.401298E-45f;
		}

		public static bool IsClockwise(Vector3 a, Vector3 b, Vector3 c)
		{
			return (b.x - a.x) * (c.z - a.z) - (c.x - a.x) * (b.z - a.z) < 0f;
		}

		public static bool IsClockwise(Int3 a, Int3 b, Int3 c)
		{
			return Polygon.LeftNotColinear(a, b, c);
		}

		public static bool IsClockwiseMargin(Int3 a, Int3 b, Int3 c)
		{
			return Polygon.Left(a, b, c);
		}

		public static bool IsClockwiseMargin(Int2 a, Int2 b, Int2 c)
		{
			return Polygon.Left(a, b, c);
		}

		public static bool IsColinear(Int3 a, Int3 b, Int3 c)
		{
			return (long)(b.x - a.x) * (long)(c.z - a.z) - (long)(c.x - a.x) * (long)(b.z - a.z) == 0L;
		}

		public static bool IsColinearAlmost(Int3 a, Int3 b, Int3 c)
		{
			long num = (long)(b.x - a.x) * (long)(c.z - a.z) - (long)(c.x - a.x) * (long)(b.z - a.z);
			return num > -1L && num < 1L;
		}

		public static bool IsColinear(Vector3 a, Vector3 b, Vector3 c)
		{
			float num = (b.x - a.x) * (c.z - a.z) - (c.x - a.x) * (b.z - a.z);
			return num <= 1E-07f && num >= -1E-07f;
		}

		public static bool IntersectsUnclamped(Vector3 a, Vector3 b, Vector3 a2, Vector3 b2)
		{
			return Polygon.Left(a, b, a2) != Polygon.Left(a, b, b2);
		}

		public static bool Intersects(Int2 a, Int2 b, Int2 a2, Int2 b2)
		{
			return Polygon.Left(a, b, a2) != Polygon.Left(a, b, b2) && Polygon.Left(a2, b2, a) != Polygon.Left(a2, b2, b);
		}

		public static bool Intersects(Int3 a, Int3 b, Int3 a2, Int3 b2)
		{
			return Polygon.Left(a, b, a2) != Polygon.Left(a, b, b2) && Polygon.Left(a2, b2, a) != Polygon.Left(a2, b2, b);
		}

		public static bool Intersects(Vector3 start1, Vector3 end1, Vector3 start2, Vector3 end2)
		{
			Vector3 vector = end1 - start1;
			Vector3 vector2 = end2 - start2;
			float num = vector2.z * vector.x - vector2.x * vector.z;
			if (num == 0f)
			{
				return false;
			}
			float num2 = vector2.x * (start1.z - start2.z) - vector2.z * (start1.x - start2.x);
			float num3 = vector.x * (start1.z - start2.z) - vector.z * (start1.x - start2.x);
			float num4 = num2 / num;
			float num5 = num3 / num;
			return num4 >= 0f && num4 <= 1f && num5 >= 0f && num5 <= 1f;
		}

		public static Vector3 IntersectionPointOptimized(Vector3 start1, Vector3 dir1, Vector3 start2, Vector3 dir2)
		{
			float num = dir2.z * dir1.x - dir2.x * dir1.z;
			if (num == 0f)
			{
				return start1;
			}
			float num2 = dir2.x * (start1.z - start2.z) - dir2.z * (start1.x - start2.x);
			float d = num2 / num;
			return start1 + dir1 * d;
		}

		public static Vector3 IntersectionPointOptimized(Vector3 start1, Vector3 dir1, Vector3 start2, Vector3 dir2, out bool intersects)
		{
			float num = dir2.z * dir1.x - dir2.x * dir1.z;
			if (num == 0f)
			{
				intersects = false;
				return start1;
			}
			float num2 = dir2.x * (start1.z - start2.z) - dir2.z * (start1.x - start2.x);
			float d = num2 / num;
			intersects = true;
			return start1 + dir1 * d;
		}

		public static bool IntersectionFactorRaySegment(Int3 start1, Int3 end1, Int3 start2, Int3 end2)
		{
			Int3 @int = end1 - start1;
			Int3 int2 = end2 - start2;
			long num = (long)(int2.z * @int.x - int2.x * @int.z);
			if (num == 0L)
			{
				return false;
			}
			long num2 = (long)(int2.x * (start1.z - start2.z) - int2.z * (start1.x - start2.x));
			long num3 = (long)(@int.x * (start1.z - start2.z) - @int.z * (start1.x - start2.x));
			return (num2 < 0L ^ num < 0L) && (num3 < 0L ^ num < 0L) && (num < 0L || num3 <= num) && (num >= 0L || num3 > num);
		}

		public static bool IntersectionFactor(Int3 start1, Int3 end1, Int3 start2, Int3 end2, out float factor1, out float factor2)
		{
			Int3 @int = end1 - start1;
			Int3 int2 = end2 - start2;
			long num = (long)(int2.z * @int.x - int2.x * @int.z);
			if (num == 0L)
			{
				factor1 = 0f;
				factor2 = 0f;
				return false;
			}
			long num2 = (long)(int2.x * (start1.z - start2.z) - int2.z * (start1.x - start2.x));
			long num3 = (long)(@int.x * (start1.z - start2.z) - @int.z * (start1.x - start2.x));
			factor1 = (float)num2 / (float)num;
			factor2 = (float)num3 / (float)num;
			return true;
		}

		public static bool IntersectionFactor(Vector3 start1, Vector3 end1, Vector3 start2, Vector3 end2, out float factor1, out float factor2)
		{
			Vector3 vector = end1 - start1;
			Vector3 vector2 = end2 - start2;
			float num = vector2.z * vector.x - vector2.x * vector.z;
			if (num <= 1E-05f && num >= -1E-05f)
			{
				factor1 = 0f;
				factor2 = 0f;
				return false;
			}
			float num2 = vector2.x * (start1.z - start2.z) - vector2.z * (start1.x - start2.x);
			float num3 = vector.x * (start1.z - start2.z) - vector.z * (start1.x - start2.x);
			float num4 = num2 / num;
			float num5 = num3 / num;
			factor1 = num4;
			factor2 = num5;
			return true;
		}

		public static float IntersectionFactorRay(Int3 start1, Int3 end1, Int3 start2, Int3 end2)
		{
			Int3 @int = end1 - start1;
			Int3 int2 = end2 - start2;
			int num = int2.z * @int.x - int2.x * @int.z;
			if (num == 0)
			{
				return float.NaN;
			}
			int num2 = int2.x * (start1.z - start2.z) - int2.z * (start1.x - start2.x);
			int num3 = @int.x * (start1.z - start2.z) - @int.z * (start1.x - start2.x);
			if ((float)num3 / (float)num < 0f)
			{
				return float.NaN;
			}
			return (float)num2 / (float)num;
		}

		public static float IntersectionFactor(Vector3 start1, Vector3 end1, Vector3 start2, Vector3 end2)
		{
			Vector3 vector = end1 - start1;
			Vector3 vector2 = end2 - start2;
			float num = vector2.z * vector.x - vector2.x * vector.z;
			if (num == 0f)
			{
				return -1f;
			}
			float num2 = vector2.x * (start1.z - start2.z) - vector2.z * (start1.x - start2.x);
			return num2 / num;
		}

		public static Vector3 IntersectionPoint(Vector3 start1, Vector3 end1, Vector3 start2, Vector3 end2)
		{
			bool flag;
			return Polygon.IntersectionPoint(start1, end1, start2, end2, out flag);
		}

		public static Vector3 IntersectionPoint(Vector3 start1, Vector3 end1, Vector3 start2, Vector3 end2, out bool intersects)
		{
			Vector3 a = end1 - start1;
			Vector3 vector = end2 - start2;
			float num = vector.z * a.x - vector.x * a.z;
			if (num == 0f)
			{
				intersects = false;
				return start1;
			}
			float num2 = vector.x * (start1.z - start2.z) - vector.z * (start1.x - start2.x);
			float d = num2 / num;
			intersects = true;
			return start1 + a * d;
		}

		public static Vector2 IntersectionPoint(Vector2 start1, Vector2 end1, Vector2 start2, Vector2 end2)
		{
			bool flag;
			return Polygon.IntersectionPoint(start1, end1, start2, end2, out flag);
		}

		public static Vector2 IntersectionPoint(Vector2 start1, Vector2 end1, Vector2 start2, Vector2 end2, out bool intersects)
		{
			Vector2 a = end1 - start1;
			Vector2 vector = end2 - start2;
			float num = vector.y * a.x - vector.x * a.y;
			if (num == 0f)
			{
				intersects = false;
				return start1;
			}
			float num2 = vector.x * (start1.y - start2.y) - vector.y * (start1.x - start2.x);
			float d = num2 / num;
			intersects = true;
			return start1 + a * d;
		}

		public static Vector3 SegmentIntersectionPoint(Vector3 start1, Vector3 end1, Vector3 start2, Vector3 end2, out bool intersects)
		{
			Vector3 a = end1 - start1;
			Vector3 vector = end2 - start2;
			float num = vector.z * a.x - vector.x * a.z;
			if (num == 0f)
			{
				intersects = false;
				return start1;
			}
			float num2 = vector.x * (start1.z - start2.z) - vector.z * (start1.x - start2.x);
			float num3 = a.x * (start1.z - start2.z) - a.z * (start1.x - start2.x);
			float num4 = num2 / num;
			float num5 = num3 / num;
			if (num4 < 0f || num4 > 1f || num5 < 0f || num5 > 1f)
			{
				intersects = false;
				return start1;
			}
			intersects = true;
			return start1 + a * num4;
		}

		public static Vector3[] ConvexHull(Vector3[] points)
		{
			if (points.Length == 0)
			{
				return new Vector3[0];
			}
			List<Vector3> obj = Polygon.hullCache;
			Vector3[] result;
			lock (obj)
			{
				List<Vector3> list = Polygon.hullCache;
				list.Clear();
				int num = 0;
				for (int i = 1; i < points.Length; i++)
				{
					if (points[i].x < points[num].x)
					{
						num = i;
					}
				}
				int num2 = num;
				int num3 = 0;
				while (true)
				{
					list.Add(points[num]);
					int num4 = 0;
					for (int j = 0; j < points.Length; j++)
					{
						if (num4 == num || !Polygon.Left(points[num], points[num4], points[j]))
						{
							num4 = j;
						}
					}
					num = num4;
					num3++;
					if (num3 > 10000)
					{
						break;
					}
					if (num == num2)
					{
						goto IL_F7;
					}
				}
				Debug.LogWarning("Infinite Loop in Convex Hull Calculation");
				IL_F7:
				result = list.ToArray();
			}
			return result;
		}

		public static bool LineIntersectsBounds(Bounds bounds, Vector3 a, Vector3 b)
		{
			a -= bounds.center;
			b -= bounds.center;
			Vector3 b2 = (a + b) * 0.5f;
			Vector3 vector = a - b2;
			Vector3 vector2 = new Vector3(Math.Abs(vector.x), Math.Abs(vector.y), Math.Abs(vector.z));
			Vector3 extents = bounds.extents;
			return Math.Abs(b2.x) <= extents.x + vector2.x && Math.Abs(b2.y) <= extents.y + vector2.y && Math.Abs(b2.z) <= extents.z + vector2.z && Math.Abs(b2.y * vector.z - b2.z * vector.y) <= extents.y * vector2.z + extents.z * vector2.y && Math.Abs(b2.x * vector.z - b2.z * vector.x) <= extents.x * vector2.z + extents.z * vector2.x && Math.Abs(b2.x * vector.y - b2.y * vector.x) <= extents.x * vector2.y + extents.y * vector2.x;
		}

		public static Vector3[] Subdivide(Vector3[] path, int subdivisions)
		{
			subdivisions = ((subdivisions >= 0) ? subdivisions : 0);
			if (subdivisions == 0)
			{
				return path;
			}
			Vector3[] array = new Vector3[(path.Length - 1) * (int)Mathf.Pow(2f, (float)subdivisions) + 1];
			int num = 0;
			for (int i = 0; i < path.Length - 1; i++)
			{
				float num2 = 1f / Mathf.Pow(2f, (float)subdivisions);
				for (float num3 = 0f; num3 < 1f; num3 += num2)
				{
					array[num] = Vector3.Lerp(path[i], path[i + 1], Mathf.SmoothStep(0f, 1f, num3));
					num++;
				}
			}
			array[num] = path[path.Length - 1];
			return array;
		}

		public static Vector3 ClosestPointOnTriangle(Vector3[] triangle, Vector3 point)
		{
			return Polygon.ClosestPointOnTriangle(triangle[0], triangle[1], triangle[2], point);
		}

		public static Vector3 ClosestPointOnTriangle(Vector3 tr0, Vector3 tr1, Vector3 tr2, Vector3 point)
		{
			Vector3 lhs = tr0 - point;
			Vector3 vector = tr1 - tr0;
			Vector3 vector2 = tr2 - tr0;
			float sqrMagnitude = vector.sqrMagnitude;
			float num = Vector3.Dot(vector, vector2);
			float sqrMagnitude2 = vector2.sqrMagnitude;
			float num2 = Vector3.Dot(lhs, vector);
			float num3 = Vector3.Dot(lhs, vector2);
			float num4 = sqrMagnitude * sqrMagnitude2 - num * num;
			float num5 = num * num3 - sqrMagnitude2 * num2;
			float num6 = num * num2 - sqrMagnitude * num3;
			if (num5 + num6 <= num4)
			{
				if (num5 < 0f)
				{
					if (num6 < 0f)
					{
						if (num2 < 0f)
						{
							num6 = 0f;
							if (-num2 >= sqrMagnitude)
							{
								num5 = 1f;
							}
							else
							{
								num5 = -num2 / sqrMagnitude;
							}
						}
						else
						{
							num5 = 0f;
							if (num3 >= 0f)
							{
								num6 = 0f;
							}
							else if (-num3 >= sqrMagnitude2)
							{
								num6 = 1f;
							}
							else
							{
								num6 = -num3 / sqrMagnitude2;
							}
						}
					}
					else
					{
						num5 = 0f;
						if (num3 >= 0f)
						{
							num6 = 0f;
						}
						else if (-num3 >= sqrMagnitude2)
						{
							num6 = 1f;
						}
						else
						{
							num6 = -num3 / sqrMagnitude2;
						}
					}
				}
				else if (num6 < 0f)
				{
					num6 = 0f;
					if (num2 >= 0f)
					{
						num5 = 0f;
					}
					else if (-num2 >= sqrMagnitude)
					{
						num5 = 1f;
					}
					else
					{
						num5 = -num2 / sqrMagnitude;
					}
				}
				else
				{
					float num7 = 1f / num4;
					num5 *= num7;
					num6 *= num7;
				}
			}
			else if (num5 < 0f)
			{
				float num8 = num + num2;
				float num9 = sqrMagnitude2 + num3;
				if (num9 > num8)
				{
					float num10 = num9 - num8;
					float num11 = sqrMagnitude - 2f * num + sqrMagnitude2;
					if (num10 >= num11)
					{
						num5 = 1f;
						num6 = 0f;
					}
					else
					{
						num5 = num10 / num11;
						num6 = 1f - num5;
					}
				}
				else
				{
					num5 = 0f;
					if (num9 <= 0f)
					{
						num6 = 1f;
					}
					else if (num3 >= 0f)
					{
						num6 = 0f;
					}
					else
					{
						num6 = -num3 / sqrMagnitude2;
					}
				}
			}
			else if (num6 < 0f)
			{
				float num8 = num + num3;
				float num9 = sqrMagnitude + num2;
				if (num9 > num8)
				{
					float num10 = num9 - num8;
					float num11 = sqrMagnitude - 2f * num + sqrMagnitude2;
					if (num10 >= num11)
					{
						num6 = 1f;
						num5 = 0f;
					}
					else
					{
						num6 = num10 / num11;
						num5 = 1f - num6;
					}
				}
				else
				{
					num6 = 0f;
					if (num9 <= 0f)
					{
						num5 = 1f;
					}
					else if (num2 >= 0f)
					{
						num5 = 0f;
					}
					else
					{
						num5 = -num2 / sqrMagnitude;
					}
				}
			}
			else
			{
				float num10 = sqrMagnitude2 + num3 - num - num2;
				if (num10 <= 0f)
				{
					num5 = 0f;
					num6 = 1f;
				}
				else
				{
					float num11 = sqrMagnitude - 2f * num + sqrMagnitude2;
					if (num10 >= num11)
					{
						num5 = 1f;
						num6 = 0f;
					}
					else
					{
						num5 = num10 / num11;
						num6 = 1f - num5;
					}
				}
			}
			return tr0 + num5 * vector + num6 * vector2;
		}

		public static float DistanceSegmentSegment3D(Vector3 s1, Vector3 e1, Vector3 s2, Vector3 e2)
		{
			Vector3 vector = e1 - s1;
			Vector3 vector2 = e2 - s2;
			Vector3 vector3 = s1 - s2;
			float num = Vector3.Dot(vector, vector);
			float num2 = Vector3.Dot(vector, vector2);
			float num3 = Vector3.Dot(vector2, vector2);
			float num4 = Vector3.Dot(vector, vector3);
			float num5 = Vector3.Dot(vector2, vector3);
			float num6 = num * num3 - num2 * num2;
			float num7 = num6;
			float num8 = num6;
			float num9;
			float num10;
			if (num6 < 1E-06f)
			{
				num9 = 0f;
				num7 = 1f;
				num10 = num5;
				num8 = num3;
			}
			else
			{
				num9 = num2 * num5 - num3 * num4;
				num10 = num * num5 - num2 * num4;
				if (num9 < 0f)
				{
					num9 = 0f;
					num10 = num5;
					num8 = num3;
				}
				else if (num9 > num7)
				{
					num9 = num7;
					num10 = num5 + num2;
					num8 = num3;
				}
			}
			if (num10 < 0f)
			{
				num10 = 0f;
				if (-num4 < 0f)
				{
					num9 = 0f;
				}
				else if (-num4 > num)
				{
					num9 = num7;
				}
				else
				{
					num9 = -num4;
					num7 = num;
				}
			}
			else if (num10 > num8)
			{
				num10 = num8;
				if (-num4 + num2 < 0f)
				{
					num9 = 0f;
				}
				else if (-num4 + num2 > num)
				{
					num9 = num7;
				}
				else
				{
					num9 = -num4 + num2;
					num7 = num;
				}
			}
			float d = (Math.Abs(num9) >= 1E-06f) ? (num9 / num7) : 0f;
			float d2 = (Math.Abs(num10) >= 1E-06f) ? (num10 / num8) : 0f;
			return (vector3 + d * vector - d2 * vector2).sqrMagnitude;
		}
	}
}
