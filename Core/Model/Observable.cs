///
/// DotOriko v1.0
/// Observable items should extend this class
/// By NoxCaos 11.02.2016
///

using System.Collections.Generic;

namespace DotOriko.Core.Model {
	public abstract class Observable : IObservable {

		private List<IObserver> observers = new List<IObserver>();

		public static IObserver operator + (Observable s, IObserver o) {
			s.AttachObserver (o);
			return o;
		}

		public static IObserver operator - (Observable s, IObserver o) {
			s.DetachObserver (o);
			return o;
		}

		public void AttachObserver(IObserver observer) {
			this.observers.Add(observer);
		}

		public void DetachObserver(IObserver observer) {
			this.observers.Remove(observer);
		}

		public void Notify() {
			foreach (IObserver o in this.observers) o.OnModelUpdate();
		}
	}
}
