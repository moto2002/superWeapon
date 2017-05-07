using System;
using UnityEngine;

public class T_Trail : MonoBehaviour
{
	private ParticleSystem trailPart;

	private void Start()
	{
		this.trailPart = base.GetComponent<ParticleSystem>();
	}

	private void Update()
	{
		if (this.trailPart.isStopped)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}
}
