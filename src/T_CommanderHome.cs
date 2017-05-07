using System;
using UnityEngine;

public class T_CommanderHome : Character
{
	public T_CommanderRoad TC_Road;

	private float speed;

	private Transform MB;

	private int mb;

	private float time1;

	private float time0;

	private int this_index;

	private float click_protect_time;

	private bool live = true;

	private int talk_id;

	private float talk_time;

	public void Sound_Play()
	{
		if (this.click_protect_time <= 0f)
		{
			this.click_protect_time = 2f;
			int num = this.this_index;
			if (num != 1)
			{
				if (num == 2)
				{
					AudioManage.inst.PlayAuidoBySelf_2D("tanya", null, false, 0uL);
				}
			}
			else
			{
				AudioManage.inst.PlayAuidoBySelf_2D("yescommand", null, false, 0uL);
			}
		}
	}

	public void Init(int index)
	{
		this.this_index = index;
		this.ModelBody = PoolManage.Ins.GetModelByBundleByName(UnitConst.GetInstance().soldierList[index].bodyId, base.transform);
		if (this.ModelBody && this.ModelBody.BlueModel)
		{
			this.ModelBody.BlueModel.gameObject.SetActive(true);
		}
		if (this.ModelBody && this.ModelBody.RedModel)
		{
			this.ModelBody.RedModel.gameObject.SetActive(false);
		}
		this.mb = UnityEngine.Random.Range(0, this.TC_Road.T_CommanderRoad_tr.Length - 1);
		base.transform.position = this.TC_Road.T_CommanderRoad_tr[this.mb].transform.position;
		this.ModelBody.tr.localScale = Vector3.one * 3f;
		this.MB = this.TC_Road.T_CommanderRoad_tr[this.mb];
		this.AnimationControler = GameTools.GetCompentIfNoAddOne<Ani_CharacterControler>(this.ModelBody.ga);
		this.AnimationControler.SetAnimaInfo();
		this.AnimationControler.AnimPlay("Idle");
		this.speed = 3.5f;
		this.time0 = (float)UnityEngine.Random.Range(0, 5);
		SenceManager.inst.Tanks_CommanderHome.Add(this);
		this.talk_id = 1;
		SenceManager.inst.CommanderHome_TalkText = UnitConst.GetInstance().CommanderTalkList[this.talk_id].description;
	}

	private void Update()
	{
		if (this.click_protect_time > 0f)
		{
			this.click_protect_time -= Time.deltaTime;
		}
		if (this.ModelBody == null || this.AnimationControler == null)
		{
			return;
		}
		if (HeroInfo.GetInstance().Commando_Fight != null && HeroInfo.GetInstance().Commando_Fight.index != this.this_index)
		{
			this.ModelBody.DesInsInPool();
			this.Init(HeroInfo.GetInstance().Commando_Fight.index);
		}
		this.talk_time += Time.deltaTime;
		if (this.talk_time >= 10f)
		{
			this.talk_time = 0f;
			this.talk_id++;
			if (this.talk_id > UnitConst.GetInstance().CommanderTalkList.Count)
			{
				this.talk_id = 1;
			}
			SenceManager.inst.CommanderHome_TalkText = UnitConst.GetInstance().CommanderTalkList[this.talk_id].description;
		}
		if (this.live)
		{
			base.transform.position = new Vector3(Mathf.MoveTowards(base.transform.position.x, this.MB.transform.position.x, this.speed * Time.deltaTime), base.transform.position.y, Mathf.MoveTowards(base.transform.position.z, this.MB.transform.position.z, this.speed * Time.deltaTime));
			float num = Vector2.Distance(new Vector2(base.transform.position.x, base.transform.position.z), new Vector2(this.MB.transform.position.x, this.MB.transform.position.z));
			if (num <= 1f)
			{
				this.AnimationControler.AnimPlay("Idle");
				this.time1 += Time.deltaTime;
				if (this.time1 >= this.time0)
				{
					this.time1 = 0f;
					this.time0 = (float)UnityEngine.Random.Range(-10, 10);
					if (this.time0 >= 0f)
					{
						this.mb++;
						if (this.mb >= this.TC_Road.T_CommanderRoad_tr.Length)
						{
							this.mb = 0;
						}
					}
					else
					{
						this.mb--;
						if (this.mb < 0)
						{
							this.mb = this.TC_Road.T_CommanderRoad_tr.Length - 1;
						}
						this.time0 = (float)UnityEngine.Random.Range(0, 10);
					}
					this.MB = this.TC_Road.T_CommanderRoad_tr[this.mb];
				}
			}
			else
			{
				base.transform.LookAt(this.MB);
				this.AnimationControler.AnimPlay("Run");
			}
		}
	}

	public override Vector3 GetVPos(T_TankAbstract thisTank)
	{
		return Vector3.zero;
	}
}
