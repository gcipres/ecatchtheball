using UnityEngine;

public class BallSpawner : MonoBehaviour {

    public static bool spawned = false;
    public static float movementSpeedX = 200f;
    public static float movementSpeedY = 300f;

    public GameObject ballPrefab;

    private RectTransform canvasRect;
    
    void Start() {
        movementSpeedX = 200f;
        movementSpeedY = 300f;
        canvasRect = GetComponent<RectTransform>();
    }

    void Update() {
        if (!spawned && GameManager.gameState == GameState.Playing) {
            SpawnBall();
            spawned = true;
        }
    }

    void SpawnBall() {
        int randomEdge = Random.Range(0, 4);
        Vector2 spawnPosition = Vector2.zero;
        Vector2 targetPosition = Vector2.zero;
        float movementSpeed = 0f;

        float canvasWidth = canvasRect.sizeDelta.x;
        float canvasHeight = canvasRect.sizeDelta.y;

        switch (randomEdge) {
            case 0: // Arriba
                spawnPosition = new Vector2(0f, canvasHeight);
                targetPosition = new Vector2(spawnPosition.x, 0f);
                movementSpeed = movementSpeedY;
                break;
            case 1: // Abajo
                spawnPosition = new Vector2(0f, -canvasHeight);
                targetPosition = new Vector2(spawnPosition.x, canvasHeight);
                movementSpeed = movementSpeedY;
                break;
            case 2: // Izquierda
                spawnPosition = new Vector2(-canvasWidth, 0f);
                targetPosition = new Vector2(canvasWidth, spawnPosition.y);
                movementSpeed = movementSpeedX;
                break;
            case 3: // Derecha
                spawnPosition = new Vector2(canvasWidth, 0f);
                targetPosition = new Vector2(0f, spawnPosition.y);
                movementSpeed = movementSpeedX;
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