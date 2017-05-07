using msg;
using System;
using UnityEngine;

public class TowerStrenghtHandler : MonoBehaviour
{
	private static Action UpGrade;

	private static Action Strength;

	public static void CS_TowerStrength(long id, Action fucn = null)
	{
		TowerStrenghtHandler.Strength = fucn;
		CSTowerStrength cSTowerStrength = new CSTowerStrength();
		cSTowerStrength.buildingId = id;
		ClientMgr.GetNet().SendHttp(2020, cSTowerStrength, new DataHandler.OpcodeHandler(TowerStrenghtHandler.OnStrength), null);
	}

	public static void OnStrength(bool isError, Opcode opcode)
	{
		if (TowerStrenghtHandler.Strength != null)
		{
			TowerStrenghtHandler.Strength();
		}
	}

	public static void CS_TowerUpGrade(long id, Action func = null)
	{
		TowerStrenghtHandler.UpGrade = func;
		CSTowerUpGrade cSTowerUpGrade = new CSTowerUpGrade();
		cSTowerUpGrade.buildingId = id;
		ClientMgr.GetNet().SendHttp(2018, cSTowerUpGrade, new DataHandler.OpcodeHandler(TowerStrenghtHandler.OnUpGrade), null);
	}

	public static void OnUpGrade(bool isError, Opcode opcode)
	{
		if (TowerStrenghtHandler.UpGrade != null)
		{
			TowerStrenghtHandler.UpGrade();
		}
	}
}
