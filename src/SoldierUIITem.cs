using System;
using System.Collections.Generic;
using UnityEngine;

public class SoldierUIITem : FightPanel_SkillAndSoliderUIItem
{
	public UISprite SoldierSkillCDSprite;

	public UILabel SoldierSkillCDLabel;

	public SoliderType soliderType;

	public int Card_No;

	public int Supply_Card_No;

	public bool IsExtraArmyCard;

	public UISprite armySprite;

	public UILabel armyNum;

	public int soldierNumInBattled;

	public UISprite showDead;

	public int times;

	public int count;

	public UISprite diamond;

	public bool isCanBuy;

	public UISprite light1;

	public UISprite light2;

	public GameObject CostDiamond;

	public UISprite CD_BMP;

	public UILabel BackLabel;

	public float CD_Time;

	public float Now_CD_Time;

	private float move_x;

	private float move_y;

	private float move_time;

	private Vector2 hit_pos0;

	private float hit_time;

	public float protect_time;

	private DieBall effectSkill;

	private bool SkillCDOver;

	public UISprite Card_BJ;

	public int soliderNum;

	private bool inClick;

	public int Supply_Tank_Index;

	public int Supply_Tank_Num;

	public List<armyData> allArmyData = new List<armyData>();

	public int soliderIndex;

	public T_Commander This_Tank;

	private bool isDraging;

	private Body_Model circleEffect;

	private Body_Model CommanderEffect;

	private Ray ray;

	private RaycastHit hit;

	public bool isCanPlaySkill;

	public bool isDisplayTeXiao;

	private void ReStart()
	{
		this.BackLabel.gameObject.SetActive(false);
	}

	private void OnEnable()
	{
		this.protect_time = 0.5f;
		if (this.soliderType == SoliderType.Commander)
		{
			this.SoldierSkillCDSprite.gameObject.SetActive(true);
		}
	}

	public void Card_ShderToGray()
	{
		base.GetComponent<UISprite>().ShaderToGray();
		this.armySprite.ShaderToGray();
	}

	public void Card_ShderToNormal()
	{
		base.GetComponent<UISprite>().ShaderToNormal();
		this.armySprite.ShaderToNormal();
	}

	private void Update()
	{
		if (this.protect_time > 0f)
		{
			this.protect_time -= Time.deltaTime;
		}
		if (this.Supply_Card_No > 0)
		{
			return;
		}
		if (this.soliderType == SoliderType.Commander)
		{
			if (this.This_Tank)
			{
				if (this.This_Tank.NowSkillState == T_Commander.SkillState.释放)
				{
					this.SoldierSkillCDLabel.text = "技能释放中";
					return;
				}
				this.SoldierSkillCDSprite.gameObject.SetActive(true);
				this.SoldierSkillCDSprite.fillAmount = 1f - this.This_Tank.SkillCD;
				if (this.This_Tank.NowSkillState == T_Commander.SkillState.初始 || this.This_Tank.NowSkillState == T_Commander.SkillState.冷却中)
				{
					this.SkillCDOver = false;
					this.SoldierSkillCDLabel.text = "技能冷却中";
					this.Card_BJ.ShaderToGray();
					this.armySprite.ShaderToGray();
				}
				if (!this.SkillCDOver && this.This_Tank.NowSkillState == T_Commander.SkillState.可释放)
				{
					this.SoldierSkillCDLabel.text = "点击释放技能";
					this.SkillCDOver = true;
					for (int i = 0; i < 10; i++)
					{
						this.Card_BJ.ShaderToNormal();
						this.armySprite.ShaderToNormal();
						this.effectSkill = PoolManage.Ins.CreatEffect("jinengchouka", this.tr.position, Quaternion.identity, this.tr);
						this.effectSkill.tr.localScale = new Vector3(0.3f, 0.3f, 0.3f);
						this.effectSkill.tr.localPosition += new Vector3(0f, -25f, 0f);
						Transform[] componentsInChildren = this.effectSkill.GetComponentsInChildren<Transform>();
						for (int j = 0; j < componentsInChildren.Length; j++)
						{
							Transform transform = componentsInChildren[j];
							transform.gameObject.layer = 8;
							if (transform.GetComponent<ParticleSystem>())
							{
								transform.GetComponent<ParticleSystem>().startSize *= 0.6f;
							}
						}
					}
				}
			}
			else
			{
				this.SoldierSkillCDSprite.gameObject.SetActive(false);
			}
		}
		if (this.Now_CD_Time > 0f)
		{
			base.GetComponent<BoxCollider>().enabled = false;
			this.CD_BMP.fillAmount = this.Now_CD_Time / this.CD_Time;
			this.Card_ShderToGray();
			this.Now_CD_Time -= Time.deltaTime;
			if (this.Now_CD_Time <= 0f)
			{
				this.effectSkill = PoolManage.Ins.CreatEffect("jinengchouka", this.tr.position, Quaternion.identity, this.tr);
				this.effectSkill.tr.localScale = new Vector3(0.3f, 0.3f, 0.3f);
				this.effectSkill.tr.localPosition += new Vector3(0f, -25f, 0f);
				Transform[] componentsInChildren2 = this.effectSkill.GetComponentsInChildren<Transform>();
				for (int k = 0; k < componentsInChildren2.Length; k++)
				{
					Transform transform2 = componentsInChildren2[k];
					transform2.gameObject.layer = 8;
					if (transform2.GetComponent<ParticleSystem>())
					{
						transform2.GetComponent<ParticleSystem>().startSize *= 0.6f;
					}
				}
				this.Card_ShderToNormal();
			}
		}
		else if (this.Now_CD_Time <= 0f)
		{
			base.GetComponent<BoxCollider>().enabled = true;
			this.Now_CD_Time -= Time.deltaTime;
			if (this.Now_CD_Time <= -3f && this.effectSkill != null)
			{
				UnityEngine.Object.Destroy(this.effectSkill.ga);
			}
		}
		this.move_time -= Time.deltaTime;
		if (this.move_x != 0f || this.move_y != 0f)
		{
			float num = Input.mousePosition.x / (float)Screen.width;
			float num2 = Input.mousePosition.y / (float)Screen.height;
			if (NewbieGuidePanel._instance.ga_Self.activeSelf)
			{
				num = 0.5f;
				num2 = 0.5f;
			}
			if (num < 0.8f && num > 0.2f)
			{
				this.move_x = 0f;
			}
			if (num2 < 0.85f && num2 > 0.15f)
			{
				this.move_y = 0f;
			}
			if ((this.move_x != 0f || this.move_y != 0f) && !this.BackLabel.gameObject.activeSelf)
			{
				Camera_FingerManager.inst.Skill_Excursion_pos(this.move_x, this.move_y);
			}
		}
		this.hit_time += Time.deltaTime;
		if (this.hit_time >= 0.1f)
		{
			this.hit_time = 0f;
			this.hit_pos0.x = Input.mousePosition.x / (float)Screen.width;
			this.hit_pos0.y = Input.mousePosition.y / (float)Screen.height;
		}
	}

