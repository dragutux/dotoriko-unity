using DotOriko.Core.Factory;
using UnityEngine;

namespace DotOriko.UI.Window {
    public abstract class UIWindowController : DotOrikoUI {

        public static RectTransform PopUpCanvas;

        protected enum PopUpTypes { };

        public static void ShowPopUp(string name, params object[] args) {
            var popup = (Window)InstantiateUI(
                CachableFactory<Window, PopUpTypes>.GetItem(name).gameObject, 
                PopUpCanvas)
                .GetComponent(typeof(Window));
            popup.SetValues(args);
        }

    }
}
