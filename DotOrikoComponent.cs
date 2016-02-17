///
/// DotOriko v1.0
/// Physics object controller
/// By NoxCaos 10.02.2016
/// 

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace DotOriko {
    /// <summary>
    /// The basic class of all framework.
    /// Use this component like a new base 
    /// for all your scripts. It contains lots of 
    /// useful stuff that MonoBehavior doesn't.
    /// </summary>
    public abstract class DotOrikoComponent : MonoBehaviour {
        #region Vector3
        /// <summary>
        /// The position of transform
        /// </summary>
        public Vector3 Position {
            get {
                return this.CachedTransform.position;
            } set {
                this.CachedTransform.position = value;
            }
        }

        /// <summary>
        /// Position in local space
        /// </summary>
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
        /// <summary>
        /// The rotation of transform
        /// </summary>
        public Quaternion Rotation {
            get {
                return this.CachedTransform.rotation;
            } set {
                this.CachedTransform.rotation = value;
            }
        }

        /// <summary>
        /// Rotation in local space
        /// </summary>
        public Quaternion LocalRotation {
            get {
                return this.CachedTransform.localRotation;
            }
            set {
                this.CachedTransform.localRotation = value;
            }
        }
        #endregion

        # region Quaternion.Euler
        /// <summary>
        /// The euler angles of transform
        /// </summary>
        public Vector3 ERotation {
            get {
                return this.CachedTransform.eulerAngles;
            }
            set {
                this.CachedTransform.eulerAngles = value;
            }
        }

        /// <summary>
        /// Euler angles in local space
        /// </summary>
        public Vector3 ELocalRotation {
            get {
                return this.CachedTransform.localEulerAngles;
            }
            set {
                this.CachedTransform.localEulerAngles = value;
            }
        }
        #endregion

        #region Transform
        /// <summary>
        /// Cached transform of gameobject for optimizations
        /// </summary>
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
        /// <summary>
        /// Called the next frame after initilialization
        /// </summary>
        protected virtual void OnStart() { }

        /// <summary>
        /// Called every frame after 'OnStart()'
        /// </summary>
        protected virtual void OnUpdate() { }

        /// <summary>
        /// Called when 'StartScheduledUpdate()' is launched. 
        /// Use 'StopScheduledUpdate()' to stop
        /// </summary>
        protected virtual void OnScheduledUpdate() { }

        /// <summary>
        /// Called when script is initialized
        /// </summary>
        protected virtual void OnInitialize() { }

        /// <summary>
        /// Called when object is destroyed. 
        /// Don't forget to unsubscribe from events here
        /// </summary>
        protected virtual void OnReleaseResources() { }
        #endregion

        #region Protected methods
        /// <summary>
        /// starts calling 'OnScheduledUpdate()' every 
        /// param seconds
        /// </summary>
        /// <param name="time">Call delay time in seconds</param>
        protected void StartScheduledUpdate(float time) {
            this._scheduledUpdateTime = time;
            this.StopCoroutine("SceduledUpdate_Coroutine");
            this.StartCoroutine("SceduledUpdate_Coroutine");
        }

        /// <summary>
        /// Aborts the sceduled update
        /// </summary>
        protected void StopScheduledUpdate() {
            this.StopCoroutine("SceduledUpdate_Coroutine");
        }

        /// <summary>
        /// Removes all children of current transform
        /// </summary>
        protected void RemoveAllChildren() {
            var children = new List<GameObject>();
            foreach (Transform child in this.CachedTransform) children.Add(child.gameObject);
            children.ForEach(child => Destroy(child));
        }

        /// <summary>
        /// Removes all children of specified target transform
        /// </summary>
        /// <param name="target">The object we work with</param>
        protected void RemoveAllChildren(Transform target) {
            var children = new List<GameObject>();
            foreach (Transform child in target) children.Add(child.gameObject);
            children.ForEach(child => Destroy(child));
        }

        /// <summary>
        /// Reflects current transform on X axis
        /// </summary>
        protected void LocalMirrorByX() {
            Vector3 oldPos = this.LocalPosition;
            this.LocalPosition = new Vector3(-oldPos.x, oldPos.y, oldPos.z);
        }

        /// <summary>
        /// Reflects current transform on Y axis
        /// </summary>
        protected void LocalMirrorByY() {
            Vector3 oldPos = this.LocalPosition;
            this.LocalPosition = new Vector3(oldPos.x, -oldPos.y, oldPos.z);
        }

        /// <summary>
        /// Reflects current transform on X and Y axis
        /// </summary>
        protected void LocalMirroByXY() {
            this.LocalMirrorByX();
            this.LocalMirrorByY();
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
    }
}
