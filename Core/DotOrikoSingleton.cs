///
/// DotOriko v1.0
/// Physics object controller
/// By NoxCaos 12.02.2016
/// 

using UnityEngine;
using System.Collections;

namespace DotOriko.Core {
    /// <summary>
    /// Extending this class will allow you to
    /// create an object that can be accessed like static.
    /// See the "Singletone pattern" for details
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class DotOrikoSingleton<T> : DotOrikoComponent where T :MonoBehaviour {
        /// <summary>
        /// The one and only instance of object
        /// </summary>
        public static T Instance {
            get {
                if (_instance == null) CreateInstance();
                return _instance;
            }
        }

        protected static T _instance;

        private static Transform singletoneParent;

        /// <summary>
        /// Creates new instance if there is no any
        /// </summary>
        private static void CreateInstance() {

            //Parent for holding all the singletones 
            //in the game
            if(singletoneParent == null) {
                var instance = GameObject.Find("DotOriko Singletones");

                if (instance) {
                    singletoneParent = instance.transform;
                } else {
                    var par = new GameObject();
                    par.name = "DotOriko Singletones";
                    singletoneParent = par.transform;

                    DontDestroyOnLoad(singletoneParent);
                }
            }

            var g = new GameObject();
            g.name = typeof(T).ToString() + "_singleton";
            _instance = g.AddComponent<T>();
            g.transform.SetParent(singletoneParent);
            print("Initialized singleton");
        }

		public static bool IsInstanceExists() {
			return _instance != null;
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
