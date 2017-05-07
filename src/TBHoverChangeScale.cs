using System;
using UnityEngine;

public class TBHoverChangeScale : MonoBehaviour
{
	public float hoverScaleFactor = 1.5f;

	private Vector3 originalScale = Vector3.one;

	private void Start()
	{
		this.originalScale = base.transform.localScale;
	}

	private void OnFingerHover(FingerHoverEvent e)
	{
		if (e.Phase == FingerHoverPhase.Enter)
		{
			base.transform.localScale = this.hoverScaleFactor * this.originalScale;
		}
		else
		{
			base.transform.localScale = this.originalScale;
		}
	}
}
