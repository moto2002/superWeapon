using FSM;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class T_TowerSelectState : StateBehaviour
{
	private T_Tower myTower;

	private Body_Model selectEffect;

	private Body_Model selectEffect_Home;

	public T_Tower MyTower
	{
		get
		{
			if (this.myTower == null)
			{
				this.myTower = base.GetComponentInParent<T_Tower>();
			}
			return this.myTower;
		}
	}

	private void Awake()
	{
		base.Initialize<Character.CharacterSelectStates>();
		this.ChangeState(Character.CharacterSelectStates.Idle);
	}

	private void Idle_Enter()
	{
		this.MyTower.characterSelectStates = Character.CharacterSelectStates.Idle;
	}

	[DebuggerHidden]
	private IEnumerator Selected_Enter()
	{
		T_TowerSelectState.<Selected_Enter>c__Iterator39 <Selected_Enter>c__Iterator = new T_TowerSelectState.<Selected_Enter>c__Iterator39();
		<Selected_Enter>c__Iterator.<>f__this = this;
		return <Selected_Enter>c__Iterator;
	}

	private void Selected_Exit()
	{
		if (this.selectEffect)
		{
			this.selectEffect.DesInsInPool();
		}
		if (this.selectEffect_Home)
		{
			this.selectEffect_Home.DesInsInPool();
		}
		if (this.MyTower.xuetiao && this.MyTower.CurLife >= (float)this.MyTower.MaxLife)
		{
			this.MyTower.xuetiao.SetActive(false);
		}
		if (this.MyTower.m_lifeInfo != null)
		{
			UnityEngine.Object.Destroy(this.MyTower.m_lifeInfo.gameObject);
		}
		this.MyTower.isChoose = true;
		this.MyTower.tr.position = new Vector3(this.MyTower.tr.position.x, 0f, this.MyTower.tr.position.z);
		this.MyTower.RebackShader();
	}
}
