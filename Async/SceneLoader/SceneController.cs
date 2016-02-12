using UnityEngine;

namespace DotOriko.Async.SceneLoad {
    public abstract class SceneController : DotOrikoComponent {

        [SerializeField]
        protected GameObject root;

        protected override void OnInitialize() {
            base.OnInitialize();

            SceneManagerAsync.Instance.cachedScenes.Add(this.root.name, this.root);
            //if(!AppController.testMode) this.root.SetActive(false);
        }

        protected override void OnStart() {
            base.OnStart();

			SceneManagerAsync.Instance.cachedScenes.Remove(this.root.name);
        }

        protected override void OnUpdate() {
            base.OnUpdate();
        }

        protected override void OnScheduledUpdate() {
            base.OnScheduledUpdate();
        }
    }
}
