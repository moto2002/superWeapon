using BattleEvent;
using System;
using UnityEngine;

public class MovePoint : MonoBehaviour
{
	public static MovePoint inst;

	public int type;

	public static Character target;

	public static RandomEventMonoBehaviour targetEventBox;

	private void Awake()
	{
		MovePoint.inst = this;
	}

	private void Start()
	{
	}

	public static void ChangeTaget(Character tar)
	{
		if (tar != null && MovePoint.target != null && tar.Equals(MovePoint.target))
		{
			return;
		}
		if (MovePoint.target != null)
		{
			MovePoint.target.ChangeSelectState(Character.CharacterSelectStates.Idle);
		}
		if (MovePoint.targetEventBox != null)
		{
			MovePoint.targetEventBox.SelectState.ChangeState(Character.CharacterSelectStates.Idle);
			MovePoint.targetEventBox = null;
		}
		if (tar != null)
		{
			tar.ChangeSelectState(Character.CharacterSelectStates.Selected);
		}
		else if (MovePoint.inst)
		{
			Body_Model modelByBundleByName = PoolManage.Ins.GetModelByBundleByName("JT3", MovePoint.inst.transform);
			if (modelByBundleByName != null)
			{
				modelByBundleByName.tr.localScale = new Vector3(0.25f, 0.25f, 0.25f);
				modelByBundleByName.DesInsInPool(0.3f);
			}
		}
		MovePoint.target = tar;
	}

	public static void ChangeEventTaget(RandomEventMonoBehaviour randomEventBox)
	{
		if (MovePoint.targetEventBox != null && randomEventBox != null && MovePoint.targetEventBox.Equals(randomEventBox))
		{
			return;
		}
		if (MovePoint.target != null)
		{
			MovePoint.target.ChangeSelectState(Character.CharacterSelectStates.Idle);
			MovePoint.target = null;
		}
		if (randomEventBox != null)
		{
			randomEventBox.SelectState.ChangeState(Character.CharacterSelectStates.Selected);
		}
		if (MovePoint.targetEventBox != null)
		{
			MovePoint.targetEventBox.SelectState.ChangeState(Character.CharacterSelectStates.Idle);
		}
		MovePoint.targetEventBox = randomEventBox;
	}
}
