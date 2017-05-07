using System;
using UnityEngine;

public class ResourItemUI : MonoBehaviour
{
	public static ResourItemUI _Inst;

	private UISprite[] itemArr;

	public Transform itemArrParnt;

	private void Awake()
	{
		ResourItemUI._Inst = this;
	}

	private void Start()
	{
	}

	public void OnDestryItemResUI()
	{
		this.itemArr = null;
		if (this.itemArrParnt.GetComponentInChildren<UISprite>() != null)
		{
			this.itemArr = base.GetComponentsInChildren<UISprite>();
		}
		if (this.itemArr != null && this.itemArr.Length > 0)
		{
			for (int i = 0; i < this.itemArr.Length; i++)
			{
				this.itemArr[i].alpha = 0f;
			}
		}
	}
}
