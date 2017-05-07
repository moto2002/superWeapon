using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class GestureRecognizer<T> : GestureRecognizer where T : Gesture, new()
{
	public delegate void GestureEventHandler(T gesture);

	private List<T> gestures;

	private static FingerGestures.FingerList tempTouchList = new FingerGestures.FingerList();

	public event GestureRecognizer<T>.GestureEventHandler OnGesture
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			this.OnGesture = (GestureRecognizer<T>.GestureEventHandler)System.Delegate.Combine(this.OnGesture, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			this.OnGesture = (GestureRecognizer<T>.GestureEventHandler)System.Delegate.Remove(this.OnGesture, value);
		}
	}

	public List<T> Gestures
	{
		get
		{
			return this.gestures;
		}
	}

	protected override void Start()
	{
		base.Start();
		this.InitGestures();
	}

	protected override void OnEnable()
	{
		base.OnEnable();
	}

	private void InitGestures()
	{
		if (this.gestures == null)
		{
			this.gestures = new List<T>();
			for (int i = 0; i < this.MaxSimultaneousGestures; i++)
			{
				T item = this.CreateGesture();
				item.OnStateChanged += new Gesture.EventHandler(this.OnStateChanged);
				item.Recognizer = this;
				this.gestures.Add(item);
			}
		}
	}

	protected virtual bool CanBegin(T gesture, FingerGestures.IFingerList touches)
	{
		return touches.Count == this.RequiredFingerCount && (!this.IsExclusive || FingerGestures.Touches.Count == this.RequiredFingerCount) && (!this.Delegate || this.Delegate.CanBegin(gesture, touches));
	}

	protected abstract void OnBegin(T gesture, FingerGestures.IFingerList touches);

	protected abstract GestureRecognitionState OnRecognize(T gesture, FingerGestures.IFingerList touches);

	protected virtual GameObject GetDefaultSelectionForSendMessage(T gesture)
	{
		return gesture.Selection;
	}

	protected virtual T CreateGesture()
	{
		return Activator.CreateInstance<T>();
	}

	public override Type GetGestureType()
	{
		return typeof(T);
	}

	protected virtual void OnStateChanged(Gesture gesture)
	{
	}

	protected virtual T FindGestureByCluster(FingerClusterManager.Cluster cluster)
	{
		return this.gestures.Find((T g) => g.ClusterId == cluster.Id);
	}

	protected virtual T MatchActiveGestureToCluster(FingerClusterManager.Cluster cluster)
	{
		return (T)((object)null);
	}

	protected virtual T FindFreeGesture()
	{
		return this.gestures.Find((T g) => g.State == GestureRecognitionState.Ready);
	}

	protected virtual void Reset(T gesture)
	{
		this.ReleaseFingers(gesture);
		gesture.ClusterId = 0;
		gesture.Fingers.Clear();
		gesture.State = GestureRecognitionState.Ready;
	}

	public virtual void Update()
	{
		if (!this.IsExclusive && this.SupportFingerClustering && this.ClusterManager)
		{
			this.UpdateUsingClusters();
		}
		else if (this.IsExclusive || this.RequiredFingerCount > 1)
		{
			this.UpdateExclusive();
		}
		else
		{
			this.UpdatePerFinger();
		}
	}

	private void UpdateUsingClusters()
	{
		this.ClusterManager.Update();
		foreach (FingerClusterManager.Cluster current in this.ClusterManager.Clusters)
		{
			this.ProcessCluster(current);
		}
		foreach (T current2 in this.gestures)
		{
			FingerClusterManager.Cluster cluster = this.ClusterManager.FindClusterById(current2.ClusterId);
			FingerGestures.IFingerList arg_9A_0;
			if (cluster != null)
			{
				FingerGestures.IFingerList fingers = cluster.Fingers;
				arg_9A_0 = fingers;
			}
			else
			{
				arg_9A_0 = GestureRecognizer.EmptyFingerList;
			}
			FingerGestures.IFingerList touches = arg_9A_0;
			this.UpdateGesture(current2, touches);
		}
	}

	private void UpdateExclusive()
	{
		T gesture = this.gestures[0];
		FingerGestures.IFingerList touches = FingerGestures.Touches;
		if (gesture.State == GestureRecognitionState.Ready && this.CanBegin(gesture, touches))
		{
			this.Begin(gesture, 0, touches);
		}
		this.UpdateGesture(gesture, touches);
	}

	private void UpdatePerFinger()
	{
		int num = 0;
		while (num < FingerGestures.Instance.MaxFingers && num < this.MaxSimultaneousGestures)
		{
			FingerGestures.Finger finger = FingerGestures.GetFinger(num);
			T gesture = this.Gestures[num];
			FingerGestures.FingerList fingerList = GestureRecognizer<T>.tempTouchList;
			fingerList.Clear();
			if (finger.IsDown)
			{
				fingerList.Add(finger);
			}
			if (gesture.State == GestureRecognitionState.Ready && this.CanBegin(gesture, fingerList))
			{
				this.Begin(gesture, 0, fingerList);
			}
			this.UpdateGesture(gesture, fingerList);
			num++;
		}
	}

	protected virtual void ProcessCluster(FingerClusterManager.Cluster cluster)
	{
		if (this.FindGestureByCluster(cluster) != null)
		{
			return;
		}
		if (cluster.Fingers.Count != this.RequiredFingerCount)
		{
			return;
		}
		T t = this.MatchActiveGestureToCluster(cluster);
		if (t != null)
		{
			t.ClusterId = cluster.Id;
		}
		else
		{
			t = this.FindFreeGesture();
			if (t == null)
			{
				return;
			}
			if (!this.CanBegin(t, cluster.Fingers))
			{
				return;
			}
			this.Begin(t, cluster.Id, cluster.Fingers);
		}
	}

	private void ReleaseFingers(T gesture)
	{
		foreach (FingerGestures.Finger current in gesture.Fingers)
		{
			base.Release(current);
		}
	}

	private void Begin(T gesture, int clusterId, FingerGestures.IFingerList touches)
	{
		gesture.ClusterId = clusterId;
		gesture.StartTime = Time.time;
		foreach (FingerGestures.Finger current in touches)
		{
			gesture.Fingers.Add(current);
			base.Acquire(current);
		}
		this.OnBegin(gesture, touches);
		gesture.PickStartSelection(this.Raycaster);
		gesture.State = GestureRecognitionState.Started;
	}

	protected virtual FingerGestures.IFingerList GetTouches(T gesture)
	{
		if (this.SupportFingerClustering && this.ClusterManager)
		{
			FingerClusterManager.Cluster cluster = this.ClusterManager.FindClusterById(gesture.ClusterId);
			FingerGestures.IFingerList arg_4A_0;
			if (cluster != null)
			{
				FingerGestures.IFingerList fingers = cluster.Fingers;
				arg_4A_0 = fingers;
			}
			else
			{
				arg_4A_0 = GestureRecognizer.EmptyFingerList;
			}
			return arg_4A_0;
		}
		return FingerGestures.Touches;
	}

	protected virtual void UpdateGesture(T gesture, FingerGestures.IFingerList touches)
	{
		if (gesture.State == GestureRecognitionState.Ready)
		{
			return;
		}
		if (gesture.State == GestureRecognitionState.Started)
		{
			gesture.State = GestureRecognitionState.InProgress;
		}
		switch (gesture.State)
		{
		case GestureRecognitionState.InProgress:
		{
			GestureRecognitionState gestureRecognitionState = this.OnRecognize(gesture, touches);
			if (gestureRecognitionState == GestureRecognitionState.InProgress)
			{
				gesture.PickSelection(this.Raycaster);
			}
			gesture.State = gestureRecognitionState;
			break;
		}
		case GestureRecognitionState.Failed:
		case GestureRecognitionState.Ended:
			if (gesture.PreviousState != gesture.State)
			{
				this.ReleaseFingers(gesture);
			}
			if (this.ResetMode == GestureResetMode.NextFrame || (this.ResetMode == GestureResetMode.EndOfTouchSequence && touches.Count == 0))
			{
				this.Reset(gesture);
			}
			break;
		default:
			Debug.LogError(string.Concat(new object[]
			{
				this,
				" - Unhandled state: ",
				gesture.State,
				". Failing gesture."
			}));
			gesture.State = GestureRecognitionState.Failed;
			break;
		}
	}

	protected void RaiseEvent(T gesture)
	{
		if (this.OnGesture != null)
		{
			this.OnGesture(gesture);
		}
		FingerGestures.FireEvent(gesture);
		if (this.UseSendMessage && !string.IsNullOrEmpty(this.EventMessageName))
		{
			if (this.EventMessageTarget)
			{
				this.EventMessageTarget.SendMessage(this.EventMessageName, gesture, SendMessageOptions.DontRequireReceiver);
			}
			if (this.SendMessageToSelection != GestureRecognizer.SelectionType.None)
			{
				GameObject gameObject = null;
				switch (this.SendMessageToSelection)
				{
				case GestureRecognizer.SelectionType.Default:
					gameObject = this.GetDefaultSelectionForSendMessage(gesture);
					break;
				case GestureRecognizer.SelectionType.StartSelection:
					gameObject = gesture.StartSelection;
					break;
				case GestureRecognizer.SelectionType.CurrentSelection:
					gameObject = gesture.Selection;
					break;
				}
				if (gameObject && gameObject != this.EventMessageTarget)
				{
					gameObject.SendMessage(this.EventMessageName, gesture, SendMessageOptions.DontRequireReceiver);
				}
			}
		}
	}
}
public abstract class GestureRecognizer : MonoBehaviour
{
	public enum SelectionType
	{
		Default,
		StartSelection,
		CurrentSelection,
		None
	}

