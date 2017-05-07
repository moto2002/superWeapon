using System;
using UnityEngine;

[AddComponentMenu("NGUI/Examples/HUD Root")]
public class HUDRoot : MonoBehaviour
{
	public static GameObject go;

	private void Awake()
	{
		HUDRoot.go = base.gameObject;
	}
}
