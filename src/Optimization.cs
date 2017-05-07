using System;
using UnityEngine;

public class Optimization
{
	public static void WriteBundle(string objName)
	{
		if (Application.platform == RuntimePlatform.WindowsEditor)
		{
			GameTools.CreateFile("D:/", "Optimization.txt", objName);
		}
	}
}
