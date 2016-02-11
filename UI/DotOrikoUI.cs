using System;
using UnityEngine;
using UnityEngine.UI;

namespace DotOriko.UI {
    public abstract class DotOrikoUI : DotOrikoComponent {

        public RectTransform CachedRectTransform {
            get {
                if (!this._rectTransform) this._rectTransform = this.GetComponent<RectTransform>();
                return this._rectTransform;
            }
        }

        private RectTransform _rectTransform;

        protected override void OnInitialize() {
            base.OnInitialize();
        }

        protected override void OnStart() {
            base.OnStart();
        }

        protected override void OnUpdate() {
            base.OnUpdate();
        }

        protected override void OnScheduledUpdate() {
            base.OnScheduledUpdate();
        }

        public static GameObject InstantiateUI(GameObject obj, Transform parent = null) {
            if(obj.GetComponent<RectTransform>() == null) {
                throw new ArgumentException("The given prefab has no RectTransform component attached");
            }

            var g = Instantiate(obj) as GameObject;
            var tr = g.GetComponent<RectTransform>();
            tr.SetParent(parent);
            tr.localScale = Vector3.one;
            tr.anchoredPosition3D = Vector3.zero;
            return g;
        }
    }
}
