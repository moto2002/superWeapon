using System;
using UnityEngine;

public class D3CamAda : MonoBehaviour
{
	public Anchors3D anchors = Anchors3D.right;

	public float baseX = 2.7f;

	public float dis = 10f;

	private void Start()
	{
		float x = this.baseX * ((float)Screen.width * 1f / (float)Screen.height) * (float)this.anchors;
		base.transform.localPosition = new Vector3(x, 0f, this.dis);
	}
}
