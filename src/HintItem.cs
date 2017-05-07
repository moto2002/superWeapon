using System;
using UnityEngine;

public class HintItem : MonoBehaviour
{
	public UILabel text;

	public Vector3 Thispostion;

	public T_Tank postion;

	public T_Tower TowerPostion;

	private Camera cam;

	private void Start()
	{
	}

	public void OnSetText(string Text)
	{
		if (!base.GetComponent<TweenAlpha>().enabled)
		{
			this.text.text = string.Empty;
			base.gameObject.SetActive(true);
			base.gameObject.transform.localScale = Vector3.one;
			base.GetComponent<TweenAlpha>().enabled = true;
			base.GetComponent<TweenAlpha>().PlayForward();
			if (!this.text.gameObject.GetComponent<TypewriterEffect>().enabled)
			{
				this.text.gameObject.GetComponent<TypewriterEffect>().enabled = true;
			}
			this.text.text = Text;
			base.Invoke("OnClosePanel", 3f);
		}
	}

	private void Update()
	{
	}

	public Vector3 PostionSet()
	{
		if (Camera.main == null)
		{
			return default(Vector3);
		}
		this.cam = UIManager.inst.uiCamera;
		Vector3 position = new Vector3(this.postion.gameObject.transform.position.x + 0.1f, this.postion.gameObject.transform.position.y + 0.2f, this.postion.gameObject.transform.position.z);
		Vector3 position2 = Camera.main.WorldToScreenPoint(position);
		Vector3 vector = this.cam.ScreenToWorldPoint(position2);
		if (vector.z < 0f)
		{
		}
		vector = new Vector3(vector.x, vector.y + 0.1f, 0f);
		base.transform.position = vector;
		return vector;
	}

	public void TowerPostionGet()
	{
		if (Camera.main == null)
		{
			return;
		}
		this.cam = UIManager.inst.uiCamera;
		Vector3 position = new Vector3(this.TowerPostion.gameObject.transform.position.x, this.TowerPostion.gameObject.transform.position.y, this.TowerPostion.gameObject.transform.position.z);
		Vector3 position2 = Camera.main.WorldToScreenPoint(position);
		Vector3 position3 = this.cam.ScreenToWorldPoint(position2);
		if (position3.z < 0f)
		{
		}
		position3 = new Vector3(position3.x, position3.y + 0.1f, 0f);
		base.transform.position = position3;
	}

	public void OnClosePanel()
	{
		base.GetComponent<TweenAlpha>().enabled = false;
		this.text.gameObject.GetComponent<TypewriterEffect>().enabled = false;
		base.gameObject.SetActive(false);
	}
}
