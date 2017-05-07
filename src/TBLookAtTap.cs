using System;
using UnityEngine;

[AddComponentMenu("FingerGestures/Toolbox/Camera/Look At Tap"), RequireComponent(typeof(TapRecognizer))]
public class TBLookAtTap : MonoBehaviour
{
	private TBDragView dragView;

	private void Awake()
	{
		this.dragView = base.GetComponent<TBDragView>();
	}

	private void Start()
	{
		if (!base.GetComponent<TapRecognizer>())
		{
			Debug.LogWarning("No tap recognizer found on " + base.name + ". Disabling TBLookAtTap.");
			base.enabled = false;
		}
	}

	private void OnTap(TapGesture gesture)
	{
		Ray ray = Camera.main.ScreenPointToRay(gesture.Position);
		RaycastHit raycastHit;
		if (Physics.Raycast(ray, out raycastHit))
		{
			if (this.dragView)
			{
				this.dragView.LookAt(raycastHit.point);
			}
			else
			{
				base.transform.LookAt(raycastHit.point);
			}
		}
	}
}
