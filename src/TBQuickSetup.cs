using System;
using UnityEngine;

[AddComponentMenu("FingerGestures/Toolbox/Quick Setup")]
public class TBQuickSetup : MonoBehaviour
{
	public GameObject MessageTarget;

	public int MaxSimultaneousGestures = 2;

	private ScreenRaycaster screenRaycaster;

	[HideInInspector]
	public FingerDownDetector FingerDown;

	[HideInInspector]
	public FingerUpDetector FingerUp;

	[HideInInspector]
	public FingerHoverDetector FingerHover;

	[HideInInspector]
	public FingerMotionDetector FingerMotion;

	[HideInInspector]
	public DragRecognizer Drag;

	[HideInInspector]
	public LongPressRecognizer LongPress;

	[HideInInspector]
	public SwipeRecognizer Swipe;

	[HideInInspector]
	public TapRecognizer Tap;

	[HideInInspector]
	public PinchRecognizer Pinch;

	[HideInInspector]
	public TwistRecognizer Twist;

	[HideInInspector]
	public TapRecognizer DoubleTap;

	[HideInInspector]
	public DragRecognizer TwoFingerDrag;

	[HideInInspector]
	public TapRecognizer TwoFingerTap;

	[HideInInspector]
	public SwipeRecognizer TwoFingerSwipe;

	[HideInInspector]
	public LongPressRecognizer TwoFingerLongPress;

	private GameObject CreateChildNode(string name)
	{
		GameObject gameObject = new GameObject(name);
		Transform transform = gameObject.transform;
		transform.parent = base.transform;
		transform.localPosition = Vector3.zero;
		transform.localRotation = Quaternion.identity;
		return gameObject;
	}

	private void Start()
	{
		if (!this.MessageTarget)
		{
			this.MessageTarget = base.gameObject;
		}
		this.screenRaycaster = base.GetComponent<ScreenRaycaster>();
		if (!this.screenRaycaster)
		{
			this.screenRaycaster = base.gameObject.AddComponent<ScreenRaycaster>();
		}
		if (!FingerGestures.Instance)
		{
			base.gameObject.AddComponent<FingerGestures>();
		}
		GameObject node = this.CreateChildNode("Finger Event Detectors");
		this.FingerDown = this.AddFingerEventDetector<FingerDownDetector>(node);
		this.FingerUp = this.AddFingerEventDetector<FingerUpDetector>(node);
		this.FingerMotion = this.AddFingerEventDetector<FingerMotionDetector>(node);
		this.FingerHover = this.AddFingerEventDetector<FingerHoverDetector>(node);
		GameObject node2 = this.CreateChildNode("Single Finger Gestures");
		this.Drag = this.AddSingleFingerGesture<DragRecognizer>(node2);
		this.Tap = this.AddSingleFingerGesture<TapRecognizer>(node2);
		this.Swipe = this.AddSingleFingerGesture<SwipeRecognizer>(node2);
		this.LongPress = this.AddSingleFingerGesture<LongPressRecognizer>(node2);
		this.DoubleTap = this.AddSingleFingerGesture<TapRecognizer>(node2);
		this.DoubleTap.RequiredTaps = 2;
		this.DoubleTap.EventMessageName = "OnDoubleTap";
		GameObject node3 = this.CreateChildNode("Two-Finger Gestures");
		this.Pinch = this.AddTwoFingerGesture<PinchRecognizer>(node3);
		this.Twist = this.AddTwoFingerGesture<TwistRecognizer>(node3);
		this.TwoFingerDrag = this.AddTwoFingerGesture<DragRecognizer>(node3, "OnTwoFingerDrag");
		this.TwoFingerTap = this.AddTwoFingerGesture<TapRecognizer>(node3, "OnTwoFingerTap");
		this.TwoFingerSwipe = this.AddTwoFingerGesture<SwipeRecognizer>(node3, "OnTwoFingerSwipe");
		this.TwoFingerLongPress = this.AddTwoFingerGesture<LongPressRecognizer>(node3, "OnTwoFingerLongPress");
	}

	private T AddFingerEventDetector<T>(GameObject node) where T : FingerEventDetector
	{
		T t = node.AddComponent<T>();
		t.Raycaster = this.screenRaycaster;
		t.MessageTarget = this.MessageTarget;
		return t;
	}

	private T AddGesture<T>(GameObject node) where T : GestureRecognizer
	{
		T t = node.AddComponent<T>();
		t.Raycaster = this.screenRaycaster;
		t.EventMessageTarget = this.MessageTarget;
		if (t.SupportFingerClustering)
		{
			t.MaxSimultaneousGestures = this.MaxSimultaneousGestures;
		}
		return t;
	}

	private T AddSingleFingerGesture<T>(GameObject node) where T : GestureRecognizer
	{
		T result = this.AddGesture<T>(node);
		result.RequiredFingerCount = 1;
		return result;
	}

	private T AddTwoFingerGesture<T>(GameObject node) where T : GestureRecognizer
	{
		T result = this.AddGesture<T>(node);
		result.RequiredFingerCount = 2;
		return result;
	}

	private T AddTwoFingerGesture<T>(GameObject node, string eventName) where T : GestureRecognizer
	{
		T t = this.AddTwoFingerGesture<T>(node);
		t.EventMessageName = eventName;
		return t;
	}
}
