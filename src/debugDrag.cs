using System;
using UnityEngine;

public class debugDrag : MonoBehaviour
{
	private Transform tr;

	public GameObject content;

	private UIWidget widget;

	private void Awake()
	{
	}

	private void Start()
	{
		this.tr = base.transform;
	}

	private void OnDrag(Vector2 vc)
	{
		this.tr.localPosition += new Vector3(vc.x, vc.y, 0f);
		float num = (float)(Screen.width + this.widget.width) * 0.5f;
		float num2 = (float)(Screen.height + this.widget.height) * 0.5f;
		if (this.tr.localPosition.x > (float)(Screen.width / 2))
		{
			this.tr.localPosition = new Vector3((float)(Screen.width / 2), this.tr.localPosition.y, 0f);
		}
		if (this.tr.localPosition.x < (float)(-1 * Screen.width / 2))
		{
			this.tr.localPosition = new Vector3((float)(-1 * Screen.width / 2), this.tr.localPosition.y, 0f);
		}
		if (this.tr.localPosition.y > (float)(Screen.height / 2))
		{
			this.tr.localPosition = new Vector3(this.tr.localPosition.x, (float)(Screen.height / 2), 0f);
		}
		if (this.tr.localPosition.y < (float)(-1 * Screen.height / 2))
		{
			this.tr.localPosition = new Vector3(this.tr.localPosition.x, (float)(-1 * Screen.height / 2), 0f);
		}
	}

	private void OnClick()
	{
		if (ButtonClick.newbiLock)
		{
			return;
		}
		FuncUIManager.inst.OpenFuncUI("ShopPanel", Loading.senceType);
		if (!FuncUIManager.inst.OpenFuncUI("ShopPanel", Loading.senceType).GetComponent<ShopPanelManage>())
		{
			FuncUIManager.inst.OpenFuncUI("ShopPanel", Loading.senceType).AddComponent<ShopPanelManage>();
			FuncUIManager.inst.OpenFuncUI("ShopPanel", Loading.senceType).GetComponent<ShopPanelManage>().enabled = true;
		}
	}

	private void OnPress(bool isPress)
	{
	}
}
