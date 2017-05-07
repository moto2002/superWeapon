using msg;
using System;
using UnityEngine;

public class SkillUIITem : FightPanel_SkillAndSoliderUIItem
{
	public UISprite qua;

	public UISprite icon;

	public SkillCarteData skill;

	public UISprite Card_BJ;

	public float CD_Time;

	public float Now_CD_Time;

	public UISprite CD_BMP;

	public UILabel CD_Text;

	public UILabel Back_Text;

	private DieBall effectSkill;

	public float protect_time;

	[HideInInspector]
	public bool isCanSendSolider = true;

	private int money = 50;

	public UILabel SkillCardName_Label;

	private Vector3 skill_use_pos;

	private bool isPlayed;

	public bool isCanPlaySkill;

	public bool isDisplayTeXiao;

	private Ray ray;

	private RaycastHit hit;

	private DieBall circleEffect;

	private Skill skillData;

	private float move_x;

	private float move_y;

	private float move_time;

	private Vector2 hit_pos0;

	private float hit_time;

	public bool ReadyToUse;

	public bool ReadyToUse_Next;

	private bool isDraging;

	public void OnDestroy()
	{
		if (FightPanelManager.inst)
		{
			FightPanelManager.inst.skillUIList.Remove(this);
		}
	}

	private void Start()
	{
		this.buttonType = FightPanel_SkillAndSoliderUIItem.FightPanel_UIItemType.skill;
		this.Now_CD_Time = 0f;
	}

	private void OnEnable()
	{
		this.protect_time = 0.5f;
	}

