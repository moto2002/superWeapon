using FSM;
using System;
using System.Collections;
using System.Diagnostics;

namespace BattleEvent
{
	public class T_EventBoxSelectState : StateBehaviour
	{
		public RandomEventMonoBehaviour myBoxEvent;

		private Body_Model selectEffect;

		private void Awake()
		{
			base.Initialize<Character.CharacterSelectStates>();
			if (this.myBoxEvent == null)
			{
				this.myBoxEvent = base.GetComponentInParent<RandomEventMonoBehaviour>();
			}
			this.ChangeState(Character.CharacterSelectStates.Idle);
		}

		private void Idle_Enter()
		{
			this.myBoxEvent.randomBoxSelectState = Character.CharacterSelectStates.Idle;
		}

		[DebuggerHidden]
		private IEnumerator Selected_Enter()
		{
			T_EventBoxSelectState.<Selected_Enter>c__Iterator2E <Selected_Enter>c__Iterator2E = new T_EventBoxSelectState.<Selected_Enter>c__Iterator2E();
			<Selected_Enter>c__Iterator2E.<>f__this = this;
			return <Selected_Enter>c__Iterator2E;
		}

		private void Selected_Exit()
		{
			if (this.selectEffect)
			{
				this.selectEffect.DesInsInPool(0f);
			}
		}
	}
}
