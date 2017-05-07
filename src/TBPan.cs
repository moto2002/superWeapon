using System;
using System.Runtime.CompilerServices;
using UnityEngine;

[AddComponentMenu("FingerGestures/Toolbox/Camera/Pan"), RequireComponent(typeof(DragRecognizer))]
public class TBPan : MonoBehaviour
{
	public delegate void PanEventHandler(TBPan source, Vector3 move);

	private Transform cachedTransform;

	public float sensitivity = 1f;

	public float smoothSpeed = 10f;

	public BoxCollider moveArea;

	private Vector3 idealPos;

	private DragGesture dragGesture;

	public event TBPan.PanEventHandler OnPan
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			this.OnPan = (TBPan.PanEventHandler)Delegate.Combine(this.OnPan, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			this.OnPan = (TBPan.PanEventHandler)Delegate.Remove(this.OnPan, value);
		}
	}

	private void Awake()
	{
		this.cachedTransform = base.transform;
	}

	private void Start()
	{
		this.idealPos = this.cachedTransform.position;
		if (!base.GetComponent<DragRecognizer>())
		{
			Debug.LogWarning("No drag recognizer found on " + base.name + ". Disabling TBPan.");
			base.enabled = false;
		}
	}

	private void OnDrag(DragGesture gesture)
	{
		this.dragGesture = ((gesture.State != GestureRecognitionState.Ended) ? gesture : null);
	}

	private void Update()
	{
		if (this.dragGesture != null && this.dragGesture.DeltaMove.SqrMagnitude() > 0f)
		{
			Vector2 vector = this.sensitivity * this.dragGesture.DeltaMove;
			Vector3 vector2 = vector.x * this.cachedTransform.right + vector.y * this.cachedTransform.up;
			this.idealPos -= vector2;
			if (this.OnPan != null)
			{
				this.OnPan(this, vector2);
			}
		}
		this.idealPos = this.ConstrainToMoveArea(this.idealPos);
		if (this.smoothSpeed > 0f)
		{
			this.cachedTransform.position = Vector3.Lerp(this.cachedTransform.position, this.idealPos, Time.deltaTime * this.smoothSpeed);
		}
		else
		{
			this.cachedTransform.position = this.idealPos;
		}
	}

	public Vector3 ConstrainToPanningPlane(Vector3 p)
	{
		Vector3 position = this.cachedTransform.InverseTransformPoint(p);
		position.z = 0f;
		return this.cachedTransform.TransformPoint(position);
	}

	public void TeleportTo(Vector3 worldPos)
	{
		this.cachedTransform.position = (this.idealPos = this.ConstrainToPanningPlane(worldPos));
	}

	public void FlyTo(Vector3 worldPos)
	{
		this.idealPos = this.ConstrainToPanningPlane(worldPos);
	}

	public Vector3 ConstrainToMoveArea(Vector3 p)
	{
		if (this.moveArea)
		{
			Vector3 min = this.moveArea.bounds.min;
			Vector3 max = this.moveArea.bounds.max;
			p.x = Mathf.Clamp(p.x, min.x, max.x);
			p.y = Mathf.Clamp(p.y, min.y, max.y);
			p.z = Mathf.Clamp(p.z, min.z, max.z);
		}
		return p;
	}
}
