///
/// DotOriko v1.0
/// Base UI component
/// By NoxCaos 13.02.2016
///

using System;
using UnityEngine;
using UnityEngine.UI;

namespace DotOriko {
    public abstract class DotOrikoUI : DotOrikoComponent {

        public static RectTransform Canvas {
            get {
                if (canvas == null) {
                    canvas = FindObjectOfType<Canvas>().GetComponent<RectTransform>();
                    Debug.LogWarning(
                        "[DoOriko UI]: The UI canvas was not defined. DotOriko will use first found canvas. "
                        + "You should set Canvas of DotOriko UI before instantiating UI components");
                }
                return canvas;
            } set {
                canvas = value;
            }
        }

        public RectTransform CachedRectTransform {
            get {
                if (!this.rectTransform) this.rectTransform = this.GetComponent<RectTransform>();
                return this.rectTransform;
            }
        }

        private static RectTransform canvas;
        private RectTransform rectTransform;

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

            if (parent == null) parent = canvas;

            var g = Instantiate(obj) as GameObject;
            var tr = g.GetComponent<RectTransform>();
            tr.SetParent(parent);
            tr.localScale = Vector3.one;
            tr.anchoredPosition3D = Vector3.zero;
            return g;
        }

		#region Utilities
		public void SetCenterAnchors() {
			var target = this.CachedRectTransform;
			var size = new Vector2(target.rect.width, target.rect.height);
			var pos = new Vector2(target.position.x, target.position.y);
			
			target.anchorMax = new Vector2(0.5f, 0.5f);
			target.anchorMin = new Vector2(0.5f, 0.5f);
			target.pivot = new Vector2(0.5f, 0.5f);
			
			target.sizeDelta = size;
			target.position = pos;
		}

		public void FitToParentSize() {
			var target = this.CachedRectTransform;
			var parent = target.transform.parent.GetComponent<RectTransform>();
			var parentSize = new Vector2(parent.rect.width, parent.rect.height);
			
			target.sizeDelta = parentSize;
			target.localPosition = Vector2.zero;
		}
		#endregion
    }
}
