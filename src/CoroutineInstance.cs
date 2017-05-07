using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class CoroutineInstance : MonoBehaviour
{
	public void Awake()
	{
		GameTools.DontDestroyOnLoad(base.gameObject);
	}

	public Coroutine ProcessWork(IEnumerator routine)
	{
		return base.StartCoroutine(this.DestroyWhenComplete(routine));
	}

	[DebuggerHidden]
	public IEnumerator DestroyWhenComplete(IEnumerator routine)
	{
		CoroutineInstance.<DestroyWhenComplete>c__Iterator97 <DestroyWhenComplete>c__Iterator = new CoroutineInstance.<DestroyWhenComplete>c__Iterator97();
		<DestroyWhenComplete>c__Iterator.routine = routine;
		<DestroyWhenComplete>c__Iterator.<$>routine = routine;
		<DestroyWhenComplete>c__Iterator.<>f__this = this;
		return <DestroyWhenComplete>c__Iterator;
	}

	public static GameObject DoJob(IEnumerator routine)
	{
		GameObject gameTemp = PoolManage.Ins.GetGameTemp();
		if (gameTemp.GetComponent<CoroutineInstance>() == null)
		{
			gameTemp.AddComponent<CoroutineInstance>().ProcessWork(routine);
		}
		else
		{
			gameTemp.GetComponent<CoroutineInstance>().ProcessWork(routine);
		}
		return gameTemp;
	}
}
