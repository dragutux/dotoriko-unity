using DotOriko.Core.Events;
using UnityEngine;
using Yandere.Character;

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
        private CharacterMovement ch;

        protected override void OnStart() {
            camera = CachedTransform.FindChild("Camera");
            cameraComponent = camera.GetComponent<Camera>();

            //LocalPosition = Vector3.zero;
            x = Rotation.eulerAngles.y;
            y = Rotation.eulerAngles.x;
            Cursor.lockState = CursorLockMode.Locked;
        }

        protected override void OnUpdate() {
            if (Cursor.lockState == CursorLockMode.None) {
                if (isDialogMode) {
                    camera.rotation = dialogTarget.rotation;
                    camera.position = Vector3.Lerp(camera.position, dialogTarget.position, Time.deltaTime * 5);
                }
                return;
            }

            if (Input.GetKeyUp(KeyCode.Tab)) isFirstPerson = !isFirstPerson;

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
                distance = Mathf.Clamp(distance, 1.5f, 3);

                bool fps = false;
                if (dist < 1.2f) fps = true;

                var curHeight = height;
                if (fps || isFirstPerson) {
                    dist = -0.07f;
                    yLerpSpeed = 50;
                    curHeight = .1f;
                    followTarget = ch.Head;

                    if (!isRotateCharacter) Rotation = followTarget.rotation;
                } else followTarget = ch.Hips;

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

                var newY = Mathf.Lerp(Position.y, followTarget.position.y + curHeight, Time.deltaTime * yLerpSpeed);
                Position = new Vector3(followTarget.position.x, newY, followTarget.position.z);
            } else {
                try {
                    ch = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>();
                    //followTarget = ch.Hips;
                    Target = ch.transform;
                    dialogTarget = Target.FindChild("DialogTarget");
                } catch { };
            }

            GetObjectInFront();
        }

        private void GetObjectInFront() {
            RaycastHit hit;
            var point = new Vector2(Screen.width * .5f, Screen.height * .5f);
            Ray ray = cameraComponent.ScreenPointToRay(point);

            if (Physics.Raycast(ray, out hit, 1000, playerMask)) {
                var obj = hit.collider.gameObject;
                if (obj != objectInFront) {
                    CheckForNPC(obj, objectInFront);

                    objectInFront = obj;
                    EventManager.Instance.Trigger("System.OnFrontObjectChanged", obj);
                    if(obj) EventManager.Instance.Trigger("onFrontObjectChanged", obj.GetInstanceID());
                }
            } else objectInFront = null;
        }

        private void CheckForNPC(GameObject newO, GameObject lastO) {
            if (newO) {
                if (newO.tag == "NPC") {
                    newO.GetComponent<Yandere.AI.NPCInteraction>().ShowInteractionLabel(true);
                }
            }

            if (lastO) {
                if (lastO.tag == "NPC") {
                    lastO.GetComponent<Yandere.AI.NPCInteraction>().ShowInteractionLabel(false);
                }
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
