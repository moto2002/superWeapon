using System;
using System.Collections;

namespace FSM
{
	public class StateMapping
	{
		public Enum state;

		public Func<IEnumerator> Enter = new Func<IEnumerator>(StateEngine.DoNothingCoroutine);

		public Func<IEnumerator> Exit = new Func<IEnumerator>(StateEngine.DoNothingCoroutine);

		public Action Finally = new Action(StateEngine.DoNothing);

		public Action Update = new Action(StateEngine.DoNothing);

		public Action LateUpdate = new Action(StateEngine.DoNothing);

		public Action FixedUpdate = new Action(StateEngine.DoNothing);

		public StateMapping(Enum state)
		{
			this.state = state;
		}
	}
}