	public int GetSoliderNum_plus()
	{
		return this.soliderNum;
	}

	public void ResetArmyNum()
	{
		this.armyNum.text = string.Format("x{0}", this.soliderNum);
		if (this.soliderNum > 0)
		{
			this.Card_ShderToNormal();
		}
		else
		{
			this.Card_ShderToGray();
		}
		if (this.soliderNum != this.allArmyData.Count || this.soliderNum == 0)
		{
			TimePanel.inst.StartTime();
		}
	}

	public void SoliderIconToGray()
	{
		this.armySprite.ShaderToGray();
		this.btnState = SoliderButtonState.banUse;
		FightPanelManager.inst.FindSoliderUIButton();
	}

	public void SoliderIconToNormal()
	{
		this.armySprite.ShaderToNormal();
		this.btnState = SoliderButtonState.canUse;
	}

	public override void ResetSelect(bool isSelect)
	{
		if (isSelect)
		{
			if (this.btnState != SoliderButtonState.banUse)
			{
				this.btnState = SoliderButtonState.inUse;
			}
		}
		else
		{
			this.inClick = false;
			if (this.btnState != SoliderButtonState.banUse)
			{
				this.btnState = SoliderButtonState.canUse;
			}
		}
	}

	private void OnClick()
	{
		if (this.protect_time > 0f)
		{
			return;
		}
		if (FightPanelManager.inst.TankTeamOperation_inst() != null && FightPanelManager.inst.TankTeamOperation_inst().TankTeamOperationOpen)
		{
			if (!this.light1.gameObject.activeSelf)
			{
				FightPanelManager.inst.TankTeamOperation_inst().CardToTank(this);
			}
			return;
		}
		if (this.soliderType == SoliderType.Commander && this.This_Tank && this.This_Tank.NowSkillState == T_Commander.SkillState.可释放)
		{
			this.This_Tank.UseSkillByPlayer();
		}
	}

	public override void DoPress()
	{
		if (this.protect_time > 0f)
		{
			return;
		}
		if (this.Supply_Card_No > 0)
		{
			return;
		}
		LogManage.Log("DoPress~~~~~~~");
		if (FightPanelManager.inst.TankTeamOperation_inst() != null && FightPanelManager.inst.TankTeamOperation_inst().TankTeamOperationOpen)
		{
			return;
		}
		FightPanelManager.inst.SetAllCardProtectTime(this, 0.25f);
		this.inClick = !this.inClick;
		if (!(FightPanelManager.inst.TankTeamOperation_inst() != null) || !FightPanelManager.inst.TankTeamOperation_inst().TankTeamOperationOpen)
		{
			if (this.btnState == SoliderButtonState.canUse)
			{
				FightPanelManager.inst.RefershSoliderUIButton(this);
				FightPanelManager.inst.ChangeButton_Close(true);
			}
			else if (this.btnState == SoliderButtonState.inUse)
			{
				FightPanelManager.inst.RefershSoliderUIButton(null);
				FightPanelManager.inst.ChangeButton_Close(false);
			}
		}
	}

