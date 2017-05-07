using System;
using UnityEngine;

public class TestDownTest : MonoBehaviour
{
	private float wwwProcess;

	private void Update()
	{
		ResmgrNative.Instance.Update(ref this.wwwProcess);
	}
}
