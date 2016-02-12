///
/// DotOriko v1.0
/// State class used by FSM
/// By NoxCaos 10.02.2016
///

using System.Collections;

namespace DotOriko.Core.FSM {
    public abstract class State : DotOrikoComponent {

		public FSM FSM { get; internal set;}

        protected sealed override void OnStart() {
            base.OnStart();

            this.OnStateInit();
			this.StartCoroutine (this.StateRun ());
        }

        protected sealed override void OnReleaseResources() {
            base.OnReleaseResources();

            this.StartCoroutine(this.StateFinish());
        }

        protected sealed override void OnUpdate() {
            base.OnUpdate();

            this.OnStateUpdate();
        }

        protected virtual void OnStateInit() { }

		protected virtual IEnumerator StateRun() {
			yield return null;
		}

        protected virtual IEnumerator StateFinish() { 
			yield return null;
		}

        protected virtual void OnStateUpdate() { }

        public IEnumerator FinishState() {
            yield return this.StartCoroutine(this.StateFinish());
			Destroy (this);
        }
    }
}
