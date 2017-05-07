using System;
using System.Collections.Generic;
using UnityEngine;

public class ResInfo
{
	public int typeId;

	public string info;

	public TowerList[] towers;

	public List<Vector3> landingInfo = new List<Vector3>();

	public List<Vector3> noMovingInfo = new List<Vector3>();

	public Vector3[] randomBox;

	public Vector3[] landingArmyArea;

	public Vector4 MoveArea = default(Vector4);
}
