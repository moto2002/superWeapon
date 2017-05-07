using System;
using UnityEngine;

public class GridAdaptation : MonoBehaviour
{
	public int size;

	private void Awake()
	{
		this.size = Screen.width;
		float num = (float)Screen.height * 1f / (float)Screen.width;
		base.transform.localPosition = new Vector3((920f - 800f * num) * -1f, 0f, 0f);
	}

	private void Update()
	{
		this.size = Screen.width;
	}
}