	public armyData GetArmyToInBattle()
	{
		armyData result = this.allArmyData[this.allArmyData.Count - this.soliderNum];
		this.soliderNum--;
		this.ResetArmyNum();
		return result;
	}

	public void SetInfo(bool createBySkill = false)
	{
		if (createBySkill)
		{
			this.soliderNum = 0;
			this.armyNum.text = string.Format("x{0}", 0);
			this.soliderType = SoliderType.Normal;
			AtlasManage.SetArmyIconSpritName(this.armySprite, this.soliderIndex);
			this.Card_ShderToGray();
		}
		if (this.soliderIndex < 1000)
		{
			this.soliderNum = this.allArmyData.Count;
			this.armyNum.text = string.Format("x{0}", this.soliderNum);
			this.soliderType = SoliderType.Normal;
			AtlasManage.SetArmyIconSpritName(this.armySprite, this.soliderIndex);
		}
		else
		{
			int num = this.soliderIndex - 1000;
			if (num != 1)
			{
				if (num == 2)
				{
					this.armySprite.spriteName = "tanya";
				}
			}
			else
			{
				this.armySprite.spriteName = "baolisi";
			}
			this.soliderType = SoliderType.Commander;
			this.armySprite.SetDimensions(100, 100);
			this.armyNum.text = string.Empty;
			this.soliderNum = 1;
		}
	}

	public override void OnClickEvent(bool isPress)
	{
		if (this.protect_time > 0f)
		{
			return;
		}
		if (this.Supply_Card_No > 0)
		{
			return;
		}
		LogManage.Log("OnPress~~~~~~~");
		if (FightPanelManager.inst.TankTeamOperation_inst() != null && FightPanelManager.inst.TankTeamOperation_inst().TankTeamOperationOpen)
		{
			return;
		}
		if (isPress)
		{
			if (this.soliderNum > 0)
			{
				FightPanelManager.inst.CurSelectUIItem = this;
				this.protect_time = 0.4f;
			}
			return;
		}
	}

	public void SetSoldierS(Vector3 _pos)
	{
		int num = 0;
		for (int i = 0; i < num; i++)
		{
			Vector3 vector = default(Vector3);
			float num2 = 1.8f;
			switch (num)
			{
			case 2:
			{
				int num3 = i;
				if (num3 != 0)
				{
					if (num3 == 1)
					{
						vector = _pos + new Vector3(num2, 0f, 0f);
					}
				}
				break;
			}
			case 3:
				switch (i)
				{
				case 1:
					vector = _pos + new Vector3(0.8f * num2, 0f, 0.8f * num2);
					break;
				case 2:
					vector = _pos + new Vector3(0.8f * num2, 0f, -0.8f * num2);
					break;
				}
				break;
			case 4:
				switch (i)
				{
				case 1:
					vector = _pos + new Vector3(-1f * num2, 0f, 0f);
					break;
				case 2:
					vector = _pos + new Vector3(0.5f * num2, 0f, 0.8f * num2);
					break;
				case 3:
					vector = _pos + new Vector3(0.5f * num2, 0f, -0.8f * num2);
					break;
				}
				break;
			case 5:
				switch (i)
				{
				case 1:
					vector = _pos + new Vector3(-0.5f * num2, 0f, 0.62f * num2);
					break;
				case 2:
					vector = _pos + new Vector3(-0.5f * num2, 0f, -0.62f * num2);
					break;
				case 3:
					vector = _pos + new Vector3(0.5f * num2, 0f, -0.62f * num2);
					break;
				case 4:
					vector = _pos + new Vector3(0.5f * num2, 0f, 0.62f * num2);
					break;
				}
				break;
			case 6:
				switch (i)
				{
				case 1:
					vector = _pos + new Vector3(-0.4f * num2, 0f, -0.8f * num2);
					break;
				case 2:
					vector = _pos + new Vector3(-num2, 0f, 0f);
					break;
				case 3:
					vector = _pos + new Vector3(-0.4f * num2, 0f, 0.8f * num2);
					break;
				case 4:
					vector = _pos + new Vector3(0.7f * num2, 0f, 0.5f * num2);
					break;
				case 5:
					vector = _pos + new Vector3(0.7f * num2, 0f, -0.5f * num2);
					break;
				}
				break;
			}
		}
	}

	protected override void OnDrag(Vector2 vc)
	{
	}

	private void OnDragOver()
	{
	}

	private void OnDragOut()
	{
	}

	private void OnDragEnd()
	{
	}

	private void OnDragStart()
	{
	}
}
