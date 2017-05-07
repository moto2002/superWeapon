using System;
using UnityEngine;

public class DestoryObj : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		UnityEngine.Object.Destroy(other.gameObject);
	}
}
