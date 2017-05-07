using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace FSM
{
	public class StateEngine : MonoBehaviour
	{
		private StateMapping currentState;

		private StateMapping destinationState;

		private Dictionary<Enum, StateMapping> stateLookup;

		private Dictionary<string, Delegate> methodLookup;

		private readonly string[] ignoredNames = new string[]
		{
			"add",
			"remove",
			"get",
			"set"
		};

		private bool isInTransition;

		private IEnumerator currentTransition;

		private IEnumerator exitRoutine;

		private IEnumerator enterRoutine;

		private IEnumerator queuedChange;

		public event Action<Enum> Changed
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.Changed = (Action<Enum>)Delegate.Combine(this.Changed, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.Changed = (Action<Enum>)Delegate.Remove(this.Changed, value);
			}
		}

		public bool IsInTransition
		{
			get
			{
				return this.isInTransition;
			}
		}

		public void Initialize<T>(StateBehaviour entity)
		{
			Array values = Enum.GetValues(typeof(T));
			this.stateLookup = new Dictionary<Enum, StateMapping>();
			for (int i = 0; i < values.Length; i++)
			{
				StateMapping stateMapping = new StateMapping((Enum)values.GetValue(i));
				this.stateLookup.Add(stateMapping.state, stateMapping);
			}
			MethodInfo[] methods = entity.GetType().GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			char[] separator = "_".ToCharArray();
			for (int j = 0; j < methods.Length; j++)
			{
				string[] array = methods[j].Name.Split(separator);
				if (array.Length > 1)
				{
					Enum key;
					try
					{
						key = (Enum)Enum.Parse(typeof(T), array[0]);
					}
					catch (ArgumentException)
					{
						for (int k = 0; k < this.ignoredNames.Length; k++)
						{
							if (array[0] == this.ignoredNames[k])
							{
								break;
							}
						}
						goto IL_2D9;
					}
					StateMapping stateMapping2 = this.stateLookup[key];
					string text = array[1];
					switch (text)
					{
					case "Enter":
						if (methods[j].ReturnType == typeof(IEnumerator))
						{
							stateMapping2.Enter = this.CreateDelegate<Func<IEnumerator>>(methods[j], entity);
						}
						else
						{
							Action action = this.CreateDelegate<Action>(methods[j], entity);
							stateMapping2.Enter = delegate
							{
								action();
								return null;
							};
						}
						break;
					case "Exit":
						if (methods[j].ReturnType == typeof(IEnumerator))
						{
							stateMapping2.Exit = this.CreateDelegate<Func<IEnumerator>>(methods[j], entity);
						}
						else
						{
							Action action = this.CreateDelegate<Action>(methods[j], entity);
							stateMapping2.Exit = delegate
							{
								action();
								return null;
							};
						}
						break;
					case "Finally":
						stateMapping2.Finally = this.CreateDelegate<Action>(methods[j], entity);
						break;
					case "Update":
						stateMapping2.Update = this.CreateDelegate<Action>(methods[j], entity);
						break;
					case "LateUpdate":
						stateMapping2.LateUpdate = this.CreateDelegate<Action>(methods[j], entity);
						break;
					case "FixedUpdate":
						stateMapping2.FixedUpdate = this.CreateDelegate<Action>(methods[j], entity);
						break;
					}
				}
				IL_2D9:;
			}
		}

		private V CreateDelegate<V>(MethodInfo method, object target) where V : class
		{
			V v = Delegate.CreateDelegate(typeof(V), target, method) as V;
			if (v == null)
			{
				throw new ArgumentException("Unabled to create delegate for method called " + method.Name);
			}
			return v;
		}

		public void ChangeState(Enum newState, StateTransition transition = StateTransition.Safe)
		{
			if (this.stateLookup == null)
			{
				throw new Exception("States have not been configured, please call initialized before trying to set state");
			}
			if (!this.stateLookup.ContainsKey(newState))
			{
				throw new Exception("No state with the name " + newState.ToString() + " can be found. Please make sure you are called the correct type the statemachine was initialized with");
			}
			StateMapping stateMapping = this.stateLookup[newState];
			if (this.currentState == stateMapping)
			{
				return;
			}
			if (this.queuedChange != null)
			{
				base.StopCoroutine(this.queuedChange);
				this.queuedChange = null;
			}
			if (transition != StateTransition.Overwrite)
			{
				if (transition == StateTransition.Safe)
				{
					if (this.isInTransition)
					{
						if (this.exitRoutine != null)
						{
							this.destinationState = stateMapping;
							return;
						}
						if (this.enterRoutine != null)
						{
							this.queuedChange = this.WaitForPreviousTransition(stateMapping);
							base.StartCoroutine(this.queuedChange);
							return;
						}
					}
				}
			}
			else
			{
				if (this.currentTransition != null)
				{
					base.StopCoroutine(this.currentTransition);
				}
				if (this.exitRoutine != null)
				{
					base.StopCoroutine(this.exitRoutine);
				}
				if (this.enterRoutine != null)
				{
					base.StopCoroutine(this.enterRoutine);
				}
				if (this.currentState != null)
				{
					this.currentState.Finally();
				}
				this.currentState = null;
			}
			this.isInTransition = true;
			this.currentTransition = this.ChangeToNewStateRoutine(stateMapping);
			base.StartCoroutine(this.currentTransition);
		}

		[DebuggerHidden]
		private IEnumerator ChangeToNewStateRoutine(StateMapping newState)
		{
			StateEngine.<ChangeToNewStateRoutine>c__Iterator40 <ChangeToNewStateRoutine>c__Iterator = new StateEngine.<ChangeToNewStateRoutine>c__Iterator40();
			<ChangeToNewStateRoutine>c__Iterator.newState = newState;
			<ChangeToNewStateRoutine>c__Iterator.<$>newState = newState;
			<ChangeToNewStateRoutine>c__Iterator.<>f__this = this;
			return <ChangeToNewStateRoutine>c__Iterator;
		}

		[DebuggerHidden]
		private IEnumerator WaitForPreviousTransition(StateMapping nextState)
		{
			StateEngine.<WaitForPreviousTransition>c__Iterator41 <WaitForPreviousTransition>c__Iterator = new StateEngine.<WaitForPreviousTransition>c__Iterator41();
			<WaitForPreviousTransition>c__Iterator.nextState = nextState;
			<WaitForPreviousTransition>c__Iterator.<$>nextState = nextState;
			<WaitForPreviousTransition>c__Iterator.<>f__this = this;
			return <WaitForPreviousTransition>c__Iterator;
		}

		private void FixedUpdate()
		{
			if (this.currentState != null)
			{
				this.currentState.FixedUpdate();
			}
		}

		private void Update()
		{
			if (this.currentState != null && !this.IsInTransition)
			{
				this.currentState.Update();
			}
		}

		private void LateUpdate()
		{
			if (this.currentState != null && !this.IsInTransition)
			{
				this.currentState.LateUpdate();
			}
		}

		public static void DoNothing()
		{
		}

		public static void DoNothingCollider(Collider other)
		{
		}

		public static void DoNothingCollision(Collision other)
		{
		}

		[DebuggerHidden]
		public static IEnumerator DoNothingCoroutine()
		{
			return new StateEngine.<DoNothingCoroutine>c__Iterator42();
		}

		public Enum GetState()
		{
			if (this.currentState != null)
			{
				return this.currentState.state;
			}
			return null;
		}
	}
}
