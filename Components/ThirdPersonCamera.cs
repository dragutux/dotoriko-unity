using DotOriko.Core.Events;
using UnityEngine;

namespace DotOriko.Components {
    public sealed class ThirdPersonCamera : DotOrikoComponent {

        [SerializeField]
        private float distance;

        [SerializeField]
        private float height;

        private new Transform camera;
        private Camera cameraComponent;
        public Transform Target;
        private Transform followTarget;

        private float x = 90, y = 0;
        private float zoomRate = 3;

        private bool isRotateCharacter = true;
        private bool isAimedOnNPC;

        [HideInInspector]
        public bool IsDialogMode { get { return isDialogMode; }
            set {
                isDialogMode = value;
            } }

        private bool isDialogMode;
        private bool isFirstPerson;

        public GameObject objectInFront { get; private set; }

        public LayerMask layerMask;
        public LayerMask playerMask;

        private Transform dialogTarget;

        protected override void OnStart() {
            camera = CachedTransform.FindChild("Camera");
            cameraComponent = camera.GetComponent<Camera>();

            //LocalPosition = Vector3.zero;
            x = Rotation.eulerAngles.y;
            y = Rotation.eulerAngles.x;
            Cursor.lockState = CursorLockMode.Locked;
        }

        protected override void OnUpdate() {
            if (Input.GetMouseButtonUp(0)) Cursor.lockState = CursorLockMode.Locked;
            else if (Cursor.lockState == CursorLockMode.None) return;

            if (Target) {
                x += Input.GetAxis("Mouse X") * 5;
                y -= Input.GetAxis("Mouse Y") * 5;
                y = Mathf.Clamp(y, -40, 50);
                var yLerpSpeed = 5;

                //Rotate camera
                var rotation = Quaternion.Euler(y, x, 0);
                Rotation = Quaternion.Lerp(Rotation, rotation, Time.deltaTime * 5);

                //Camera position
                RaycastHit hit;
                var tgPos = CachedTransform.position - CachedTransform.forward * distance;
                var dist = distance - Input.GetAxis("Mouse ScrollWheel") * zoomRate;
                if (Physics.Linecast(Position, tgPos, out hit, layerMask)) {
                    dist = Vector3.Distance(Position, hit.point + camera.forward * .2f);
                } else distance = dist;
                distance = Mathf.Clamp(distance, 2f, 8f);

                bool fps = false;
                if (dist < 1.2f) fps = true;

                var curHeight = height;

                var newPos = CachedTransform.position - CachedTransform.forward * dist;
                camera.position = Vector3.Lerp(camera.position, newPos, Time.deltaTime * 10);
                if (!fps && !isFirstPerson) camera.LookAt(CachedTransform);

                //Rotate player
                if (isRotateCharacter) {
                    var h = Input.GetAxis("Horizontal");
                    var v = Input.GetAxis("Vertical");
                    if (h != 0 || v != 0) {
                        var ang = Vector3.Angle(Vector3.forward, new Vector3(h, 0, v));
                        if (h < 0) ang *= -1;
                        Target.rotation = Quaternion.Euler(0, x + ang, 0);
                    } else Target.rotation = Quaternion.Euler(0, Target.eulerAngles.y, 0);
                }

                followTarget = Target;
                var newY = Mathf.Lerp(Position.y, followTarget.position.y + curHeight, Time.deltaTime * yLerpSpeed);
                Position = new Vector3(followTarget.position.x, newY, followTarget.position.z);
            } else {
                try {
                    Target = GameObject.FindGameObjectWithTag("Player").transform;
                } catch { };
            }
        }

        public void LookAt(Transform target) {
            var rot = Rotation;
            CachedTransform.LookAt(target);
            x = Rotation.eulerAngles.y;
            Rotation = rot;
        }

        public void SetRotateCharacter(bool r) {
            isRotateCharacter = r;
        }

        public void UpdateFOV(float value) {
            cameraComponent.fieldOfView = value;
        }
    }
}
