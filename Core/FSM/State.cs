///
/// DotOriko v1.0
/// State class used by FSM
/// By NoxCaos 10.02.2016
///

using System.Collections;

namespace DotOriko.Core.FSM {
    public abstract class State : DotOrikoComponent {

		public FSM FSM {
            get {
                return this.fsm;
            } internal set {
                this.fsm = value;
                this.container = value.container;
            }
        }

        protected FSMContainer container { get; private set; }
        protected object[] arguments;

        private FSM fsm;

        protected override void OnInitialize() {
            base.OnInitialize();
        }

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

        protected abstract IEnumerator StateRun();

        protected abstract IEnumerator StateFinish();

        protected virtual void OnStateUpdate() { }

        public IEnumerator FinishState() {
            yield return this.StartCoroutine(this.StateFinish());
			Destroy (this);
        }

        internal void SetArgs(object[] args) {
            this.arguments = args;
        }
    }
}
