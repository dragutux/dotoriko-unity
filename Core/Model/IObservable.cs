///
/// DotOriko v1.0
/// Observable Interface
/// By NoxCaos 11.02.2016
///

namespace DotOriko.Core.Model {
    public interface IObservable {

        void AttachObserver(IObserver observer);

        void DetachObserver(IObserver observer);

        void Notify();
    }
}
