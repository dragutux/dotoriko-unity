///
/// DotOriko v1.0
/// VisualObservable items should extend this class
/// VisualObservables are attached to gameobjects
/// to store data along with prefabs
/// By NoxCaos 13.02.2016
///

using System;

namespace DotOriko.Core.Model {
    public class VisualObservable : DotOrikoComponent, IObservable {
        public void AttachObserver(IObserver observer) {
            throw new NotImplementedException();
        }

        public void DetachObserver(IObserver observer) {
            throw new NotImplementedException();
        }

        public void Notify() {
            throw new NotImplementedException();
        }
    }
}
