using System;
using UnityEngine;

public class TaskByNewBie : MonoBehaviour
{
	public UISprite thisBtnBMP;

	public UISprite thisBtnBJBMP;

	public UISprite BJBMP;

	public UILabel Title;

	public UILabel State;

	public string Title_label;

	public bool Finish;

	public bool CannotFinish;

	public int Task_id;

	public string NewbieGroup;

	public int Task_Build_index;

	public int Task_Build_Second_index;

	public int Task_Build_Step;

	private bool AlreadyStart;

	public UISprite NewBieGif;

	public UISprite NewBieGif_BJ;

	public float step_now;

	public float step_all;

	private DieBall effect;

	public bool NewBtnMessage_0;

	public float gif_speed;

	public float gif_stay_time;

	private float gif_stay_time0;

	private float gif_time;

	private int no;

	private void Start()
	{
		this.BJBMP = base.transform.FindChild("BJ").GetComponent<UISprite>();
		this.Title = base.transform.FindChild("Title").GetComponent<UILabel>();
		this.State = base.transform.FindChild("State").GetComponent<UILabel>();
		if (base.transform.FindChild("Label"))
		{
			this.NewBieGif = base.transform.FindChild("Label").GetComponent<UISprite>();
		}
		if (base.transform.FindChild("Label_BJ"))
		{
			this.NewBieGif_BJ = base.transform.FindChild("Label_BJ").GetComponent<UISprite>();
		}
		this.AlreadyStart = true;
		this.Message_Close();
		this.gif_speed = 0.05f;
		this.gif_stay_time = 2f;
		if (this.NewBieGif)
		{
			this.NewBieGif.transform.localScale = Vector3.one;
		}
	}

	public void Message_Close()
	{
		this.NewBtnMessage_0 = false;
		if (!this.AlreadyStart)
		{
			return;
		}
		this.Message_Open();
	}

	public void Message_Open()
	{
		this.NewBtnMessage_0 = true;
		this.BJBMP.gameObject.SetActive(true);
		this.Title.gameObject.SetActive(true);
		this.State.gameObject.SetActive(true);
		if (this.NewBieGif_BJ)
		{
			this.NewBieGif_BJ.gameObject.SetActive(false);
		}
		if (this.effect != null)
		{
			UnityEngine.Object.Destroy(this.effect.ga);
		}
		this.CannotFinish = false;
		this.Title.text = this.Title_label;
		if (TaskByNewBieManager._inst.TaskByNewBiePanel_No == 0)
		{
			if (this.step_all > 0f)
			{
				this.Title.transform.localPosition = new Vector3(0f, -33f, 0f);
				UILabel expr_DD = this.Title;
				expr_DD.text += string.Format("\n进度：{0}/{1}", Mathf.Min(this.step_now, this.step_all), this.step_all);
			}
			else
			{
				this.Title.transform.localPosition = new Vector3(0f, -28f, 0f);
				UILabel expr_147 = this.Title;
				expr_147.text += string.Format("\n", new object[0]);
			}
		}
		if (this.Finish)
		{
			UILabel expr_178 = this.Title;
			expr_178.text = expr_178.text + "(" + LanguageManage.GetTextByKey("完成", "Task") + ")";
			this.State.text = LanguageManage.GetTextByKey("领取奖励", "Task");
			this.State.color = new Color(1f, 1f, 0.25f, 1f);
			this.effect = PoolManage.Ins.CreatEffect("XSYD", this.BJBMP.transform.position, Quaternion.identity, this.BJBMP.transform);
			this.effect.tr.localPosition = Vector3.zero;
			this.effect.tr.localScale = Vector3.one;
		}
		else
		{
			UILabel expr_249 = this.Title;
			expr_249.text = expr_249.text + "(" + LanguageManage.GetTextByKey("未完成", "Task") + ")";
			if (this.NewbieGroup == string.Empty)
			{
				this.State.text = LanguageManage.GetTextByKey("跳转", "Task");
			}
			else
			{
				this.State.text = LanguageManage.GetTextByKey("点击进入引导", "Task");
			}
			if (this.Task_Build_index == 33 && this.Task_Build_Second_index == 1 && UnitConst.GetInstance().buildingConst[SenceManager.inst.MainBuilding.index].lvInfos[SenceManager.inst.MainBuilding.lv + 1].needCommandLevel > HeroInfo.GetInstance().playerlevel)
			{
				this.State.text = LanguageManage.GetTextByKey("玩家需要", "build") + UnitConst.GetInstance().buildingConst[SenceManager.inst.MainBuilding.index].lvInfos[SenceManager.inst.MainBuilding.lv + 1].needCommandLevel + LanguageManage.GetTextByKey("级才能开启司令部升级", "build");
				this.CannotFinish = true;
			}
			if (this.Task_Build_index == 40)
			{
				int num = HeroInfo.GetInstance().All_PeopleNum - HeroInfo.GetInstance().All_PeopleNum_Occupy;
				int num2 = (int)(((float)this.Task_Build_Step - this.step_now) * (float)UnitConst.GetInstance().soldierConst[this.Task_Build_Second_index].peopleNum) - num;
				if (num2 > 0)
				{
					this.State.text = LanguageManage.GetTextByKey("人口不足", "Army") + "，还需人口：" + num2;
					this.CannotFinish = true;
				}
			}
			this.State.color = new Color(1f, 1f, 1f, 1f);
		}
		if (base.name == "Task_R1")
		{
			this.Title.SetDimensions(200, 100);
		}
		else if (base.name == "Task_R2")
		{
			this.Title.SetDimensions(350, 22);
		}
	}

	private void Update()
	{
		this.gif_stay_time0 -= Time.deltaTime;
		if (this.gif_stay_time0 < 0f)
		{
			if (this.no > 9)
			{
				this.no = 0;
			}
			if (this.NewBieGif)
			{
				this.NewBieGif.spriteName = "zi_0000" + this.no;
			}
			this.gif_time += Time.deltaTime;
			if (this.gif_time >= this.gif_speed)
			{
				this.gif_time = 0f;
				this.no++;
				if (this.no > 9)
				{
					this.gif_stay_time0 = this.gif_stay_time;
				}
			}
		}
		if (!this.Finish)
		{
		}
	}
}
