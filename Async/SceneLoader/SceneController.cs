using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DotOriko.Async.SceneLoad {
    public abstract class SceneController : DotOrikoComponent {

        [SerializeField]
        protected GameObject root;

        [SerializeField]
        protected Material skybox;

        [SerializeField]
        protected float fogDensity = 0;

        public SceneStatus status { get; private set; }
        public List<GameObject> SceneObjects;

        protected override void OnInitialize() {
            base.OnInitialize();
        }

        protected override void OnStart() {
            base.OnStart();
        }

        protected override void OnReleaseResources() {
            base.OnReleaseResources();
        }

        protected override void OnScheduledUpdate() {
            base.OnScheduledUpdate();
        }

        protected virtual void OnSceneStartedActivating() { }

        protected virtual void OnSceneActivated() { }

        protected virtual void OnSceneStartedDeactivating() { }

        protected virtual void OnSceneDeactivated() { }

        internal void Activate() {
            this.StartCoroutine(this.ActivateAsync());
        }

        internal void Deactivate() {
            this.StartCoroutine(this.DeactivateAsync());
        }

        protected virtual IEnumerator ActivateAsync() {
            this.OnSceneStartedActivating();
            this.status = SceneStatus.Acitvating;
            this.SetRenderSettings();

            foreach (var p in this.SceneObjects) {
                yield return new WaitForEndOfFrame();
                p.SetActive(true);
            }
            this.OnSceneActivated();
            this.status = SceneStatus.Active;
        }

        protected virtual IEnumerator DeactivateAsync() {
            this.OnSceneStartedDeactivating();
            this.status = SceneStatus.Deactivating;
            this.SetRenderSettings();

            for (int i= this.SceneObjects.Count - 1; i >= 0; i++) { 
                yield return new WaitForEndOfFrame();
                this.SceneObjects[i].SetActive(false);
            }

            this.OnSceneDeactivated();
            this.status = SceneStatus.Hidden;
        }

        protected virtual void SetRenderSettings() {
#if UNITY_4_6
            RenderSettings.skybox = this.skybox;
            RenderSettings.fogDensity = this.fogDensity;
#endif
        }

    }
}
