using System;
using UnityEngine;

public class RecoverUserName : MonoBehaviour
{
	public UILabel userNameInput;

	public GameObject clearUserName;

	public UISprite userBg;

	public static RecoverUserName _ins;

	private void Start()
	{
		RecoverUserName._ins = this;
	}
}
