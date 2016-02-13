using UnityEngine;
using DotOriko.Core.Model;
using System;

namespace DotOriko.Core {
    public abstract class DotOrikoController : DotOrikoComponent, IObserver {

        public IObservable Model { get; protected set; }

        public DotOrikoView View { get; protected set; }

        [SerializeField]
        [Tooltip("Leave empty to use VisualObservable. Don't attach VisualObservable to use controller without model")]
        private string modelConfigName;

        public abstract void OnModelUpdate();

        protected override void OnInitialize() {
            base.OnInitialize();

            if (this.modelConfigName != string.Empty) this.GetModelFromConfig();
            else this.Model = this.GetComponent(typeof(VisualObservable)) as VisualObservable;

            this.View = this.GetComponent(typeof(DotOrikoView)) as DotOrikoView;
            if(this.Model != null) this.Model.AttachObserver(this);
        }

        private IObservable GetModelFromConfig() {
            throw new NotImplementedException();
        }

    }
}
