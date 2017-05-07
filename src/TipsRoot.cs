using DG.Tweening;
using System;
using UnityEngine;

public class TipsRoot : MonoBehaviour
{
	public int up;

	public int down;

	public int left;

	public int right;

	public UILabel ButtonA_Label;

	public UILabel ButtonB_Label;

	private Camera cam;

	private Transform tr;

	public Transform under;

	public T_Island tar;

	private DieBall effect;

	private Camera mainCam
	{
		get
		{
			return WMap_DragManager.inst.camer;
		}
	}

	private void Awake()
	{
		this.tr = base.transform;
	}

	private void Start()
	{
		this.cam = TipsManager.inst.uiCamera;
	}

	public void SetPos(Transform t)
	{
		if (this.ButtonB_Label && this.ButtonB_Label.parent != null)
		{
			if (this.ButtonB_Label.parent.transform.GetComponent<UISprite>())
			{
				this.ButtonB_Label.parent.transform.GetComponent<UISprite>().ShaderToNormal();
			}
			if (this.ButtonB_Label.parent.transform.GetComponent<BoxCollider>())
			{
				this.ButtonB_Label.parent.transform.GetComponent<BoxCollider>().enabled = true;
			}
			if (this.ButtonB_Label.parent.transform.GetComponent<ButtonClick>())
			{
				this.ButtonB_Label.parent.transform.GetComponent<ButtonClick>().IsAutoSetColor = true;
			}
			if (this.ButtonB_Label.parent.transform.GetComponent<UIButton>())
			{
				this.ButtonB_Label.parent.transform.GetComponent<UIButton>().enabled = true;
			}
		}
		if (this.ButtonB_Label)
		{
			this.ButtonB_Label.GetComponent<languageLableKey>().pickName = "others";
		}
		if (this.tar.uiType == WMapTipsType.battle)
		{
			if (this.ButtonA_Label)
			{
				this.ButtonA_Label.GetComponent<languageLableKey>().key = "攻击";
				this.ButtonB_Label.GetComponent<languageLableKey>().pickName = "Battle";
				this.ButtonB_Label.GetComponent<languageLableKey>().key = "扫荡";
			}
			if (!this.tar.battleItem.isCanSweep && this.ButtonB_Label && this.ButtonB_Label.parent != null)
			{
				if (this.ButtonB_Label.parent.transform.GetComponent<UISprite>())
				{
					this.ButtonB_Label.parent.transform.GetComponent<UISprite>().ShaderToGray();
				}
				if (this.ButtonB_Label.parent.transform.GetComponent<BoxCollider>())
				{
					this.ButtonB_Label.parent.transform.GetComponent<BoxCollider>().enabled = false;
				}
				if (this.ButtonB_Label.parent.transform.GetComponent<ButtonClick>())
				{
					this.ButtonB_Label.parent.transform.GetComponent<ButtonClick>().IsAutoSetColor = false;
				}
				if (this.ButtonB_Label.parent.transform.GetComponent<UIButton>())
				{
					this.ButtonB_Label.parent.transform.GetComponent<UIButton>().enabled = false;
				}
			}
		}
		else if (this.ButtonA_Label)
		{
			this.ButtonA_Label.GetComponent<languageLableKey>().key = "侦查";
			this.ButtonB_Label.GetComponent<languageLableKey>().key = "攻击";
		}
		if (this.tr == null)
		{
			this.tr = base.transform;
		}
		int num = 0;
		int num2 = 0;
		Vector3 position = new Vector3(t.position.x, t.position.y, t.position.z);
		Vector3 position2 = this.mainCam.WorldToScreenPoint(position);
		Vector3 position3 = TipsManager.inst.uiCamera.ScreenToWorldPoint(position2);
		if (position2.x < (float)Screen.width * 0.15f)
		{
			num = this.right;
		}
		else if (position2.x > (float)Screen.width * 0.85f)
		{
			num = this.left;
		}
		if (position2.y < (float)Screen.height * 0.55f)
		{
			num2 = this.up;
		}
		else if (position2.y > (float)Screen.height * 0.55f)
		{
			num2 = this.down;
		}
		this.tr.position = position3;
		this.tr.localPosition += new Vector3((float)num, (float)num2, -this.tr.localPosition.z);
		LogManage.Log(this.tr.gameObject.activeSelf);
		this.tr.gameObject.SetActive(true);
		LogManage.Log(this.tr.gameObject.activeSelf);
		if (this.under != null)
		{
			Vector3 position4 = TipsManager.inst.uiCamera.WorldToScreenPoint(this.tr.position);
			Vector3 vector = new Vector3(position4.x, position4.y, this.under.localPosition.z);
			Vector3 vector2 = this.mainCam.ScreenToWorldPoint(position4);
			this.under.position = new Vector3(vector2.x, vector2.y, this.under.localPosition.z);
		}
	}

	public void SetPosNoPianyi(Transform t)
	{
		if (this.tr == null)
		{
			this.tr = base.transform;
		}
		int num = 0;
		int num2 = 0;
		Vector3 position = new Vector3(t.position.x, t.position.y, t.position.z);
		Vector3 position2 = this.mainCam.WorldToScreenPoint(position);
		Vector3 position3 = TipsManager.inst.uiCamera.ScreenToWorldPoint(position2);
		this.tr.position = position3;
		this.tr.localPosition += new Vector3((float)num, (float)num2, -this.tr.localPosition.z);
		LogManage.Log(this.tr.gameObject.activeSelf);
		this.tr.gameObject.SetActive(true);
		LogManage.Log(this.tr.gameObject.activeSelf);
		if (this.under != null)
		{
			Vector3 position4 = TipsManager.inst.uiCamera.WorldToScreenPoint(this.tr.position);
			Vector3 vector = new Vector3(position4.x, position4.y, this.under.localPosition.z);
			Vector3 vector2 = this.mainCam.ScreenToWorldPoint(position4);
			this.under.position = new Vector3(vector2.x, vector2.y, this.under.localPosition.z);
		}
	}

	private void OnEnable()
	{
		if (this.tar)
		{
			if (this.tar.OwnerType == OwnerType.user)
			{
				this.effect = PoolManage.Ins.CreatEffect("worldmap_blue", this.tar.tr.position, Quaternion.identity, this.tar.tr);
				this.effect.tr.DOPunchScale(this.effect.tr.localScale * 1.2f, 0.8f, 10, 1f).OnComplete(delegate
				{
					TransRotate transRotate = this.effect.ga.AddComponent<TransRotate>();
					transRotate.Bg = this.effect.tr;
					transRotate.xyz = TransRotate.RotateXYZ.Y;
					transRotate.FuDu = 50f;
				});
			}
			else
			{
				this.effect = PoolManage.Ins.CreatEffect("worldmap_red", this.tar.tr.position, Quaternion.identity, this.tar.tr);
				this.effect.tr.DOPunchScale(this.effect.tr.localScale * 1.2f, 0.8f, 10, 1f).OnComplete(delegate
				{
					TransRotate transRotate = this.effect.ga.AddComponent<TransRotate>();
					transRotate.Bg = this.effect.tr;
					transRotate.xyz = TransRotate.RotateXYZ.Y;
					transRotate.FuDu = 50f;
				});
			}
		}
	}

	private void OnDisable()
	{
		if (this.tar && this.effect && this.effect.ga.activeSelf)
		{
			this.effect.DesInPool();
		}
	}
}
