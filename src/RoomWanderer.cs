using System;
using UnityEngine;

public class RoomWanderer : MonoBehaviour
{
	public Camera WanderCam;

	public Transform[] Spots;

	public float WanderSpeed = 1f;

	public AnimationCurve AnimCurve;

	public bool AutoStart;

	private Transform mCamTransform;

	private int mSpotIndex;

	private float mLerpTimer;

	private Vector3 mTempPos;

	private Quaternion mTempRot;

	public bool IsWandering
	{
		get;
		private set;
	}

	private void Awake()
	{
		this.mCamTransform = this.WanderCam.transform;
	}

	private void Start()
	{
		this.IsWandering = false;
		this.mSpotIndex = 0;
		this.mLerpTimer = 0f;
		this.mTempPos = Vector3.zero;
		this.mTempRot = Quaternion.identity;
		if (this.AutoStart)
		{
			this.StartWandering();
		}
	}

	private void Update()
	{
		if (this.IsWandering)
		{
			this.Wander();
		}
		if (Input.GetKeyDown(KeyCode.Q))
		{
			this.StartWandering();
		}
		else if (Input.GetKeyDown(KeyCode.W))
		{
			this.StopWander();
		}
	}

	public void StartWandering()
	{
		this.mSpotIndex = 0;
		this.mLerpTimer = 0f;
		this.mCamTransform.position = this.Spots[this.mSpotIndex].position;
		this.mCamTransform.rotation = this.Spots[this.mSpotIndex].rotation;
		this.mTempPos = this.mCamTransform.position;
		this.mTempRot = this.mCamTransform.rotation;
		this.IsWandering = true;
		if (this.Spots.Length > 1)
		{
			this.mSpotIndex++;
		}
	}

	public void StopWander()
	{
		this.IsWandering = false;
	}

	private void Wander()
	{
		float num = this.AnimCurve.Evaluate(this.mLerpTimer);
		this.mCamTransform.position = this.mTempPos + (this.Spots[this.mSpotIndex].position - this.mTempPos) * num;
		this.mCamTransform.rotation = Quaternion.Slerp(this.mTempRot, this.Spots[this.mSpotIndex].rotation, num);
		this.mLerpTimer += Time.deltaTime * this.WanderSpeed;
		if (this.mLerpTimer > 1f)
		{
			this.NextSpot();
		}
	}

	private void NextSpot()
	{
		this.mTempPos = this.mCamTransform.position;
		this.mTempRot = this.mCamTransform.rotation;
		this.mLerpTimer = 0f;
		this.mSpotIndex++;
		this.mSpotIndex %= this.Spots.Length;
	}
}
