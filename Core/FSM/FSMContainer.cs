///
/// DotOriko v1.0
/// Object containing FSM
/// By NoxCaos 10.02.2016
///

namespace DotOriko.Core.FSM {

	public abstract class FSMContainer : DotOrikoComponent {

		public FSM stateMachine { get; private set;}

		protected override void OnStart () {
			base.OnStart ();

			this.stateMachine = this.gameObject.AddComponent<FSM> ();
            this.stateMachine.container = this;
		}
	}
}
