using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class Agent_Scene : MonoBehaviour
{
	public static Agent_Scene inst;

	public void OnDestroy()
	{
		Agent_Scene.inst = null;
	}

	private void Awake()
	{
		Agent_Scene.inst = this;
	}

	[DebuggerHidden]
	public IEnumerator InstanceScene()
	{
		Agent_Scene.<InstanceScene>c__Iterator1B <InstanceScene>c__Iterator1B = new Agent_Scene.<InstanceScene>c__Iterator1B();
		<InstanceScene>c__Iterator1B.<>f__this = this;
		return <InstanceScene>c__Iterator1B;
	}
}
