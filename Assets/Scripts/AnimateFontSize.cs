using UnityEngine;
using TMPro;

public class AnimateFontSize : MonoBehaviour {
    public float sizeMin = 48f;
    public float sizeMax = 60f;
    public float speed = 24f;

    private bool incremented = true;
    private TextMeshProUGUI text;

    void Start() {
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update() {
        if (incremented) {
            text.fontSize += speed * Time.deltaTime;
            if (text.fontSize >= sizeMax)
                incremented = false;
        } else {
            text.fontSize -= speed * Time.deltaTime;
            if (text.fontSize <= sizeMin)
                incremented = true;
        }
    }
}
