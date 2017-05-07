using System;
using UnityEngine;

public class WMapDrag : MonoBehaviour
{
	public static WMapDrag inst;

	public void OnDestroy()
	{
		WMapDrag.inst = null;
	}

	private void Awake()
	{
		WMapDrag.inst = this;
	}
}
