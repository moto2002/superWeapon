using System;
using UnityEngine;

public class FingerHoverEvent : FingerEvent
{
	private FingerHoverPhase phase;

	internal GameObject PreviousSelection;

	public FingerHoverPhase Phase
	{
		get
		{
			return this.phase;
		}
		internal set
		{
			this.phase = value;
		}
	}
}
