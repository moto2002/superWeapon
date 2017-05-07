using System;
using System.Collections.Generic;
using UnityEngine;

public class EarthStar : MonoBehaviour
{
	public UILabel StarBattleName_Label;

	private Body_Model BattleModel;

	private GameObject go;

	public float JD;

	public float WD;

	public EarthStar NextEarthStar;

	public List<GameObject> EarthStarLine;

	public LineRenderer EarthStarLineRenderer;

	public LineRenderer DesLineRenderer;

	public bool IsUnlocked;

	public UITexture Icon1;

	public UITexture Icon2;

	public UITexture This_Icon;

	private UITexture This_Texture;

	public float JD_DIS;

	private float alpha;

	private void Start()
	{
		this.EarthStarLineRenderer = base.transform.FindChild("Line0").GetComponent<LineRenderer>();
		this.DesLineRenderer = base.transform.FindChild("Line1").GetComponent<LineRenderer>();
		this.Icon1 = base.transform.FindChild("Icon1").GetComponent<UITexture>();
		this.Icon2 = base.transform.FindChild("Icon2").GetComponent<UITexture>();
		this.This_Texture = base.GetComponent<UITexture>();
		this.Icon1.gameObject.layer = 15;
		this.Icon2.gameObject.layer = 15;
		if (LegionMapManager._inst.NowBattleType == LegionMapManager.BattleType.普通副本)
		{
			if (int.Parse(base.transform.parent.name) <= LegionMapManager._inst.PlayBattle)
			{
				this.IsUnlocked = true;
				this.Icon1.transform.gameObject.SetActive(true);
				this.Icon2.transform.gameObject.SetActive(false);
				this.This_Icon = this.Icon1;
			}
			else
			{
				this.IsUnlocked = false;
				this.Icon2.transform.gameObject.SetActive(true);
				this.Icon1.transform.gameObject.SetActive(false);
				this.This_Icon = this.Icon2;
			}
		}
		else if (LegionMapManager._inst.NowBattleType == LegionMapManager.BattleType.军团副本)
		{
			if (int.Parse(base.transform.parent.name) == LegionMapManager._inst.PlayBattle)
			{
				this.IsUnlocked = true;
				this.Icon1.transform.gameObject.SetActive(true);
				this.Icon2.transform.gameObject.SetActive(false);
				this.This_Icon = this.Icon1;
			}
			else
			{
				this.IsUnlocked = false;
				this.Icon2.transform.gameObject.SetActive(true);
				this.Icon1.transform.gameObject.SetActive(false);
				this.This_Icon = this.Icon2;
			}
		}
		this.StarBattleName_Label = base.GetComponentInChildren<UILabel>();
		this.StarBattleName_Label.fontSize = 30;
		this.StarBattleName_Label.transform.localPosition = new Vector3(0f, 0f, -4f);
		this.StarBattleName_Label.transform.localScale = Vector3.one * 0.025f;
		if (LegionMapManager._inst.NowBattleType == LegionMapManager.BattleType.普通副本)
		{
			this.StarBattleName_Label.text = string.Format("副本第{0}章", base.transform.parent.name);
		}
		else if (LegionMapManager._inst.NowBattleType == LegionMapManager.BattleType.军团副本)
		{
			this.StarBattleName_Label.text = string.Format("军团副本-{0}", base.transform.parent.name);
		}
		this.StarBattleName_Label.transform.FindChild("Sprite").GetComponent<UISprite>().SetDimensions(133, 36);
		this.go = new GameObject("point");
		this.go.transform.parent = base.transform;
		this.go.transform.localPosition = this.StarBattleName_Label.transform.localPosition;
		this.StarBattleName_Label.transform.parent = base.transform.parent.transform;
		this.This_Icon.transform.parent = base.transform.parent.transform;
	}

	public void SetEarthStarLineRenderer()
	{
		GameObject gameObject = new GameObject("null");
		gameObject.transform.parent = base.transform;
		this.EarthStarLineRenderer = base.transform.FindChild("Line0").GetComponent<LineRenderer>();
		int num = 2 + this.EarthStarLine.Count;
		this.EarthStarLineRenderer.SetVertexCount(num);
		this.EarthStarLineRenderer.SetPosition(0, Vector3.zero);
		for (int i = 0; i < this.EarthStarLine.Count; i++)
		{
			gameObject.transform.position = this.EarthStarLine[i].transform.position;
			this.EarthStarLineRenderer.SetPosition(i + 1, gameObject.transform.localPosition);
		}
		for (int j = 0; j < this.EarthStarLine.Count; j++)
		{
			UnityEngine.Object.Destroy(this.EarthStarLine[j].transform.parent.parent.gameObject);
		}
		gameObject.transform.position = this.NextEarthStar.transform.position;
		this.EarthStarLineRenderer.SetPosition(num - 1, gameObject.transform.localPosition);
	}

	private void Update()
	{
		this.JD_DIS = Mathf.Abs(this.JD + LegionMapManager._inst.Earth_JD);
		if (this.JD_DIS <= 180f)
		{
			this.alpha = (this.JD_DIS - 90f) / 90f;
		}
		else
		{
			this.alpha = (270f - this.JD_DIS) / 90f;
		}
		this.alpha *= 1.3f;
		if (this.StarBattleName_Label)
		{
			this.StarBattleName_Label.transform.position = this.go.transform.position;
			this.StarBattleName_Label.transform.eulerAngles = new Vector3(0f, 0f, 0f);
			this.StarBattleName_Label.alpha = this.alpha;
		}
		if (this.This_Icon)
		{
			this.This_Icon.transform.position = base.transform.position;
			this.This_Icon.transform.eulerAngles = new Vector3(0f, 0f, 0f);
			this.This_Icon.alpha = this.alpha;
		}
		this.This_Texture.alpha = this.alpha;
		this.DesLineRenderer.SetColors(new Color(1f, 1f, 1f, this.alpha), new Color(1f, 1f, 1f, this.alpha));
	}
}
