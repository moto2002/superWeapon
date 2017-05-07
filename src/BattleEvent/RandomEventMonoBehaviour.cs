using msg;
using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

namespace BattleEvent
{
	[RequireComponent(typeof(SphereCollider))]
	public class RandomEventMonoBehaviour : PointingBox
	{
		public int eventKey;

		public int boxKey;

		public int boxIndex;

		private Body_Model body;

		public T_EventBoxSelectState SelectState;

		public Character.CharacterSelectStates randomBoxSelectState = Character.CharacterSelectStates.Idle;

		private bool isOpen;

		private Ray ray;

		private RaycastHit hit;

		private void Start()
		{
			base.StartCoroutine(this.JLS());
		}

		[DebuggerHidden]
		private IEnumerator JLS()
		{
			RandomEventMonoBehaviour.<JLS>c__Iterator2D <JLS>c__Iterator2D = new RandomEventMonoBehaviour.<JLS>c__Iterator2D();
			<JLS>c__Iterator2D.<>f__this = this;
			return <JLS>c__Iterator2D;
		}

		private void OnTriggerEnter(Collider other)
		{
			if (this.isOpen)
			{
				return;
			}
			if (other.CompareTag("Tank") && other.GetComponent<Character>().charaType == Enum_CharaType.attacker)
			{
				if (UIManager.curState != SenceState.WatchVideo)
				{
					this.OpenBox(this.eventKey);
					EventNoteMgr.NoticeOpenBox(this.boxIndex);
				}
				else
				{
					UnityEngine.Object.Destroy(base.gameObject);
				}
				Body_Model effectByName = PoolManage.Ins.GetEffectByName("xiangzi_kaiqi", null);
				effectByName.tr.position = base.transform.position + new Vector3(0f, 0.2f, 0f);
				effectByName.DesInsInPool(effectByName.GetComponentInChildren<ParticleSystem>().duration);
				this.isOpen = true;
				if (!RandomEventManage.RandomEventConst.ContainsKey(this.eventKey))
				{
					HUDTextTool.inst.SetText("不包含事件Key的随机事件表+ " + this.eventKey, HUDTextTool.TextUITypeEnum.Num5);
					return;
				}
				switch (RandomEventManage.RandomEventConst[this.eventKey].eventID)
				{
				case 1:
				{
					int num = int.Parse(RandomEventManage.RandomEventConst[this.eventKey].num1.Split(new char[]
					{
						':'
					})[1]);
					break;
				}
				case 2:
				{
					int num = int.Parse(RandomEventManage.RandomEventConst[this.eventKey].num1.Split(new char[]
					{
						':'
					})[1]);
					int num2 = RandomEventManage.RandomEventConst[this.eventKey].num2;
					for (int i = 0; i < SenceManager.inst.Tanks_Attack.Count; i++)
					{
						T_TankAbstract t_TankAbstract = SenceManager.inst.Tanks_Attack[i];
						if (t_TankAbstract != null && (Vector3.Distance(base.transform.position, t_TankAbstract.tr.position) <= (float)num2 || num2 == 0))
						{
							t_TankAbstract.DoHurt((int)((float)(t_TankAbstract.MaxLife * num) * -0.01f));
						}
					}
					break;
				}
				case 4:
				{
					int index = int.Parse(RandomEventManage.RandomEventConst[this.eventKey].num1.Split(new char[]
					{
						':'
					})[0]);
					int num = int.Parse(RandomEventManage.RandomEventConst[this.eventKey].num1.Split(new char[]
					{
						':'
					})[1]);
					for (int j = 0; j < num; j++)
					{
						T_Tank tank = PoolManage.Ins.GetTank<T_Tank>(this.tr.position, Quaternion.identity, SenceManager.inst.tankPool);
						tank.roleType = Enum_RoleType.tank;
						tank.charaType = Enum_CharaType.attacker;
						tank.index = index;
						if (ActivityManager.GetIns().IsFromAct && ActivityManager.GetIns().curActItem.soliders.Count > 0)
						{
							tank.lv = (from p in ActivityManager.GetIns().curActItem.soliders
							where p.id == tank.index
							select p).FirstOrDefault<ActivityItem.Solider>().lv;
						}
						else
						{
							tank.lv = ((!HeroInfo.GetInstance().PlayerArmyData.ContainsKey(tank.index)) ? HeroInfo.GetInstance().playerlevel : HeroInfo.GetInstance().PlayerArmyData[tank.index].level);
						}
						tank.InitInfo();
						SenceManager.inst.Tanks_Attack.Add(tank);
					}
					break;
				}
				case 5:
				{
					int num = int.Parse(RandomEventManage.RandomEventConst[this.eventKey].num1.Split(new char[]
					{
						':'
					})[1]);
					int num2 = RandomEventManage.RandomEventConst[this.eventKey].num2;
					for (int k = 0; k < SenceManager.inst.Tanks_Attack.Count; k++)
					{
						T_TankAbstract t_TankAbstract2 = SenceManager.inst.Tanks_Attack[k];
						if (t_TankAbstract2 != null && (Vector3.Distance(base.transform.position, t_TankAbstract2.tr.position) <= (float)num2 || num2 == 0))
						{
							t_TankAbstract2.MyBuffRuntime.AddBuffIndex(0, null, new int[]
							{
								num
							});
						}
					}
					break;
				}
				case 6:
				{
					int num = int.Parse(RandomEventManage.RandomEventConst[this.eventKey].num1.Split(new char[]
					{
						':'
					})[1]);
					int num2 = RandomEventManage.RandomEventConst[this.eventKey].num2;
					for (int l = 0; l < SenceManager.inst.Tanks_Attack.Count; l++)
					{
						T_TankAbstract t_TankAbstract3 = SenceManager.inst.Tanks_Attack[l];
						if (t_TankAbstract3 != null && (Vector3.Distance(base.transform.position, t_TankAbstract3.tr.position) <= (float)num2 || num2 == 0))
						{
							t_TankAbstract3.DoHurt((int)((float)(t_TankAbstract3.MaxLife * num) * 0.01f));
						}
					}
					break;
				}
				case 7:
				{
					int num = int.Parse(RandomEventManage.RandomEventConst[this.eventKey].num1.Split(new char[]
					{
						':'
					})[1]);
					if (FightPanelManager.inst.BoxSkill.activeSelf)
					{
						HUDTextTool.inst.SetText("指挥官，我们已经有支援了", HUDTextTool.TextUITypeEnum.Num5);
					}
					else
					{
						FightPanelManager.inst.OnBoxSkillSet(num);
					}
					break;
				}
				case 8:
				{
					int num = int.Parse(RandomEventManage.RandomEventConst[this.eventKey].num1.Split(new char[]
					{
						':'
					})[1]);
					FightPanelManager.inst.OnEnd(num);
					break;
				}
				}
				if (NewbieGuidePanel._instance.isStrikeBox)
				{
					NewbieGuidePanel._instance.box = null;
					NewbieGuidePanel._instance.HideSelf();
					HUDTextTool.inst.NextLuaCallByIsEnemyAttck("随机事件箱子 碰撞 调用···", new object[]
					{
						this.ga
					});
				}
			}
		}

		private void OpenBox(int eventID)
		{
			if (this.body)
			{
				this.body.DesInsInPool();
			}
			LogManage.Log("开箱子 " + eventID);
			CSOpenBox cSOpenBox = new CSOpenBox();
			cSOpenBox.id = eventID;
			ClientMgr.GetNet().SendHttp(5020, cSOpenBox, null, null);
			UnityEngine.Object.Destroy(this.ga);
		}

		private void OpenBoxCallBack(bool isError, Opcode code)
		{
		}

		public override void OnMouseUp()
		{
			MovePoint.ChangeEventTaget(this);
			this.ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(this.ray, out this.hit) && this.hit.collider.CompareTag("RandomEventBox"))
			{
				EventNoteMgr.NoticeFoceMove(this.hit.point);
				SenceManager.inst.ForceMoveRandomEventBox(this.hit.point);
			}
		}
	}
}
