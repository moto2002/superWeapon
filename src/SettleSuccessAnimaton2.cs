using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class SettleSuccessAnimaton2 : MonoBehaviour
{
	public Transform star1;

	public Transform star2;

	public Transform star3;

	public Transform top;

	public Transform bottom;

	public GameObject[] DataGames;

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnEnable()
	{
		base.StartCoroutine(this.Animati(0.5f));
	}

	[DebuggerHidden]
	private IEnumerator Animati(float times)
	{
		SettleSuccessAnimaton2.<Animati>c__Iterator79 <Animati>c__Iterator = new SettleSuccessAnimaton2.<Animati>c__Iterator79();
		<Animati>c__Iterator.times = times;
		<Animati>c__Iterator.<$>times = times;
		<Animati>c__Iterator.<>f__this = this;
		return <Animati>c__Iterator;
	}

	private void OnGUI()
	{
	}
}
