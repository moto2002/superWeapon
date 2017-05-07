using System;
using UnityEngine;

public class ResTips : MonoBehaviour
{
	private UILabel resTipLable;

	public GameObject bg;

	private void Awake()
	{
		this.resTipLable = base.GetComponentInChildren<UILabel>();
	}

	private void Update()
	{
	}

	public void PlayTextTip(Transform tar, string text)
	{
		if (!base.gameObject.activeSelf)
		{
			base.gameObject.SetActive(true);
		}
		UIEventListener.Get(this.bg.gameObject).onClick = new UIEventListener.VoidDelegate(this.OnClick);
		base.gameObject.transform.position = tar.position;
		base.gameObject.transform.localScale = Vector3.one;
		this.resTipLable.text = text;
		TweenScale component = base.gameObject.GetComponent<TweenScale>();
		if (!component.enabled)
		{
			component.enabled = true;
		}
		component.ResetToBeginning();
	}

	public void PlayTextTip(Transform tar, string text, float yOffset)
	{
		if (!base.gameObject.activeSelf)
		{
			base.gameObject.SetActive(true);
		}
		UIEventListener.Get(this.bg.gameObject).onClick = new UIEventListener.VoidDelegate(this.OnClick);
		base.gameObject.transform.position = new Vector3(tar.position.x, tar.position.y + yOffset, 0f);
		base.gameObject.transform.localScale = Vector3.one;
		this.resTipLable.text = text;
		TweenScale component = base.gameObject.GetComponent<TweenScale>();
		if (!component.enabled)
		{
			component.enabled = true;
		}
		component.ResetToBeginning();
	}

	public void OnPlayTextTipsPostion(Vector3 tras, string text)
	{
		if (!base.gameObject.activeSelf)
		{
			base.gameObject.SetActive(true);
		}
		UIEventListener.Get(this.bg.gameObject).onClick = new UIEventListener.VoidDelegate(this.OnClick);
		base.gameObject.transform.localPosition = tras;
		base.gameObject.transform.localScale = Vector3.one;
		this.resTipLable.text = text;
		TweenScale component = base.gameObject.GetComponent<TweenScale>();
		if (!component.enabled)
		{
			component.enabled = true;
		}
		component.ResetToBeginning();
	}

	public void OnClick(GameObject o)
	{
		base.gameObject.SetActive(false);
	}
}
