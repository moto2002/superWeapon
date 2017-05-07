using System;

public class SetingPanelManage : FuncUIPanel
{
	public UILabel vip;

	public UILabel name_Client;

	public UILabel lv;

	public UILabel exprecess;

	public UILabel jiangPei;

	public UILabel zhanghao;

	public UILabel jingongZhandouli;

	public UILabel fuwuqi;

	public UILabel fangshouzhandouli;

	public UISprite lvProcess;

	private void Start()
	{
	}

	public override void Awake()
	{
	}

	private void Refresh()
	{
		this.name_Client.text = HeroInfo.GetInstance().userName;
		this.lv.text = HeroInfo.GetInstance().playerlevel.ToString();
		this.zhanghao.text = HeroInfo.GetInstance().userId.ToString();
		this.jiangPei.text = HeroInfo.GetInstance().playerRes.playermedal.ToString();
	}

	public override void OnEnable()
	{
		this.Refresh();
		base.OnEnable();
	}

	public override void OnDisable()
	{
		base.OnDisable();
	}
}
