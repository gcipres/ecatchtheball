using UnityEngine;
using UnityEngine.UI;

public class DontScaleObject : MonoBehaviour {
    private RectTransform rectTransform;
    private RectTransform canvasRectTransform;
    private CanvasScaler canvasScaler;
    private Vector3 originalScale;

    void Start() {
        rectTransform = GetComponent<RectTransform>();
        canvasScaler = GetComponentInParent<CanvasScaler>();

        originalScale = transform.localScale;

        if (canvasScaler != null)
            canvasRectTransform = canvasScaler.GetComponent<RectTransform>();
    }

    void Update()
    {
        if (canvasScaler == null || rectTransform == null || canvasRectTransform == null)
            return;

        float referenceHeight = canvasScaler.referenceResolution.y;
        float actualCanvasHeight = canvasRectTransform.rect.height;
        float scaleFactor = actualCanvasHeight / referenceHeight;

        if (scaleFactor == 0)
            return;

        float correction = 1f * scaleFactor;
        transform.localScale = new Vector3(originalScale.x * correction, originalScale.y * correction, originalScale.z);
    }
}
