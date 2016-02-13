using UnityEngine;
using UnityEngine.UI;

namespace DotOriko.UI.Window {
	public abstract class Window : DotOrikoUI {

        [SerializeField]
        private Button closeButton;

        protected override void OnInitialize() {
            base.OnInitialize();

            if(this.closeButton) this.closeButton.onClick.AddListener(this.OnWindowClose);
        }

        protected sealed override void OnStart() {
            base.OnStart();

            this.SetCenterAnchors();
            this.OnWindowOpen();
        }

        public virtual void SetValues(object[] args) { }

        protected virtual void OnWindowOpen() { }

        protected virtual void OnWindowClose() { }

    }
}
