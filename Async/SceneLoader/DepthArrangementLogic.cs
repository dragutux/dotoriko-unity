using UnityEngine;

namespace DotOriko.Async.SceneLoad {
    /// <summary>
    /// Depth arrangement
    /// Places new scene in certain direction
    /// with specified distance
    /// </summary>
    public class DepthArrangementLogic : ISceneArrangmentLogic {

        private Vector3 axis;

        public DepthArrangementLogic(float dist, Vector3 ax) {
            this.axis = ax.normalized * dist;
        }

        public void AddScene(int iterator, ref Transform root) {
            root.position += this.axis * iterator;
        }
    }
}
