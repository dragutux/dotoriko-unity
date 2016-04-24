///
/// DotOriko v1.0
/// Physics object controller
/// By NoxCaos 15.02.2016
/// 

using UnityEngine;

namespace DotOriko {

    /// <summary>
    /// Extend this class if you work with
    /// gameobject that uses physics
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class DotOrikoPhysicComponent : DotOrikoComponent {

        #region Rigidbody
        /// <summary>
        /// Cached rigidbody of gameobject
        /// </summary>
        public Rigidbody Rigidbody {
            get {
                if (!this.rigidbody) this.rigidbody = this.GetComponent<Rigidbody>();
                return this.rigidbody;
            }
        }

        private new Rigidbody rigidbody;
        #endregion

        #region Collider
        /// <summary>
        /// Cached collider of gameobject
        /// </summary>
        public Collider Collider {
            get {
                if (!this.collider) this.collider = this.GetComponent<Collider>();
                return this.collider;
            }
        }

        private new Collider collider;
        #endregion

        #region Raycast
        /// <summary>
        /// Checks for the object with certain tag
        /// in certain direction and returns it.
        /// Basically casts the ray in direction
        /// </summary>
        /// <param name="dir">Direction in which ray should be cated</param>
        /// <param name="length">The length of the ray</param>
        /// <param name="tag">Tag of target gameobject</param>
        /// <param name="maskLayers">Layer that should be ignored</param>
        /// <returns>
        /// GameObject which ray collided with. 
        /// Null if there is no gameobject found
        /// </returns>
        public GameObject CastForObjectInDirection(Vector3 dir, float length, string tag = "", string[] maskLayers = null) {
            RaycastHit hit;
            LayerMask mask = 0;
            if (maskLayers != null) {
                foreach (var n in maskLayers) {
                    mask += LayerMask.NameToLayer(n);
                }
            }

            if (mask != 0) {
                if (Physics.Raycast(this.CachedTransform.position, 
                    dir.normalized, out hit, length, mask)
                    && (hit.collider.tag == tag || tag == "")) {
                    return hit.collider.gameObject;
                }
            } else {
                if (Physics.Raycast(this.CachedTransform.position, 
                    dir.normalized, out hit, length)
                    && (hit.collider.tag == tag || tag == "")) {
                    return hit.collider.gameObject;
                }
            }
            return null;
        }
        #endregion
    }
}
