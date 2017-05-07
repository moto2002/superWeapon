using System;
using UnityEngine;

public class PlayParticleRender : MonoBehaviour
{
	public ParticleSystemRenderer partRender;

	public ParticleSystem System;

	public void Awake()
	{
		this.partRender = base.GetComponent<ParticleSystemRenderer>();
	}

	public void PlayParticle(float length)
	{
		this.partRender.lengthScale = length * -1f / this.System.startSize;
		if (!this.System.isPlaying)
		{
			this.System.Play();
		}
	}
}
