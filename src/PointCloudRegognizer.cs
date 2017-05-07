using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("FingerGestures/Gestures/PointCloud Recognizer")]
public class PointCloudRegognizer : DiscreteGestureRecognizer<PointCloudGesture>
{
	[Serializable]
	public struct Point
	{
		public int StrokeId;

		public Vector2 Position;

		public Point(int strokeId, Vector2 pos)
		{
			this.StrokeId = strokeId;
			this.Position = pos;
		}

		public Point(int strokeId, float x, float y)
		{
			this.StrokeId = strokeId;
			this.Position = new Vector2(x, y);
		}
	}

	private class NormalizedTemplate
	{
		public PointCloudGestureTemplate Source;

		public List<PointCloudRegognizer.Point> Points;
	}

	private class GestureNormalizer
	{
		private List<PointCloudRegognizer.Point> normalizedPoints;

		private List<PointCloudRegognizer.Point> pointBuffer;

		public GestureNormalizer()
		{
			this.normalizedPoints = new List<PointCloudRegognizer.Point>();
			this.pointBuffer = new List<PointCloudRegognizer.Point>();
		}

		public List<PointCloudRegognizer.Point> Apply(List<PointCloudRegognizer.Point> inputPoints, int normalizedPointsCount)
		{
			this.normalizedPoints = this.Resample(inputPoints, normalizedPointsCount);
			PointCloudRegognizer.GestureNormalizer.Scale(this.normalizedPoints);
			PointCloudRegognizer.GestureNormalizer.TranslateToOrigin(this.normalizedPoints);
			return this.normalizedPoints;
		}

		private List<PointCloudRegognizer.Point> Resample(List<PointCloudRegognizer.Point> points, int normalizedPointsCount)
		{
			float num = PointCloudRegognizer.GestureNormalizer.PathLength(points) / (float)(normalizedPointsCount - 1);
			float num2 = 0f;
			PointCloudRegognizer.Point item = default(PointCloudRegognizer.Point);
			this.normalizedPoints.Clear();
			this.normalizedPoints.Add(points[0]);
			this.pointBuffer.Clear();
			this.pointBuffer.AddRange(points);
			for (int i = 1; i < this.pointBuffer.Count; i++)
			{
				PointCloudRegognizer.Point point = this.pointBuffer[i - 1];
				PointCloudRegognizer.Point point2 = this.pointBuffer[i];
				if (point.StrokeId == point2.StrokeId)
				{
					float num3 = Vector2.Distance(point.Position, point2.Position);
					if (num2 + num3 > num)
					{
						item.Position = Vector2.Lerp(point.Position, point2.Position, (num - num2) / num3);
						item.StrokeId = point.StrokeId;
						this.normalizedPoints.Add(item);
						this.pointBuffer.Insert(i, item);
						num2 = 0f;
					}
					else
					{
						num2 += num3;
					}
				}
			}
			if (this.normalizedPoints.Count == normalizedPointsCount - 1)
			{
				this.normalizedPoints.Add(this.pointBuffer[this.pointBuffer.Count - 1]);
			}
			return this.normalizedPoints;
		}

		private static float PathLength(List<PointCloudRegognizer.Point> points)
		{
			float num = 0f;
			for (int i = 1; i < points.Count; i++)
			{
				if (points[i].StrokeId == points[i - 1].StrokeId)
				{
					num += Vector2.Distance(points[i - 1].Position, points[i].Position);
				}
			}
			return num;
		}

		private static void Scale(List<PointCloudRegognizer.Point> points)
		{
			Vector2 b = new Vector2(float.PositiveInfinity, float.PositiveInfinity);
			Vector2 vector = new Vector2(float.NegativeInfinity, float.NegativeInfinity);
			foreach (PointCloudRegognizer.Point current in points)
			{
				b.x = Mathf.Min(b.x, current.Position.x);
				b.y = Mathf.Min(b.y, current.Position.y);
				vector.x = Mathf.Max(vector.x, current.Position.x);
				vector.y = Mathf.Max(vector.y, current.Position.y);
			}
			float num = Mathf.Max(vector.x - b.x, vector.y - b.y);
			float d = 1f / num;
			for (int i = 0; i < points.Count; i++)
			{
				PointCloudRegognizer.Point value = points[i];
				value.Position = (value.Position - b) * d;
				points[i] = value;
			}
		}

