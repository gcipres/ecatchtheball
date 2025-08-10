using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlatformHandler : MonoBehaviour {
    [SerializeField] private float rotationSpeed = 810f; 
    [SerializeField] private Sprite[] sprites; 
    
    private float rotationAngle = 180f;
    private Quaternion targetRotation;

    void Start() {
        SetRandomColor();
        targetRotation = transform.rotation;
    }

    void Update() {
        if (GameManager.gameState == GameState.Playing) {
            if (Input.touchCount > 0){
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                    HandleTouchOrClick();
            }
            else if (Input.GetMouseButtonDown(0))
                HandleTouchOrClick();

            if (transform.rotation != targetRotation)
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    public void ResetPlatformValues() {
        targetRotation = Quaternion.Euler(0f, 0f, 0f);
        SetRandomColor();
    }

    private void SetRandomColor() {
        int spriteIndex = Random.Range(0, sprites.Length - 1);
        Image image = GetComponent<Image>();
        image.sprite = sprites[spriteIndex];
    }

    private void HandleTouchOrClick() {
        int randomRotation = Random.Range(0, 2);
        targetRotation *= Quaternion.Euler(0f, 0f, randomRotation == 0 ? rotationAngle : -rotationAngle);
    }
}