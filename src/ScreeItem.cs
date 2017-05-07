using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ScreeItem : MonoBehaviour
{
	private BoxCollider box;

	private void Awake()
	{
		this.box = base.GetComponent<BoxCollider>();
	}

	private void OnEnable()
	{
		this.box.size = new Vector3((float)Screen.width / 20f, (float)Screen.height / 20f, 0f);
	}
}