	protected static readonly FingerGestures.IFingerList EmptyFingerList = new FingerGestures.FingerList();

	[SerializeField]
	private int requiredFingerCount = 1;

	public int MaxSimultaneousGestures = 1;

	public GestureResetMode ResetMode;

	public ScreenRaycaster Raycaster;

	public FingerClusterManager ClusterManager;

	public GestureRecognizerDelegate Delegate;

	public bool UseSendMessage = true;

	public string EventMessageName;

	public GameObject EventMessageTarget;

	public GestureRecognizer.SelectionType SendMessageToSelection;

	public bool IsExclusive;

	public virtual int RequiredFingerCount
	{
		get
		{
			return this.requiredFingerCount;
		}
		set
		{
			this.requiredFingerCount = value;
		}
	}

	public virtual bool SupportFingerClustering
	{
		get
		{
			return true;
		}
	}

	public virtual GestureResetMode GetDefaultResetMode()
	{
		return GestureResetMode.EndOfTouchSequence;
	}

	public abstract string GetDefaultEventMessageName();

	public abstract Type GetGestureType();

	protected virtual void Awake()
	{
		if (string.IsNullOrEmpty(this.EventMessageName))
		{
			this.EventMessageName = this.GetDefaultEventMessageName();
		}
		if (this.ResetMode == GestureResetMode.Default)
		{
			this.ResetMode = this.GetDefaultResetMode();
		}
		if (!this.EventMessageTarget)
		{
			this.EventMessageTarget = base.gameObject;
		}
		if (!this.Raycaster)
		{
			this.Raycaster = base.GetComponent<ScreenRaycaster>();
		}
	}

	protected virtual void OnEnable()
	{
		FingerGestures.Register(this);
	}

	protected virtual void OnDisable()
	{
		FingerGestures.Unregister(this);
	}

	protected void Acquire(FingerGestures.Finger finger)
	{
		if (!finger.GestureRecognizers.Contains(this))
		{
			finger.GestureRecognizers.Add(this);
		}
	}

	protected bool Release(FingerGestures.Finger finger)
	{
		return finger.GestureRecognizers.Remove(this);
	}

	protected virtual void Start()
	{
		if (!this.ClusterManager && this.SupportFingerClustering)
		{
			this.ClusterManager = base.GetComponent<FingerClusterManager>();
			if (!this.ClusterManager)
			{
				this.ClusterManager = FingerGestures.DefaultClusterManager;
			}
		}
	}

	protected bool Young(FingerGestures.IFingerList touches)
	{
		FingerGestures.Finger oldest = touches.GetOldest();
		if (oldest == null)
		{
			return false;
		}
		float num = Time.time - oldest.StarTime;
		return num < 0.25f;
	}
}
