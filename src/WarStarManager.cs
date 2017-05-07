using System;
using UnityEngine;

public class WarStarManager : MonoBehaviour
{
	public static WarStarManager _inst;

	public int WarStar;

	public int GetResourceNum;

	public int AllTowerNum;

	public int AllTowerNum_0;

	public float AllTowerNum_Percent;

	public float NewAllTowerNum_Percent;

	public float NewAllTowerNum_Percent_0;

	public UISprite This_Sprite;

	public UILabel WS_Percent_Text;

	public UISprite[] Star;

	public UISprite[] Star_BJ;

	public UISprite[] Star_BJ1;

	public UISprite[] Star_BJ2;

	public Vector3[] Star_pos;

	public string StarSpriteName;

	public UILabel Title;

	public float star_fly_time;

	private float[] star_pos_x;

	private float[] star_pos_y;

	private float[] star_pos_rot;

	private float[] newstar;

	public bool StarMoveEnd;

	public int xs_percent;

	public bool MainBuildingDead;

	public void OnDestroy()
	{
		WarStarManager._inst = null;
	}

	private void Awake()
	{
		WarStarManager._inst = this;
	}

	public void Init()
	{
		this.WarStar = 0;
		this.This_Sprite = base.GetComponent<UISprite>();
		this.This_Sprite.alpha = 0f;
		for (int i = 0; i < 3; i++)
		{
			this.Star[i].alpha = 0f;
			this.Star_pos[i] = Vector3.zero;
			this.Star_BJ[i].spriteName = this.StarSpriteName;
			this.Star_BJ1[i].spriteName = this.StarSpriteName;
			this.Star_BJ2[i].spriteName = this.StarSpriteName;
			this.Star_BJ[i].alpha = 1f;
			this.Star_BJ[i].transform.localScale = Vector3.one;
			this.Star_BJ1[i].alpha = this.Star_BJ[i].alpha;
			this.Star_BJ2[i].alpha = this.Star_BJ[i].alpha;
			this.Star_BJ1[i].transform.localScale = Vector3.one;
			this.Star_BJ2[i].transform.localScale = Vector3.one;
		}
		this.AllTowerNum_0 = 0;
		this.xs_percent = 0;
		for (int j = 0; j < SenceManager.inst.towers.Count; j++)
		{
			if (UnitConst.GetInstance().buildingConst[SenceManager.inst.towers[j].index].resType < 3 && UnitConst.GetInstance().buildingConst[SenceManager.inst.towers[j].index].secType != 1)
			{
				this.AllTowerNum_0++;
			}
		}
		this.AllTowerNum = this.AllTowerNum_0;
		this.AllTowerNum_Percent = 0f;
		this.WS_Percent_Text.text = (int)this.AllTowerNum_Percent + "%";
		this.star_pos_x = new float[3];
		this.star_pos_y = new float[3];
		this.star_pos_rot = new float[3];
	}

	private void Start()
	{
		this.StarMoveEnd = true;
		this.MainBuildingDead = false;
		this.Title = base.transform.Find("Title").GetComponent<UILabel>();
		this.Title.text = LanguageManage.GetTextByKey("战果", "Battle");
		this.StarSpriteName = this.Star_BJ[0].spriteName;
		this.Star_BJ1 = new UISprite[3];
		this.Star_BJ2 = new UISprite[3];
		this.newstar = new float[3];
		for (int i = 0; i < 3; i++)
		{
			this.Star_BJ1[i] = (UISprite)UnityEngine.Object.Instantiate(this.Star_BJ[i]);
			this.Star_BJ1[i].transform.parent = this.Star_BJ[i].transform.parent;
			this.Star_BJ2[i] = (UISprite)UnityEngine.Object.Instantiate(this.Star_BJ[i]);
			this.Star_BJ2[i].transform.parent = this.Star_BJ[i].transform.parent;
			this.Star_BJ1[i].depth = this.Star_BJ[i].depth + 1;
			this.Star_BJ2[i].depth = this.Star_BJ[i].depth + 2;
			this.Star[i].depth = this.Star_BJ[i].depth + 3;
			this.Star_BJ1[i].transform.localPosition = this.Star_BJ[i].transform.localPosition;
			this.Star_BJ2[i].transform.localPosition = this.Star_BJ[i].transform.localPosition;
		}
		this.Init();
	}

	public void SomeTowerDead(T_Tower tower)
	{
		if (FightHundler.FightEnd)
		{
			return;
		}
		if (UnitConst.GetInstance().buildingConst[tower.index].resType < 3)
		{
			if (UnitConst.GetInstance().buildingConst[tower.index].secType == 1)
			{
				this.MainBuildingDead = true;
				this.GetWarStar();
				return;
			}
			this.AllTowerNum--;
			this.NewAllTowerNum_Percent = (1f - (float)this.AllTowerNum / (float)this.AllTowerNum_0) * 100f;
			if (this.NewAllTowerNum_Percent_0 < 66.66f && this.NewAllTowerNum_Percent >= 66.66f)
			{
				this.GetWarStar();
			}
			if (this.NewAllTowerNum_Percent >= 100f)
			{
				this.GetWarStar();
			}
			this.NewAllTowerNum_Percent_0 = this.NewAllTowerNum_Percent;
		}
	}

	private void GetWarStar()
	{
		this.WarStar++;
		if (this.MainBuildingDead)
		{
			this.StarMoveEnd = false;
		}
		this.star_fly_time = 10f;
	}

