using System.Collections.Generic;
using LinqTools;
using UnityEngine;
using System.Collections;

namespace DotOriko.Core.FSM {

    internal class Transition {
        public State from;
        public State to;

        public Transition(State f, State t) {
            this.from = f;
            this.to = t;
        }
    }

    public sealed class FSM {
        private List<Transition> transitions;

        private State currentState;

        public FSM() { }

        internal void AddTransition(Transition t) {

        }

        public bool CanMakeTransitionTo(State state) {
            if (this.currentState == null || this.currentState.IsUniversal) return true;
            else return this.transitions
                .Where(f => f.from = this.currentState)
                .Where(f => f.to = state) != null;
        }

        public void ApplyState(State state) {
            if (this.CanMakeTransitionTo(state)) {
                
            } else {
                Debug.Log(string.Format("Can't transit from {0} to {1}", 
                    this.currentState.GetType(), state.GetType()));
            }
        }

        private IEnumerator MakeTransitionTo(State state) {
            yield return null;
        }

    }
}
