using UnityEngine;

public sealed class RotateWithMouse : JoystickControl {

    [SerializeField] float /*verticalCameraArc = 0f, */speed = 0f;

    Transform cam;

    protected override void Start() {
        base.Start();
        cam = Camera.main.transform;
    }

    void LateUpdate() {
        if (controlVector != Vector2.zero) {
            /*float rotationX = cam.eulerAngles.x - controlVector.y * speed * Time.deltaTime;
            if (rotationX > verticalCameraArc && rotationX <= 180f) rotationX = verticalCameraArc;
            else if (rotationX < 360f - verticalCameraArc && rotationX >= 180f) rotationX = 360f - verticalCameraArc;

            float rotationY = cam.eulerAngles.y + controlVector.x * speed * Time.deltaTime;

            cam.eulerAngles = new Vector3(rotationX, rotationY);*/

            cam.Rotate(new Vector3(-controlVector.y, controlVector.x) * speed * Time.deltaTime);
        }
    }
}