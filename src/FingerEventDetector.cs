using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class FingerEventDetector<T> : FingerEventDetector where T : FingerEvent, new()
{
	public delegate void FingerEventHandler(T eventData);

	private List<T> fingerEventsList;

	protected virtual T CreateFingerEvent()
	{
		return Activator.CreateInstance<T>();
	}

	public override Type GetEventType()
	{
		return typeof(T);
	}

	protected override void Start()
	{
		base.Start();
		this.InitFingerEventsList(FingerGestures.Instance.MaxFingers);
	}

	protected virtual void InitFingerEventsList(int fingersCount)
	{
		this.fingerEventsList = new List<T>(fingersCount);
		for (int i = 0; i < fingersCount; i++)
		{
			T item = this.CreateFingerEvent();
			item.Detector = this;
			item.Finger = FingerGestures.GetFinger(i);
			this.fingerEventsList.Add(item);
		}
	}

	protected T GetEvent(FingerGestures.Finger finger)
	{
		return this.GetEvent(finger.Index);
	}

	protected virtual T GetEvent(int fingerIndex)
	{
		return this.fingerEventsList[fingerIndex];
	}
}
public abstract class FingerEventDetector : MonoBehaviour
{
	public int FingerIndexFilter = -1;

	public ScreenRaycaster Raycaster;

	public bool UseSendMessage = true;

	public bool SendMessageToSelection = true;

	public GameObject MessageTarget;

	private FingerGestures.Finger activeFinger;

	private RaycastHit lastHit = default(RaycastHit);

	internal RaycastHit LastHit
	{
		get
		{
			return this.lastHit;
		}
	}

	protected abstract void ProcessFinger(FingerGestures.Finger finger);

	public abstract Type GetEventType();

	protected virtual void Awake()
	{
		if (!this.Raycaster)
		{
			this.Raycaster = base.GetComponent<ScreenRaycaster>();
		}
		if (!this.MessageTarget)
		{
			this.MessageTarget = base.gameObject;
		}
	}

	protected virtual void Start()
	{
	}

	protected virtual void Update()
	{
		this.ProcessFingers();
	}

	protected virtual void ProcessFingers()
	{
		if (this.FingerIndexFilter >= 0 && this.FingerIndexFilter < FingerGestures.Instance.MaxFingers)
		{
			this.ProcessFinger(FingerGestures.GetFinger(this.FingerIndexFilter));
		}
		else
		{
			for (int i = 0; i < FingerGestures.Instance.MaxFingers; i++)
			{
				this.ProcessFinger(FingerGestures.GetFinger(i));
			}
		}
	}

	protected void TrySendMessage(FingerEvent eventData)
	{
		FingerGestures.FireEvent(eventData);
		if (this.UseSendMessage)
		{
			this.MessageTarget.SendMessage(eventData.Name, eventData, SendMessageOptions.DontRequireReceiver);
			if (this.SendMessageToSelection && eventData.Selection && eventData.Selection != this.MessageTarget)
			{
				eventData.Selection.SendMessage(eventData.Name, eventData, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	public GameObject PickObject(Vector2 screenPos)
	{
		if (!this.Raycaster || !this.Raycaster.enabled)
		{
			return null;
		}
		if (!this.Raycaster.Raycast(screenPos, out this.lastHit))
		{
			return null;
		}
		return this.lastHit.collider.gameObject;
	}

	protected void UpdateSelection(FingerEvent e)
	{
		e.Selection = this.PickObject(e.Position);
		e.Hit = this.LastHit;
	}
}
