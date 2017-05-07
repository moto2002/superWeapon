using System;
using UnityEngine;

public class FogOfWarUnit : MonoBehaviour
{
	public float radius = 5f;

	private float _nextUpdate;

	public LayerMask lineOfSightMask = 0;

	private Transform _transform;

	public float updateFrequency
	{
		get
		{
			return FogOfWar.current.updateFrequency;
		}
	}

	private void Start()
	{
		this._transform = base.transform;
		this._nextUpdate = UnityEngine.Random.Range(0f, this.updateFrequency);
	}

	private void Update()
	{
		this._nextUpdate -= Time.deltaTime;
		if (this._nextUpdate > 0f)
		{
			return;
		}
		this._nextUpdate = this.updateFrequency;
		FogOfWar.current.Unfog(this._transform.position, this.radius, this.lineOfSightMask);
	}
}
