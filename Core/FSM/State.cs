
namespace DotOriko.Core.FSM {
    public abstract class State : DotOrikoComponent {

        public FSM FSM;
        public bool IsUniversal;

        protected sealed override void OnStart() {
            base.OnStart();

            this.OnStateInit();
        }

        protected sealed override void OnReleaseResources() {
            base.OnReleaseResources();

            this.OnStateFinished();
        }

        protected sealed override void OnUpdate() {
            base.OnUpdate();

            this.OnStateUpdate();
        }

        protected virtual void OnStateInit() { }

        protected virtual void OnStateFinished() { }

        protected virtual void OnStateUpdate() { }

        public void FinishState() {
            this.OnStateFinished();
        }
    }
}
