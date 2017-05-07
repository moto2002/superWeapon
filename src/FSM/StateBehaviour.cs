using System;
using UnityEngine;

namespace FSM
{
	[RequireComponent(typeof(StateEngine))]
	public class StateBehaviour : MonoBehaviour
	{
		private StateEngine _stateMachine;

		public StateEngine stateMachine
		{
			get
			{
				if (this._stateMachine == null)
				{
					this._stateMachine = base.GetComponent<StateEngine>();
				}
				if (this._stateMachine == null)
				{
					throw new Exception("Please make sure StateEngine is also present on any StateBehaviour objects");
				}
				return this._stateMachine;
			}
		}

		public Enum GetState()
		{
			return this.stateMachine.GetState();
		}

		protected void Initialize<T>()
		{
			this.stateMachine.Initialize<T>(this);
		}

		public virtual void ChangeState(Enum newState)
		{
			this.stateMachine.ChangeState(newState, StateTransition.Safe);
		}

		public virtual void ChangeState(Enum newState, StateTransition transition)
		{
			this.stateMachine.ChangeState(newState, transition);
		}
	}
}
