namespace DotOriko.Network {

	internal class Debugger : DotOriko.Utils.Debugger {

        public static new void Log (object obj) {
			DotOriko.Utils.Debugger.Log("[Network] " + obj.ToString());
        }

        public static new void Warn (object obj) {
			DotOriko.Utils.Debugger.Log("[Network] " + obj.ToString());
        }

        public static new void Err (object obj) {
			DotOriko.Utils.Debugger.Log("[Network] " + obj.ToString());
        }
    }
}