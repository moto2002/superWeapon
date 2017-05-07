using System;
using UnityEngine;

public class GmaeAnnounceItem : MonoBehaviour
{
	public UILabel showinfo;

	public UIScrollView _ScrllView;

	public void OnEnable()
	{
		this._ScrllView.ResetPosition();
	}
}
