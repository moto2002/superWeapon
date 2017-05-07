using System;
using UnityEngine;

[AddComponentMenu("FingerGestures/Components/Screen Raycaster")]
public class ScreenRaycaster : MonoBehaviour
{
	public Camera[] Cameras;

	public LayerMask IgnoreLayerMask;

	public float RayThickness;

	public bool VisualizeRaycasts = true;

	private void Start()
	{
		if (this.Cameras == null || this.Cameras.Length == 0)
		{
			this.Cameras = new Camera[]
			{
				Camera.main
			};
		}
	}

	public bool Raycast(Vector2 screenPos, out RaycastHit hit)
	{
		Camera[] cameras = this.Cameras;
		for (int i = 0; i < cameras.Length; i++)
		{
			Camera cam = cameras[i];
			if (this.Raycast(cam, screenPos, out hit))
			{
				return true;
			}
		}
		hit = default(RaycastHit);
		return false;
	}

	private bool Raycast(Camera cam, Vector2 screenPos, out RaycastHit hit)
	{
		Ray ray = cam.ScreenPointToRay(screenPos);
		bool result;
		if (this.RayThickness > 0f)
		{
			result = Physics.SphereCast(ray, 0.5f * this.RayThickness, out hit, float.PositiveInfinity, ~this.IgnoreLayerMask);
		}
		else
		{
			result = Physics.Raycast(ray, out hit, float.PositiveInfinity, ~this.IgnoreLayerMask);
		}
		return result;
	}
}
