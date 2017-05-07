using System;
using UnityEngine;

public class FogOfWarPlayer : MonoBehaviour
{
	public Transform FogOfWarPlane;

	public int Number = 1;

	private void Start()
	{
	}

	private void Update()
	{
		Vector3 position = Camera.mainCamera.WorldToScreenPoint(base.transform.position);
		Ray ray = Camera.mainCamera.ScreenPointToRay(position);
		int layerMask = 256;
		RaycastHit raycastHit;
		if (Physics.Raycast(ray, out raycastHit, 1000f, layerMask))
		{
			this.FogOfWarPlane.GetComponent<Renderer>().material.SetVector("_Player" + this.Number.ToString() + "_Pos", raycastHit.point);
		}
	}
}
