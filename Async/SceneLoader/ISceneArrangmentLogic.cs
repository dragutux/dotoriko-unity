///
/// DotOriko v1.0
/// Interface for scene arrangement
/// By NoxCaos 13.02.2016
///
using UnityEngine;

namespace DotOriko.Async.SceneLoad {
    public interface ISceneArrangmentLogic {

        void AddScene(int iterator, ref Transform root);

    }
}
