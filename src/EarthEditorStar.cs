using System;
using UnityEngine;

public class EarthEditorStar : MonoBehaviour
{
	public Battle _ThisBattle;

	public float JD;

	public float WD;

	public UILabel Title;

	public LineRenderer line;

	public EarthEditorStar LastStar;

	public Camera MainCamera;

	public UILabel JW_Label;

	private bool Drag;

	public void Init()
	{
		base.transform.localPosition = new Vector3(this.JD / 180f * 770f, this.WD / 90f * 385f);
		this.Title.text = string.Format("第{0}关", this._ThisBattle.number);
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (this.LastStar)
		{
			this.line.SetPosition(0, this.LastStar.transform.localPosition - base.transform.localPosition + new Vector3(0f, 0f, -1f));
			this.line.SetPosition(1, Vector3.zero + new Vector3(0f, 0f, -1f));
		}
		if (this.Drag)
		{
			Ray ray = this.MainCamera.ScreenPointToRay(Input.mousePosition);
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit))
			{
				GameObject gameObject = raycastHit.collider.gameObject;
				if (gameObject.name != "MapBMP")
				{
					return;
				}
				base.transform.position = raycastHit.point;
			}
			this.JD = (float)((int)(base.transform.localPosition.x / 770f * 180f * 100f)) * 0.01f;
			this.WD = (float)((int)(base.transform.localPosition.y / 385f * 90f * 100f)) * 0.01f;
			this.JW_Label.enabled = true;
			this.JW_Label.text = string.Format("经度：{0},纬度：{1}", this.JD, this.WD);
		}
		else
		{
			this.JW_Label.enabled = false;
		}
	}

	private void OnPress()
	{
		this.Drag = !this.Drag;
	}
}