	public void SkillUIITem_Update()
	{
		if (this.protect_time > 0f)
		{
			this.protect_time -= Time.deltaTime;
		}
		if (this.Now_CD_Time > 0f)
		{
			this.Now_CD_Time -= Time.deltaTime;
		}
		else if (this.Now_CD_Time <= 0f)
		{
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
			if ((this.move_x != 0f || this.move_y != 0f) && !this.Back_Text.gameObject.activeSelf)
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

	public string SetQuality(int level)
	{
		switch (level)
		{
		case 1:
			return "绿卡";
		case 2:
			return "蓝卡";
		case 3:
			return "紫卡";
		case 4:
			return "橙卡";
		case 5:
			return "橙卡";
		default:
			return string.Empty;
		}
	}

	public override void DoPress()
	{
		if (!this.ReadyToUse)
		{
			if (this.protect_time > 0f)
			{
				return;
			}
			this.ReadyToUse = true;
			this.SetUse();
			this.protect_time = 1f;
		}
		else
		{
			this.protect_time = 1f;
			this.tr.localPosition = new Vector3(this.tr.localPosition.x, 0f, this.tr.localPosition.z);
			this.ReadyToUse = false;
			SkillManage.inst.ReadyUseSkill = false;
			SkillManage.inst.ReadyUseSkill_Next = false;
			SkillManage.inst.useskillCard = null;
			UnityEngine.Object.Destroy(SkillManage.inst.ReadyUseSkill_circleEffect.gameObject);
			CameraControl.inst.enabled = true;
			FightPanelManager.inst.ChangeButton_Close(false);
		}
	}

	public void DOPress(bool isPressed)
	{
		if (this.protect_time > 0f)
		{
			return;
		}
	}

	public void SoliderIconToGray()
	{
	}

	public override void SetInfo()
	{
		this.ReadyToUse = false;
		AtlasManage.SetSkillFSpritName(this.icon, this.skill.itemID);
		AtlasManage.SetSkillQuilityInBattleSpriteName(this.qua, UnitConst.GetInstance().skillList[this.skill.itemID].skillQuality);
		this.CD_Time = 1f;
		this.SkillCardName_Label.text = UnitConst.GetInstance().skillList[this.skill.itemID].name;
	}

	public override void ResetSelect(bool isSelect)
	{
		if (isSelect)
		{
			if (this.btnState == SoliderButtonState.canUse)
			{
				this.tr.localPosition = new Vector3(this.tr.localPosition.x, 70f);
				this.btnState = SoliderButtonState.inUse;
			}
		}
		else if (this.btnState == SoliderButtonState.inUse)
		{
			this.tr.localPosition = new Vector3(this.tr.localPosition.x, 0f);
			this.btnState = SoliderButtonState.canUse;
		}
	}

	public override void OnClickEvent(bool isPress)
	{
		if (this.protect_time > 0f)
		{
			return;
		}
		if (this.Now_CD_Time <= 0f)
		{
			LogManage.Log("isPress ::" + isPress);
			if (!this.ReadyToUse)
			{
				this.ReadyToUse = true;
				this.SetUse();
				this.protect_time = 0.3f;
				FightPanelManager.inst.CurSelectUIItem = this;
			}
			else
			{
				this.protect_time = 0.3f;
				this.ReadyToUse = false;
				FightPanelManager.inst.CurSelectUIItem = this;
				SkillManage.inst.ReadyUseSkill = false;
				SkillManage.inst.ReadyUseSkill_Next = false;
				SkillManage.inst.useskillCard = null;
				UnityEngine.Object.Destroy(SkillManage.inst.ReadyUseSkill_circleEffect.gameObject);
				CameraControl.inst.enabled = true;
				FightPanelManager.inst.ChangeButton_Close(false);
			}
			return;
		}
	}

	public void True_UseCard(Vector3 skill_pos)
	{
		this.skill_use_pos = skill_pos;
		if (NewbieGuidePanel.isEnemyAttck)
		{
			CSSkillRemove cSSkillRemove = new CSSkillRemove();
			cSSkillRemove.skillId = this.skill.id;
			cSSkillRemove.type = 2;
			ClientMgr.GetNet().SendHttp(2306, cSSkillRemove, new DataHandler.OpcodeHandler(this.UseSkillCallBack), null);
		}
		else
		{
			this.PlaySkill();
		}
	}

	private void UseSkillCallBack(bool isError, Opcode opcode)
	{
		if (!isError)
		{
			this.PlaySkill();
		}
		else
		{
			FightPanelManager.inst.RefershSkilUIButton(null);
		}
	}

	private void PlaySkill()
	{
		if (this.isPlayed)
		{
			return;
		}
		HUDTextTool.inst.NextLuaCall("放技能", new object[0]);
		this.isPlayed = true;
		SkillManage.inst.AcquittalSkill(this.skill.itemID, this.skill_use_pos, -1);
		TimePanel.inst.StartTime();
		if (this.circleEffect)
		{
			if (this.circleEffect.GetComponent<DragCameraMaster>().dragCamera)
			{
				this.circleEffect.GetComponent<DragCameraMaster>().dragCamera.GetComponent<DragCamera>().SetDragState(false, true);
			}
			UnityEngine.Object.Destroy(this.circleEffect.ga);
		}
		FightPanelManager.inst.RemoveSkillButton(this);
		FightPanelManager.inst.RefreshGrid();
	}

	public void Card_ShderToGray()
	{
		this.qua.ShaderToGray();
		this.icon.ShaderToGray();
	}

	public void Card_ShderToNormal()
	{
		this.qua.ShaderToNormal();
		this.icon.ShaderToNormal();
	}

	private void SetUse()
	{
		this.tr.localPosition = new Vector3(this.tr.localPosition.x, 80f, this.tr.localPosition.z);
		Vector3 position = new Vector3(25f, 0f, 25f);
		if (!this.circleEffect)
		{
			this.skillData = UnitConst.GetInstance().skillList[this.skill.itemID];
			if (this.skillData.skillType == 5 || this.skillData.skillType == 7 || this.skillData.skillType == 9 || this.skillData.skillType == 10 || this.skillData.skillType == 13 || this.skillData.skillType == 14)
			{
				this.circleEffect = PoolManage.Ins.CreatEffect("jinjiweixiu_fanwei", position, Quaternion.identity, null);
			}
			else if (this.skillData.skillType == 11 || this.skillData.skillType == 12)
			{
				this.circleEffect = PoolManage.Ins.CreatEffect("jinjiweixiu_fanwei", position, Quaternion.identity, null);
			}
			else if (this.skillData.skillType == 15)
			{
				this.circleEffect = PoolManage.Ins.CreatEffect("jinjiweixiu_fanwei", position, Quaternion.identity, null);
			}
			else if (this.skillData.skillType == 16)
			{
				this.circleEffect = PoolManage.Ins.CreatEffect("jinjiweixiu_fanwei", position, Quaternion.identity, null);
			}
			else if (this.skillData.skillType == 17)
			{
				this.circleEffect = PoolManage.Ins.CreatEffect("dingxiangbaopo_fanwei", position, Quaternion.identity, null);
			}
			else if (this.skillData.skillType == 18)
			{
				this.circleEffect = PoolManage.Ins.CreatEffect("jinjiweixiu_fanwei", position, Quaternion.identity, null);
			}
			else if (this.skillData.skillType == 19)
			{
				this.circleEffect = PoolManage.Ins.CreatEffect("jianduipaoji_fanwei", position, Quaternion.identity, null);
			}
			else if (this.skillData.skillType == 20)
			{
				this.circleEffect = PoolManage.Ins.CreatEffect("jinjiweixiu_fanwei", position, Quaternion.identity, null);
			}
			else
			{
				this.circleEffect = PoolManage.Ins.CreatEffect(this.skillData.circleEffect, position, Quaternion.identity, null);
			}
			Camera_FingerManager.inst.GetDragCamera(this.circleEffect.tr);
			FightPanelManager.inst.ChangeButton_Close(true);
			this.circleEffect.LifeTime = 0f;
		}
		SkillManage.inst.AddToReadyUseSkill(this, this.circleEffect);
	}

	protected override void OnDrag(Vector2 vc)
	{
	}

	private void OnDragOver()
	{
		this.isDisplayTeXiao = false;
	}

	private void OnDragOut()
	{
		this.isDisplayTeXiao = true;
	}

	private void OnDragEnd()
	{
	}

	private void OnDragStart()
	{
	}
}
