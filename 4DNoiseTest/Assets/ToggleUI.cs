using UnityEngine;
using UnityEngine.UI;

public sealed class ToggleUI : MonoBehaviour {

    [SerializeField] CanvasGroup canvasGroup = null;

    Image image;
    Color originalColor;

    void Start() {
        image = GetComponent<Image>();
        originalColor = image.color;
    }

    public void Toggle() {
        if (canvasGroup.interactable) {
            canvasGroup.interactable = false;
            canvasGroup.alpha = 0f;
            image.color = Color.clear;
        } else {
            canvasGroup.interactable = true;
            canvasGroup.alpha = 1f;
            image.color = originalColor;
        }
    }
}
