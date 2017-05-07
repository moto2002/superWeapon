using msg;
using System;
using UnityEngine;

public class PVPMessageItem : MonoBehaviour
{
	public UISprite PVPMI_Result;

	public UISprite PVPMI_VS;

	public UILabel PVPMI_VS_Label;

	public UILabel Time;

	public UILabel FightTime;

	public UILabel FighterLeft;

	public UILabel FighterRight;

	public UISprite PVPMI_Cup;

	public UILabel PVPMI_Cup_Label;

	public UISprite PVPMI_Cup1;

	public UILabel PVPMI_Cup1_Label;

	public GameObject PVPMI_ButtonA;

	public GameObject PVPMI_ButtonB;

	public UILabel PVPMI_BtnA_Label;

	public UILabel PVPMI_BtnB_Label;

	public int depth;

	public PVPType m_pvptype;

	public long m_id;

	public bool m_fightback;

	public long m_fightback_id;

	public bool m_result;

	public string time;

	public string fight_time;

	public string fighter_left;

	public string fighter_right;

	public string vs_text;

	public int cup_Attack;

	public int cup_Def;

	public long m_video_id;

	public UIGrid PVP_Grid;

	public UILabel Des;

	public ReportData ThisPVPData;

	public bool Win;

	public long M_id;

	public long M_fightback_id;

	public long M_video_id;

	public void Awake()
	{
		if (this.PVPMI_Cup1 == null)
		{
			this.PVPMI_Cup1 = (UnityEngine.Object.Instantiate(this.PVPMI_Cup) as UISprite);
			this.PVPMI_Cup1.transform.parent = base.transform;
			this.PVPMI_Cup1.transform.localScale = this.PVPMI_Cup.transform.localScale;
			this.PVPMI_Cup1.transform.localPosition = new Vector3(25f, 13.7f, 0f);
			this.PVPMI_Cup1_Label = this.PVPMI_Cup1.transform.FindChild("Label").GetComponent<UILabel>();
		}
	}

	public void Init()
	{
		this.PVPMI_Result.depth = this.depth + 2;
		this.PVPMI_VS.depth = this.depth + 2;
		this.PVPMI_VS_Label.depth = this.depth + 3;
		this.Time.depth = this.depth + 2;
		this.FightTime.depth = this.depth + 2;
		this.FighterLeft.depth = this.depth + 2;
		this.FighterRight.depth = this.depth + 2;
		this.PVPMI_Cup.depth = this.depth + 2;
		this.PVPMI_Cup_Label.depth = this.depth + 2;
		this.PVPMI_Cup1.depth = this.depth + 2;
		this.PVPMI_Cup1_Label.depth = this.depth + 2;
		this.PVPMI_ButtonA.GetComponent<UISprite>().depth = this.depth + 2;
		this.PVPMI_ButtonB.GetComponent<UISprite>().depth = this.depth + 2;
		this.PVPMI_BtnA_Label.depth = this.depth + 3;
		this.PVPMI_BtnB_Label.depth = this.depth + 3;
		Color color = new Color(0.0627451f, 0.4627451f, 0.968627453f, 1f);
		Color color2 = new Color(0.8509804f, 0.07058824f, 0f, 1f);
		Color color3 = new Color(0.0784313753f, 0.8980392f, 0.0784313753f, 1f);
		if (this.m_pvptype == PVPType.Att)
		{
			if (this.m_result)
			{
				this.PVPMI_Result.spriteName = "胜利";
				this.PVPMI_Cup_Label.text = "+" + this.cup_Attack;
				this.PVPMI_Cup_Label.color = color3;
				this.PVPMI_Cup1_Label.text = "-" + this.cup_Def;
				this.PVPMI_Cup1_Label.color = color2;
				this.Win = true;
			}
			else
			{
				this.PVPMI_Result.spriteName = "失败";
				this.PVPMI_Cup_Label.text = "-" + this.cup_Attack;
				this.PVPMI_Cup_Label.color = color2;
				this.PVPMI_Cup1_Label.text = "+" + this.cup_Def;
				this.PVPMI_Cup1_Label.color = color3;
				this.Win = false;
			}
			this.PVPMI_ButtonA.gameObject.SetActive(false);
			this.FighterLeft.color = color;
			this.FighterRight.color = color2;
		}
		else if (this.m_pvptype == PVPType.Def)
		{
			if (!this.m_result)
			{
				this.PVPMI_Result.spriteName = "胜利";
				this.PVPMI_Cup_Label.text = "-" + this.cup_Attack;
				this.PVPMI_Cup_Label.color = color2;
				this.PVPMI_Cup1_Label.text = "+" + this.cup_Def;
				this.PVPMI_Cup1_Label.color = color3;
				this.PVPMI_ButtonA.gameObject.SetActive(false);
				this.Win = true;
			}
			else
			{
				this.PVPMI_Result.spriteName = "失败";
				this.PVPMI_Cup_Label.text = "+" + this.cup_Attack;
				this.PVPMI_Cup_Label.color = color3;
				this.PVPMI_Cup1_Label.text = "-" + this.cup_Def;
				this.PVPMI_Cup1_Label.color = color2;
				this.Win = false;
				if (this.m_fightback)
				{
					this.PVPMI_ButtonA.gameObject.SetActive(true);
				}
				else
				{
					this.PVPMI_ButtonA.gameObject.SetActive(false);
					this.PVPMI_VS_Label.text = "复仇";
				}
			}
			this.FighterRight.color = color;
			this.FighterLeft.color = color2;
		}
		if (this.Win)
		{
			this.Des.text = "获得战利品";
			foreach (KVStruct current in this.ThisPVPData.addRes)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate(PVPMessage.inst.AddRes_Prefab) as GameObject;
				gameObject.transform.parent = this.PVP_Grid.transform;
				gameObject.transform.localScale = Vector3.one;
				AtlasManage.SetResSpriteName(gameObject.transform.FindChild("Icon").GetComponent<UISprite>(), (ResType)current.key);
				gameObject.transform.Find("Label").GetComponent<UILabel>().text = "+" + current.value;
			}
		}
		else
		{
			this.Des.text = "损失单位";
			foreach (KVStruct current2 in this.ThisPVPData.destroyArmys)
			{
				GameObject gameObject2 = UnityEngine.Object.Instantiate(PVPMessage.inst.DesSoldier_Prefab) as GameObject;
				gameObject2.transform.parent = this.PVP_Grid.transform;
				gameObject2.transform.localScale = Vector3.one;
				AtlasManage.SetArmyIconSpritName(gameObject2.transform.FindChild("Icon").GetComponent<UISprite>(), (int)current2.key);
				gameObject2.transform.Find("Label").GetComponent<UILabel>().text = "-" + current2.value;
			}
		}
		base.StartCoroutine(this.PVP_Grid.RepositionAfterFrame());
		this.Time.text = this.time;
		this.FightTime.text = this.fight_time;
		this.FighterLeft.text = this.fighter_left;
		this.FighterRight.text = this.fighter_right;
		this.M_id = this.m_id;
		this.M_fightback_id = this.m_fightback_id;
		this.M_video_id = this.m_video_id;
	}
}
