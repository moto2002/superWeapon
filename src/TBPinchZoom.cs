using System;
using UnityEngine;

[AddComponentMenu("FingerGestures/Toolbox/Camera/Pinch-Zoom"), RequireComponent(typeof(Camera)), RequireComponent(typeof(PinchRecognizer))]
public class TBPinchZoom : MonoBehaviour
{
	public enum ZoomMethod
	{
		Position,
		FOV
	}

	public TBPinchZoom.ZoomMethod zoomMethod;

	public float zoomSpeed = 1.5f;

	public float minZoomAmount;

	public float maxZoomAmount = 50f;

	private Vector3 defaultPos = Vector3.zero;

	private float defaultFov;

	private float defaultOrthoSize;

	private float zoomAmount;

	public Vector3 DefaultPos
	{
		get
		{
			return this.defaultPos;
		}
		set
		{
			this.defaultPos = value;
		}
	}

	public float DefaultFov
	{
		get
		{
			return this.defaultFov;
		}
		set
		{
			this.defaultFov = value;
		}
	}

	public float DefaultOrthoSize
	{
		get
		{
			return this.defaultOrthoSize;
		}
		set
		{
			this.defaultOrthoSize = value;
		}
	}

	public float ZoomAmount
	{
		get
		{
			return this.zoomAmount;
		}
		set
		{
			this.zoomAmount = Mathf.Clamp(value, this.minZoomAmount, this.maxZoomAmount);
			TBPinchZoom.ZoomMethod zoomMethod = this.zoomMethod;
			if (zoomMethod != TBPinchZoom.ZoomMethod.Position)
			{
				if (zoomMethod == TBPinchZoom.ZoomMethod.FOV)
				{
					if (base.camera.orthographic)
					{
						base.camera.orthographicSize = Mathf.Max(this.defaultOrthoSize - this.zoomAmount, 0.1f);
					}
					else
					{
						base.camera.fov = Mathf.Max(this.defaultFov - this.zoomAmount, 0.1f);
					}
				}
			}
			else
			{
				base.transform.position = this.defaultPos + this.zoomAmount * base.transform.forward;
			}
		}
	}

	public float ZoomPercent
	{
		get
		{
			return (this.ZoomAmount - this.minZoomAmount) / (this.maxZoomAmount - this.minZoomAmount);
		}
	}

	private void Start()
	{
		if (!base.GetComponent<PinchRecognizer>())
		{
			Debug.LogWarning("No pinch recognizer found on " + base.name + ". Disabling TBPinchZoom.");
			base.enabled = false;
		}
		this.SetDefaults();
	}

	public void SetDefaults()
	{
		this.DefaultPos = base.transform.position;
		this.DefaultFov = base.camera.fov;
		this.DefaultOrthoSize = base.camera.orthographicSize;
	}

	private void OnPinch(PinchGesture gesture)
	{
		this.ZoomAmount += this.zoomSpeed * gesture.Delta;
	}
}
