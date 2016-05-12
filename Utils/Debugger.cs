namespace DotOriko.Utils {

    public class Debugger {

        // 0 - errors
        // 1 - warnings
        // 2 - logs
        private const int DEBUG_LEVEL = 2;

        public static void Log (object obj) {
            if (DEBUG_LEVEL >= 2) {
                UnityEngine.Debug.Log("[Log] " + obj.ToString() + "\n");
            }
        }

        public static void Warn (object obj) {
            if (DEBUG_LEVEL >= 1) {
                UnityEngine.Debug.Log("[Warning] " + obj.ToString() + "\n");
            }
        }

        public static void Err (object obj) {
            if (DEBUG_LEVEL >= 0) {
                UnityEngine.Debug.Log("[Error] " + obj.ToString() + "\n");
            }
        }
    }
}