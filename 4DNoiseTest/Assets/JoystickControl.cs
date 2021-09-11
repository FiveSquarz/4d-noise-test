using UnityEngine;
using UnityEngine.EventSystems;

public abstract class JoystickControl : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler {

    [SerializeField] float extent = 0f;
    [SerializeField] RectTransform outer = null, inner = null;

    Canvas canvas;

    int pointerId = -1;

    protected Vector2 controlVector;

    protected virtual void Start() {
        canvas = outer.GetComponentInParent<Canvas>();
    }

    public void OnPointerDown(PointerEventData eventData) {
        if (pointerId == -1) {
            pointerId = eventData.pointerId;

            outer.position = eventData.position;
            inner.position = eventData.position;
            outer.gameObject.SetActive(true);
        }
    }

    public void OnPointerUp(PointerEventData eventData) {
        if (eventData.pointerId == pointerId) {
            pointerId = -1;

            outer.gameObject.SetActive(false);
            controlVector = Vector2.zero;
        }
    }

    public void OnDrag(PointerEventData eventData) {
        if (eventData.pointerId == pointerId) {

            Vector2 outerPosition = outer.TransformPoint(outer.rect.center);
            Vector2 clampedVector = Vector2.ClampMagnitude(eventData.position - outerPosition, extent * canvas.scaleFactor);
            inner.position = clampedVector + outerPosition;

            controlVector = clampedVector / (extent * canvas.scaleFactor);
        }
    }
}