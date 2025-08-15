using UnityEngine;

public class BallSpawner : MonoBehaviour {

    public static bool spawned = false;
    public static float movementSpeed;

    public GameObject ballPrefab;

    private RectTransform canvasRect;
    
    void Start() {
        movementSpeed = 750f;
        canvasRect = GetComponent<RectTransform>();
    }

    void Update() {
        if (!spawned && GameManager.gameState == GameState.Playing) {
            SpawnBall();
            spawned = true;
        }
    }

    void SpawnBall() {
        int randomEdge = Random.Range(0, 2);
        Vector2 spawnPosition = Vector2.zero;
        Vector2 targetPosition = Vector2.zero;

        float canvasWidth = canvasRect.sizeDelta.x;
        float canvasHeight = canvasRect.sizeDelta.y;

        switch (randomEdge) {
            case 0: // Arriba
                spawnPosition = new Vector2(0f, canvasHeight);
                targetPosition = new Vector2(spawnPosition.x, 0f);
                break;
            case 1: // Abajo
                spawnPosition = new Vector2(0f, -canvasHeight);
                targetPosition = new Vector2(spawnPosition.x, canvasHeight);
                break;
        }
        
        GameObject newBall = Instantiate(ballPrefab, transform);
        RectTransform ballRect = newBall.GetComponent<RectTransform>();
        ballRect.anchoredPosition = spawnPosition;
        
        BallHandler ball = newBall.GetComponent<BallHandler>();
        if (ball != null)
            ball.Initialize(canvasRect, targetPosition, movementSpeed);
    }
}