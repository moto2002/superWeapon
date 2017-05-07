using System;
using UnityEngine;

[AddComponentMenu("FingerGestures/Toolbox/Drag To Move")]
public class TBDragToMove : MonoBehaviour
{
	public enum DragPlaneType
	{
		Camera,
		UseCollider
	}

	public Collider DragPlaneCollider;

	public float DragPlaneOffset;

	public Camera RaycastCamera;

	private bool dragging;

	private FingerGestures.Finger draggingFinger;

	private GestureRecognizer gestureRecognizer;

	private bool oldUseGravity;

	private bool oldIsKinematic;

	private Vector3 physxDragMove = Vector3.zero;

	public bool Dragging
	{
		get
		{
			return this.dragging;
		}
		private set
		{
			if (this.dragging != value)
			{
				this.dragging = value;
				if (base.rigidbody)
				{
					if (this.dragging)
					{
						this.oldUseGravity = base.rigidbody.useGravity;
						this.oldIsKinematic = base.rigidbody.isKinematic;
						base.rigidbody.useGravity = false;
						base.rigidbody.isKinematic = true;
					}
					else
					{
						base.rigidbody.isKinematic = this.oldIsKinematic;
						base.rigidbody.useGravity = this.oldUseGravity;
						base.rigidbody.velocity = Vector3.zero;
					}
				}
			}
		}
	}

	private void Start()
	{
		if (!this.RaycastCamera)
		{
			this.RaycastCamera = Camera.main;
		}
	}

	public bool ProjectScreenPointOnDragPlane(Vector3 refPos, Vector2 screenPos, out Vector3 worldPos)
	{
		worldPos = refPos;
		if (this.DragPlaneCollider)
		{
			Ray ray = this.RaycastCamera.ScreenPointToRay(screenPos);
			RaycastHit raycastHit;
			if (!this.DragPlaneCollider.Raycast(ray, out raycastHit, 3.40282347E+38f))
			{
				return false;
			}
			worldPos = raycastHit.point + this.DragPlaneOffset * raycastHit.normal;
		}
		else
		{
			Transform transform = this.RaycastCamera.transform;
			Plane plane = new Plane(-transform.forward, refPos);
			Ray ray2 = this.RaycastCamera.ScreenPointToRay(screenPos);
			float distance = 0f;
			if (!plane.Raycast(ray2, out distance))
			{
				return false;
			}
			worldPos = ray2.GetPoint(distance);
		}
		return true;
	}

	private void HandleDrag(DragGesture gesture)
	{
		if (!base.enabled)
		{
			return;
		}
		if (gesture.Phase == ContinuousGesturePhase.Started)
		{
			this.Dragging = true;
			this.draggingFinger = gesture.Fingers[0];
		}
		else if (this.Dragging)
		{
			if (gesture.Fingers[0] != this.draggingFinger)
			{
				return;
			}
			if (gesture.Phase == ContinuousGesturePhase.Updated)
			{
				Transform transform = base.transform;
				Vector3 b;
				Vector3 a;
				if (this.ProjectScreenPointOnDragPlane(transform.position, this.draggingFinger.PreviousPosition, out b) && this.ProjectScreenPointOnDragPlane(transform.position, this.draggingFinger.Position, out a))
				{
					Vector3 b2 = a - b;
					if (base.rigidbody)
					{
						this.physxDragMove += b2;
					}
					else
					{
						transform.position += b2;
					}
				}
			}
			else
			{
				this.Dragging = false;
			}
		}
	}

	private void FixedUpdate()
	{
		if (this.Dragging && base.rigidbody)
		{
			base.rigidbody.MovePosition(base.rigidbody.position + this.physxDragMove);
			this.physxDragMove = Vector3.zero;
		}
	}

	private void OnDrag(DragGesture gesture)
	{
		this.HandleDrag(gesture);
	}

	private void OnDisable()
	{
		if (this.Dragging)
		{
			this.Dragging = false;
		}
	}
}
