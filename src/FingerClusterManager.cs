using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("FingerGestures/Components/Finger Cluster Manager")]
public class FingerClusterManager : MonoBehaviour
{
	[Serializable]
	public class Cluster
	{
		public int Id;

		public float StartTime;

		public FingerGestures.FingerList Fingers = new FingerGestures.FingerList();

		public void Reset()
		{
			this.Fingers.Clear();
		}
	}

	public float ClusterRadius = 250f;

	public float TimeTolerance = 0.5f;

	private int lastUpdateFrame = -1;

	private int nextClusterId = 1;

	private List<FingerClusterManager.Cluster> clusters;

	private List<FingerClusterManager.Cluster> clusterPool;

	private FingerGestures.FingerList fingersAdded;

	private FingerGestures.FingerList fingersRemoved;

	public FingerGestures.IFingerList FingersAdded
	{
		get
		{
			return this.fingersAdded;
		}
	}

	public FingerGestures.IFingerList FingersRemoved
	{
		get
		{
			return this.fingersRemoved;
		}
	}

	public List<FingerClusterManager.Cluster> Clusters
	{
		get
		{
			return this.clusters;
		}
	}

	public List<FingerClusterManager.Cluster> GetClustersPool()
	{
		return this.clusterPool;
	}

	private void Awake()
	{
		this.clusters = new List<FingerClusterManager.Cluster>();
		this.clusterPool = new List<FingerClusterManager.Cluster>();
		this.fingersAdded = new FingerGestures.FingerList();
		this.fingersRemoved = new FingerGestures.FingerList();
	}

	public void Update()
	{
		if (this.lastUpdateFrame == Time.frameCount)
		{
			return;
		}
		this.lastUpdateFrame = Time.frameCount;
		this.fingersAdded.Clear();
		this.fingersRemoved.Clear();
		for (int i = 0; i < FingerGestures.Instance.MaxFingers; i++)
		{
			FingerGestures.Finger finger = FingerGestures.GetFinger(i);
			if (finger.IsDown)
			{
				if (!finger.WasDown)
				{
					this.fingersAdded.Add(finger);
				}
			}
			else if (finger.WasDown)
			{
				this.fingersRemoved.Add(finger);
			}
		}
		foreach (FingerGestures.Finger current in this.fingersRemoved)
		{
			for (int j = this.clusters.Count - 1; j >= 0; j--)
			{
				FingerClusterManager.Cluster cluster = this.clusters[j];
				if (cluster.Fingers.Remove(current) && cluster.Fingers.Count == 0)
				{
					this.clusters.RemoveAt(j);
					this.clusterPool.Add(cluster);
				}
			}
		}
		foreach (FingerGestures.Finger current2 in this.fingersAdded)
		{
			FingerClusterManager.Cluster cluster2 = this.FindExistingCluster(current2);
			if (cluster2 == null)
			{
				cluster2 = this.NewCluster();
				cluster2.StartTime = current2.StarTime;
			}
			cluster2.Fingers.Add(current2);
		}
	}

	public FingerClusterManager.Cluster FindClusterById(int clusterId)
	{
		return this.clusters.Find((FingerClusterManager.Cluster c) => c.Id == clusterId);
	}

	private FingerClusterManager.Cluster NewCluster()
	{
		FingerClusterManager.Cluster cluster;
		if (this.clusterPool.Count == 0)
		{
			cluster = new FingerClusterManager.Cluster();
		}
		else
		{
			int index = this.clusterPool.Count - 1;
			cluster = this.clusterPool[index];
			cluster.Reset();
			this.clusterPool.RemoveAt(index);
		}
		cluster.Id = this.nextClusterId++;
		this.clusters.Add(cluster);
		return cluster;
	}

	private FingerClusterManager.Cluster FindExistingCluster(FingerGestures.Finger finger)
	{
		FingerClusterManager.Cluster result = null;
		float num = 3.40282347E+38f;
		float adjustedPixelDistance = FingerGestures.GetAdjustedPixelDistance(this.ClusterRadius);
		foreach (FingerClusterManager.Cluster current in this.clusters)
		{
			float num2 = finger.StarTime - current.StartTime;
			if (num2 <= this.TimeTolerance)
			{
				Vector2 averagePosition = current.Fingers.GetAveragePosition();
				float num3 = Vector2.SqrMagnitude(finger.Position - averagePosition);
				if (num3 < num && num3 < adjustedPixelDistance * adjustedPixelDistance)
				{
					result = current;
					num = num3;
				}
			}
		}
		return result;
	}
}
