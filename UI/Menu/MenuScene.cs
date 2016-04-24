using UnityEngine;
using System.Collections;
using DespairWorld;

namespace DotOriko.UI.Menu {
    public abstract class MenuScene : DotOrikoUI {

        [SerializeField]
        protected GameObject root;

        protected override void OnInitialize() {
            base.OnInitialize();

            AppController.Instance.cachedScenes.Add(root.name, root);
            if(!AppController.testMode) this.root.SetActive(false);
        }

        protected override void OnStart() {
            base.OnStart();

            AppController.Instance.cachedScenes.Remove(root.name);
        }

    }
}