	private void WarStarMove(int i)
	{
		float num = 650f;
		float num2 = -100f;
		if (this.Star_pos[i] == Vector3.zero)
		{
			this.Star_pos[i] = this.Star[i].transform.localPosition;
			this.star_pos_x[i] = num;
			this.star_pos_y[i] = num2;
			this.star_pos_rot[i] = 3600f;
			this.Star[i].transform.localPosition = this.Star_pos[i] + new Vector3(this.star_pos_x[i], this.star_pos_y[i], 0f);
			this.newstar[i] = 0f;
		}
		this.Star[i].alpha = Mathf.Min(1f, this.Star[i].alpha + 1f * Time.deltaTime);
		this.star_pos_rot[i] = Mathf.Max(0f, this.star_pos_rot[i] - 2000f * Time.deltaTime);
		this.Star[i].transform.eulerAngles = new Vector3(0f, this.star_pos_rot[i], 0f);
		this.Star[i].transform.localScale = Vector3.one * (1f + 3f * Mathf.Abs(this.star_pos_x[i]) / Mathf.Abs(num));
		if (this.star_pos_rot[i] <= 2880f)
		{
			this.star_pos_x[i] = Mathf.MoveTowards(this.star_pos_x[i], 0f, Mathf.Abs(num) * 1.5f * Time.deltaTime);
			this.star_pos_y[i] = Mathf.MoveTowards(this.star_pos_y[i], 0f, Mathf.Abs(num2) * 1.5f * Time.deltaTime);
			this.Star[i].transform.localPosition = this.Star_pos[i] + new Vector3(this.star_pos_x[i], this.star_pos_y[i], 0f);
			if (this.Star[i].transform.localScale.x <= 1.1f)
			{
				if (this.newstar[i] == 0f)
				{
					this.newstar[i] = 1f;
					PoolManage.Ins.CreatEffect("huodejingyan", this.Star_BJ[i].transform.position, Quaternion.identity, null);
				}
				this.Star_BJ[i].spriteName = this.Star[i].spriteName;
				this.Star_BJ[i].alpha = Mathf.Max(0f, this.Star_BJ[i].alpha - 1f * Time.deltaTime);
				this.Star_BJ[i].transform.localScale = Vector3.one * (1f - this.Star_BJ[i].alpha) * 4f;
				if (this.Star_BJ[i].alpha < 0.9f)
				{
					this.Star_BJ1[i].spriteName = this.Star[i].spriteName;
					this.Star_BJ1[i].alpha = Mathf.Max(0f, this.Star_BJ1[i].alpha - 1f * Time.deltaTime);
					this.Star_BJ1[i].transform.localScale = Vector3.one * (1f - this.Star_BJ1[i].alpha) * 4f;
					if (this.Star_BJ1[i].alpha < 0.9f)
					{
						this.Star_BJ2[i].spriteName = this.Star[i].spriteName;
						this.Star_BJ2[i].alpha = Mathf.Max(0f, this.Star_BJ2[i].alpha - 1f * Time.deltaTime);
						this.Star_BJ2[i].transform.localScale = Vector3.one * (1f - this.Star_BJ2[i].alpha) * 4f;
						if (this.Star_BJ2[i].alpha <= 0f && i == this.WarStar - 1)
						{
							if (this.MainBuildingDead && !this.StarMoveEnd)
							{
								this.StarMoveEnd = true;
								FightHundler.FightEnd = true;
								if (NewbieGuidePanel.curGuideIndex == -1)
								{
									SenceManager.inst.settType = SettlementType.success;
									CameraInitMove.isPlayMainBuildingAnimation = true;
									HUDTextTool.inst.NextLuaCall("开场战斗结束 调用·  ·", new object[0]);
								}
								else
								{
									SenceManager.inst.settType = SettlementType.success;
									FightHundler.CG_FinishFight();
								}
							}
							this.newstar[i] += Time.deltaTime;
							if (this.newstar[i] >= 2f)
							{
							}
						}
					}
				}
			}
		}
	}

	private void Update()
	{
		if (this.star_fly_time >= 0f)
		{
			this.star_fly_time -= Time.deltaTime;
		}
		this.This_Sprite.alpha = Mathf.Min(1f, this.This_Sprite.alpha + 1f * Time.deltaTime);
		if (this.AllTowerNum_Percent < this.NewAllTowerNum_Percent)
		{
			this.AllTowerNum_Percent = Mathf.Min(this.NewAllTowerNum_Percent, this.AllTowerNum_Percent + (this.NewAllTowerNum_Percent - this.AllTowerNum_Percent + 20f) * 2f * Time.deltaTime);
			if (this.xs_percent != (int)this.AllTowerNum_Percent)
			{
				this.xs_percent = (int)this.AllTowerNum_Percent;
			}
		}
		else
		{
			this.AllTowerNum_Percent = this.NewAllTowerNum_Percent;
			this.xs_percent = (int)this.AllTowerNum_Percent;
		}
		if (this.MainBuildingDead)
		{
			this.WS_Percent_Text.text = (int)((float)this.xs_percent * 0.9f + 10f + 0.5f) + "%";
		}
		else
		{
			this.WS_Percent_Text.text = (int)((float)this.xs_percent * 0.9f + 0.5f) + "%";
		}
		if (this.WarStar > 3)
		{
			this.Init();
		}
		else
		{
			for (int i = 0; i < this.WarStar; i++)
			{
				this.WarStarMove(i);
			}
		}
	}
}
