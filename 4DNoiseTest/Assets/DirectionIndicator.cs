using UnityEngine;
using UnityEngine.UI;

public sealed class DirectionIndicator : MonoBehaviour {

    [SerializeField] float offsetPercentOfSmaller = 0f, rotationOffset = 0f;
    float offset;

    [SerializeField] Image image = null;

    Camera cam;

    void Start() {
        cam = Camera.main;
        offset = offsetPercentOfSmaller / 100f * (Screen.width < Screen.height ? Screen.width : Screen.height);
    }

    void Update() {
        Vector3 screenPos = cam.WorldToScreenPoint(new Vector3(0f, 0f, 0f));

        if (screenPos.z > 0f
            && screenPos.x > offset && screenPos.x < Screen.width - offset
            && screenPos.y > offset && screenPos.y < Screen.height - offset) {

            image.enabled = false;
        } else {
            if (screenPos.z < 0f) screenPos = new Vector3(Screen.width - screenPos.x, Screen.height - screenPos.y);

            Vector3 screenCenter = new Vector3(Screen.width, Screen.height, 0f) / 2f;

            screenPos -= screenCenter;

            Vector2 screenBounds = new Vector2(screenCenter.x - offset, screenCenter.y - offset);

            float angle = Mathf.Atan2(screenPos.y, screenPos.x);
            screenPos = new Vector3(screenBounds.x * Mathf.Cos(angle), screenBounds.y * Mathf.Sin(angle));

            //float m = (screenPos.y != 0f ? screenPos.y : Mathf.Epsilon) / screenPos.x;

            //Vector2 rect = new Vector3(screenBounds.y / m, screenBounds.y) * Mathf.Sign(screenPos.y);
            //if (Mathf.Abs(rect.x) > screenBounds.x) rect = new Vector3(screenBounds.x, screenBounds.x * m) * Mathf.Sign(rect.x);

            //screenPos = Vector3.Lerp(ellipse, rect, lerpEllipseRect);

            screenPos += screenCenter;

            transform.position = screenPos;
            transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, angle * Mathf.Rad2Deg + rotationOffset);

            image.enabled = true;
        }
    }
}