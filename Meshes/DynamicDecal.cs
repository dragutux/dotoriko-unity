///
/// DotOriko v1.0
/// Dynamic decal
/// By NoxCaos 14.02.2016
/// 

using UnityEngine;
using DotOriko.Utils;

namespace DotOriko.Meshes {

    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshFilter))]
    public sealed class DynamicDecal : DotOrikoComponent {

        [SerializeField]
        private float updateRate;

        [SerializeField]
        private LayerMask mask;

        private MeshFilter filter;

        protected override void OnInitialize() {
            base.OnInitialize();
            this.filter = this.GetComponent<MeshFilter>();
        }

        protected override void OnStart() {
            base.OnStart();
            this.StartScheduledUpdate(1 / this.updateRate);
        }

        protected override void OnScheduledUpdate() {
            base.OnScheduledUpdate();
            var newMesh = this.filter.mesh.Clone();
            DecalSystem.SetPointsOnSurface(newMesh.vertices, this.mask, -this.CachedTransform.forward);
            this.filter.mesh = newMesh;
        }

    }
}
