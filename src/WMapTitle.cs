using System;
using UnityEngine;

public class WMapTitle : MonoBehaviour
{
	public Transform tar;

	protected Transform tr;

	private Camera mainCam;

	private Camera cam;

	private void Awake()
	{
		this.cam = TipsManager.inst.uiCamera;
		this.tr = base.transform;
		this.mainCam = WMap_DragManager.inst.camer;
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (this.tar == null || !this.tar.gameObject.activeSelf)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}
}
