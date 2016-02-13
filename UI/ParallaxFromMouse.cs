using UnityEngine;

namespace DotOriko.UI {
    public sealed class ParallaxFromMouse : DotOrikoUI {

        [SerializeField]
        public int maxX;

        [SerializeField]
        public float moveRate;

        protected override void OnStart() {
            base.OnStart();
        }

        protected override void OnUpdate() {
            base.OnUpdate();

            var mouse = (Input.mousePosition.x / Screen.width) - 0.5f;
            if (this.CachedRectTransform) {
               var newPos = new Vector2(
                    this.maxX * moveRate * mouse,
                    this.CachedRectTransform.anchoredPosition.y);
                this.CachedRectTransform.anchoredPosition = Vector2.Lerp(
                    this.CachedRectTransform.anchoredPosition,
                    newPos, Time.deltaTime * 10
                    );
            } else {
                var newPos = new Vector3(
                    this.maxX * moveRate * mouse, 
                    this.CachedTransform.position.y, 
                    this.CachedTransform.position.z);
                this.CachedTransform.position = Vector3.Lerp(
                    this.CachedTransform.position,
                    newPos, Time.deltaTime * 10
                    );
            }
        }

    }
}
