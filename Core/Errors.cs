
namespace DotOriko.Core {
    public static class Errors {

        public static string objectPoolAddError = "You're trying to add an object that is already in pool. This is not supported";
        public static string sceneManagerShowError = "SceneManager has no '{0}' cached";
        public static string configLoadError = "[Data Config] {0}";

        public static void Log(string err, params string[] args) {
            UnityEngine.Debug.LogError("[DotOriko] " + string.Format(err, args));
        }
    }
}
