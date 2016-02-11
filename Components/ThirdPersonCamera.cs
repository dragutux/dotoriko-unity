using UnityEngine;

namespace DotOriko.Components {
    public sealed class ThirdPersonCamera : DotOrikoComponent {

        [SerializeField]
        private float distance;

        [SerializeField]
        private float height;

        private new Transform camera;
        private Camera cameraComponent;
        public Transform Target { get; set; }

        private float x = 90, y = 0;
        private float zoomRate = 10;

        private LayerMask layerMask;

        protected override void OnStart() {
            this.camera = this.CachedTransform.FindChild("Camera");
            this.layerMask = LayerMask.NameToLayer("Player");
            this.cameraComponent = this.camera.GetComponent<Camera>();

            this.LocalPosition = Vector3.zero;
            this.x = this.Rotation.eulerAngles.y;
            this.y = this.Rotation.eulerAngles.x;

            DontDestroyOnLoad(this);
        }

        protected override void OnUpdate() {
            if (this.Target && !Controller.instance.isPaused) {
                this.x += Input.GetAxis("Mouse X") * 5;
                this.y -= Input.GetAxis("Mouse Y") * 5;
                this.y = Mathf.Clamp(this.y, -40, 60);

                //Rotate camera
                var rotation = Quaternion.Euler(this.y, this.x, 0);
                this.Rotation = Quaternion.Lerp(this.Rotation, rotation, Time.deltaTime * 10);

                //Camera position
                RaycastHit hit;
                var dist = this.distance - Input.GetAxis("Mouse ScrollWheel") * zoomRate;
                if (UnityEngine.Physics.Linecast(this.Position, this.camera.position, out hit, layerMask)) {
                    dist = Vector3.Distance(this.Position, hit.point);
                } else this.distance = dist;
                this.distance = Mathf.Clamp(this.distance, 3, 10);

                var newPos = new Vector3(0, this.height, -dist);
                this.camera.localPosition = Vector3.Lerp(this.camera.localPosition, newPos, Time.deltaTime * 10);

                //Rotate player
                if (Input.GetButton("Fire1") || Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0 ||
                        Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3)) {
                    Target.rotation = Quaternion.Euler(0, x, 0);
                }

                var newY = Mathf.Lerp(this.Position.y, this.Target.position.y, Time.deltaTime * 5);
                this.Position = new Vector3(this.Target.position.x, newY, this.Target.position.z);
            }
        }

        public void LookAt(Transform target) {
            var rot = this.Rotation;
            this.CachedTransform.LookAt(target);
            this.x = this.Rotation.eulerAngles.y;
            this.Rotation = rot;
        }

        public void UpdateFOV(float value) {
            this.cameraComponent.fieldOfView = value;
        }
    }
}
