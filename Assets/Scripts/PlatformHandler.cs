using UnityEngine;
using UnityEngine.EventSystems;

public class Platform : MonoBehaviour {
    [SerializeField] private float rotationSpeed = 810f; 
    
    private float rotationAngle = 90f;
    private Quaternion targetRotation;

    void Start() {
        targetRotation = transform.rotation;
    }

    void Update() {
        if (Input.touchCount > 0){
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
                HandleTouchOrClick(touch.position);
        }
        else if (Input.GetMouseButtonDown(0))
            HandleTouchOrClick(Input.mousePosition);

        if (transform.rotation != targetRotation)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void HandleTouchOrClick(Vector2 screenPos) {
        float screenMidX = Screen.width / 2f;

        if (screenPos.x < screenMidX)
            targetRotation *= Quaternion.Euler(0f, 0f, rotationAngle);
        else
            targetRotation *= Quaternion.Euler(0f, 0f, -rotationAngle);
    }
}