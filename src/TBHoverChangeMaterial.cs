using System;
using UnityEngine;

public class TBHoverChangeMaterial : MonoBehaviour
{
	public Material hoverMaterial;

	private Material normalMaterial;

	private void Start()
	{
		this.normalMaterial = base.renderer.sharedMaterial;
	}

	private void OnFingerHover(FingerHoverEvent e)
	{
		if (e.Phase == FingerHoverPhase.Enter)
		{
			base.renderer.sharedMaterial = this.hoverMaterial;
		}
		else
		{
			base.renderer.sharedMaterial = this.normalMaterial;
		}
	}
}
