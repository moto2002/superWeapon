using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UIButton))]
public class UIButtonAttach : MonoBehaviour
{
	public List<UIButton> target = new List<UIButton>();

	private UIButton curButton;

	private void Start()
	{
		this.curButton = base.GetComponent<UIButton>();
		if (this.target.Count > 0)
		{
			foreach (UIButton current in this.target)
			{
				UIButton expr_3C = this.curButton;
				expr_3C.SetStateCallBack = (Action<UIButtonColor.State, bool>)Delegate.Combine(expr_3C.SetStateCallBack, new Action<UIButtonColor.State, bool>(current.SetState));
			}
		}
		else
		{
			UnityEngine.Object.Destroy(this);
		}
	}
}
