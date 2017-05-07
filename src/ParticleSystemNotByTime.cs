using System;
using UnityEngine;

public class ParticleSystemNotByTime : MonoBehaviour
{
	private ParticleSystem[] _particleSystem;

	private void Awake()
	{
		this._particleSystem = base.GetComponentsInChildren<ParticleSystem>(true);
	}

	public void Update()
	{
		for (int i = 0; i < this._particleSystem.Length; i++)
		{
			this._particleSystem[i].Simulate(Time.unscaledDeltaTime, true, false);
		}
	}
}