		private static void TranslateToOrigin(List<PointCloudRegognizer.Point> points)
		{
			Vector2 b = PointCloudRegognizer.GestureNormalizer.Centroid(points);
			for (int i = 0; i < points.Count; i++)
			{
				PointCloudRegognizer.Point value = points[i];
				value.Position -= b;
				points[i] = value;
			}
		}

		private static Vector2 Centroid(List<PointCloudRegognizer.Point> points)
		{
			Vector2 vector = Vector2.zero;
			foreach (PointCloudRegognizer.Point current in points)
			{
				vector += current.Position;
			}
			vector /= (float)points.Count;
			return vector;
		}
	}

	private const int NormalizedPointCount = 32;

	private const float gizmoSphereRadius = 0.01f;

	public float MinDistanceBetweenSamples = 5f;

	public float MaxMatchDistance = 3.5f;

	public List<PointCloudGestureTemplate> Templates;

	private PointCloudRegognizer.GestureNormalizer normalizer;

	private List<PointCloudRegognizer.NormalizedTemplate> normalizedTemplates;

	private static bool[] matched = new bool[32];

	private PointCloudGesture debugLastGesture;

	private PointCloudRegognizer.NormalizedTemplate debugLastMatchedTemplate;

	protected override void Awake()
	{
		base.Awake();
		this.normalizer = new PointCloudRegognizer.GestureNormalizer();
		this.normalizedTemplates = new List<PointCloudRegognizer.NormalizedTemplate>();
		foreach (PointCloudGestureTemplate current in this.Templates)
		{
			this.AddTemplate(current);
		}
	}

	private PointCloudRegognizer.NormalizedTemplate FindNormalizedTemplate(PointCloudGestureTemplate template)
	{
		return this.normalizedTemplates.Find((PointCloudRegognizer.NormalizedTemplate t) => t.Source == template);
	}

	private List<PointCloudRegognizer.Point> Normalize(List<PointCloudRegognizer.Point> points)
	{
		return new List<PointCloudRegognizer.Point>(this.normalizer.Apply(points, 32));
	}

	public bool AddTemplate(PointCloudGestureTemplate template)
	{
		if (this.FindNormalizedTemplate(template) != null)
		{
			Debug.LogWarning("The PointCloud template " + template.name + " is already present in the list");
			return false;
		}
		List<PointCloudRegognizer.Point> list = new List<PointCloudRegognizer.Point>();
		for (int i = 0; i < template.PointCount; i++)
		{
			list.Add(new PointCloudRegognizer.Point(template.GetStrokeId(i), template.GetPosition(i)));
		}
		PointCloudRegognizer.NormalizedTemplate normalizedTemplate = new PointCloudRegognizer.NormalizedTemplate();
		normalizedTemplate.Source = template;
		normalizedTemplate.Points = this.Normalize(list);
		this.normalizedTemplates.Add(normalizedTemplate);
		return true;
	}

	protected override void OnBegin(PointCloudGesture gesture, FingerGestures.IFingerList touches)
	{
		gesture.StartPosition = touches.GetAverageStartPosition();
		gesture.Position = touches.GetAveragePosition();
		gesture.RawPoints.Clear();
		gesture.RawPoints.Add(new PointCloudRegognizer.Point(0, gesture.Position));
	}

	private bool RecognizePointCloud(PointCloudGesture gesture)
	{
		this.debugLastGesture = gesture;
		gesture.MatchDistance = 0f;
		gesture.MatchScore = 0f;
		gesture.RecognizedTemplate = null;
		gesture.NormalizedPoints.Clear();
		if (gesture.RawPoints.Count < 2)
		{
			return false;
		}
		gesture.NormalizedPoints.AddRange(this.normalizer.Apply(gesture.RawPoints, 32));
		float num = float.PositiveInfinity;
		foreach (PointCloudRegognizer.NormalizedTemplate current in this.normalizedTemplates)
		{
			float num2 = this.GreedyCloudMatch(gesture.NormalizedPoints, current.Points);
			if (num2 < num)
			{
				num = num2;
				gesture.RecognizedTemplate = current.Source;
				this.debugLastMatchedTemplate = current;
			}
		}
		if (gesture.RecognizedTemplate != null)
		{
			gesture.MatchDistance = num;
			gesture.MatchScore = Mathf.Max((this.MaxMatchDistance - num) / this.MaxMatchDistance, 0f);
		}
		return gesture.MatchScore > 0f;
	}

