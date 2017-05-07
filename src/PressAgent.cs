using System;
using UnityEngine;

public class PressAgent : MonoBehaviour
{
	private void OnPress(bool isPress)
	{
		WMap_DragManager.inst.btnInUse = isPress;
	}
}
