using System;
using UnityEngine;

public class FGMouseInputProvider : FGInputProvider
{
	public int maxButtons = 3;

	public string pinchAxis = "Mouse ScrollWheel";

	public float pinchAxisScale = 100f;

	public float pinchResetTimeDelay = 0.15f;

	public float initialPinchDistance = 150f;

	public string twistAxis = "Mouse ScrollWheel";

	public float twistAxisScale = 100f;

	public KeyCode twistKey = KeyCode.LeftControl;

	public float twistResetTimeDelay = 0.15f;

	public KeyCode twistAndPinchKey = KeyCode.LeftShift;

	private Vector2 pivot = Vector2.zero;

	private Vector2[] pos = new Vector2[]
	{
		Vector2.zero,
		Vector2.zero
	};

	private bool pinching;

	private float pinchResetTime;

	private float pinchDistance;

	private bool twisting;

	private float twistAngle;

	private float twistResetTime;

	public override int MaxSimultaneousFingers
	{
		get
		{
			return this.maxButtons;
		}
	}

	private void Start()
	{
		this.pinchDistance = this.initialPinchDistance;
	}

	private void Update()
	{
		this.UpdatePinchEmulation();
		this.UpdateTwistEmulation();
		if (this.pinching || this.twisting)
		{
			this.pivot = Input.mousePosition;
			float f = 0f;
			float num = this.initialPinchDistance;
			if (this.pinching && this.twisting && Input.GetKey(this.twistAndPinchKey))
			{
				f = 0.0174532924f * this.twistAngle;
				num = this.pinchDistance;
			}
			else if (this.twisting)
			{
				f = 0.0174532924f * this.twistAngle;
			}
			else if (this.pinching)
			{
				num = this.pinchDistance;
			}
			float num2 = Mathf.Cos(f);
			float num3 = Mathf.Sin(f);
			this.pos[0].x = this.pivot.x - 0.5f * num * num2;
			this.pos[0].y = this.pivot.y - 0.5f * num * num3;
			this.pos[1].x = this.pivot.x + 0.5f * num * num2;
			this.pos[1].y = this.pivot.y + 0.5f * num * num3;
		}
	}

	private void UpdatePinchEmulation()
	{
		float num = this.pinchAxisScale * Input.GetAxis(this.pinchAxis);
		if (Mathf.Abs(num) > 0.0001f)
		{
			if (!this.pinching)
			{
				this.pinching = true;
				this.pinchDistance = this.initialPinchDistance;
			}
			this.pinchResetTime = Time.time + this.pinchResetTimeDelay;
			this.pinchDistance = Mathf.Max(5f, this.pinchDistance + num);
		}
		else if (this.pinchResetTime <= Time.time)
		{
			this.pinching = false;
			this.pinchDistance = this.initialPinchDistance;
		}
	}

	private void UpdateTwistEmulation()
	{
		float num = this.twistAxisScale * Input.GetAxis(this.twistAxis);
		if (this.twistKey != KeyCode.None && Input.GetKey(this.twistKey) && Mathf.Abs(num) > 0.0001f)
		{
			if (!this.twisting)
			{
				this.twisting = true;
				this.twistAngle = 0f;
			}
			this.twistResetTime = Time.time + this.twistResetTimeDelay;
			this.twistAngle += num;
		}
		else if (this.twistResetTime <= Time.time)
		{
			this.twisting = false;
			this.twistAngle = 0f;
		}
	}

	public override void GetInputState(int fingerIndex, out bool down, out Vector2 position)
	{
		down = Input.GetMouseButton(fingerIndex);
		position = Input.mousePosition;
		if ((this.pinching || this.twisting) && (fingerIndex == 0 || fingerIndex == 1))
		{
			down = true;
			position = this.pos[fingerIndex];
		}
	}
}
