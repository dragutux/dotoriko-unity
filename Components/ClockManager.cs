using UnityEngine;
using DotOriko.Core.Time;

namespace DotOriko.Core.Components {
	public class ClockManager : DotOrikoComponent {

		private Clock clock;

		void Start() {
			clock = new Clock(24);
			this.StartScheduledUpdate (1);
		}

		protected override void OnScheduledUpdate () {
			base.OnScheduledUpdate ();

			Debug.Log(this.clock.GetTime ());
		}
	}
}