using UnityEngine;

namespace DotOriko.Async.SceneLoad {

    /// <summary>
    /// Center alingment
    /// Is a standart logic, places all new scenes at Vector3.zero
    /// </summary>
    public class CenterArrangementLogic : ISceneArrangmentLogic {

        public void AddScene(int iterator, ref Transform root) {
            root.position = Vector3.zero;
        }
    }
}
