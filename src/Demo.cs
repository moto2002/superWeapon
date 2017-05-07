using System;
using UnityEngine;

public class Demo : MonoBehaviour
{
	private void Start()
	{
		LogManage.LogError("start===================>>>>>>>>>>");
	}

	private void Update()
	{
	}

	public static void echo(string str)
	{
		LogManage.LogError(str);
	}

	public static void instance()
	{
		GameObject original = Resources.Load("Sphere") as GameObject;
		GameObject gameObject = UnityEngine.Object.Instantiate(original) as GameObject;
	}
}
