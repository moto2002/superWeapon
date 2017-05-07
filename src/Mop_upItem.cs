using System;
using UnityEngine;

public class Mop_upItem : MonoBehaviour
{
	public static Mop_upItem inst;

	public UILabel CommandLabel;

	public UILabel PetroleumLabel;

	public UILabel SteelLabel;

	public UILabel REOLabel;

	public GameObject propSprite;

	public UILabel NumLabel;

	public Transform gredTrans;

	public int Id;

	public void OnDestroy()
	{
		Mop_upItem.inst = null;
	}

	private void Awake()
	{
		Mop_upItem.inst = this;
	}
}
