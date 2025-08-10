using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { Start, Playing, GameOver, Share }

public class GameManager : MonoBehaviour {
    public static GameState gameState;
    
    [SerializeField] private GameObject GameOverCanvas;
    [SerializeField] private GameObject Score;
    [SerializeField] private GameObject Platform;
    [SerializeField] private GameObject MenuCanvas;

    void Start() {
        gameState = GameState.Start;
    }

    void Update() {
        MenuCanvas.SetActive(gameState == GameState.Start);
        Score.SetActive(gameState == GameState.Playing);
        Platform.SetActive(gameState == GameState.Playing);
        GameOverCanvas.SetActive(gameState == GameState.GameOver);
    }

    public void StartGame() {
        ScoreManager.score = 0;
        BallSpawner.spawned = false;
        gameState = GameState.Playing;
    }
}
