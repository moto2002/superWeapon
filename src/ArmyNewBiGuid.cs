using DG.Tweening;
using System;
using UnityEngine;

public class ArmyNewBiGuid : MonoBehaviour
{
	public Transform endPosition;

	public Vector3 endVector3;

	public GameObject Arrows;

	public GameObject pianyi;

	public GameObject left;

	public GameObject right;

	public GameObject up;

	public GameObject down;

	private Tweener tween;

	public void OnDisable()
	{
		if (this.Arrows)
		{
			this.Arrows.SetActive(false);
		}
	}

	private void Awake()
	{
	}

	private void Start()
	{
	}

	public void DisplayNewBiGuid(string des, int num, Vector3 _pianyi, Vector3 Scale)
	{
		if (!base.gameObject.activeSelf)
		{
			base.gameObject.SetActive(true);
		}
		if (this.tween != null)
		{
			this.tween.Kill(false);
		}
		if (string.IsNullOrEmpty(des))
		{
			this.Arrows.SetActive(false);
		}
		else
		{
			this.left.SetActive(false);
			this.right.SetActive(false);
			this.up.SetActive(false);
			this.down.SetActive(false);
			this.Arrows.SetActive(true);
			this.Arrows.transform.position = base.transform.position;
			switch (num)
			{
			case 1:
				this.left.SetActive(true);
				this.left.transform.FindChild("qipao/DES").GetComponent<UILabel>().text = LanguageManage.GetTextByKey(des, "Halftalk");
				break;
			case 2:
				this.right.SetActive(true);
				this.right.transform.FindChild("qipao/DES").GetComponent<UILabel>().text = LanguageManage.GetTextByKey(des, "Halftalk");
				break;
			case 3:
				this.up.SetActive(true);
				this.up.transform.FindChild("qipao/DES").GetComponent<UILabel>().text = LanguageManage.GetTextByKey(des, "Halftalk");
				break;
			case 4:
				this.down.SetActive(true);
				this.down.transform.FindChild("qipao/DES").GetComponent<UILabel>().text = LanguageManage.GetTextByKey(des, "Halftalk");
				break;
			}
			this.pianyi.transform.localPosition = _pianyi;
			this.pianyi.transform.localScale = Scale;
		}
		if (this.endPosition)
		{
			this.tween = base.transform.DOMove(new Vector3(this.endPosition.position.x, this.endPosition.position.y, base.transform.position.z), 2f, false).SetLoops(-1);
		}
		else if (this.endVector3 != Vector3.zero)
		{
			Vector3 vector = UIManager.inst.uiCamera.ScreenToWorldPoint(this.endVector3);
			this.tween = base.transform.DOMove(new Vector3(vector.x, vector.y, base.transform.position.z), 2f, false).SetLoops(-1);
		}
	}
}
