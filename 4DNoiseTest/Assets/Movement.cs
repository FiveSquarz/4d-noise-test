using UnityEngine;

public sealed class Movement : JoystickControl {

    [SerializeField] float speed = 0f;

    Transform cam;

    protected override void Start() {
        base.Start();
        cam = Camera.main.transform;
    }

    void LateUpdate() {
        if (controlVector != Vector2.zero) cam.Translate(new Vector3(controlVector.x, 0f, controlVector.y) * speed * Time.deltaTime);
    }
}