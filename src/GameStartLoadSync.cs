using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GameStartLoadSync : MonoBehaviour
{
	public static List<GameObject> AllNeedDelInReStartGame = new List<GameObject>();

	private void Start()
	{
		base.StartCoroutine(this.StartLoading_1(1));
	}

	[DebuggerHidden]
	private IEnumerator StartLoading_1(int scene)
	{
		GameStartLoadSync.<StartLoading_1>c__Iterator43 <StartLoading_1>c__Iterator = new GameStartLoadSync.<StartLoading_1>c__Iterator43();
		<StartLoading_1>c__Iterator.scene = scene;
		<StartLoading_1>c__Iterator.<$>scene = scene;
		return <StartLoading_1>c__Iterator;
	}

	[DebuggerHidden]
	private IEnumerator StartLoading_4(int scene)
	{
		GameStartLoadSync.<StartLoading_4>c__Iterator44 <StartLoading_4>c__Iterator = new GameStartLoadSync.<StartLoading_4>c__Iterator44();
		<StartLoading_4>c__Iterator.scene = scene;
		<StartLoading_4>c__Iterator.<$>scene = scene;
		return <StartLoading_4>c__Iterator;
	}
}
