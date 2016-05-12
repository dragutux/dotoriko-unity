///
/// DotOriko v1.0
/// Finite State Machine
/// By NoxCaos 10.02.2016
///

using LinqTools;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace DotOriko.Core.FSM {

    internal class Transition {
        public string from;
        public string to;

		public Transition(Type f, Type t) {
			if (f.IsSubclassOf (typeof(State)) && 
				t.IsSubclassOf (typeof(State))) {
				this.from = f.ToString ();
				this.to = t.ToString ();
			} else {
				throw new TypeLoadException("Types of FSM should extend State");
			}
        }
    }

    public sealed class FSM : DotOrikoComponent {
        private List<Transition> transitions;

        private State currentState;

        public FSMContainer container { get; internal set;}
		public bool IsMakingTransition { get; private set;}

        public FSM() {
			this.transitions = new List<Transition> ();
		}

        internal void AddTransition<T, B>() where T:State where B:State {
			this.AddTransition(new Transition(typeof(T), typeof(B)));
		}

        internal void AddTransitions(params Transition[] transitions) {
            foreach(var t in transitions) this.AddTransition(t);
        }

        internal void AddTransition(Transition t) {
			this.transitions.Add (t);
        }

        public bool CanMakeTransitionTo(Type state) {
            if (this.currentState == null) return true;
            else return this.transitions
                .Where(f => f.from == this.currentState.GetType().ToString())
				.Where(f => f.to == state.ToString()) != null;
        }

        public void ApplyState<T>(params object[] args) where T : State {
            if (this.CanMakeTransitionTo(typeof(T))) {
				this.StartCoroutine(this.MakeTransitionTo<T>(args));
            } else {
                Debug.Log(string.Format("Can't transit from {0} to {1}", 
                    this.currentState.GetType(), typeof(T)));
            }
        }

		internal IEnumerator MakeTransitionTo<T>(object[] args) where T : State {
			this.IsMakingTransition = true;
			if(this.currentState != null) 
				yield return this.StartCoroutine (this.currentState.FinishState ());

			this.currentState = this.gameObject.AddComponent<T>();
            this.currentState.SetArgs(args);
			this.currentState.FSM = this;
			this.IsMakingTransition = false;
        }
    }
}
