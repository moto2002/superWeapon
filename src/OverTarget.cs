using System;
using UnityEngine;

public class OverTarget : IMonoBehaviour
{
	public Transform Target;

	public void Update()
	{
		if (this.Target != null)
		{
			Camera camera = NGUITools.FindCameraForLayer(this.Target.gameObject.layer);
			Vector3 vector = camera.WorldToScreenPoint(this.Target.position);
			vector.z = 0f;
			vector += new Vector3(0f, 80f, 0f);
			this.tr.position = NGUITools.FindCameraForLayer(FuncUIManager.inst.gameObject.layer).ScreenToWorldPoint(vector);
		}
	}
}
