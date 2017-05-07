using System;
using UnityEngine;

public class LangSwitcher : MonoBehaviour
{
	private UILabel Label;

	private void Start()
	{
		this.Label = base.GetComponent<UILabel>();
	}
}
