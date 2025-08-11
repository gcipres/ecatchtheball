using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BallHandler : MonoBehaviour {
    private const string badColliderTag = "Bad";
    private const string goodColliderTag = "Good";

    private RectTransform canvasRect;
    private CanvasScaler canvasScaler;
    private Vector2 targetPosition;
    private float movementSpeed;
    private Coroutine movementCoroutine;

    public void Initialize(RectTransform canvas, Vector2 target, float speed) {
        this.canvasRect = canvas;
        this.targetPosition = target;
        this.movementSpeed = speed;

        this.canvasScaler = canvas.GetComponent<CanvasScaler>();

        if (movementCoroutine != null)
            StopCoroutine(movementCoroutine);

        movementCoroutine = StartCoroutine(MoveToOppositeEdge());
    }

    private IEnumerator MoveToOppositeEdge() {
        while (Vector2.Distance(GetComponent<RectTransform>().anchoredPosition, targetPosition) > 0.1f) {
            float adjustedSpeed = movementSpeed;
            if (canvasScaler != null && canvasRect != null) {
                float referenceHeight = canvasScaler.referenceResolution.y;
                float actualCanvasHeight = canvasRect.rect.height;
                float scaleFactor = referenceHeight != 0f ? (actualCanvasHeight / referenceHeight) : 1f;
                if (scaleFactor != 0f)
                    adjustedSpeed = movementSpeed * scaleFactor;
            } else {
                float screenRef = 1280f;
                adjustedSpeed = movementSpeed * (screenRef / Mathf.Max(1f, Screen.height));
            }

            Vector2 newPosition = Vector2.MoveTowards(
                GetComponent<RectTransform>().anchoredPosition,
                targetPosition,
                adjustedSpeed * Time.deltaTime
            );
            GetComponent<RectTransform>().anchoredPosition = newPosition;
            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag(goodColliderTag)) {
            // WIN ONE POINT
            GameManager.score++;
            BallSpawner.spawned = false;
            Destroy(gameObject);
        } else if (other.gameObject.CompareTag(badColliderTag)) {
            // GAME OVER
            Handheld.Vibrate();
            Destroy(gameObject);
            GameManager.gameState = GameState.GameOver;
            BallSpawner.movementSpeed = 600f;
            PlatformHandler platformHandler = FindObjectOfType<PlatformHandler>();
            platformHandler.ResetPlatformValues();

            BannerAds.ShowBanner();
        }
    }
}