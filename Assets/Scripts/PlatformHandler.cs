using UnityEngine;
using UnityEngine.EventSystems;

public class PlatformHandler : MonoBehaviour {
    [SerializeField] private float rotationSpeed = 810f; 
    
    private float rotationAngle = 180f;
    private Quaternion targetRotation;

    void Start() {
        targetRotation = transform.rotation;
    }

    void Update() {
        if (GameManager.gameState == GameState.Playing) {
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
    }

    private void HandleTouchOrClick(Vector2 screenPos) {
        float screenMidX = Screen.width / 2f;
        targetRotation *= Quaternion.Euler(0f, 0f, rotationAngle);;
    }

    public void ResetRotation() {
        targetRotation = Quaternion.Euler(0f, 0f, 0f);
    }
}