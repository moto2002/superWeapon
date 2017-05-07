using System;
using UnityEngine;

public class FightMessage : MonoBehaviour
{
	private UILabel this_label;

	public Transform this_tr;

	public Vector3 pos;

	public Vector3 pos_no_tr;

	private float time0;

	public void Init()
	{
		this.this_label = base.GetComponent<UILabel>();
		this.this_label.fontSize = 35;
	}

	private void Update()
	{
		if (this.this_tr)
		{
			this.pos = CameraControl.inst.MainCamera.WorldToScreenPoint(this.this_tr.position);
			this.pos = UIManager.inst.uiCamera.ScreenToWorldPoint(this.pos);
		}
		else if (this.pos == Vector3.zero)
		{
			this.pos = CameraControl.inst.MainCamera.WorldToScreenPoint(this.pos_no_tr);
			this.pos = UIManager.inst.uiCamera.ScreenToWorldPoint(this.pos);
		}
		base.transform.position = new Vector3(this.pos.x, this.pos.y, 0f);
		base.transform.localPosition += new Vector3(0f, 60f, 0f);
		this.this_label.fontSize = (int)Mathf.Max(25f, (float)this.this_label.fontSize - 1f * Time.deltaTime);
		if (this.this_label.fontSize >= 30)
		{
			this.this_label.color = new Color(this.this_label.color.r, this.this_label.color.g, this.this_label.color.b, 1f - ((float)this.this_label.fontSize - 30f) / 5f);
		}
		if (this.this_label.fontSize <= 25)
		{
			this.time0 += Time.deltaTime;
			if (this.time0 > 1f)
			{
				this.this_label.color = new Color(this.this_label.color.r, this.this_label.color.g, this.this_label.color.b, this.this_label.color.a - 0.5f * Time.deltaTime);
				if (this.this_label.color.a <= 0f)
				{
					UnityEngine.Object.Destroy(base.gameObject);
				}
			}
		}
	}
}
