using System.Collections;
using UnityEngine;

public class BallHandler : MonoBehaviour {
    private const string badColliderTag = "Bad";
    private const string goodColliderTag = "Good";

    private RectTransform canvasRect;

    private Vector2 targetPosition;

    private float movementSpeed;
    private Coroutine movementCoroutine;

    public void Initialize(RectTransform canvas, Vector2 target, float speed) {
        this.canvasRect = canvas;
        this.targetPosition = target;
        this.movementSpeed = speed;

        if (movementCoroutine != null)
            StopCoroutine(movementCoroutine);

        movementCoroutine = StartCoroutine(MoveToOppositeEdge());
    }

    private IEnumerator MoveToOppositeEdge() {
        while (Vector2.Distance(GetComponent<RectTransform>().anchoredPosition, targetPosition) > 0.1f) {
            Vector2 newPosition = Vector2.MoveTowards(
                GetComponent<RectTransform>().anchoredPosition,
                targetPosition,
                movementSpeed * Time.deltaTime
            );
            GetComponent<RectTransform>().anchoredPosition = newPosition;
            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag(goodColliderTag)) {
            // WIN ONE POINT
            ScoreManager.score++;            
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