	private float GreedyCloudMatch(List<PointCloudRegognizer.Point> points, List<PointCloudRegognizer.Point> refPoints)
	{
		float num = 0.5f;
		int num2 = Mathf.FloorToInt(Mathf.Pow((float)points.Count, 1f - num));
		float num3 = float.PositiveInfinity;
		for (int i = 0; i < points.Count; i += num2)
		{
			float num4 = PointCloudRegognizer.CloudDistance(points, refPoints, i);
			float num5 = PointCloudRegognizer.CloudDistance(refPoints, points, i);
			num3 = Mathf.Min(new float[]
			{
				num3,
				num4,
				num5
			});
		}
		return num3;
	}

	private static float CloudDistance(List<PointCloudRegognizer.Point> points1, List<PointCloudRegognizer.Point> points2, int startIndex)
	{
		int count = points1.Count;
		PointCloudRegognizer.ResetMatched(count);
		float num = 0f;
		int num2 = startIndex;
		do
		{
			int num3 = -1;
			float num4 = float.PositiveInfinity;
			for (int i = 0; i < count; i++)
			{
				if (!PointCloudRegognizer.matched[i])
				{
					float num5 = Vector2.Distance(points1[num2].Position, points2[i].Position);
					if (num5 < num4)
					{
						num4 = num5;
						num3 = i;
					}
				}
			}
			PointCloudRegognizer.matched[num3] = true;
			float num6 = (float)(1 - (num2 - startIndex + points1.Count) % points1.Count / points1.Count);
			num += num6 * num4;
			num2 = (num2 + 1) % points1.Count;
		}
		while (num2 != startIndex);
		return num;
	}

	private static void ResetMatched(int count)
	{
		if (PointCloudRegognizer.matched.Length < count)
		{
			PointCloudRegognizer.matched = new bool[count];
		}
		for (int i = 0; i < count; i++)
		{
			PointCloudRegognizer.matched[i] = false;
		}
	}

	protected override GestureRecognitionState OnRecognize(PointCloudGesture gesture, FingerGestures.IFingerList touches)
	{
		if (touches.Count == this.RequiredFingerCount)
		{
			gesture.Position = touches.GetAveragePosition();
			float adjustedPixelDistance = FingerGestures.GetAdjustedPixelDistance(this.MinDistanceBetweenSamples);
			Vector2 position = gesture.RawPoints[gesture.RawPoints.Count - 1].Position;
			if (Vector2.SqrMagnitude(gesture.Position - position) > adjustedPixelDistance * adjustedPixelDistance)
			{
				int strokeId = 0;
				gesture.RawPoints.Add(new PointCloudRegognizer.Point(strokeId, gesture.Position));
			}
			return GestureRecognitionState.InProgress;
		}
		if (touches.Count >= this.RequiredFingerCount)
		{
			return GestureRecognitionState.Failed;
		}
		if (this.RecognizePointCloud(gesture))
		{
			return GestureRecognitionState.Ended;
		}
		return GestureRecognitionState.Failed;
	}

	public override string GetDefaultEventMessageName()
	{
		return "OnCustomGesture";
	}

	public void OnDrawGizmosSelected()
	{
		if (this.debugLastMatchedTemplate != null)
		{
			Gizmos.color = Color.yellow;
			this.DrawNormalizedPointCloud(this.debugLastMatchedTemplate.Points, 15f);
		}
		if (this.debugLastGesture != null)
		{
			Gizmos.color = Color.green;
			this.DrawNormalizedPointCloud(this.debugLastGesture.NormalizedPoints, 15f);
		}
	}

	private void DrawNormalizedPointCloud(List<PointCloudRegognizer.Point> points, float scale)
	{
		if (points.Count > 0)
		{
			Gizmos.DrawWireSphere(scale * points[0].Position, 0.01f);
			for (int i = 1; i < points.Count; i++)
			{
				Gizmos.DrawLine(scale * points[i - 1].Position, scale * points[i].Position);
				Gizmos.DrawWireSphere(scale * points[i].Position, 0.01f);
			}
		}
	}
}
