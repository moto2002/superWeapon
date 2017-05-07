using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class Gesture
{
	public delegate void EventHandler(Gesture gesture);

	internal int ClusterId;

	private GestureRecognizer recognizer;

	private float startTime;

	private Vector2 startPosition = Vector2.zero;

	private Vector2 position = Vector2.zero;

	private GestureRecognitionState state;

	private GestureRecognitionState prevState;

	private FingerGestures.FingerList fingers = new FingerGestures.FingerList();

	private GameObject startSelection;

	private GameObject selection;

	private RaycastHit lastHit = default(RaycastHit);

	public event Gesture.EventHandler OnStateChanged
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			this.OnStateChanged = (Gesture.EventHandler)Delegate.Combine(this.OnStateChanged, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			this.OnStateChanged = (Gesture.EventHandler)Delegate.Remove(this.OnStateChanged, value);
		}
	}

	public FingerGestures.FingerList Fingers
	{
		get
		{
			return this.fingers;
		}
		internal set
		{
			this.fingers = value;
		}
	}

	public GestureRecognizer Recognizer
	{
		get
		{
			return this.recognizer;
		}
		internal set
		{
			this.recognizer = value;
		}
	}

	public float StartTime
	{
		get
		{
			return this.startTime;
		}
		internal set
		{
			this.startTime = value;
		}
	}

	public Vector2 StartPosition
	{
		get
		{
			return this.startPosition;
		}
		internal set
		{
			this.startPosition = value;
		}
	}

	public Vector2 Position
	{
		get
		{
			return this.position;
		}
		internal set
		{
			this.position = value;
		}
	}

	public GestureRecognitionState State
	{
		get
		{
			return this.state;
		}
		set
		{
			if (this.state != value)
			{
				this.prevState = this.state;
				this.state = value;
				if (this.OnStateChanged != null)
				{
					this.OnStateChanged(this);
				}
			}
		}
	}

	public GestureRecognitionState PreviousState
	{
		get
		{
			return this.prevState;
		}
	}

	public float ElapsedTime
	{
		get
		{
			return Time.time - this.StartTime;
		}
	}

	public GameObject StartSelection
	{
		get
		{
			return this.startSelection;
		}
		internal set
		{
			this.startSelection = value;
		}
	}

	public GameObject Selection
	{
		get
		{
			return this.selection;
		}
		internal set
		{
			this.selection = value;
		}
	}

	public RaycastHit Hit
	{
		get
		{
			return this.lastHit;
		}
		internal set
		{
			this.lastHit = value;
		}
	}

	internal GameObject PickObject(ScreenRaycaster raycaster, Vector2 screenPos)
	{
		if (!raycaster || !raycaster.enabled)
		{
			return null;
		}
		if (!raycaster.Raycast(screenPos, out this.lastHit))
		{
			return null;
		}
		return this.lastHit.collider.gameObject;
	}

	internal void PickStartSelection(ScreenRaycaster raycaster)
	{
		this.StartSelection = this.PickObject(raycaster, this.StartPosition);
		this.Selection = this.StartSelection;
	}

	internal void PickSelection(ScreenRaycaster raycaster)
	{
		this.Selection = this.PickObject(raycaster, this.Position);
	}
}
