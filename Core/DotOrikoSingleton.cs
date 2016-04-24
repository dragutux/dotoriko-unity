using UnityEngine;
using System.Collections;

namespace DotOriko.Core {
    public abstract class DotOrikoSingleton<T> : DotOrikoComponent where T :MonoBehaviour {
        public static T Instance {
            get {
                if (_instance == null) CreateInstance();
                return _instance;
            }
        }

        protected static T _instance;

        private static Transform singletoneParent;

        private static void CreateInstance() {
            if(singletoneParent == null) {
                var par = new GameObject();
                par.name = "DotOriko Singletones";
                singletoneParent = par.transform;
                DontDestroyOnLoad(singletoneParent);
            }

            var g = new GameObject();
            g.name = typeof(T).ToString() + "_singletone";
            _instance = g.AddComponent<T>();
            g.transform.SetParent(singletoneParent);
        }

        protected override void OnInitialize() {
            base.OnInitialize();
        }

        protected override void OnStart() {
            base.OnStart();
        }

        protected override void OnUpdate() {
            base.OnUpdate();
        }

        protected override void OnScheduledUpdate() {
            base.OnScheduledUpdate();
        }
    }
}
