using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace DotOriko {
    public abstract class DotOrikoComponent : MonoBehaviour {
        #region Vector3
        public Vector3 Position {
            get {
                return this.CachedTransform.position;
            } set {
                this.CachedTransform.position = value;
            }
        }

        public Vector3 LocalPosition {
            get {
                return this.CachedTransform.localPosition;
            }
            set {
                this.CachedTransform.localPosition = value;
            }
        }
        #endregion

        #region Quaternion
        public Quaternion Rotation {
            get {
                return this.CachedTransform.rotation;
            } set {
                this.CachedTransform.rotation = value;
            }
        }

        public Quaternion LocalRotation {
            get {
                return this.CachedTransform.localRotation;
            }
            set {
                this.CachedTransform.localRotation = value;
            }
        }
        #endregion

        #region Transform
        protected Transform CachedTransform {
            get {
                if (!this._transform) this._transform = this.transform;
                return this._transform;
            }
        }
        #endregion

        private Transform _transform;

        private float _scheduledUpdateTime;

        #region Virtual methods
        protected virtual void OnStart() { }

        protected virtual void OnUpdate() { }

        protected virtual void OnScheduledUpdate() { }

        protected virtual void OnInitialize() { }

        protected virtual void OnReleaseResources() { }
        #endregion

        #region Protected methods
        protected void StartScheduledUpdate(float time) {
            this._scheduledUpdateTime = time;
            this.StopCoroutine("SceduledUpdate_Coroutine");
            this.StartCoroutine("SceduledUpdate_Coroutine");
        }

        protected void StopScheduledUpdate() {
            this.StopCoroutine("SceduledUpdate_Coroutine");
        }
        #endregion

        private void Start() { this.OnStart(); }

        private void Update() { this.OnUpdate(); }

        private void Awake() { this.OnInitialize(); }

        private void OnDestroy() { this.OnReleaseResources(); }

        private IEnumerator SceduledUpdate_Coroutine() {
            while (true) {
                yield return new WaitForSeconds(this._scheduledUpdateTime);
                this.OnScheduledUpdate();
            }
        }

        protected void RemoveAllChildren() {
            var children = new List<GameObject>();
            foreach (Transform child in this.CachedTransform) children.Add(child.gameObject);
            children.ForEach(child => Destroy(child));
        }

        protected void RemoveAllChildren(Transform target) {
            var children = new List<GameObject>();
            foreach (Transform child in target) children.Add(child.gameObject);
            children.ForEach(child => Destroy(child));
        }
    }
}
