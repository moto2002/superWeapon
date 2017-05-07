using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class RenderQueueEdit : MonoBehaviour
{
	private int PanelRenderQueue = 2500;

	private void OnEnable()
	{
		base.StartCoroutine(this.SetRenderQueue());
	}

	[DebuggerHidden]
	private IEnumerator SetRenderQueue()
	{
		RenderQueueEdit.<SetRenderQueue>c__Iterator99 <SetRenderQueue>c__Iterator = new RenderQueueEdit.<SetRenderQueue>c__Iterator99();
		<SetRenderQueue>c__Iterator.<>f__this = this;
		return <SetRenderQueue>c__Iterator;
	}

	public static T GetCompentByParent<T>(GameObject ga) where T : Component
	{
		T componentInParent = ga.GetComponentInParent<T>();
		if (componentInParent)
		{
			return componentInParent;
		}
		return RenderQueueEdit.GetCompentByParent<T>(ga.transform.parent);
	}

	public static T GetCompentByParent<T>(Transform ga) where T : Component
	{
		if (!ga)
		{
			return (T)((object)null);
		}
		T componentInParent = ga.GetComponentInParent<T>();
		if (componentInParent)
		{
			return componentInParent;
		}
		return RenderQueueEdit.GetCompentByParent<T>(ga.transform.parent);
	}
}
