using FSM;
using System;
using System.Collections;
using System.Diagnostics;

public class T_TankSelectState : StateBehaviour
{
	private T_TankAbstract myTank;

	private Body_Model selectEffect;

	public T_TankAbstract MyTank
	{
		get
		{
			if (this.myTank == null)
			{
				this.myTank = base.GetComponentInParent<T_TankAbstract>();
			}
			return this.myTank;
		}
	}

	private void Awake()
	{
		base.Initialize<Character.CharacterSelectStates>();
		this.ChangeState(Character.CharacterSelectStates.Idle);
	}

	private void Idle_Enter()
	{
		this.MyTank.characterSelectStates = Character.CharacterSelectStates.Idle;
	}

	[DebuggerHidden]
	private IEnumerator Selected_Enter()
	{
		T_TankSelectState.<Selected_Enter>c__Iterator38 <Selected_Enter>c__Iterator = new T_TankSelectState.<Selected_Enter>c__Iterator38();
		<Selected_Enter>c__Iterator.<>f__this = this;
		return <Selected_Enter>c__Iterator;
	}

	private void Selected_Exit()
	{
		if (this.selectEffect)
		{
			this.selectEffect.DesInsInPool();
		}
	}
}
